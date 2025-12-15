namespace AdvanceCRM.Settings
{
    using System;
    using MicrosoftSqlException = Microsoft.Data.SqlClient.SqlException;
    using LegacySqlException = System.Data.SqlClient.SqlException;

    internal static class ProductModulePricingHelper
    {
        public static bool IsMissingPricingColumnException(Exception? exception)
        {
            while (exception != null)
            {
                if (exception is MicrosoftSqlException microsoft && ContainsMissingPricingColumnMessage(microsoft.Message))
                    return true;

                if (exception is LegacySqlException legacy && ContainsMissingPricingColumnMessage(legacy.Message))
                    return true;

                exception = exception.InnerException;
            }

            return false;
        }

        private static bool ContainsMissingPricingColumnMessage(string? message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return false;

            return message.IndexOf("Invalid column name 'Price'", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   message.IndexOf("Invalid column name 'Currency'", StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
