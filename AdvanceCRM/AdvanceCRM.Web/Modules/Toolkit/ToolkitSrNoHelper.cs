using System;
using System.Data;

namespace AdvanceCRM.Toolkit
{
    // Shared serial-number helpers for the Tool Kit sub-modules. SrNo doubles as the upsert key
    // on import. It is unique per table everywhere except Master Suppression, which is imported
    // account-wise and numbers each Master Account separately - see the scoped overload below.
    internal static class ToolkitSrNoHelper
    {
        // Next free SrNo for a table (current max + 1, or 1 when empty). Used when a record is
        // added through the dialog without a SrNo. There is a small race under heavy concurrency,
        // but the table's filtered unique index rejects any collision rather than duplicating.
        public static int NextSrNo(IDbConnection connection, string qualifiedTableName)
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT MAX([SrNo]) FROM {qualifiedTableName}";
                var result = cmd.ExecuteScalar();
                var max = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                return max + 1;
            }
        }

        // Next free SrNo inside one scope column value (current max + 1, or 1 when that scope has
        // no rows yet), so each Master Account restarts its numbering at 1. Rows whose scope
        // column is null form a scope of their own, matching how the filtered unique index groups
        // them. scopeColumn is a compile-time constant at every call site, never user input.
        public static int NextSrNo(IDbConnection connection, string qualifiedTableName,
            string scopeColumn, int? scopeValue)
        {
            using (var cmd = connection.CreateCommand())
            {
                if (scopeValue == null)
                {
                    cmd.CommandText =
                        $"SELECT MAX([SrNo]) FROM {qualifiedTableName} WHERE [{scopeColumn}] IS NULL";
                }
                else
                {
                    cmd.CommandText =
                        $"SELECT MAX([SrNo]) FROM {qualifiedTableName} WHERE [{scopeColumn}] = @scope";

                    var parameter = cmd.CreateParameter();
                    parameter.ParameterName = "@scope";
                    parameter.Value = scopeValue.Value;
                    cmd.Parameters.Add(parameter);
                }

                var result = cmd.ExecuteScalar();
                var max = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                return max + 1;
            }
        }
    }
}
