using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sentry;
using Serilog;
using OfficeOpenXml;
namespace AdvanceCRM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (System.Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog((context, services, configuration) =>
                    configuration.ReadFrom.Configuration(context.Configuration))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStaticWebAssets();
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSentry((context, o) =>
                    {
                        var cfg = context.Configuration;
                        var dsn = cfg["Sentry:Dsn"];

                        // Enable Sentry only when explicitly turned on AND a DSN is configured.
                        // Default: OFF in Development (so a locally-unreachable Sentry server can
                        // never stall requests), ON elsewhere.
                        var enabled = cfg.GetValue<bool?>("Sentry:Enabled")
                                      ?? !context.HostingEnvironment.IsDevelopment();

                        if (!enabled || string.IsNullOrWhiteSpace(dsn))
                        {
                            // An empty DSN makes the SDK inert: no background worker, no network
                            // calls, CaptureException/CaptureMessage become no-ops.
                            o.Dsn = string.Empty;
                            return;
                        }

                        o.Dsn = dsn;
                        o.Debug = false;
                        // Sample a fraction of transactions instead of every single request.
                        o.TracesSampleRate = cfg.GetValue<double?>("Sentry:TracesSampleRate") ?? 0.1;
                        // Never let an unreachable/slow Sentry server block app shutdown or requests.
                        o.ShutdownTimeout = System.TimeSpan.FromSeconds(2);
                    });
                })
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    config.AddJsonFile("appsettings.bundles.json");
                    config.AddJsonFile($"appsettings.machine.json", optional: true);
                });
        }
    }
}
