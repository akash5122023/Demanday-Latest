using System;
using System.Data;

namespace AdvanceCRM.Toolkit
{
    // Shared serial-number helpers for the Tool Kit sub-modules. SrNo is globally unique per
    // table and doubles as the upsert key on import.
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
    }
}
