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

                // Check if ToolkitTMEnquiry record exists
                var toolkitTMEnquiryFields = ToolkitTMEnquiryRow.Fields;
                LogToFile(logPath, $"[QUERY] Looking for existing record...");

                var existing = connection.TryFirst<ToolkitTMEnquiryRow>(
                    q => q.Where(toolkitTMEnquiryFields.DemandayTeleMarketingEnquiryId == (int)row.Id.Value));

                if (existing != null)
                {
                    LogToFile(logPath, $"[UPDATE] Found existing record SrNo={existing.SrNo}, updating...");

                    // Update existing record
                    toolkitTMEnquiryRow.SrNo = existing.SrNo;
                    toolkitTMEnquiryRow.MasterAccountId = row.MasterAccountId;
                    toolkitTMEnquiryRow.CampaignId = row.CampaignId;
                    toolkitTMEnquiryRow.FirstName = row.FirstName;
                    toolkitTMEnquiryRow.LastName = row.LastName;
                    toolkitTMEnquiryRow.Email = row.Email;
                    toolkitTMEnquiryRow.CompanyName = row.CompanyName;
                    toolkitTMEnquiryRow.Timestamp = DateTime.Now;
                    toolkitTMEnquiryRow.DemandayTeleMarketingEnquiryId = row.Id;
                    toolkitTMEnquiryRow.UpdatedOn = DateTime.Now;
                    toolkitTMEnquiryRow.UpdatedBy = "SYNC";

                    connection.UpdateById(toolkitTMEnquiryRow);
                    LogToFile(logPath, $"[UPDATE_DONE] Updated successfully");
                }
                else
                {
                    LogToFile(logPath, $"[INSERT] Creating new record...");

                    // Create new record - let database defaults handle CreatedOn
                    toolkitTMEnquiryRow.MasterAccountId = row.MasterAccountId;
                    toolkitTMEnquiryRow.CampaignId = row.CampaignId;
                    toolkitTMEnquiryRow.FirstName = row.FirstName;
                    toolkitTMEnquiryRow.LastName = row.LastName;
                    toolkitTMEnquiryRow.Email = row.Email;
                    toolkitTMEnquiryRow.CompanyName = row.CompanyName;
                    toolkitTMEnquiryRow.Timestamp = DateTime.Now;
                    toolkitTMEnquiryRow.DemandayTeleMarketingEnquiryId = row.Id;
                    toolkitTMEnquiryRow.CreatedBy = "SYNC";
                    // Don't set CreatedOn - let database DEFAULT handle it

                    LogToFile(logPath, $"[INSERT_CALL] About to call connection.Insert()...");
                    connection.Insert(toolkitTMEnquiryRow);
                    LogToFile(logPath, $"[INSERT_DONE] Insert completed successfully, SrNo={toolkitTMEnquiryRow.SrNo}");
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
