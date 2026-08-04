using AdvanceCRM.Demanday;
using Microsoft.AspNetCore.Mvc;
using Serenity.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvanceCRM.Demanday
{
    [Route("Test/[action]")]
    public class DemandayTeleMarketingEnquiryTestController : Controller
    {
        private readonly ITMEnquirySyncHandler syncHandler;
        private readonly ISqlConnections sqlConnections;

        public DemandayTeleMarketingEnquiryTestController(ITMEnquirySyncHandler syncHandler, ISqlConnections sqlConnections)
        {
            this.syncHandler = syncHandler;
            this.sqlConnections = sqlConnections;
        }

        [HttpGet]
        public IActionResult TestSync()
        {
            var debugLog = new List<string>();

            try
            {
                debugLog.Add("1. Starting TestSync");

                using (var connection = sqlConnections.NewByKey("Default"))
                {
                    debugLog.Add("2. Got connection");

                    // Get the last DemandayTeleMarketingEnquiry record using List
                    var rows = connection.List<DemandayTeleMarketingEnquiryRow>();
                    debugLog.Add($"3. Retrieved {rows.Count} DemandayTeleMarketingEnquiry rows");

                    var lastRow = rows.OrderByDescending(r => r.Id).FirstOrDefault();
                    debugLog.Add($"4. Last row: ID={lastRow?.Id}");

                    if (lastRow == null)
                        return Ok(new { success = false, message = "No DemandayTeleMarketingEnquiry records found", debug = debugLog });

                    // Manually trigger sync
                    debugLog.Add($"5. About to create UnitOfWork");
                    using (var uow = new UnitOfWork(connection))
                    {
                        debugLog.Add($"6. Created UnitOfWork, about to call syncHandler");
                        syncHandler.SyncToTMEnquiry(uow, lastRow);
                        debugLog.Add($"7. syncHandler.SyncToTMEnquiry completed");
                        uow.Commit();
                        debugLog.Add($"8. uow.Commit() completed");
                    }

                    // Check if record was synced - use List to avoid SQL generation issues
                    debugLog.Add($"9. Checking ToolkitTMEnquiry for synced record");
                    var syncedRows = connection.List<Toolkit.ToolkitTMEnquiryRow>();
                    debugLog.Add($"10. Found {syncedRows.Count} rows in ToolkitTMEnquiry");

                    var syncedRecord = syncedRows.FirstOrDefault(r => r.DemandayTeleMarketingEnquiryId == lastRow.Id);
                    debugLog.Add($"11. Synced record found: {(syncedRecord != null ? "YES" : "NO")}");

                    return Ok(new
                    {
                        success = syncedRecord != null,
                        message = syncedRecord != null ? "Sync successful!" : "Sync failed - record not found in ToolkitTMEnquiry",
                        sourceId = lastRow.Id,
                        sourceName = lastRow.FirstName,
                        syncedId = syncedRecord?.SrNo,
                        syncedName = syncedRecord?.FirstName,
                        debug = debugLog
                    });
                }
            }
            catch (Exception ex)
            {
                debugLog.Add($"EXCEPTION: {ex.GetType().Name}: {ex.Message}");
                debugLog.Add($"STACK: {ex.StackTrace}");
                return Ok(new { success = false, error = ex.Message, stackTrace = ex.StackTrace, debug = debugLog });
            }
        }
    }
}
