using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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
using AdvanceCRM.MultiTenancy;
using AdvanceCRM.Membership;
using System;
using System.Data.Common;
using System.IO;
using System.Net.Http.Headers;

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
            services.AddHttpClient<IRazorpayOrderService, RazorpayOrderService>();
            //services.Configure<EnvironmentSettings>(Configuration.GetSection(EnvironmentSettings.SectionKey));
            services.AddHttpContextAccessor();

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "wwwroot")),
                RequestPath = ""
            });
            var legacyContentPath = Path.Combine(env.ContentRootPath, "Content");
            if (Directory.Exists(legacyContentPath))
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(legacyContentPath),
                    RequestPath = "/Content"
                });
            }
            app.UseMiddleware<TenantResolutionMiddleware>();
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
    }
}

