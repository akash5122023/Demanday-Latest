using Serenity.Services;
using System;

namespace AdvanceCRM.Toolkit
{
    // The Team Leader picks a Campaign in the dialog and uploads a file of specification rows;
    // every imported row is tagged with that Campaign (and its parent Master Account).
    public class DemandaySpecsExcelImportRequest : ServiceRequest
    {
        public String FileName { get; set; }
        public Int32? CampaignId { get; set; }
    }
}
