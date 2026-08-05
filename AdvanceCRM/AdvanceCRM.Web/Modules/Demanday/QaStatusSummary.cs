using Serenity.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AdvanceCRM.Demanday
{
    /// <summary>
    /// Row counts per QA Status, feeding the summary bar above the Quality grids.
    /// </summary>
    public class QaStatusSummaryResponse : ServiceResponse
    {
        /// <summary>Every row in the module, whatever its QA Status.</summary>
        public int Total { get; set; }
        /// <summary>One entry per tracked QA Status, always in the same order.</summary>
        public List<QaStatusCount> Items { get; set; } = new List<QaStatusCount>();
    }

    public class QaStatusCount
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }

    public static class QaStatusSummary
    {
        /// <summary>
        /// The statuses the summary bar shows, in display order. Fixed on purpose: a chip stays
        /// on screen showing 0 rather than disappearing when nothing currently carries it.
        /// </summary>
        public static readonly string[] Statuses = { "Disqualified", "Qualified", "TBD", "Pending" };

        /// <summary>
        /// Forced onto the comparison so the match is case-sensitive. The database's own
        /// collation is case-insensitive, which would count "qualified" as "Qualified".
        /// </summary>
        private const string CaseSensitiveCollation = "Latin1_General_CS_AS";

        /// <summary>
        /// Counts a Quality table's rows per QA Status in a single aggregate query - no rows are
        /// read into memory, whatever the table's size.
        /// </summary>
        /// <param name="tableName">Row metadata table name, e.g. "[dbo].[DemandayQuality]".</param>
        /// <param name="columnName">QA Status column name from the same metadata.</param>
        public static QaStatusSummaryResponse Build(IDbConnection connection,
            string tableName, string columnName)
        {
            var response = new QaStatusSummaryResponse();

            // Table and column names come from compiled row metadata; the statuses themselves
            // are bound as parameters.
            var sql = new StringBuilder("SELECT COUNT(*)");
            for (var i = 0; i < Statuses.Length; i++)
            {
                sql.Append($", SUM(CASE WHEN [{columnName}] COLLATE {CaseSensitiveCollation} = @s{i} " +
                           "THEN 1 ELSE 0 END)");
            }
            sql.Append($" FROM {tableName}");

            if (connection.State != ConnectionState.Open)
                connection.Open();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql.ToString();
                for (var i = 0; i < Statuses.Length; i++)
                {
                    var p = cmd.CreateParameter();
                    p.ParameterName = "@s" + i;
                    p.DbType = DbType.String;
                    p.Value = Statuses[i];
                    cmd.Parameters.Add(p);
                }

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    response.Total = ToInt(reader.GetValue(0));
                    for (var i = 0; i < Statuses.Length; i++)
                    {
                        response.Items.Add(new QaStatusCount
                        {
                            Status = Statuses[i],
                            Count = ToInt(reader.GetValue(i + 1))
                        });
                    }
                }
                else
                {
                    // Empty table: still hand back every chip, all on zero.
                    foreach (var status in Statuses)
                        response.Items.Add(new QaStatusCount { Status = status, Count = 0 });
                }
            }

            return response;
        }

        // SUM over no rows comes back as NULL, not 0.
        private static int ToInt(object value)
        {
            return value == null || value == DBNull.Value ? 0 : Convert.ToInt32(value);
        }
    }
}
