using System;
using System.Threading;
using System.Threading.Tasks;
using AdvanceCRM.Administration;
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
        private readonly ISqlConnections sqlConnections;
        private readonly MultiTenancyOptions options;
        private readonly ILogger<TenantResolver> logger;

        public TenantResolver(ISqlConnections sqlConnections, IOptions<MultiTenancyOptions> options, ILogger<TenantResolver> logger)
        {
            this.sqlConnections = sqlConnections ?? throw new ArgumentNullException(nameof(sqlConnections));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.options = options?.Value ?? new MultiTenancyOptions();
        }

        public Task<TenantInfo?> ResolveAsync(string? subdomain, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(subdomain))
                return Task.FromResult<TenantInfo?>(null);

            subdomain = subdomain.Trim().TrimEnd('.').ToLowerInvariant();

            try
            {
                using var connection = sqlConnections.NewByKey(options.DefaultConnectionKey);
                var fields = TenantRow.Fields;

                var tenantRow = connection.TryFirst<TenantRow>(query => query
                    .Select(fields.TenantId)
                    .Select(fields.Subdomain)
                    .Select(fields.DbName)
                    .Where(fields.Subdomain == subdomain));

                if (tenantRow == null)
                {
                    if (options.StaticTenants != null && options.StaticTenants.TryGetValue(subdomain, out var staticTenant) && staticTenant != null && !string.IsNullOrWhiteSpace(staticTenant.Database))
                    {
                        var dbNameFromConfig = staticTenant.Database.Trim();
                        var tenantName = string.IsNullOrWhiteSpace(staticTenant.Name) ? subdomain : staticTenant.Name.Trim();
                        var tenantPlan = string.IsNullOrWhiteSpace(staticTenant.Plan) ? null : staticTenant.Plan.Trim();

                        try
                        {
                            var newTenantId = Convert.ToInt32(connection.InsertAndGetID(new TenantRow
                            {
                                Name = tenantName,
                                Subdomain = subdomain,
                                DbName = dbNameFromConfig,
                                Plan = tenantPlan
                            }));

                            tenantRow = new TenantRow
                            {
                                TenantId = newTenantId,
                                Subdomain = subdomain,
                                DbName = dbNameFromConfig
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
                                .Where(fields.Subdomain == subdomain));

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
                        return Task.FromResult<TenantInfo?>(null);
                    }
                }

                var dbName = tenantRow.DbName?.Trim();
                if (string.IsNullOrEmpty(dbName))
                {
                    logger.LogWarning("Tenant {TenantId} resolved for subdomain {Subdomain} does not have a database name configured", tenantRow.TenantId, subdomain);
                    return Task.FromResult<TenantInfo?>(null);
                }

                var baseInfo = sqlConnections.TryGetConnectionString(options.DefaultConnectionKey);
                if (baseInfo == null)
                {
                    logger.LogError("Unable to read base connection string for key {ConnectionKey}", options.DefaultConnectionKey);
                    return Task.FromResult<TenantInfo?>(null);
                }

                var connectionString = TenantAwareSqlConnections.BuildTenantConnectionString(baseInfo.ConnectionString, dbName);

                if (!CanOpenTenantDatabase(connectionString, out var validationException))
                {
                    if (validationException != null)
                        logger.LogWarning(validationException, "Tenant database {Database} for subdomain {Subdomain} could not be opened. Falling back to default connection.", dbName, subdomain);
                    else
                        logger.LogWarning("Tenant database {Database} for subdomain {Subdomain} could not be opened. Falling back to default connection.", dbName, subdomain);

                    return Task.FromResult<TenantInfo?>(new TenantInfo
                    {
                        TenantId = tenantRow.TenantId ?? 0,
                        Subdomain = tenantRow.Subdomain?.Trim(),
                        DbName = null,
                        ConnectionString = baseInfo.ConnectionString
                    });
                }

                return Task.FromResult<TenantInfo?>(new TenantInfo
                {
                    TenantId = tenantRow.TenantId ?? 0,
                    Subdomain = tenantRow.Subdomain?.Trim(),
                    DbName = dbName,
                    ConnectionString = connectionString
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to resolve tenant for subdomain {Subdomain}", subdomain);
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
    }
}
