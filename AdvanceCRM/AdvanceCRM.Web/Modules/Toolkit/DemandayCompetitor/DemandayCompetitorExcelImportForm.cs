namespace AdvanceCRM.Toolkit.Forms
{
    using Serenity.ComponentModel;
    using Serenity.Web;
    using System;
    using System.ComponentModel;

    [FormScript("Toolkit.DemandayCompetitorExcelImport")]
    public class DemandayCompetitorExcelImportForm
    {
        [LookupEditor("Masters.DemandayCampaignId"), Required]
        public Int32? CampaignId { get; set; }

        [FileUploadEditor(DisplayFileName = true), Required]
        public String FileName { get; set; }
    }
}
