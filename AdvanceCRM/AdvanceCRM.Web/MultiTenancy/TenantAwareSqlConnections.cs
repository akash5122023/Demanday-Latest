using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

using System.Linq;

using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serenity.Data;

namespace AdvanceCRM.MultiTenancy
{
    public class TenantAwareSqlConnections : ISqlConnections
    {
        private readonly IConfiguration configuration;
        private readonly ITenantAccessor tenantAccessor;
        private readonly ILogger<TenantAwareSqlConnections> logger;
        private readonly MultiTenancyOptions options;
        private readonly ConcurrentDictionary<string, ConnectionStringInfo> baseConnectionCache;
        private readonly ConcurrentDictionary<string, ConnectionStringInfo> tenantConnectionCache;
        private readonly HashSet<string> tenantAwareConnectionKeys;


        public IConnectionStrings ConnectionStrings => this;


        public TenantAwareSqlConnections(IConfiguration configuration, ITenantAccessor tenantAccessor, IOptions<MultiTenancyOptions> options, ILogger<TenantAwareSqlConnections> logger)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.tenantAccessor = tenantAccessor ?? throw new ArgumentNullException(nameof(tenantAccessor));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var optionValue = options?.Value ?? new MultiTenancyOptions();
            this.options = optionValue;

            baseConnectionCache = new ConcurrentDictionary<string, ConnectionStringInfo>(StringComparer.OrdinalIgnoreCase);
            tenantConnectionCache = new ConcurrentDictionary<string, ConnectionStringInfo>(StringComparer.OrdinalIgnoreCase);
            tenantAwareConnectionKeys = new HashSet<string>(
                (optionValue.ConnectionKeys?.Length ?? 0) > 0 ? optionValue.ConnectionKeys : new[] { optionValue.DefaultConnectionKey },
                StringComparer.OrdinalIgnoreCase);
        }

        public IDbConnection New()
        {
            return New(options.DefaultConnectionKey);
        }

        public IDbConnection New(string connectionKey)
        {
            var info = GetConnectionStringInternal(NormalizeConnectionKey(connectionKey));
            return CreateConnection(info);
        }

        public IDbConnection NewByKey(string connectionKey)
        {
            return New(connectionKey);
        }

        public IDbConnection New(string connectionString, string providerName, ISqlDialect dialect)
        {
            var info = CreateConnectionInfo(null, connectionString, providerName, dialect);
            return CreateConnection(info);
        }

        public IDbConnection Open()
        {
            return Open(options.DefaultConnectionKey);
        }

        public IDbConnection Open(string connectionKey)
        {
            var connection = New(connectionKey);
            connection.Open();
            return connection;
        }

        public IDbConnection OpenByKey(string connectionKey)
        {
            var connection = New(connectionKey);
            connection.Open();
            return connection;
        }

        public IDbConnection Open(string connectionString, string providerName, ISqlDialect dialect)
        {
            var connection = New(connectionString, providerName, dialect);
            connection.Open();
            return connection;
        }

        public IDbConnection NewFor<TRow>() where TRow : class, IRow, new()
        {
            var connectionKey = GetConnectionKey(typeof(TRow));
            return New(connectionKey);
        }

        public IDbConnection OpenFor<TRow>() where TRow : class, IRow, new()
        {
            var connection = NewFor<TRow>();
            connection.Open();
            return connection;
        }


        public IConnectionString TryGetConnectionString(string connectionKey)

        {
            try
            {
                return GetConnectionStringInternal(NormalizeConnectionKey(connectionKey));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to resolve connection string for key {ConnectionKey}", connectionKey);
                return null;
            }
        }

        public IConnectionString GetConnectionString(string connectionKey)

        {
            var info = TryGetConnectionString(connectionKey);
            if (info == null)
                throw new ArgumentOutOfRangeException(nameof(connectionKey));

            return info;
        }


        public IEnumerable<IConnectionString> ListConnectionStrings()
        {
            var keys = new List<string>();
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            void AddKey(string candidate)
            {
                if (string.IsNullOrWhiteSpace(candidate))
                    return;

                if (seen.Add(candidate))
                    keys.Add(candidate);
            }

            var dataSection = configuration.GetSection("Data");
            if (dataSection.Exists())
            {
                foreach (var child in dataSection.GetChildren())
                    AddKey(child.Key);
            }

            var connectionStringsSection = configuration.GetSection("ConnectionStrings");
            if (connectionStringsSection.Exists())
            {
                foreach (var child in connectionStringsSection.GetChildren())
                    AddKey(child.Key);
            }

            if (options.ConnectionKeys?.Length > 0)
            {
                foreach (var key in options.ConnectionKeys)
                    AddKey(key);
            }

            if (keys.Count == 0)
                return Array.Empty<IConnectionString>();

            var result = new List<IConnectionString>();

            foreach (var key in keys)
            {
                try
                {
                    var normalized = NormalizeConnectionKey(key);
                    var baseInfo = GetBaseConnectionInfo(normalized);
                    result.Add(CloneConnectionInfo(baseInfo));
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Unable to list connection key {ConnectionKey}", key);
                }
            }

            return result;
        }


        private string NormalizeConnectionKey(string connectionKey)
        {
            return string.IsNullOrWhiteSpace(connectionKey) ? options.DefaultConnectionKey : connectionKey;
        }

        private ConnectionStringInfo GetConnectionStringInternal(string connectionKey)
        {
            var baseInfo = GetBaseConnectionInfo(connectionKey);
            var tenant = tenantAccessor.CurrentTenant;

            if (tenant == null || string.IsNullOrWhiteSpace(tenant.DbName) || !ShouldUseTenantDatabase(connectionKey))
                return CloneConnectionInfo(baseInfo);

            var cacheKey = $"{tenant.DbName.ToLowerInvariant()}@{connectionKey.ToLowerInvariant()}";
            if (tenantConnectionCache.TryGetValue(cacheKey, out var cached))
                return CloneConnectionInfo(cached);

            var clone = CloneConnectionInfo(baseInfo);
            var connectionString = tenant.ConnectionString ?? BuildTenantConnectionString(baseInfo.ConnectionString, tenant.DbName);

            TrySetProperty(clone, "ConnectionString", connectionString);

            if (baseInfo.Dialect != null && GetPropertyValue(clone, "Dialect") == null)
                TrySetProperty(clone, "Dialect", baseInfo.Dialect);


            tenantConnectionCache[cacheKey] = clone;
            tenant.ConnectionString ??= connectionString;

            return CloneConnectionInfo(clone);
        }

        private ConnectionStringInfo GetBaseConnectionInfo(string connectionKey)
        {
            return baseConnectionCache.GetOrAdd(connectionKey, key =>
            {
                var info = LoadConnectionInfo(key);
                if (info == null)
                    throw new InvalidOperationException($"Connection string for key '{key}' is not configured.");
                return info;
            });
        }

        private ConnectionStringInfo LoadConnectionInfo(string connectionKey)
        {

            var dataSection = configuration.GetSection("Data").GetSection(connectionKey);
            var connectionStringsSection = configuration.GetSection("ConnectionStrings").GetSection(connectionKey);

            var connectionString = GetConnectionStringValue(dataSection) ??
                GetConnectionStringValue(connectionStringsSection) ??
                configuration.GetConnectionString(connectionKey) ??
                configuration[$"ConnectionStrings:{connectionKey}"] ??
                configuration[$"Data:{connectionKey}"];

            var providerName = GetSectionValue(dataSection, "ProviderName") ??
                GetSectionValue(connectionStringsSection, "ProviderName") ??
                configuration[$"ConnectionStrings:{connectionKey}:ProviderName"] ??
                configuration[$"Data:{connectionKey}:ProviderName"];

            var dialectName = GetSectionValue(dataSection, "Dialect") ??
                GetSectionValue(connectionStringsSection, "Dialect") ??
                configuration[$"ConnectionStrings:{connectionKey}:Dialect"] ??
                configuration[$"Data:{connectionKey}:Dialect"];

            var environmentOverrides = ResolveEnvironmentConnectionSettings(connectionKey);

            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = environmentOverrides.ConnectionString;

            if (string.IsNullOrWhiteSpace(providerName))
                providerName = environmentOverrides.ProviderName;

            if (string.IsNullOrWhiteSpace(dialectName))
                dialectName = environmentOverrides.DialectName;

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                logger.LogWarning("Connection key {ConnectionKey} was not found in configuration", connectionKey);
                return null;
            }

            var info = CreateConnectionInfo(connectionKey, connectionString, providerName, ResolveDialect(dialectName, providerName));
            return info;
        }

        private (string? ConnectionString, string? ProviderName, string? DialectName) ResolveEnvironmentConnectionSettings(string connectionKey)
        {
            string? connectionString = null;
            string? providerName = null;
            string? dialectName = null;

            string? candidate = Environment.GetEnvironmentVariable($"ConnectionStrings__{connectionKey}__ConnectionString");
            if (!string.IsNullOrWhiteSpace(candidate))
                connectionString = candidate.Trim();

            candidate = Environment.GetEnvironmentVariable($"ConnectionStrings__{connectionKey}");
            if (!string.IsNullOrWhiteSpace(candidate))
                connectionString ??= candidate.Trim();

            candidate = Environment.GetEnvironmentVariable($"ConnectionStrings__{connectionKey}__ProviderName");
            if (!string.IsNullOrWhiteSpace(candidate))
                providerName = candidate.Trim();

            candidate = Environment.GetEnvironmentVariable($"ConnectionStrings__{connectionKey}__Dialect");
            if (!string.IsNullOrWhiteSpace(candidate))
                dialectName = candidate.Trim();

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                var azureCandidates = new (string Prefix, string? ProviderName, string? Dialect)[]
                {
                    ($"SQLCONNSTR_{connectionKey}", "System.Data.SqlClient", null),
                    ($"SQLAZURECONNSTR_{connectionKey}", "System.Data.SqlClient", null),
                    ($"MYSQLCONNSTR_{connectionKey}", "MySql.Data.MySqlClient", null),
                    ($"POSTGRESQLCONNSTR_{connectionKey}", "Npgsql", "Serenity.Data.PostgresDialect, Serenity.Net.Data"),
                    ($"CUSTOMCONNSTR_{connectionKey}", null, null)
                };

                foreach (var azureCandidate in azureCandidates)
                {
                    candidate = Environment.GetEnvironmentVariable(azureCandidate.Prefix);
                    if (!string.IsNullOrWhiteSpace(candidate))
                    {
                        connectionString = candidate.Trim();
                        if (!string.IsNullOrWhiteSpace(azureCandidate.ProviderName))
                            providerName ??= azureCandidate.ProviderName;
                        if (!string.IsNullOrWhiteSpace(azureCandidate.Dialect))
                            dialectName ??= azureCandidate.Dialect;
                        break;
                    }
                }
            }

            return (connectionString, providerName, dialectName);
        }

        private static string GetConnectionStringValue(IConfigurationSection section)
        {
            if (section == null || !section.Exists())
                return null;

            var value = section["ConnectionString"];
            if (!string.IsNullOrWhiteSpace(value))
                return value;

            value = section.Value;
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        private static string GetSectionValue(IConfigurationSection section, string key)
        {
            if (section == null || !section.Exists())
                return null;

            var value = section[key];
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }

        private static ConnectionStringInfo CloneConnectionInfo(ConnectionStringInfo source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var type = typeof(ConnectionStringInfo);
            var ctor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                binder: null,
                types: new[] { typeof(string), typeof(string), typeof(string), typeof(ISqlDialect) },
                modifiers: null);

            if (ctor != null)
            {
                try
                {
                    var key = (GetPropertyValue(source, "ConnectionKey") ?? GetPropertyValue(source, "Key")) as string;
                    var providerName = GetPropertyValue(source, "ProviderName") as string;
                    var dialect = GetPropertyValue(source, "Dialect") as ISqlDialect;

                    if (key != null)
                        return (ConnectionStringInfo)ctor.Invoke(new object[] { key, source.ConnectionString, providerName, dialect });
                }
                catch (TargetInvocationException)
                {
                    // fall back to reflection path below if constructor invocation fails
                }
            }

            var clone = (ConnectionStringInfo)CreateInstanceWithoutConstructor(type);

            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var value = property.GetValue(source);
                TrySetProperty(clone, property, value);
            }

            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                field.SetValue(clone, field.GetValue(source));
            }

            return clone;
        }

        private bool ShouldUseTenantDatabase(string connectionKey)
        {
            return tenantAwareConnectionKeys.Contains(connectionKey);
        }

        private ConnectionStringInfo CreateConnectionInfo(string connectionKey, string connectionString, string providerName, ISqlDialect dialect)
        {
            providerName ??= "System.Data.SqlClient";
            dialect ??= ResolveDialect(null, providerName);

            connectionString = NormalizeConnectionStringForProvider(connectionString, providerName);

            var type = typeof(ConnectionStringInfo);
            var ctor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                binder: null,
                types: new[] { typeof(string), typeof(string), typeof(string), typeof(ISqlDialect) },
                modifiers: null);

            ConnectionStringInfo info;
            if (ctor != null && connectionKey != null)
            {
                info = (ConnectionStringInfo)ctor.Invoke(new object[] { connectionKey, connectionString, providerName, dialect });
            }
            else
            {
                info = (ConnectionStringInfo)CreateInstanceWithoutConstructor(type);
                TrySetProperty(info, "ConnectionKey", connectionKey);
                TrySetProperty(info, "Key", connectionKey);
                TrySetProperty(info, "ConnectionString", connectionString);
                TrySetProperty(info, "ProviderName", providerName);
                TrySetProperty(info, "Dialect", dialect);
            }

            return info;
        }

        private static string NormalizeConnectionStringForProvider(string connectionString, string providerName)
        {
            if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(providerName))
                return connectionString;

            if (providerName.Equals("System.Data.SqlClient", StringComparison.OrdinalIgnoreCase) ||
                providerName.Equals("Microsoft.Data.SqlClient", StringComparison.OrdinalIgnoreCase))
            {
                return NormalizeSqlServerKeywords(connectionString);
            }

            return connectionString;
        }

        private static object CreateInstanceWithoutConstructor(Type type)
        {
            return FormatterServices.GetUninitializedObject(type);
        }

        private static void TrySetProperty(ConnectionStringInfo info, string propertyName, object value)
        {
            var property = typeof(ConnectionStringInfo).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                TrySetProperty(info, property, value);
                return;
            }

            var field = typeof(ConnectionStringInfo).GetField(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(info, value);
            }
        }

        private static void TrySetProperty(ConnectionStringInfo info, PropertyInfo property, object value)
        {
            if (!property.CanRead)
                return;

            var setMethod = property.GetSetMethod(true);
            if (setMethod != null)
            {
                setMethod.Invoke(info, new[] { value });
                return;
            }

            var backingField = typeof(ConnectionStringInfo).GetField($"<{property.Name}>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);
            if (backingField != null)
                backingField.SetValue(info, value);
        }

        private static object GetPropertyValue(ConnectionStringInfo info, string propertyName)
        {
            var type = typeof(ConnectionStringInfo);
            var property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
                return property.GetValue(info);

            var field = type.GetField(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return field?.GetValue(info);

        }

        private ISqlDialect ResolveDialect(string dialectName, string providerName)
        {
            if (!string.IsNullOrWhiteSpace(dialectName))
            {
                var byName = CreateDialectInstance(Type.GetType(dialectName, throwOnError: false));
                if (byName != null)
                    return byName;
            }

            if (!string.IsNullOrEmpty(providerName))
            {
                if (providerName.IndexOf("sqlite", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var sqlite = CreateDialectInstance("Serenity.Data.SqliteDialect, Serenity.Net.Data");
                    if (sqlite != null)
                        return sqlite;
                }
                if (providerName.IndexOf("npgsql", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var postgres = CreateDialectInstance("Serenity.Data.PostgresDialect, Serenity.Net.Data");
                    if (postgres != null)
                        return postgres;
                }
                if (providerName.IndexOf("mysql", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var mySql = CreateDialectInstance("Serenity.Data.MySqlDialect, Serenity.Net.Data");
                    if (mySql != null)
                        return mySql;
                }
                if (providerName.IndexOf("firebird", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var firebird = CreateDialectInstance("Serenity.Data.FirebirdDialect, Serenity.Net.Data");
                    if (firebird != null)
                        return firebird;
                }
            }

            var sqlServer = CreateDialectInstance("Serenity.Data.SqlServer2012Dialect, Serenity.Net.Data") ??
                CreateDialectInstance("Serenity.Data.SqlServer2008Dialect, Serenity.Net.Data");

            if (sqlServer == null)
                throw new InvalidOperationException($"Unable to resolve SQL dialect for provider '{providerName ?? ""}'.");

            return sqlServer;
        }

        private static ISqlDialect? CreateDialectInstance(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
                return null;

            var type = Type.GetType(typeName, throwOnError: false);
            return CreateDialectInstance(type);
        }

        private static ISqlDialect? CreateDialectInstance(Type type)
        {
            if (type == null)
                return null;

            if (!typeof(ISqlDialect).IsAssignableFrom(type))
                return null;

            var instanceProperty = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
            if (instanceProperty?.GetValue(null) is ISqlDialect dialect)
                return dialect;

            return Activator.CreateInstance(type) as ISqlDialect;
        }

        private IDbConnection CreateConnection(ConnectionStringInfo info)
        {
            var actual = CreateProviderConnection(info);
            var dialect = info.Dialect ?? ResolveDialect(null, info.ProviderName);
            return new WrappedConnection(actual, dialect);
        }

        private IDbConnection CreateProviderConnection(ConnectionStringInfo info)
        {
            try
            {
                if (!string.IsNullOrEmpty(info.ProviderName))
                {
                    var factory = DbProviderFactories.GetFactory(info.ProviderName);
                    var connection = factory.CreateConnection();
                    if (connection == null)
                        throw new InvalidOperationException($"Unable to create connection for provider '{info.ProviderName}'.");

                    connection.ConnectionString = info.ConnectionString;
                    return connection;
                }
            }
            catch (ArgumentException ex)
            {
                logger.LogError(ex, "Unable to build provider factory for {Provider}", info.ProviderName);
            }

            var sqlConnection = new SqlConnection
            {
                ConnectionString = info.ConnectionString
            };
            return sqlConnection;
        }

        private string GetConnectionKey(Type rowType)
        {
            var attr = rowType.GetCustomAttribute<ConnectionKeyAttribute>();

            var attrValue = attr?.Value;

            if (!string.IsNullOrEmpty(attrValue))
                return attrValue;

            var fieldsProperty = rowType.GetProperty("Fields", BindingFlags.Public | BindingFlags.Static);
            if (fieldsProperty?.GetValue(null) is RowFieldsBase fields && !string.IsNullOrEmpty(fields.ConnectionKey))
                return fields.ConnectionKey;

            return options.DefaultConnectionKey;
        }

        public static string BuildTenantConnectionString(string baseConnectionString, string databaseName)
        {
            if (string.IsNullOrWhiteSpace(baseConnectionString) || string.IsNullOrWhiteSpace(databaseName))
                return baseConnectionString;

            try
            {
                var normalized = NormalizeSqlServerKeywords(baseConnectionString);
                var builder = new SqlConnectionStringBuilder(normalized)
                {
                    InitialCatalog = databaseName
                };
                builder["Database"] = databaseName;
                if (builder.ContainsKey("AttachDBFilename"))
                    builder.Remove("AttachDBFilename");
                return builder.ConnectionString;
            }
            catch (ArgumentException)
            {
                var normalized = NormalizeSqlServerKeywords(baseConnectionString);
                var genericBuilder = new DbConnectionStringBuilder
                {
                    ConnectionString = normalized
                };

                if (genericBuilder.ContainsKey("Database"))
                    genericBuilder["Database"] = databaseName;
                else if (genericBuilder.ContainsKey("Initial Catalog"))
                    genericBuilder["Initial Catalog"] = databaseName;
                else if (genericBuilder.ContainsKey("Catalog"))
                    genericBuilder["Catalog"] = databaseName;
                else
                    genericBuilder["Database"] = databaseName;

                return genericBuilder.ConnectionString;
            }
        }

        private static string NormalizeSqlServerKeywords(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return connectionString;

            const string currentKey = "Trust Server Certificate";
            const string normalizedKey = "TrustServerCertificate";

            var index = 0;
            StringBuilder result = null;
            var changed = false;

            while (index < connectionString.Length)
            {
                var matchIndex = connectionString.IndexOf(currentKey, index, StringComparison.OrdinalIgnoreCase);
                if (matchIndex < 0)
                    break;

                var precedingIndex = matchIndex - 1;
                while (precedingIndex >= 0 && char.IsWhiteSpace(connectionString[precedingIndex]))
                    precedingIndex--;

                if (precedingIndex >= 0 && connectionString[precedingIndex] != ';' &&
                    connectionString[precedingIndex] != '"' && connectionString[precedingIndex] != '\'')
                {
                    index = matchIndex + currentKey.Length;
                    continue;
                }

                var afterIndex = matchIndex + currentKey.Length;
                while (afterIndex < connectionString.Length && char.IsWhiteSpace(connectionString[afterIndex]))
                    afterIndex++;

                if (afterIndex >= connectionString.Length || connectionString[afterIndex] != '=')
                {
                    index = matchIndex + currentKey.Length;
                    continue;
                }

                result ??= new StringBuilder(connectionString.Length);
                result.Append(connectionString, index, matchIndex - index);
                result.Append(normalizedKey);

                index = matchIndex + currentKey.Length;
                changed = true;
            }

            if (!changed)
                return connectionString;

            result.Append(connectionString, index, connectionString.Length - index);
            return result.ToString();

        }
    }
}












