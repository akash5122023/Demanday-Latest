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

            // Headers are written only from here. OnStarting runs on the response-start path,
            // before headers are flushed, and exactly once. Writing them from the finally block
            // below instead raced with this callback: HasStarted could flip between the check
            // and the write, so two threads mutated the same header dictionary and corrupted it
            // (IndexOutOfRangeException inside Dictionary.TryInsert).
            context.Response.OnStarting(state =>
            {
                var (httpContext, sw) = ((HttpContext Context, Stopwatch Stopwatch))state;

                // Elapsed-so-far is the time to first byte, which is what Server-Timing reports.
                AppendTimingHeaders(httpContext.Response, sw.Elapsed.TotalMilliseconds);

                return Task.CompletedTask;
            }, (context, stopwatch));

            try
            {
                await next(context);
            }
            finally
            {
                stopwatch.Stop();

                // Logging touches no shared response state, so it is safe here — and unlike
                // OnStarting it also runs for responses that never started (e.g. aborted).
                var totalMs = stopwatch.Elapsed.TotalMilliseconds;
                if (totalMs >= SlowRequestThresholdMilliseconds)
                {
                    logger.LogWarning("Request {Method} {Path} took {Elapsed} ms",
                        context.Request.Method, context.Request.Path, totalMs);
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
