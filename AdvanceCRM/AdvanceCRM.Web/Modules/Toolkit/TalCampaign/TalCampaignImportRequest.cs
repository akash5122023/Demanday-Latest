using Serenity.Services;
using System;

namespace AdvanceCRM.Toolkit
{
    // Request for the campaign-wise TAL Campaign Excel upload: the Team Leader picks a Campaign in the
    // dialog and uploads a file whose rows are assigned to users by the "User Name" column.
    public class TalCampaignExcelImportRequest : ServiceRequest
    {
        public String FileName { get; set; }
        public Int32? CampaignId { get; set; }
    }
}
