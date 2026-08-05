using Serenity.Services;
using System;
using System.Collections.Generic;
using System.Data;

namespace AdvanceCRM.Demanday
{
    /// <summary>
    /// Row counts per Master Account and Campaign ID, feeding the summary bar above the
    /// Team Leader grids.
    /// </summary>
    public class AccountCampaignSummaryResponse : ServiceResponse
    {
        /// <summary>Every row in the module.</summary>
        public int Total { get; set; }
        /// <summary>One entry per Account + Campaign pair, biggest count first.</summary>
        public List<AccountCampaignCount> Items { get; set; } = new List<AccountCampaignCount>();
    }

    public class AccountCampaignCount
    {
        /// <summary>Kept so clicking the row can narrow the grid to this account.</summary>
        public int? MasterAccountId { get; set; }
        public string AccountNumber { get; set; }
        public string CampaignId { get; set; }
        public int Count { get; set; }
    }

    public static class AccountCampaignSummary
    {
        /// <summary>Shown when a row carries no Master Account / Campaign ID.</summary>
        private const string NotSetLabel = "(Not set)";

        /// <summary>
        /// Groups a Team Leader table by Master Account and Campaign ID in one query - no rows
        /// are read into memory, whatever the table's size. The account number comes from a join,
        /// because the module itself only stores the account key.
        /// </summary>
        /// <param name="tableName">Row metadata table name, e.g. "[dbo].[DemandayTeamLeader]".</param>
        /// <param name="accountIdColumn">Master Account column name from the same metadata.</param>
        /// <param name="campaignIdColumn">Campaign ID column name from the same metadata.</param>
        public static AccountCampaignSummaryResponse Build(IDbConnection connection,
            string tableName, string accountIdColumn, string campaignIdColumn)
        {
            var response = new AccountCampaignSummaryResponse();

            var accFields = Masters.DemandayMasterAccountRow.Fields;
            var accTable = accFields.TableName;
            var accId = accFields.Id.Name;
            var accNumber = accFields.AccountNumber.Name;

            // Every name here comes from compiled row metadata, never from the request.
            var sql =
                $"SELECT t.[{accountIdColumn}], a.[{accNumber}], t.[{campaignIdColumn}], COUNT(*) " +
                $"FROM {tableName} t " +
                $"LEFT JOIN {accTable} a ON a.[{accId}] = t.[{accountIdColumn}] " +
                $"GROUP BY t.[{accountIdColumn}], a.[{accNumber}], t.[{campaignIdColumn}] " +
                $"ORDER BY COUNT(*) DESC";

            if (connection.State != ConnectionState.Open)
                connection.Open();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql;
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var accountId = reader.IsDBNull(0) ? (int?)null : Convert.ToInt32(reader.GetValue(0));
                    var accountNumber = reader.IsDBNull(1) ? null : Convert.ToString(reader.GetValue(1));
                    var campaignId = reader.IsDBNull(2) ? null : Convert.ToString(reader.GetValue(2));
                    var count = Convert.ToInt32(reader.GetValue(3));

                    response.Total += count;
                    response.Items.Add(new AccountCampaignCount
                    {
                        MasterAccountId = accountId,
                        AccountNumber = string.IsNullOrWhiteSpace(accountNumber) ? NotSetLabel : accountNumber.Trim(),
                        CampaignId = string.IsNullOrWhiteSpace(campaignId) ? NotSetLabel : campaignId.Trim(),
                        Count = count
                    });
                }
            }

            return response;
        }
    }
}
