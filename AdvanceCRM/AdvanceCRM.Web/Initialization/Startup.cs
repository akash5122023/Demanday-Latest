using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;
using Serenity;
using Serenity.Abstractions;
using Serenity.Data;
using Serenity.Extensions.DependencyInjection;
using Serenity.Localization;
using Serenity.Reporting;
using Serenity.Services;
using Serenity.Web;
using AdvanceCRM.AppServices;
using AdvanceCRM.Common;
using AdvanceCRM.Marketing;
using AdvanceCRM.MultiTenancy;
using AdvanceCRM.Membership;
using AdvanceCRM.Web.Middleware;
using System;
using System.Data.Common;
using System.IO;
using System.Net.Http.Headers;
using System.Linq;
using System.IO.Compression;
using System.Threading;

using Microsoft.EntityFrameworkCore;
using AdvanceCRM.Web.Helpers;
using NLog;
using Microsoft.Extensions.Options;
using MailChimp.Net;

namespace AdvanceCRM
{
    public partial class Startup
    {
        public static string basePath = "";
        public static bool isTest = false;
        public static string connectionString
        {
            get
            {
                return Startup.getConfigValue("Data:Default:ConnectionString");
            }
        }
        public static string getConfigValue(string Id)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            return config.GetValue<string>(Id);
        }
        public static string getHeartBeat
        {
            get
            {
                return Startup.getConfigValue("HeartBeat");
            }
        }

        private readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();
        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
            SqlSettings.AutoQuotedIdentifiers = true;
            RegisterDataProviders();
            logger.Info("Startup");
            Sentry.SentrySdk.CaptureMessage("Startup");
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Fix for sync IO export error: allow synchronous IO for Kestrel and IIS
            services.Configure<Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddSingleton<ITypeSource>(new DefaultTypeSource(new[]
            {
                typeof(LocalTextRegistry).Assembly,
                typeof(ISqlConnections).Assembly,
                typeof(IRow).Assembly,
                typeof(SaveRequestHandler<>).Assembly,
                typeof(IDynamicScriptManager).Assembly,
                typeof(Startup).Assembly,
            }));

            services.Configure<ConnectionStringOptions>(Configuration.GetSection(ConnectionStringOptions.SectionKey));
            services.Configure<CssBundlingOptions>(Configuration.GetSection(CssBundlingOptions.SectionKey));
            services.Configure<LocalTextPackages>(Configuration.GetSection(LocalTextPackages.SectionKey));
            services.Configure<ScriptBundlingOptions>(Configuration.GetSection(ScriptBundlingOptions.SectionKey));
            services.Configure<UploadSettings>(Configuration.GetSection(UploadSettings.SectionKey));
            services.PostConfigure<UploadSettings>(options =>
            {
                options.Path = UploadHelper.ResolveUploadRoot(options.Path, HostEnvironment);
            });
            services.Configure<MultiTenancyOptions>(Configuration.GetSection(MultiTenancyOptions.SectionKey));
            services.Configure<SmtpSettings>(Configuration.GetSection(SmtpSettings.SectionKey));
            services.Configure<RazorpaySettings>(Configuration.GetSection(RazorpaySettings.SectionKey));
            services.Configure<AdvanceCRM.Modules.MailChimp.MailChimpSettings>(Configuration.GetSection(AdvanceCRM.Modules.MailChimp.MailChimpSettings.SectionKey));
            services.AddTransient<MailChimpManager>(sp =>
            {
                var options = sp.GetService<IOptions<AdvanceCRM.Modules.MailChimp.MailChimpSettings>>();
                var apiKey = options?.Value?.ApiKey ?? string.Empty;
                return new MailChimpManager(apiKey);
            });
            services.AddSingleton<IRazorpayPlanService, RazorpayPlanService>();
            services.AddHttpClient<IRazorpayOrderService, RazorpayOrderService>();
            services.AddHttpClient("RazorpayCheckoutAssets", client =>
            {
                client.BaseAddress = new Uri("https://checkout.razorpay.com/");
                client.Timeout = TimeSpan.FromSeconds(15);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/javascript"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/javascript"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.UserAgent.ParseAdd("AdvanceCRM-RazorpayProxy/1.0");
            });
            //services.Configure<EnvironmentSettings>(Configuration.GetSection(EnvironmentSettings.SectionKey));
            services.AddHttpContextAccessor();
            services.AddMemoryCache();

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                    ForwardedHeaders.XForwardedProto |
                    ForwardedHeaders.XForwardedHost;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
                options.RequireHeaderSymmetry = false;
                options.ForwardLimit = null;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes
                    .Concat(new[]
                    {
                        "application/json",
                        "application/javascript",
                        "text/css"
                    });
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            var builder = services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(AutoValidateAntiforgeryTokenAttribute));
                options.Filters.Add(typeof(AntiforgeryCookieResultFilterAttribute));
                options.ModelBinderProviders.Insert(0, new ServiceEndpointModelBinderProvider());
                options.Conventions.Add(new ServiceEndpointActionModelConvention());
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            builder.AddControllersAsServices();

            if (HostEnvironment.IsDevelopment())
                builder.AddRazorRuntimeCompilation();

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(o =>
            {
                o.Cookie.Name = ".AspNetAuth";
                o.LoginPath = new PathString("/Account/Login/");
                o.AccessDeniedPath = new PathString("/Account/AccessDenied");
                o.ExpireTimeSpan = TimeSpan.FromMinutes(120);
                o.SlidingExpiration = true;
            });

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<ITenantAccessor, TenantAccessor>();
            services.AddSingleton<ITenantResolver, TenantResolver>();
            // In single-tenant deployments this still resolves to the central
            // database because MultiTenancy is disabled via configuration.
            services.Replace(ServiceDescriptor.Singleton<ISqlConnections, TenantAwareSqlConnections>());

            services.Replace(ServiceDescriptor.Singleton<IConnectionStrings, TenantAwareSqlConnections>());

            services.AddSingleton<IReportRegistry, ReportRegistry>();
            services.AddSingleton<IDataMigrations, DataMigrations>();
            services.AddSingleton<Common.IEmailSender, Common.EmailSender>();
            services.AddSingleton<Common.ICommonService, Common.CommonService>();
            services.AddHttpClient<Administration.SubdomainService>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddRepositories();
            services.AddServiceHandlers();
            services.AddDynamicScripts();
            services.AddCssBundling();
            services.AddScriptBundling();
            services.AddUploadStorage();
            services.AddSingleton<Administration.IUserPasswordValidator, Administration.UserPasswordValidator>();
            services.AddSingleton<IHttpContextItemsAccessor, HttpContextItemsAccessor>();
            services.AddSingleton<IUserAccessor, Administration.UserAccessor>();
            services.AddSingleton<IUserRetrieveService, Administration.UserRetrieveService>();
            services.AddSingleton<IPermissionService, Administration.PermissionService>();
        }

        public static void InitializeLocalTexts(IServiceProvider services)
        {
            var env = services.GetRequiredService<IWebHostEnvironment>();

            services.AddAllTexts(new[]
            {
                Path.Combine(env.WebRootPath, "Scripts", "serenity", "texts"),
                Path.Combine(env.WebRootPath, "Scripts", "site", "texts"),
                Path.Combine(env.ContentRootPath, "App_Data", "texts")
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IAntiforgery antiforgery)
        {
            // expose service provider for classes using Dependency.Resolve
            Serenity.Extensions.DependencyInjection.Dependency.SetProvider(app.ApplicationServices);
            RowFieldsProvider.SetDefaultFrom(app.ApplicationServices);
            InitializeLocalTexts(app.ApplicationServices);
            LicenseHelper.Initialize(env, Configuration);
            UploadHelper.Configure(Configuration, env);

            ThreadPool.GetMinThreads(out var currentWorkerMin, out var currentIoMin);
            var desiredWorkerMin = Math.Max(currentWorkerMin, Environment.ProcessorCount * 8);
            var desiredIoMin = Math.Max(currentIoMin, Environment.ProcessorCount * 4);
            if (desiredWorkerMin != currentWorkerMin || desiredIoMin != currentIoMin)
            {
                ThreadPool.SetMinThreads(desiredWorkerMin, desiredIoMin);
            }

            var useHttpsRedirection = ShouldRedirectToHttps(app);

            var reqLocOpt = new RequestLocalizationOptions();
            reqLocOpt.SupportedUICultures = UserCultureProvider.SupportedCultures;
            reqLocOpt.SupportedCultures = UserCultureProvider.SupportedCultures;
            reqLocOpt.RequestCultureProviders.Clear();
            reqLocOpt.RequestCultureProviders.Add(new UserCultureProvider());
            app.UseRequestLocalization(reqLocOpt);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                if (useHttpsRedirection)
                {
                    app.UseHsts();
                }
            }

            app.UseForwardedHeaders();
            app.UseMiddleware<ServerTimingMiddleware>();

            if (useHttpsRedirection)
            {
                app.UseHttpsRedirection();
            }

            app.UseMiddleware<RazorpayAssetProxyMiddleware>();

            app.Use(async (context, next) =>
            {
                if (context.Request.ContentType != null &&
                    context.Request.ContentType.IndexOf("application/json", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    var syncIOFeature = context.Features.Get<IHttpBodyControlFeature>();
                    if (syncIOFeature != null)
                        syncIOFeature.AllowSynchronousIO = true;
                }

                await next();
            });

            var staticAssetCacheDuration = TimeSpan.FromDays(30);
            var staticAssetCacheControl = $"public,max-age={(int)staticAssetCacheDuration.TotalSeconds},immutable";

            void ApplyStaticAssetCacheHeaders(StaticFileResponseContext context)
            {
                if (context.File.Name.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
                    return;

                context.Context.Response.Headers["Cache-Control"] = staticAssetCacheControl;
                context.Context.Response.Headers["Expires"] = DateTime.UtcNow.Add(staticAssetCacheDuration).ToString("R");
            }

            app.UseResponseCompression();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ApplyStaticAssetCacheHeaders
            });
            var legacyContentPath = Path.Combine(env.ContentRootPath, "Content");
            if (Directory.Exists(legacyContentPath))
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(legacyContentPath),
                    RequestPath = "/Content",
                    OnPrepareResponse = ApplyStaticAssetCacheHeaders
                });
            }
            // For a single-customer deployment we skip tenant resolution and
            // run entirely against the default (central) database.
            // app.UseMiddleware<TenantResolutionMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDynamicScripts();
            app.UseExceptional();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });

            app.ApplicationServices.GetRequiredService<IDataMigrations>().Initialize();

            // After migrations, attempt a one-time seed of Razorpay keys from configuration/env into central SaaS settings table
            try
            {
                SeedRazorpayKeys(app.ApplicationServices);
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Razorpay key seeding failed (non-fatal)." );
            }
        }

        public static void RegisterDataProviders()
        {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("Microsoft.Data.Sqlite", Microsoft.Data.Sqlite.SqliteFactory.Instance);

            // Uncomment to enable other DB providers:
            // DbProviderFactories.RegisterFactory("FirebirdSql.Data.FirebirdClient", FirebirdSql.Data.FirebirdClient.FirebirdClientFactory.Instance);
            // DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", MySql.Data.MySqlClient.MySqlClientFactory.Instance);
            // DbProviderFactories.RegisterFactory("Npgsql", Npgsql.NpgsqlFactory.Instance);
        }

        private bool ShouldRedirectToHttps(IApplicationBuilder app)
        {
            if (Configuration.GetValue<bool>("Hosting:DisableHttpsRedirection"))
            {
                logger.Info("HTTPS redirection disabled via configuration.");
                return false;
            }

            if (Configuration.GetValue<bool>("Hosting:ForceHttpsRedirection"))
            {
                return true;
            }

            var hasHttpsBinding = HasHttpsBinding(app);

            if (!hasHttpsBinding)
            {
                logger.Info("No HTTPS endpoint detected. Skipping HTTPS redirection to keep the application reachable behind HTTP-only reverse proxies.");
            }

            return hasHttpsBinding;
        }

        private bool HasHttpsBinding(IApplicationBuilder app)
        {
            var serverAddressesFeature = app.ServerFeatures.Get<IServerAddressesFeature>();
            if (serverAddressesFeature?.Addresses?.Any(IsHttpsAddress) == true)
            {
                return true;
            }

            if (HasHttpsUrl(Configuration["urls"]))
            {
                return true;
            }

            if (HasHttpsUrl(Environment.GetEnvironmentVariable("ASPNETCORE_URLS")))
            {
                return true;
            }

            var kestrelEndpoints = Configuration.GetSection("Kestrel:Endpoints");
            foreach (var endpoint in kestrelEndpoints.GetChildren())
            {
                if (IsHttpsAddress(endpoint["Url"]))
                {
                    return true;
                }

                var protocols = endpoint["Protocols"];
                if (!string.IsNullOrEmpty(protocols) && protocols.IndexOf("https", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
            }

            var httpsPortHints = new[]
            {
                Configuration["ASPNETCORE_HTTPS_PORT"],
                Configuration["HTTPS_PORT"],
                Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_PORT"),
                Environment.GetEnvironmentVariable("HTTPS_PORT")
            };

            if (httpsPortHints.Any(v => !string.IsNullOrWhiteSpace(v)))
            {
                return true;
            }

            return false;
        }

        private static bool HasHttpsUrl(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            return value.Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Any(IsHttpsAddress);
        }

        private static bool IsHttpsAddress(string value)
        {
            return !string.IsNullOrWhiteSpace(value) &&
                value.TrimStart().StartsWith("https://", StringComparison.OrdinalIgnoreCase);
        }

        private void SeedRazorpayKeys(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var cfg = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var connections = scope.ServiceProvider.GetRequiredService<ISqlConnections>();
            var tenantAccessor = scope.ServiceProvider.GetService<ITenantAccessor>();

            string keyId = null;
            string keySecret = null;

            var section = cfg.GetSection("Razorpay");
            keyId = section?["KeyId"]?.Trim();
            keySecret = section?["KeySecret"]?.Trim();

            if (string.IsNullOrWhiteSpace(keyId) || string.IsNullOrWhiteSpace(keySecret))
                return; // nothing to seed

            var originalTenant = tenantAccessor?.CurrentTenant;
            try
            {
                if (tenantAccessor != null)
                    tenantAccessor.CurrentTenant = null; // host context

                using var connection = connections.NewByKey("Default");
                // verify table exists
                try
                {
                    var existing = Dapper.SqlMapper.Query<string>(connection, "SELECT [Key] FROM SassApplicationSetting WHERE [Key] IN (@k1,@k2)", new { k1 = "Razorpay.KeyId", k2 = "Razorpay.KeySecret" }).ToList();
                    if (!existing.Contains("Razorpay.KeyId"))
                    {
                        Dapper.SqlMapper.Execute(connection, "INSERT INTO SassApplicationSetting ([Key], Value) VALUES (@k,@v)", new { k = "Razorpay.KeyId", v = keyId });
                    }
                    if (!existing.Contains("Razorpay.KeySecret"))
                    {
                        Dapper.SqlMapper.Execute(connection, "INSERT INTO SassApplicationSetting ([Key], Value) VALUES (@k,@v)", new { k = "Razorpay.KeySecret", v = keySecret });
                    }
                }
                catch
                {
                    // table may not exist (older migration set) - ignore
                }
            }
            finally
            {
                if (tenantAccessor != null)
                    tenantAccessor.CurrentTenant = originalTenant;
            }
        }
    }
}
