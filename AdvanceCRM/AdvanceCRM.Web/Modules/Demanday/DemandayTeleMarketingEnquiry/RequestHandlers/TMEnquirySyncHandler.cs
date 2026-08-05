using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using AdvanceCRM.Toolkit;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryRow;

namespace AdvanceCRM.Demanday
{
    /// <summary>
    /// Handler that automatically syncs DemandayTeleMarketingEnquiry records to TMEnquiry table
    /// </summary>
    public interface ITMEnquirySyncHandler
    {
        void SyncToTMEnquiry(IUnitOfWork uow, MyRow row);
    }

    public class TMEnquirySyncHandler : ITMEnquirySyncHandler
    {
        private readonly IRequestContext context;

        public TMEnquirySyncHandler(IRequestContext context)
        {
            this.context = context;
        }

        public void SyncToTMEnquiry(IUnitOfWork uow, MyRow row)
        {
            if (row == null || row.Id == null)
                return;

            var logPath = @"C:\temp\tm_sync_test.log";

            try
            {
                LogToFile(logPath, $"[START] SyncToTMEnquiry called for ID={row.Id}, Name={row.FirstName}");

                var connection = uow.Connection;
                var toolkitTMEnquiryRow = new ToolkitTMEnquiryRow();

                // Check if ToolkitTMEnquiry record exists.
                // Only SrNo is selected - TryFirst does not select any field on its own,
                // and selecting the whole row would also pull in the joined expression fields.
                var fld = ToolkitTMEnquiryRow.Fields;
                LogToFile(logPath, $"[QUERY] Looking for existing record...");

                var existing = connection.TryFirst<ToolkitTMEnquiryRow>(q => q
                    .Select(fld.SrNo)
                    .Where(fld.DemandayTeleMarketingEnquiryId == row.Id.Value));

                // On update the request row only carries the fields the client actually sent,
                // so copy a field only when it was assigned - otherwise a partial save would
                // wipe the existing ToolkitTMEnquiry values with nulls.
                var src = MyRow.Fields;
                if (row.IsAssigned(src.MasterAccountId))
                    toolkitTMEnquiryRow.MasterAccountId = row.MasterAccountId;
                // TM Enquiry stores the campaign's text value; ToolkitTMEnquiry keys on
                // DemandayCampaignId.Id like every other Tool Kit module, so resolve it here.
                if (row.IsAssigned(src.CampaignId))
                    toolkitTMEnquiryRow.CampaignId = ResolveCampaignId(connection, row.MasterAccountId, row.CampaignId);
                if (row.IsAssigned(src.FirstName))
                    toolkitTMEnquiryRow.FirstName = row.FirstName;
                if (row.IsAssigned(src.LastName))
                    toolkitTMEnquiryRow.LastName = row.LastName;
                if (row.IsAssigned(src.Email))
                    toolkitTMEnquiryRow.Email = row.Email;
                if (row.IsAssigned(src.CompanyName))
                    toolkitTMEnquiryRow.CompanyName = row.CompanyName;

                if (existing != null)
                {
                    LogToFile(logPath, $"[UPDATE] Found existing record SrNo={existing.SrNo}, updating...");

                    toolkitTMEnquiryRow.SrNo = existing.SrNo;
                    if (row.IsAssigned(src.Date))
                        toolkitTMEnquiryRow.Timestamp = row.Date ?? DateTime.Now;
                    toolkitTMEnquiryRow.UpdatedOn = DateTime.Now;
                    toolkitTMEnquiryRow.UpdatedBy = "SYNC";

                    connection.UpdateById(toolkitTMEnquiryRow);
                    LogToFile(logPath, $"[UPDATE_DONE] Updated successfully");
                }
                else
                {
                    LogToFile(logPath, $"[INSERT] Creating new record...");

                    // Create new record - let database defaults handle CreatedOn
                    toolkitTMEnquiryRow.Timestamp = row.Date ?? DateTime.Now;
                    toolkitTMEnquiryRow.DemandayTeleMarketingEnquiryId = row.Id;
                    toolkitTMEnquiryRow.CreatedBy = "SYNC";
                    // Don't set CreatedOn - let database DEFAULT handle it

                    LogToFile(logPath, $"[INSERT_CALL] About to call connection.Insert()...");
                    connection.Insert(toolkitTMEnquiryRow);
                    LogToFile(logPath, $"[INSERT_DONE] Insert completed successfully");
                }

                LogToFile(logPath, $"[END_SUCCESS] Sync completed successfully");
            }
            catch (Exception ex)
            {
                LogToFile(logPath, $"[ERROR] Exception occurred: {ex.GetType().Name}: {ex.Message}");
                LogToFile(logPath, $"[ERROR_STACK] {ex.StackTrace}");

                System.Diagnostics.Debug.WriteLine($"Error syncing ToolkitTMEnquiry: {ex.Message}");
            }
        }

        /// <summary>
        /// Maps a Campaign ID text ("79580") to its DemandayCampaignId key. Scoped to the
        /// enquiry's Master Account - the same Campaign ID text may exist under more than one
        /// account, and a synced row must never point at another account's campaign.
        /// Returns null when the text is blank or has no match, which just leaves the column empty.
        /// </summary>
        private static int? ResolveCampaignId(IDbConnection connection, int? masterAccountId, string campaignText)
        {
            if (masterAccountId == null || string.IsNullOrWhiteSpace(campaignText))
                return null;

            var fld = Masters.DemandayCampaignIdRow.Fields;
            var match = connection.TryFirst<Masters.DemandayCampaignIdRow>(q => q
                .Select(fld.Id)
                .Where(fld.DemandayMasterAccountId == masterAccountId.Value &
                       fld.CampaignId == campaignText.Trim()));

            return match?.Id;
        }

        private void LogToFile(string path, string message)
        {
            try
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));
                System.IO.File.AppendAllText(path, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}\n");
            }
            catch { }
        }
    }
}
