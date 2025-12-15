using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace AdvanceCRM.Web.Middleware
{
    public class ServerTimingMiddleware
    {
        private const double SlowRequestThresholdMilliseconds = 1000d;
        private readonly RequestDelegate next;
        private readonly ILogger<ServerTimingMiddleware> logger;

        public ServerTimingMiddleware(RequestDelegate next, ILogger<ServerTimingMiddleware> logger)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var stopwatch = Stopwatch.StartNew();

            context.Response.OnStarting(state =>
            {
                var (httpContext, sw, log) = ((HttpContext Context, Stopwatch Stopwatch, ILogger<ServerTimingMiddleware> Logger))state;

                if (sw.IsRunning)
                    sw.Stop();

                var totalMs = sw.Elapsed.TotalMilliseconds;
                AppendTimingHeaders(httpContext.Response, totalMs);

                if (totalMs >= SlowRequestThresholdMilliseconds)
                {
                    log.LogWarning("Request {Method} {Path} took {Elapsed} ms", httpContext.Request.Method, httpContext.Request.Path, totalMs);
                }

                return Task.CompletedTask;
            }, (context, stopwatch, logger));

            try
            {
                await next(context);
            }
            finally
            {
                if (!context.Response.HasStarted)
                {
                    if (stopwatch.IsRunning)
                        stopwatch.Stop();

                    var totalMs = stopwatch.Elapsed.TotalMilliseconds;
                    AppendTimingHeaders(context.Response, totalMs);

                    if (totalMs >= SlowRequestThresholdMilliseconds)
                    {
                        logger.LogWarning("Request {Method} {Path} took {Elapsed} ms", context.Request.Method, context.Request.Path, totalMs);
                    }
                }
            }
        }

        private static void AppendTimingHeaders(HttpResponse response, double totalMs)
        {
            var formatted = totalMs.ToString("F1", CultureInfo.InvariantCulture);
            var timingValue = $"app;dur={formatted}";

            if (response.Headers.TryGetValue("Server-Timing", out var existing) && !StringValues.IsNullOrEmpty(existing))
            {
                response.Headers["Server-Timing"] = StringValues.Concat(existing, timingValue);
            }
            else
            {
                response.Headers["Server-Timing"] = timingValue;
            }

            response.Headers["X-App-Processing-Time"] = formatted + "ms";
        }
    }
}
