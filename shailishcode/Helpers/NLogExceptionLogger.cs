using NLog;
using Serenity.Abstractions;
using System;

namespace AdvanceCRM.Web.Helpers
{
   

    public static  class NLogExceptionLogger 
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static  void Log(this Exception exception, string category = null)
        {
            var logEvent = new LogEventInfo(LogLevel.Error, Logger.Name, exception.Message)
            {
                Exception = exception
            };

            if (!string.IsNullOrEmpty(category))
            {
                logEvent.Properties["Category"] = category;
            }
            Sentry.SentrySdk.CaptureException(exception);
            Logger.Log(logEvent);
        }
    }

}
