using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using AdvanceCRM.Administration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serenity.Data;

namespace AdvanceCRM.MultiTenancy
{
    public interface ITenantResolver
    {
        Task<TenantInfo?> ResolveAsync(string? subdomain, CancellationToken cancellationToken = default);
    }

    public class TenantResolver : ITenantResolver
    {
        private const string CacheKeyPrefix = "tenant-info:";
        private readonly ISqlConnections sqlConnections;
        private readonly MultiTenancyOptions options;
        private readonly ILogger<TenantResolver> logger;
        private readonly IMemoryCache memoryCache;
        private readonly MemoryCacheEntryOptions cacheEntryOptions;

        public TenantResolver(ISqlConnections sqlConnections, IOptions<MultiTenancyOptions> options, ILogger<TenantResolver> logger, IMemoryCache memoryCache)
        {
            this.sqlConnections = sqlConnections ?? throw new ArgumentNullException(nameof(sqlConnections));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.options = options?.Value ?? new MultiTenancyOptions();
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));

            cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
        }

        public Task<TenantInfo?> ResolveAsync(string? subdomain, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(subdomain))
                return Task.FromResult<TenantInfo?>(null);

            subdomain = subdomain.Trim().TrimEnd('.').ToLowerInvariant();

            var cacheKey = CacheKeyPrefix + subdomain;
            if (memoryCache.TryGetValue(cacheKey, out TenantInfo? cachedTenant))
            {
                return Task.FromResult(CloneTenantInfo(cachedTenant));
            }

            try
            {
                using var connection = sqlConnections.NewByKey(options.DefaultConnectionKey);
                var fields = TenantRow.Fields;

                var tenantRow = connection.TryFirst<TenantRow>(query => query
                    .Select(fields.TenantId)
                    .Select(fields.Subdomain)
                    .Select(fields.DbName)
                    .Select(fields.Name)
                    .Select(fields.Plan)
                    .Select(fields.Modules)
                    .Where(fields.Subdomain == subdomain));

                if (tenantRow == null)
                    tenantRow = TryResolveTenantCaseInsensitive(connection, subdomain, fields);

                if (tenantRow == null)
                {
                    if (options.StaticTenants != null && options.StaticTenants.TryGetValue(subdomain, out var staticTenant) && staticTenant != null && !string.IsNullOrWhiteSpace(staticTenant.Database))
                    {
                        var dbNameFromConfig = staticTenant.Database.Trim();
                        var tenantName = string.IsNullOrWhiteSpace(staticTenant.Name) ? subdomain : staticTenant.Name.Trim();
                        var tenantPlan = string.IsNullOrWhiteSpace(staticTenant.Plan) ? null : staticTenant.Plan.Trim();
                        var tenantModules = string.IsNullOrWhiteSpace(staticTenant.Modules) ? null : staticTenant.Modules.Trim();

                        try
                        {
                            var newTenantId = Convert.ToInt32(connection.InsertAndGetID(new TenantRow
                            {
                                Name = tenantName,
                                Subdomain = subdomain,
                                DbName = dbNameFromConfig,
                                Plan = tenantPlan,
                                Modules = tenantModules
                            }));

                            tenantRow = new TenantRow
                            {
                                TenantId = newTenantId,
                                Subdomain = subdomain,
                                DbName = dbNameFromConfig,
                                Plan = tenantPlan,
                                Modules = tenantModules
                            };

                            logger.LogInformation("Created static tenant mapping for subdomain {Subdomain}", subdomain);
                        }
                        catch (Exception insertEx)
                        {
                            logger.LogWarning(insertEx, "Failed to create static tenant mapping for subdomain {Subdomain}. Attempting to reuse existing mapping.", subdomain);
                            tenantRow = connection.TryFirst<TenantRow>(query => query
                                .Select(fields.TenantId)
                                .Select(fields.Subdomain)
                                .Select(fields.DbName)
                                .Select(fields.Name)
                                .Select(fields.Plan)
                                .Select(fields.Modules)
                                .Where(fields.Subdomain == subdomain));

                            if (tenantRow == null)
                                tenantRow = TryResolveTenantCaseInsensitive(connection, subdomain, fields);

                            if (tenantRow == null)
                            {
                                tenantRow = new TenantRow
                                {
                                    TenantId = null,
                                    Subdomain = subdomain,
                                    DbName = dbNameFromConfig
                                };
                            }
                        }
                    }

                    if (tenantRow == null)
                    {
                        logger.LogWarning("Tenant not found for subdomain {Subdomain}", subdomain);
                        memoryCache.Set<TenantInfo?>(cacheKey, null, cacheEntryOptions);
                        return Task.FromResult<TenantInfo?>(null);
                    }
                }

                var dbName = tenantRow.DbName?.Trim();
                var plan = tenantRow.Plan?.Trim();
                var modules = TenantInfo.ParseModules(tenantRow.Modules);
                if (string.IsNullOrEmpty(dbName))
                {
                    logger.LogWarning("Tenant {TenantId} resolved for subdomain {Subdomain} does not have a database name configured", tenantRow.TenantId, subdomain);
                    memoryCache.Set<TenantInfo?>(cacheKey, null, cacheEntryOptions);
                    return Task.FromResult<TenantInfo?>(null);
                }

                var baseInfo = sqlConnections.TryGetConnectionString(options.DefaultConnectionKey);
                if (baseInfo == null)
                {
                    logger.LogError("Unable to read base connection string for key {ConnectionKey}", options.DefaultConnectionKey);
                    memoryCache.Set<TenantInfo?>(cacheKey, null, cacheEntryOptions);
                    return Task.FromResult<TenantInfo?>(null);
                }

                var connectionString = TenantAwareSqlConnections.BuildTenantConnectionString(baseInfo.ConnectionString, dbName);

                if (!CanOpenTenantDatabase(connectionString, out var validationException))
                {
                    if (validationException != null)
                        logger.LogWarning(validationException, "Tenant database {Database} for subdomain {Subdomain} could not be opened. Falling back to default connection.", dbName, subdomain);
                    else
                        logger.LogWarning("Tenant database {Database} for subdomain {Subdomain} could not be opened. Falling back to default connection.", dbName, subdomain);

                    var fallbackTenant = new TenantInfo
                    {
                        TenantId = tenantRow.TenantId ?? 0,
                        Subdomain = tenantRow.Subdomain?.Trim(),
                        DbName = null,
                        ConnectionString = baseInfo.ConnectionString,
                        CompanyName = tenantRow.Name?.Trim(),
                        Plan = plan,
                        Modules = modules
                    };

                    memoryCache.Set(cacheKey, fallbackTenant, cacheEntryOptions);
                    return Task.FromResult<TenantInfo?>(CloneTenantInfo(fallbackTenant));
                }

                var resolvedCompanyName = TryResolveCompanyName(connectionString) ?? tenantRow.Name?.Trim();

                var resolvedTenant = new TenantInfo
                {
                    TenantId = tenantRow.TenantId ?? 0,
                    Subdomain = tenantRow.Subdomain?.Trim(),
                    DbName = dbName,
                    ConnectionString = connectionString,
                    CompanyName = resolvedCompanyName,
                    Plan = plan,
                    Modules = modules
                };

                memoryCache.Set(cacheKey, resolvedTenant, cacheEntryOptions);
                return Task.FromResult<TenantInfo?>(CloneTenantInfo(resolvedTenant));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to resolve tenant for subdomain {Subdomain}", subdomain);
                memoryCache.Set<TenantInfo?>(cacheKey, null, cacheEntryOptions);
                return Task.FromResult<TenantInfo?>(null);
            }
        }

        private bool CanOpenTenantDatabase(string connectionString, out Exception? exception)
        {
            exception = null;

            if (string.IsNullOrWhiteSpace(connectionString))
                return false;

            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();
                return true;
            }
            catch (Exception ex) when (ex is SqlException || ex is InvalidOperationException)
            {
                exception = ex;
                return false;
            }
        }

        private string? TryResolveCompanyName(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return null;

            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = "SELECT TOP 1 [Name] FROM [dbo].[CompanyDetails] ORDER BY [Id]";
                command.CommandType = System.Data.CommandType.Text;

                var result = command.ExecuteScalar() as string;
                return string.IsNullOrWhiteSpace(result) ? null : result.Trim();
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex, "Failed to resolve company name from tenant database.");
                return null;
            }
        }

        private static TenantRow? TryResolveTenantCaseInsensitive(IDbConnection connection, string subdomain, TenantRow.RowFields fields)
        {
            if (connection == null)
                return null;

            var loweredExpression = $"LOWER({fields.Subdomain.Expression})";

            return connection.TryFirst<TenantRow>(query => query
                .Select(fields.TenantId)
                .Select(fields.Subdomain)
                .Select(fields.DbName)
                .Select(fields.Name)
                .Where(new Criteria(loweredExpression) == new ValueCriteria(subdomain)));
        }

        private static TenantInfo? CloneTenantInfo(TenantInfo? tenant)
        {
            if (tenant == null)
                return null;

            var moduleSet = tenant.Modules ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            return new TenantInfo
            {
                TenantId = tenant.TenantId,
                Subdomain = tenant.Subdomain,
                DbName = tenant.DbName,
                ConnectionString = tenant.ConnectionString,
                CompanyName = tenant.CompanyName,
                Plan = tenant.Plan,
                Modules = new HashSet<string>(moduleSet, StringComparer.OrdinalIgnoreCase)
            };
        }
    }
}
