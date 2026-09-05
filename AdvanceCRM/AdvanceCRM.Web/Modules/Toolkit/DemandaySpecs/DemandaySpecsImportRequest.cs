using Serenity.Services;
using System;

namespace AdvanceCRM.Toolkit
{
    // The Team Leader can pick a Campaign in the dialog as the default every row falls back to;
    // a row may instead (or also) name its own Master Account Id / Campaign Id column, created
    // automatically if it doesn't already exist.
    public class DemandaySpecsExcelImportRequest : ServiceRequest
    {
        public String FileName { get; set; }
        public Int32? CampaignId { get; set; }
    }
}
