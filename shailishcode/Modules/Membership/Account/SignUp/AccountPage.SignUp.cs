using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Conventions;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using Serenity;
using Serenity.Data;
using Serenity.Services;
using Serenity.Web;
using AdvanceCRM.Administration;
using AdvanceCRM.Administration.Repositories;
using AdvanceCRM.Common;
using AdvanceCRM.Web.Helpers;
using AdvanceCRM.MultiTenancy;
using System;
using System.IO;
using System.Diagnostics;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using MicrosoftSqlException = Microsoft.Data.SqlClient.SqlException;
using MicrosoftSqlError = Microsoft.Data.SqlClient.SqlError;
using LegacySqlException = System.Data.SqlClient.SqlException;
using LegacySqlError = System.Data.SqlClient.SqlError;



namespace AdvanceCRM.Membership.Pages
{
    public partial class AccountController : Controller
    {
        [HttpGet, Route("SignUp")]
        public ActionResult SignUp([FromServices] IRazorpayOrderService razorpayOrderService = null)
        {
            RazorpayClientConfig config;

            if (razorpayOrderService != null)
            {
                config = new RazorpayClientConfig
                {
                    Enabled = razorpayOrderService.IsEnabled,
                    Key = razorpayOrderService.KeyId,
                    Currency = razorpayOrderService.Currency,
                    Plans = razorpayOrderService.PlanAmounts?.ToDictionary(x => x.Key, x => x.Value) ?? new Dictionary<string, int>()
                };
            }
            else
            {
                config = new RazorpayClientConfig();
            }

            ViewData["RazorpayConfig"] = config;

            if (UseAdminLTELoginBox)
                return View(MVC.Views.Membership.Account.SignUp.AccountSignUp_AdminLTE);
            else
                return View(MVC.Views.Membership.Account.SignUp.AccountSignUp);
        }

        [HttpPost, Route("SignUp/CreateOrder"), JsonRequest]
        public async Task<JsonResult> CreateRazorpayOrder(RazorpayCreateOrderRequest request,
            [FromServices] IRazorpayOrderService razorpayOrderService)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            if (razorpayOrderService == null || !razorpayOrderService.IsEnabled)
                throw new ValidationError("PaymentUnavailable", "Payment", "Online payments are temporarily unavailable. Please contact support.");

            if (string.IsNullOrWhiteSpace(request.Plan))
                throw new ValidationError("PlanRequired", "Plan", "A subscription plan is required before creating a payment order.");

            var order = await razorpayOrderService.CreateOrderAsync(request.Plan);

            var response = new RazorpayCreateOrderResponse
            {
                OrderId = order.Id,
                Amount = order.Amount,
                Currency = order.Currency,
                Key = razorpayOrderService.KeyId,
                Success = true
            };

            return Json(response);
        }

        [HttpPost, Route("SignUp"), JsonRequest]
        public Result<ServiceResponse> SignUp(SignUpRequest request,
            [FromServices] IEmailSender emailSender,
            [FromServices] IOptions<EnvironmentSettings> options = null,
            [FromServices] ITenantAccessor tenantAccessor = null,
            [FromServices] IRazorpayOrderService razorpayOrderService = null)
        {
            TenantInfo? originalTenant = null;
            if (tenantAccessor != null)
            {
                originalTenant = tenantAccessor.CurrentTenant;
                tenantAccessor.CurrentTenant = null;
            }

            try
            {
            return this.UseConnection("Default", connection =>
            {
                if (request is null)
                    throw new ArgumentNullException(nameof(request));

                if (string.IsNullOrWhiteSpace(request.Email))
                    throw new ArgumentNullException(nameof(request.Email));
                var adminPassword = request.Password;
                if (string.IsNullOrEmpty(adminPassword))
                {
                    adminPassword = Q.GeneratePassword(12);
                }
                else
                {
                    UserRepository.ValidatePassword(adminPassword, Localizer);
                }
                if (string.IsNullOrWhiteSpace(request.DisplayName))
                    throw new ArgumentNullException(nameof(request.DisplayName));

                if (string.IsNullOrWhiteSpace(request.Company))
                    throw new ArgumentNullException(nameof(request.Company));

                if (string.IsNullOrWhiteSpace(request.Plan))
                    throw new ArgumentNullException(nameof(request.Plan));

                if (string.IsNullOrWhiteSpace(request.MobileNumber))
                    throw new ArgumentNullException(nameof(request.MobileNumber));

                var (licenseStart, licenseEnd) = CalculateLicensePeriod(request.Plan);

                if (razorpayOrderService != null && razorpayOrderService.IsEnabled)
                {
                    if (string.IsNullOrWhiteSpace(request.PaymentOrderId) ||
                        string.IsNullOrWhiteSpace(request.PaymentId) ||
                        string.IsNullOrWhiteSpace(request.PaymentSignature))
                    {
                        throw new ValidationError("PaymentRequired", "PaymentId", "Payment confirmation is required before creating an account.");
                    }

                    if (!razorpayOrderService.VerifySignature(request.PaymentOrderId, request.PaymentId, request.PaymentSignature))
                    {
                        throw new ValidationError("PaymentVerificationFailed", "PaymentSignature", "Unable to verify the payment with Razorpay. Please contact support.");
                    }

                    var expectedAmount = razorpayOrderService.GetAmountForPlan(request.Plan);
                    if (expectedAmount > 0)
                    {
                        if (!int.TryParse(request.PaymentAmount, out var paidAmount) || paidAmount < expectedAmount)
                        {
                            throw new ValidationError("PaymentAmountMismatch", "PaymentAmount", "The payment amount does not match the selected plan.");
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(request.PaymentCurrency) &&
                        !string.Equals(request.PaymentCurrency, razorpayOrderService.Currency, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new ValidationError("PaymentCurrencyMismatch", "PaymentCurrency", "Unexpected payment currency received from the payment gateway.");
                    }
                }

                var company = request.Company.TrimToEmpty();

                if (connection.Exists<TenantRow>(TenantRow.Fields.Name == company))
                {
                    throw new ValidationError("CompanyNameInUse", "Company", "Company name already exists.");
                }

                if (connection.Exists<UserRow>(
                        UserRow.Fields.Username == request.Email |
                        UserRow.Fields.Email == request.Email))
                {
                    throw new ValidationError("EmailInUse", Texts.Validation.EmailInUse.ToString(Localizer));
                }

                using (var uow = new UnitOfWork(connection))
                {
                    string salt = null;
                    var hash = UserRepository.GenerateHash(adminPassword, ref salt);
                    var displayName = request.DisplayName.TrimToEmpty();
                    var email = request.Email;
                    var username = request.Email;

                    var fld = UserRow.Fields;
                    var userId = (int)connection.InsertAndGetID(new UserRow
                    {
                        Username = username,
                        Source = "sign",
                        DisplayName = displayName,
                        Email = email,
                        Plan = request.Plan,
                        Phone = request.MobileNumber.TrimToEmpty(),
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        IsActive = false,
                        InsertDate = DateTime.Now,
                        InsertUserId = 1,
                        LastDirectoryUpdate = DateTime.Now
                    });

                    // Synchronously provision tenant and get url
                    var result = ProvisionTenantAndGetUrl(userId, company, request.Plan, displayName, email, hash, salt, request.Password, licenseStart, licenseEnd);
                    var tenantId = result.tenantId;
                    var url = result.url;

                    // Update user with tenantId and url
                    connection.UpdateById(new UserRow
                    {
                        UserId = userId,
                        TenantId = tenantId,
                        Url = url
                    });

                    var externalUrl = options?.Value.SiteExternalUrl ??
                        Request.GetBaseUri().ToString();

                    var tenantBaseUrl = string.IsNullOrWhiteSpace(url) ? externalUrl : url;
                    var loginUrl = UriHelper.Combine(tenantBaseUrl, "Account/Login");

                    var emailModel = new ActivateEmailModel
                    {
                        Username = username,
                        DisplayName = displayName,
                        TenantUrl = tenantBaseUrl,
                        LoginUrl = loginUrl,
                        AdminUsername = username,
                        AdminPassword = adminPassword
                    };

                    var emailSubject = Texts.Forms.Membership.SignUp.ActivateEmailSubject.ToString(Localizer);
                    var emailBody = TemplateHelper.RenderViewToString(HttpContext.RequestServices,
                        MVC.Views.Membership.Account.SignUp.AccountActivateEmail, emailModel);

                    if (emailSender is null)
                        throw new ArgumentNullException(nameof(emailSender));

                    var activationSubject = emailSubject;
                    var activationBody = emailBody;
                    var activationRecipient = email;

                    uow.Commit();
                    UserRetrieveService.RemoveCachedUser(Cache, userId, username);

                    QueueActivationEmail(emailSender, activationSubject, activationBody, activationRecipient);

                    return new ServiceResponse();
                }
                });
            }
            finally
            {
                if (tenantAccessor != null)
                    tenantAccessor.CurrentTenant = originalTenant;
            }
        }

        // Helper method to provision tenant and return (tenantId, url)
        private (int tenantId, string url) ProvisionTenantAndGetUrl(int userId, string companyName, string plan, string displayName, string email, string passwordHash, string passwordSalt, string plainPassword, DateTime licenseStartDate, DateTime licenseEndDate)
        {
            var sub = companyName?.Trim().ToLowerInvariant();
            sub = Regex.Replace(sub ?? string.Empty, @"[^a-z0-9-]", string.Empty);
            sub = sub.Trim('-');
            if (sub.Length > 63)
                sub = sub.Substring(0, 63);
            sub = string.IsNullOrEmpty(sub) ? null : sub;

            const int dnsStatusMaxLength = 2000;
            string dnsStatus = ResolveDnsStatus(companyName, sub);
            if (dnsStatus?.Length > dnsStatusMaxLength)
                dnsStatus = dnsStatus.Substring(0, dnsStatusMaxLength);

            var port = GenerateFreePort();
            var tenantDb = $"Tenant_{userId}";
            int tenantId;
            string url = null;
            using (var scope = _scopeFactory.CreateScope())
            {
                var connections = scope.ServiceProvider.GetRequiredService<ISqlConnections>();
                using (var connection = connections.NewByKey("Default"))
                {
                    var tenantRow = new TenantRow
                    {
                        Name = companyName,
                        Subdomain = sub,
                        DbName = tenantDb,
                        Port = port,
                        Plan = plan,
                        Modules = null,
                        LicenseStartDate = licenseStartDate,
                        LicenseEndDate = licenseEndDate,
                        DnsStatus = dnsStatus
                    };
                    try
                    {
                        tenantId = Convert.ToInt32(connection.InsertAndGetID(tenantRow));
                    }
                    catch (Exception ex)
                    {
                        if (!IsMissingDnsStatusColumn(ex))
                            throw;
                        var warning = new StringBuilder("Tenants table is missing the DnsStatus column. Apply the latest migrations to persist DNS provisioning status.");
                        if (!string.IsNullOrWhiteSpace(dnsStatus))
                        {
                            warning.Append(" Latest status: ").Append(dnsStatus);
                        }
                        new InvalidOperationException(warning.ToString(), ex).Log("ProvisionTenant");
                        tenantId = InsertTenantWithoutDnsStatus(connection, companyName, sub, tenantDb, port, plan, licenseStartDate, licenseEndDate);
                    }
                    var domain = _config.GetSection("Cloudflare")["RootDomain"];
                    if (!string.IsNullOrEmpty(sub) && !string.IsNullOrEmpty(domain))
                        url = $"https://{sub}.{domain}/";
                }
                var seedDefaultsTask = Task.Run(() => SeedTenantDefaults(connections, tenantDb, companyName, displayName, email, plan));
                var seedAdminTask = Task.Run(() => SeedTenantAdminUser(connections, tenantDb, displayName, email, passwordHash, passwordSalt, plan, plainPassword));

                try
                {
                    Task.WaitAll(seedDefaultsTask, seedAdminTask);
                }
                catch (AggregateException aggregate)
                {
                    foreach (var exception in aggregate.Flatten().InnerExceptions)
                        new InvalidOperationException("Error occurred while seeding tenant data.", exception).Log("ProvisionTenant");

                    throw;
                }
            }

            // Linux script (unchanged)
            var scriptPath = "/opt/advancecrm/provision.sh";
            if (OperatingSystem.IsLinux())
            {
                if (System.IO.File.Exists(scriptPath))
                {
                    Process.Start(scriptPath, $"{port} {tenantDb}");
                }
                else
                {
                    new FileNotFoundException($"Provisioning script not found at {scriptPath}").Log("ProvisionTenant");
                }
            }
            return (tenantId, url);
        }

        private string ResolveDnsStatus(string companyName, string subdomain)
        {
            const string defaultPendingMessage = "Pending: Subdomain provisioning scheduled.";

            if (string.IsNullOrEmpty(subdomain))
                return "Skipped: No valid subdomain generated.";

            if (_subdomainService == null)
                return "Skipped: DNS provisioning service unavailable.";

            try
            {
                var provisioningTask = _subdomainService.CreateSubdomainAsync(subdomain);
                var completedTask = Task.WhenAny(provisioningTask, Task.Delay(TimeSpan.FromSeconds(8))).GetAwaiter().GetResult();

                if (completedTask == provisioningTask)
                {
                    var status = provisioningTask.GetAwaiter().GetResult();
                    return string.IsNullOrWhiteSpace(status) ? "Created" : status.Trim();
                }

                _ = provisioningTask.ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        var exception = task.Exception?.Flatten().InnerExceptions.FirstOrDefault() ?? task.Exception;
                        if (exception != null)
                        {
                            var failure = BuildDnsFailureStatus(companyName, subdomain, exception);
                            new InvalidOperationException(failure, exception).Log("ProvisionTenant");
                        }
                    }
                    else if (!task.IsCanceled)
                    {
                        var backgroundStatus = task.Result;
                        if (!string.IsNullOrWhiteSpace(backgroundStatus))
                            new InvalidOperationException($"DNS provisioning completed asynchronously with status: {backgroundStatus.Trim()}.").Log("ProvisionTenant");
                    }
                }, TaskScheduler.Default);

                return defaultPendingMessage;
            }
            catch (Exception ex)
            {
                if (ex is AggregateException aggregate && aggregate.InnerExceptions.Count == 1)
                    ex = aggregate.InnerExceptions[0];

                var failureStatus = BuildDnsFailureStatus(companyName, subdomain, ex);
                new InvalidOperationException(failureStatus, ex).Log("ProvisionTenant");
                return failureStatus;
            }
        }

        private void QueueActivationEmail(IEmailSender emailSender, string subject, string body, string recipient)
        {
            if (emailSender == null || string.IsNullOrWhiteSpace(recipient))
                return;

            Task.Run(() =>
            {
                try
                {
                    emailSender.Send(subject: subject, body: body, mailTo: recipient);
                }
                catch (Exception ex)
                {
                    new InvalidOperationException("Failed to send activation email.", ex).Log("SignUp");
                }
            });
        }

        private (DateTime Start, DateTime End) CalculateLicensePeriod(string plan)
        {
            var start = DateTime.Today;
            var trialDays = GetTrialDays(plan);

            if (trialDays <= 0)
                return (start, start);

            return (start, start.AddDays(trialDays - 1));
        }

        private int GetTrialDays(string plan)
        {
            var trialSection = _config.GetSection("TrialSettings");
            var defaultDays = trialSection.GetValue<int?>("DefaultDays") ?? 0;

            if (!string.IsNullOrWhiteSpace(plan))
            {
                var plansSection = trialSection.GetSection("Plans");
                if (plansSection.Exists())
                {
                    foreach (var child in plansSection.GetChildren())
                    {
                        if (string.Equals(child.Key, plan, StringComparison.OrdinalIgnoreCase))
                        {
                            if (int.TryParse(child.Value, out var specific) && specific > 0)
                                return specific;
                        }
                    }
                }
            }

            return defaultDays > 0 ? defaultDays : 0;
        }

        private void SeedTenantAdminUser(ISqlConnections connections, string tenantDb, string displayName, string email, string passwordHash, string passwordSalt, string plan, string plainPassword = null)
        {
            if (string.IsNullOrWhiteSpace(tenantDb) || connections == null)
                return;

            try
            {
                string adminSalt;
                string adminHash;

                if (!string.IsNullOrEmpty(plainPassword))
                {
                    adminSalt = null;
                    adminHash = UserRepository.GenerateHash(plainPassword, ref adminSalt);
                }
                else
                {
                    adminHash = passwordHash ?? string.Empty;
                    adminSalt = passwordSalt ?? string.Empty;
                }

                var connectionInfo = connections.TryGetConnectionString("Default");
                var baseConnectionString = connectionInfo?.ConnectionString;
                if (string.IsNullOrWhiteSpace(baseConnectionString))
                    return;

                var tenantConnectionString = TenantAwareSqlConnections.BuildTenantConnectionString(baseConnectionString, tenantDb);
                using var sqlConnection = new SqlConnection(tenantConnectionString);
                sqlConnection.Open();

                var now = DateTime.Now;
                var planParameterValue = string.IsNullOrWhiteSpace(plan) ? (object)DBNull.Value : plan.Trim();

                using var updateCommand = sqlConnection.CreateCommand();
                updateCommand.CommandText = @"UPDATE [dbo].[Users]
SET Username = @Email,
    DisplayName = @DisplayName,
    Email = @Email,
    PasswordHash = @PasswordHash,
    PasswordSalt = @PasswordSalt,
    Plan = @Plan,
    Source = @Source,
    IsActive = 1,
    LastDirectoryUpdate = @Now
WHERE UserId = @UserId;";
                updateCommand.Parameters.AddWithValue("@Email", email);
                updateCommand.Parameters.AddWithValue("@DisplayName", displayName ?? string.Empty);
                updateCommand.Parameters.AddWithValue("@PasswordHash", adminHash ?? string.Empty);
                updateCommand.Parameters.AddWithValue("@PasswordSalt", adminSalt ?? string.Empty);
                updateCommand.Parameters.AddWithValue("@Plan", planParameterValue);
                updateCommand.Parameters.AddWithValue("@Source", "sign");
                updateCommand.Parameters.AddWithValue("@Now", now);
                updateCommand.Parameters.AddWithValue("@UserId", 1);

                var affected = updateCommand.ExecuteNonQuery();

                if (affected == 0)
                {
                    updateCommand.Parameters.Clear();
                    updateCommand.CommandText = @"UPDATE [dbo].[Users]
SET Username = @Email,
    DisplayName = @DisplayName,
    Email = @Email,
    PasswordHash = @PasswordHash,
    PasswordSalt = @PasswordSalt,
    Plan = @Plan,
    Source = @Source,
    IsActive = 1,
    LastDirectoryUpdate = @Now
WHERE Username = @AdminUsername;";
                    updateCommand.Parameters.AddWithValue("@Email", email);
                    updateCommand.Parameters.AddWithValue("@DisplayName", displayName ?? string.Empty);
                    updateCommand.Parameters.AddWithValue("@PasswordHash", adminHash ?? string.Empty);
                    updateCommand.Parameters.AddWithValue("@PasswordSalt", adminSalt ?? string.Empty);
                    updateCommand.Parameters.AddWithValue("@Plan", planParameterValue);
                    updateCommand.Parameters.AddWithValue("@Source", "sign");
                    updateCommand.Parameters.AddWithValue("@Now", now);
                    updateCommand.Parameters.AddWithValue("@AdminUsername", "admin");

                    affected = updateCommand.ExecuteNonQuery();
                }

                if (affected == 0)
                {
                    using var insertCommand = sqlConnection.CreateCommand();
                    insertCommand.CommandText = @"INSERT INTO [dbo].[Users]
(Username, DisplayName, Email, PasswordHash, PasswordSalt, Plan, Source, IsActive, InsertDate, LastDirectoryUpdate)
VALUES (@Email, @DisplayName, @Email, @PasswordHash, @PasswordSalt, @Plan, @Source, 1, @Now, @Now);
SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    insertCommand.Parameters.AddWithValue("@Email", email);
                    insertCommand.Parameters.AddWithValue("@DisplayName", displayName ?? string.Empty);
                    insertCommand.Parameters.AddWithValue("@PasswordHash", adminHash ?? string.Empty);
                    insertCommand.Parameters.AddWithValue("@PasswordSalt", adminSalt ?? string.Empty);
                    insertCommand.Parameters.AddWithValue("@Plan", planParameterValue);
                    insertCommand.Parameters.AddWithValue("@Source", "sign");
                    insertCommand.Parameters.AddWithValue("@Now", now);

                    var newUserId = Convert.ToInt32(insertCommand.ExecuteScalar());

                    using (var auditCommand = sqlConnection.CreateCommand())
                    {
                        auditCommand.CommandText = @"UPDATE [dbo].[Users]
SET InsertUserId = @UserId,
    UpdateUserId = @UserId,
    LastDirectoryUpdate = @Now
WHERE UserId = @UserId;";
                        auditCommand.Parameters.AddWithValue("@UserId", newUserId);
                        auditCommand.Parameters.AddWithValue("@Now", now);
                        auditCommand.ExecuteNonQuery();
                    }

                    using var roleCommand = sqlConnection.CreateCommand();
                    roleCommand.CommandText = @"DECLARE @RoleId INT = (SELECT TOP 1 RoleId FROM [dbo].[Roles] ORDER BY RoleId);
IF @RoleId IS NOT NULL
BEGIN
    INSERT INTO [dbo].[UserRoles] (UserId, RoleId) VALUES (@UserId, @RoleId);
END";
                    roleCommand.Parameters.AddWithValue("@UserId", newUserId);
                    roleCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                new InvalidOperationException($"Failed to seed admin user for tenant database '{tenantDb}'.", ex).Log("ProvisionTenant");
            }
        }
        private void SeedTenantDefaults(ISqlConnections connections, string tenantDb, string companyName, string adminDisplayName, string adminEmail, string plan)
        {
            if (string.IsNullOrWhiteSpace(tenantDb) || connections == null)
                return;

            try
            {
                var connectionInfo = connections.TryGetConnectionString("Default");
                var baseConnectionString = connectionInfo?.ConnectionString;
                if (string.IsNullOrWhiteSpace(baseConnectionString))
                    throw new InvalidOperationException("The default connection string is not configured. Unable to provision the tenant database.");

                EnsureTenantDatabaseCreated(baseConnectionString, tenantDb);
                RunTenantMigrations(baseConnectionString, tenantDb);

                var tenantConnectionString = TenantAwareSqlConnections.BuildTenantConnectionString(baseConnectionString, tenantDb);

                EnsureTenantSchemaReady(tenantConnectionString);

                ResetTenantIdentitySeeds(tenantConnectionString);

                using var templateConnection = new SqlConnection(baseConnectionString);
                templateConnection.Open();

                using var tenantConnection = new SqlConnection(tenantConnectionString);
                tenantConnection.Open();

                using var transaction = tenantConnection.BeginTransaction();

                var tablesToCopy = new StaticTableDefinition[]
                {
                    new("dbo", "State", keepIdentity: true),
                    new("dbo", "City", keepIdentity: true),
                    new("dbo", "CompanyDetails", keepIdentity: true),
                    new("dbo", "Branch", keepIdentity: true)
                };

                foreach (var table in tablesToCopy)
                    CopyStaticTable(templateConnection, tenantConnection, transaction, table);

                EnsureTenantCompanyRecord(tenantConnection, transaction, companyName, adminDisplayName, adminEmail, plan);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                var wrapped = new InvalidOperationException($"Failed to seed default tenant data for database '{tenantDb}'.", ex);
                wrapped.Log("ProvisionTenant");
                throw wrapped;
            }
        }

        private static void EnsureTenantDatabaseCreated(string baseConnectionString, string tenantDb)
        {
            if (string.IsNullOrWhiteSpace(baseConnectionString) || string.IsNullOrWhiteSpace(tenantDb))
                return;

            var masterBuilder = new SqlConnectionStringBuilder(baseConnectionString);
            if (masterBuilder.ContainsKey("AttachDBFilename"))
                masterBuilder.Remove("AttachDBFilename");
            masterBuilder.InitialCatalog = "master";
            masterBuilder["Database"] = "master";

            using var masterConnection = new SqlConnection(masterBuilder.ConnectionString);
            masterConnection.Open();

            using var command = masterConnection.CreateCommand();
            command.CommandText = @"
DECLARE @dbName sysname = @TenantDatabase;
IF DB_ID(@dbName) IS NULL
BEGIN
    DECLARE @sql NVARCHAR(MAX) = N'CREATE DATABASE [' + REPLACE(@dbName, N']', N']]') + N']';
    EXEC (@sql);
END;

DECLARE @alterSql NVARCHAR(MAX) = N'ALTER DATABASE [' + REPLACE(@dbName, N']', N']]') + N'] SET READ_WRITE WITH ROLLBACK IMMEDIATE;';
EXEC (@alterSql);

SET @alterSql = N'ALTER DATABASE [' + REPLACE(@dbName, N']', N']]') + N'] SET MULTI_USER WITH ROLLBACK IMMEDIATE;';
EXEC (@alterSql);";
            var parameter = command.Parameters.Add("@TenantDatabase", SqlDbType.NVarChar, 128);
            parameter.Value = tenantDb;
            command.ExecuteNonQuery();
        }

        private static void RunTenantMigrations(string baseConnectionString, string tenantDb)
        {
            if (string.IsNullOrWhiteSpace(baseConnectionString) || string.IsNullOrWhiteSpace(tenantDb))
                return;

            var tenantConnectionString = TenantAwareSqlConnections.BuildTenantConnectionString(baseConnectionString, tenantDb);
            var migrationConnectionString = SanitizeConnectionStringForMigrations(tenantConnectionString);
            var assembly = typeof(DataMigrations).Assembly;
            var assemblyLocation = assembly.Location;
            var migrationsPath = string.IsNullOrEmpty(assemblyLocation)
                ? AppContext.BaseDirectory
                : Path.GetDirectoryName(assemblyLocation);

            var conventionSet = new DefaultConventionSet(defaultSchemaName: null, migrationsPath);

            using var serviceProvider = new ServiceCollection()
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .AddSingleton<IConventionSet>(conventionSet)
                .Configure<TypeFilterOptions>(options =>
                {
                    options.Namespace = "AdvanceCRM.Migrations.DefaultDB";
                })
                .Configure<ProcessorOptions>(options =>
                {
                    options.Timeout = TimeSpan.FromMinutes(5);
                })
                .ConfigureRunner(builder =>
                {
                    builder.AddSqlServer();
                    builder.WithGlobalConnectionString(migrationConnectionString);
                    builder.WithMigrationsIn(assembly);
                })
                .BuildServiceProvider(false);

            using var scope = serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

            try
            {
                runner.MigrateUp();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to run migrations for tenant database '{tenantDb}'.", ex);
            }
        }

        private static string SanitizeConnectionStringForMigrations(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return connectionString;

            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                RemoveTrustServerCertificate(builder);
                return builder.ConnectionString;
            }
            catch (ArgumentException)
            {
                var genericBuilder = new DbConnectionStringBuilder
                {
                    ConnectionString = connectionString
                };

                RemoveTrustServerCertificate(genericBuilder);
                return genericBuilder.ConnectionString;
            }
        }

        private static void RemoveTrustServerCertificate(IDictionary builder)
        {
            if (builder == null)
                return;

            var keysToRemove = new List<string>();
            foreach (DictionaryEntry entry in builder)
            {
                var key = entry.Key?.ToString();
                if (string.Equals(key, "TrustServerCertificate", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(key, "Trust Server Certificate", StringComparison.OrdinalIgnoreCase))
                {
                    keysToRemove.Add(key);
                }
            }

            foreach (var key in keysToRemove)
            {
                builder.Remove(key);
            }
        }

        private static void EnsureTenantSchemaReady(string tenantConnectionString)
        {
            if (string.IsNullOrWhiteSpace(tenantConnectionString))
                throw new InvalidOperationException("Tenant connection string is not available.");

            using var connection = new SqlConnection(tenantConnectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT COUNT(*) FROM sys.tables WHERE name = @TableName AND schema_id = SCHEMA_ID('dbo');";
            command.Parameters.AddWithValue("@TableName", "Users");

            var result = Convert.ToInt32(command.ExecuteScalar());
            if (result <= 0)
                throw new InvalidOperationException("Tenant database provisioning failed. Expected table 'dbo.Users' was not created by the migration process.");
        }

        private static void ResetTenantIdentitySeeds(string tenantConnectionString)
        {
            if (string.IsNullOrWhiteSpace(tenantConnectionString))
                return;

            using var connection = new SqlConnection(tenantConnectionString);
            connection.Open();

            var tables = new List<string>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT '[' + s.name + '].[' + t.name + ']' AS TableName FROM sys.identity_columns ic JOIN sys.tables t ON ic.object_id = t.object_id JOIN sys.schemas s ON t.schema_id = s.schema_id";
                using var reader = command.ExecuteReader();
                while (reader.Read())
                    tables.Add(reader.GetString(0));
            }

            foreach (var table in tables)
            {
                using (var checkCommand = connection.CreateCommand())
                {
                    checkCommand.CommandText = $"SELECT TOP 1 1 FROM {table}";
                    var hasRows = checkCommand.ExecuteScalar();
                    if (hasRows != null)
                        continue;
                }

                using var reseed = connection.CreateCommand();
                var quotedTable = table.Replace("'", "''");
                reseed.CommandText = $"DBCC CHECKIDENT ('{quotedTable}', RESEED, 0)";
                reseed.ExecuteNonQuery();
            }
        }

        private static void CopyStaticTable(SqlConnection sourceConnection, SqlConnection tenantConnection, SqlTransaction transaction, StaticTableDefinition table)
        {
            if (sourceConnection == null || tenantConnection == null || table == null)
                return;

            var fullName = $"[{table.Schema}].[{table.Table}]";

            using (var checkCommand = tenantConnection.CreateCommand())
            {
                checkCommand.Transaction = transaction;
                checkCommand.CommandText = $"SELECT TOP 1 1 FROM {fullName}";
                var exists = checkCommand.ExecuteScalar();
                if (exists != null)
                    return;
            }

            var columns = GetColumnNames(sourceConnection, transaction, table.Schema, table.Table);
            if (columns.Count == 0)
                return;

            using var selectCommand = sourceConnection.CreateCommand();
            selectCommand.CommandText = $"SELECT {string.Join(",", columns.Select(c => "[" + c + "]"))} FROM {fullName}";
            using var reader = selectCommand.ExecuteReader();
            if (!reader.HasRows)
                return;

            var options = SqlBulkCopyOptions.Default;
            if (table.KeepIdentity)
                options |= SqlBulkCopyOptions.KeepIdentity;

            using var bulkCopy = new SqlBulkCopy(tenantConnection, options, transaction);
            bulkCopy.DestinationTableName = fullName;
            foreach (var column in columns)
                bulkCopy.ColumnMappings.Add(column, column);
            bulkCopy.WriteToServer(reader);
        }

        private static List<string> GetColumnNames(SqlConnection connection, SqlTransaction transaction, string schema, string table)
        {
            var result = new List<string>();
            using var command = connection.CreateCommand();
            if (transaction != null && transaction.Connection == connection)
                command.Transaction = transaction;
            command.CommandText = @"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = @Schema AND TABLE_NAME = @Table ORDER BY ORDINAL_POSITION";
            command.Parameters.AddWithValue("@Schema", schema);
            command.Parameters.AddWithValue("@Table", table);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                result.Add(reader.GetString(0));
            return result;
        }

        private static void EnsureTenantCompanyRecord(SqlConnection tenantConnection, SqlTransaction transaction, string companyName, string adminDisplayName, string adminEmail, string plan)
        {
            if (tenantConnection == null || transaction == null)
                return;

            if (string.IsNullOrWhiteSpace(companyName))
                return;

            var trimmedName = companyName.Trim();
            if (trimmedName.Length == 0)
                return;

            var normalizedDisplayName = string.IsNullOrWhiteSpace(adminDisplayName) ? trimmedName : adminDisplayName.Trim();
            if (string.IsNullOrWhiteSpace(normalizedDisplayName))
                normalizedDisplayName = trimmedName;

            var normalizedEmail = string.IsNullOrWhiteSpace(adminEmail) ? null : adminEmail.Trim();
            var normalizedPlan = string.IsNullOrWhiteSpace(plan) ? null : plan.Trim();
            var planValue = normalizedPlan != null && normalizedPlan.Length > 250 ? normalizedPlan.Substring(0, 250) : normalizedPlan;
            var emailValue = normalizedEmail != null && normalizedEmail.Length > 200 ? normalizedEmail.Substring(0, 200) : normalizedEmail;

            var companyNameValue = trimmedName.Length > 250 ? trimmedName.Substring(0, 250) : trimmedName;
            var companyAddressValue = normalizedDisplayName.Length > 500 ? normalizedDisplayName.Substring(0, 500) : normalizedDisplayName;
            const string defaultPhone = "N/A";

            var branchNameValue = trimmedName.Length > 200 ? trimmedName.Substring(0, 200) : trimmedName;
            var branchAddressValue = normalizedDisplayName.Length > 800 ? normalizedDisplayName.Substring(0, 800) : normalizedDisplayName;

            var companyColumns = GetColumnNames(tenantConnection, transaction, "dbo", "CompanyDetails");
            bool HasCompanyColumn(string columnName) =>
                companyColumns.Any(x => string.Equals(x, columnName, StringComparison.OrdinalIgnoreCase));

            var branchColumns = GetColumnNames(tenantConnection, transaction, "dbo", "Branch");
            bool HasBranchColumn(string columnName) =>
                branchColumns.Any(x => string.Equals(x, columnName, StringComparison.OrdinalIgnoreCase));

            int companyId;
            using (var selectCompanyId = tenantConnection.CreateCommand())
            {
                selectCompanyId.Transaction = transaction;
                selectCompanyId.CommandText = "SELECT TOP (1) Id FROM [dbo].[CompanyDetails] ORDER BY Id;";
                var existingId = selectCompanyId.ExecuteScalar();

                if (existingId == null || existingId == DBNull.Value)
                {
                    var columns = new List<string> { "[Name]" };
                    var values = new List<string> { "@CompanyName" };

                    if (HasCompanyColumn("Slogan"))
                    {
                        columns.Add("[Slogan]");
                        values.Add("@CompanySlogan");
                    }

                    if (HasCompanyColumn("Address"))
                    {
                        columns.Add("[Address]");
                        values.Add("@CompanyAddress");
                    }

                    if (HasCompanyColumn("Phone"))
                    {
                        columns.Add("[Phone]");
                        values.Add("@CompanyPhone");
                    }

                    if (HasCompanyColumn("EmailId"))
                    {
                        columns.Add("[EmailId]");
                        values.Add("@CompanyEmail");
                    }

                    using var insertCompany = tenantConnection.CreateCommand();
                    insertCompany.Transaction = transaction;
                    insertCompany.CommandText = $"INSERT INTO [dbo].[CompanyDetails] ({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)}); SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    AddParameter(insertCompany, "@CompanyName", companyNameValue);
                    if (HasCompanyColumn("Slogan"))
                        AddParameter(insertCompany, "@CompanySlogan", planValue);
                    if (HasCompanyColumn("Address"))
                        AddParameter(insertCompany, "@CompanyAddress", companyAddressValue);
                    if (HasCompanyColumn("Phone"))
                        AddParameter(insertCompany, "@CompanyPhone", defaultPhone);
                    if (HasCompanyColumn("EmailId"))
                        AddParameter(insertCompany, "@CompanyEmail", emailValue);
                    companyId = Convert.ToInt32(insertCompany.ExecuteScalar());
                }
                else
                {
                    companyId = Convert.ToInt32(existingId);
                    var assignments = new List<string> { "[Name] = @CompanyName" };

                    if (HasCompanyColumn("Slogan"))
                        assignments.Add("[Slogan] = @CompanySlogan");

                    if (HasCompanyColumn("Address"))
                        assignments.Add("[Address] = CASE WHEN LTRIM(RTRIM(ISNULL([Address], ''))) = '' THEN @CompanyAddress ELSE [Address] END");

                    if (HasCompanyColumn("Phone"))
                        assignments.Add("[Phone] = CASE WHEN LTRIM(RTRIM(ISNULL([Phone], ''))) = '' THEN @CompanyPhone ELSE [Phone] END");

                    if (HasCompanyColumn("EmailId"))
                        assignments.Add("[EmailId] = @CompanyEmail");

                    using var updateCompany = tenantConnection.CreateCommand();
                    updateCompany.Transaction = transaction;
                    updateCompany.CommandText = $"UPDATE [dbo].[CompanyDetails] SET {string.Join(", ", assignments)} WHERE Id = @CompanyId;";
                    AddParameter(updateCompany, "@CompanyName", companyNameValue);
                    if (HasCompanyColumn("Slogan"))
                        AddParameter(updateCompany, "@CompanySlogan", planValue);
                    if (HasCompanyColumn("Address"))
                        AddParameter(updateCompany, "@CompanyAddress", companyAddressValue);
                    if (HasCompanyColumn("Phone"))
                        AddParameter(updateCompany, "@CompanyPhone", defaultPhone);
                    if (HasCompanyColumn("EmailId"))
                        AddParameter(updateCompany, "@CompanyEmail", emailValue);
                    AddParameter(updateCompany, "@CompanyId", companyId);
                    updateCompany.ExecuteNonQuery();
                }
            }

            bool hasBranchEmail = HasBranchColumn("Email");
            bool hasBranchAddress = HasBranchColumn("Address");
            bool hasBranchCompanyId = HasBranchColumn("CompanyId");

            using var branchCountCommand = tenantConnection.CreateCommand();
            branchCountCommand.Transaction = transaction;
            branchCountCommand.CommandText = "SELECT COUNT(*) FROM [dbo].[Branch];";
            var branchCount = Convert.ToInt32(branchCountCommand.ExecuteScalar() ?? 0);

            if (branchCount == 0)
            {
                var branchColumnsList = new List<string> { "[Branch]" };
                var branchValuesList = new List<string> { "@BranchName" };

                if (hasBranchEmail)
                {
                    branchColumnsList.Add("[Email]");
                    branchValuesList.Add("@BranchEmail");
                }

                if (hasBranchAddress)
                {
                    branchColumnsList.Add("[Address]");
                    branchValuesList.Add("@BranchAddress");
                }

                if (hasBranchCompanyId)
                {
                    branchColumnsList.Add("[CompanyId]");
                    branchValuesList.Add("@BranchCompanyId");
                }

                using var insertBranch = tenantConnection.CreateCommand();
                insertBranch.Transaction = transaction;
                insertBranch.CommandText = $"INSERT INTO [dbo].[Branch] ({string.Join(", ", branchColumnsList)}) VALUES ({string.Join(", ", branchValuesList)});";
                AddParameter(insertBranch, "@BranchName", branchNameValue);
                if (hasBranchEmail)
                    AddParameter(insertBranch, "@BranchEmail", emailValue);
                if (hasBranchAddress)
                    AddParameter(insertBranch, "@BranchAddress", branchAddressValue);
                if (hasBranchCompanyId)
                    AddParameter(insertBranch, "@BranchCompanyId", companyId);
                insertBranch.ExecuteNonQuery();
            }
            else
            {
                var branchAssignments = new List<string> { "[Branch] = @BranchName" };
                if (hasBranchEmail)
                    branchAssignments.Add("[Email] = @BranchEmail");
                if (hasBranchAddress)
                    branchAssignments.Add("[Address] = CASE WHEN LTRIM(RTRIM(ISNULL([Address], ''))) = '' THEN @BranchAddress ELSE [Address] END");
                if (hasBranchCompanyId)
                    branchAssignments.Add("[CompanyId] = @BranchCompanyId");

                var builder = new StringBuilder();
                builder.Append("UPDATE TOP (1) [dbo].[Branch] SET ");
                builder.Append(string.Join(", ", branchAssignments));
                if (hasBranchCompanyId)
                    builder.Append(" WHERE CompanyId = @BranchCompanyId OR CompanyId IS NULL;");
                else
                    builder.Append(";");

                using var updateBranch = tenantConnection.CreateCommand();
                updateBranch.Transaction = transaction;
                updateBranch.CommandText = builder.ToString();
                AddParameter(updateBranch, "@BranchName", branchNameValue);
                if (hasBranchEmail)
                    AddParameter(updateBranch, "@BranchEmail", emailValue);
                if (hasBranchAddress)
                    AddParameter(updateBranch, "@BranchAddress", branchAddressValue);
                if (hasBranchCompanyId)
                    AddParameter(updateBranch, "@BranchCompanyId", companyId);
                updateBranch.ExecuteNonQuery();
            }
        }

        private sealed class StaticTableDefinition
        {
            public StaticTableDefinition(string schema, string table, bool keepIdentity)
            {
                Schema = schema;
                Table = table;
                KeepIdentity = keepIdentity;
            }

            public string Schema { get; }
            public string Table { get; }
            public bool KeepIdentity { get; }
        }
        private async Task ProvisionTenantAsync(int userId, string companyName, string plan, DateTime licenseStartDate, DateTime licenseEndDate)
        {
            try
            {
                var sub = companyName?.Trim().ToLowerInvariant();
                sub = Regex.Replace(sub ?? string.Empty, @"[^a-z0-9-]", string.Empty);
                sub = sub.Trim('-');
                if (sub.Length > 63)
                    sub = sub.Substring(0, 63);
                sub = string.IsNullOrEmpty(sub) ? null : sub;

                const int dnsStatusMaxLength = 2000;

                string dnsStatus;

                if (!string.IsNullOrEmpty(sub))
                {
                    try
                    {

                        var status = await _subdomainService.CreateSubdomainAsync(sub);
                        dnsStatus = string.IsNullOrWhiteSpace(status) ? "Created" : status.Trim();
                    }
                    catch (Exception ex)
                    {
                        dnsStatus = BuildDnsFailureStatus(companyName, sub, ex);
                        new InvalidOperationException(dnsStatus, ex).Log("ProvisionTenant");

                    }
                }
                else
                {
                    dnsStatus = "Skipped: No valid subdomain generated.";
                }


                if (dnsStatus?.Length > dnsStatusMaxLength)
                    dnsStatus = dnsStatus.Substring(0, dnsStatusMaxLength);


                var port = GenerateFreePort();
                var tenantDb = $"Tenant_{userId}";

                using (var scope = _scopeFactory.CreateScope())
                {
                    var connections = scope.ServiceProvider.GetRequiredService<ISqlConnections>();
                    UserRow userRow = null;
                    using (var connection = connections.NewByKey("Default"))
                    {
                        // insert tenant record and get identity as int
                        var tenantRow = new TenantRow
                        {
                            Name = companyName,
                            Subdomain = sub,
                            DbName = tenantDb,
                            Port = port,
                            Plan = plan,
                            Modules = null,
                            LicenseStartDate = licenseStartDate,
                            LicenseEndDate = licenseEndDate,
                            DnsStatus = dnsStatus
                        };

                        int tenantId;
                        try
                        {
                            tenantId = Convert.ToInt32(connection.InsertAndGetID(tenantRow));
                        }
                        catch (Exception ex)
                        {
                            if (!IsMissingDnsStatusColumn(ex))
                                throw;

                            var warning = new StringBuilder("Tenants table is missing the DnsStatus column. Apply the latest migrations to persist DNS provisioning status.");
                            if (!string.IsNullOrWhiteSpace(dnsStatus))
                            {
                                warning.Append(" Latest status: ").Append(dnsStatus);
                            }

                            new InvalidOperationException(warning.ToString(), ex).Log("ProvisionTenant");


                            tenantId = InsertTenantWithoutDnsStatus(connection, companyName, sub, tenantDb, port, plan, licenseStartDate, licenseEndDate);
                        }


                        // assign user to tenant and store generated url
                        var domain = _config.GetSection("Cloudflare")["RootDomain"];
                        string url = null;
                        if (!string.IsNullOrEmpty(sub) && !string.IsNullOrEmpty(domain))
                            url = $"https://{sub}.{domain}/";
                        connection.UpdateById(new UserRow
                        {
                            UserId = userId,
                            TenantId = tenantId,
                            Url = url
                        });
                        userRow = connection.TryById<UserRow>(userId);
                        var tenantDisplayName = userRow?.DisplayName;
                        SeedTenantDefaults(connections, tenantDb, companyName, tenantDisplayName ?? string.Empty, userRow?.Email, plan);
                    }
                    if (userRow != null)
                    {
                        var seedPlan = string.IsNullOrWhiteSpace(userRow.Plan) ? plan : userRow.Plan;
                        SeedTenantAdminUser(connections, tenantDb, userRow.DisplayName, userRow.Email, userRow.PasswordHash, userRow.PasswordSalt, seedPlan);
                    }

                }

                // invoke provisioning script to start instance and create service (Linux only)
                var scriptPath = "/opt/advancecrm/provision.sh";
                if (OperatingSystem.IsLinux())
                {
                    if (System.IO.File.Exists(scriptPath))

                    {
                        Process.Start(scriptPath, $"{port} {tenantDb}");
                    }
                    else
                    {
                        new FileNotFoundException($"Provisioning script not found at {scriptPath}").Log("ProvisionTenant");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Log("ProvisionTenant");
            }
        }

        private static bool IsMissingDnsStatusColumn(Exception exception)
        {
            if (exception == null)
                return false;

            var visited = new HashSet<Exception>();
            var stack = new Stack<Exception>();
            stack.Push(exception);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current == null || !visited.Add(current))
                    continue;

                if (current is MicrosoftSqlException microsoftException && ContainsMissingDnsStatusMicrosoft(microsoftException))
                    return true;

                if (current is LegacySqlException legacyException && ContainsMissingDnsStatusLegacy(legacyException))
                    return true;

                if (IsMissingDnsStatusMessage(current.Message))
                    return true;

                if (current is AggregateException aggregateException)
                {
                    foreach (var inner in aggregateException.InnerExceptions)
                    {
                        if (inner != null)
                            stack.Push(inner);
                    }
                }

                if (current.InnerException != null)
                    stack.Push(current.InnerException);
            }

            return false;
        }

        private static bool ContainsMissingDnsStatusMicrosoft(MicrosoftSqlException exception)
        {
            if (exception == null)
                return false;

            foreach (MicrosoftSqlError error in exception.Errors)
            {
                if (IsMissingDnsStatusError(error.Number, error.Message))
                    return true;
            }

            return false;
        }

        private static bool ContainsMissingDnsStatusLegacy(LegacySqlException exception)
        {
            if (exception == null)
                return false;

            foreach (LegacySqlError error in exception.Errors)
            {
                if (IsMissingDnsStatusError(error.Number, error.Message))
                    return true;
            }

            return false;
        }

        private static bool IsMissingDnsStatusError(int number, string message)
        {
            if (number == 207 &&
                !string.IsNullOrWhiteSpace(message) &&
                message.IndexOf("DnsStatus", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            return false;
        }

        private static bool IsMissingDnsStatusMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return false;

            if (message.IndexOf("DnsStatus", StringComparison.OrdinalIgnoreCase) < 0)
                return false;

            return message.IndexOf("Invalid column", StringComparison.OrdinalIgnoreCase) >= 0 ||
                message.IndexOf("Unknown column", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static int InsertTenantWithoutDnsStatus(IDbConnection connection, string companyName, string subdomain, string tenantDb, int port, string plan, DateTime licenseStartDate, DateTime licenseEndDate)
        {
            if (connection is null)
                throw new ArgumentNullException(nameof(connection));

            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO [dbo].[Tenants] ([Name], [Subdomain], [DbName], [Port], [Plan], [Modules], [LicenseStartDate], [LicenseEndDate]) VALUES (@Name, @Subdomain, @DbName, @Port, @Plan, @Modules, @LicenseStartDate, @LicenseEndDate);" +
                    "\nSELECT CAST(SCOPE_IDENTITY() AS INT);";

                AddParameter(command, "@Name", companyName);
                AddParameter(command, "@Subdomain", string.IsNullOrEmpty(subdomain) ? null : subdomain);
                AddParameter(command, "@DbName", tenantDb);
                AddParameter(command, "@Port", port);
                AddParameter(command, "@Plan", plan);
                AddParameter(command, "@Modules", DBNull.Value);
                AddParameter(command, "@LicenseStartDate", licenseStartDate);
                AddParameter(command, "@LicenseEndDate", licenseEndDate);

                var result = command.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        private static void AddParameter(IDbCommand command, string name, object value)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(parameter);
        }

        private static string BuildDnsFailureStatus(string tenantName, string subdomain, Exception exception)
        {
            var safeSubdomain = string.IsNullOrEmpty(subdomain) ? "<none>" : subdomain;
            var baseMessage = $"Failed to create Cloudflare DNS record for tenant '{tenantName}' using subdomain '{safeSubdomain}'.";

            if (exception is CloudflareProvisioningException cloudflareEx)
            {
                var builder = new StringBuilder(baseMessage);

                if (cloudflareEx.StatusCode.HasValue)
                {
                    builder.Append(" Cloudflare API responded with HTTP ")
                        .Append((int)cloudflareEx.StatusCode.Value)
                        .Append(' ')
                        .Append(cloudflareEx.StatusCode.Value)
                        .Append('.');

                    var hint = string.IsNullOrWhiteSpace(cloudflareEx.DiagnosticHint)
                        ? null
                        : cloudflareEx.DiagnosticHint.Trim();

                    if (!string.IsNullOrEmpty(hint))
                    {
                        builder.Append(' ').Append(hint);
                    }
                    else if (cloudflareEx.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        builder.Append(" Verify that the API token has DNS edit permissions or configure a valid API key and email.");
                    }
                }
                else
                {
                    builder.Append(" Cloudflare API returned an unexpected error.");
                }

                var response = string.IsNullOrWhiteSpace(cloudflareEx.ResponseContent)
                    ? null
                    : cloudflareEx.ResponseContent.Trim();

                if (!string.IsNullOrEmpty(response))
                {
                    builder.Append(" Response: ").Append(response);
                }
                else if (!string.IsNullOrWhiteSpace(cloudflareEx.Message))
                {
                    builder.Append(' ').Append(cloudflareEx.Message.Trim());
                }

                return builder.ToString();
            }

            var exceptionMessage = exception?.Message;
            if (!string.IsNullOrWhiteSpace(exceptionMessage))
                return $"{baseMessage} {exceptionMessage.Trim()}";

            return baseMessage;
        }

        private static int GenerateFreePort()
        {
            var ipProps = IPGlobalProperties.GetIPGlobalProperties();
            var used = new HashSet<int>(ipProps.GetActiveTcpListeners().Select(p => p.Port));
            var rnd = new Random();
            int port;
            do
            {
                port = rnd.Next(1024, 65535);
            } while (used.Contains(port));
            return port;
        }

        [HttpGet, Route("Activate")]
        public ActionResult Activate(string t,
            [FromServices] ISqlConnections sqlConnections)
        {
            using (var connection = sqlConnections.NewByKey("Default"))
            using (var uow = new UnitOfWork(connection))
            {
                int userId;
                try
                {
                    var bytes = HttpContext.RequestServices
                        .GetDataProtector("Activate").Unprotect(Convert.FromBase64String(t));

                    using (var ms = new MemoryStream(bytes))
                    using (var br = new BinaryReader(ms))
                    {
                        var dt = DateTime.FromBinary(br.ReadInt64());
                        if (dt < DateTime.UtcNow)
                            return Error(Texts.Validation.InvalidActivateToken.ToString(Localizer));

                        userId = br.ReadInt32();
                    }
                }
                catch (Exception)
                {
                    return Error(Texts.Validation.InvalidActivateToken.ToString(Localizer));
                }

                var user = uow.Connection.TryById<UserRow>(userId);
                if (user == null || user.IsActive != false)
                    return Error(Texts.Validation.InvalidActivateToken.ToString(Localizer));

                uow.Connection.UpdateById(new UserRow
                {
                    UserId = user.UserId.Value,
                    IsActive = true
                });

                Cache.InvalidateOnCommit(uow, UserRow.Fields);
                uow.Commit();

                return new RedirectResult("~/Account/Login?activated=" + Uri.EscapeDataString(user.Email));
            }
        }
    }
}











