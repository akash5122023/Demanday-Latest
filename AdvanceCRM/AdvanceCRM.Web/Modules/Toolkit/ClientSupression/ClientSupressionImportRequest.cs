using Serenity.Services;
using System;

namespace AdvanceCRM.Toolkit
{
    // The user picks a Campaign in the dialog and uploads a file of suppression rows;
    // every imported row is tagged with that Campaign (and its parent Master Account).
    public class ClientSupressionExcelImportRequest : ServiceRequest
    {
        public String FileName { get; set; }
        public Int32? CampaignId { get; set; }
    }
}
