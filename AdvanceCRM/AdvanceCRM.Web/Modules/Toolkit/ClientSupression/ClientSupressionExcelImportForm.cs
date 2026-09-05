namespace AdvanceCRM.Toolkit.Forms
{
    using Serenity.ComponentModel;
    using Serenity.Web;
    using System;
    using System.ComponentModel;

    [FormScript("Toolkit.ClientSupressionExcelImport")]
    public class ClientSupressionExcelImportForm
    {
        // Optional: a row can instead name its own Campaign Id / Master Account Id column,
        // which is created automatically if it doesn't already exist.
        [LookupEditor("Masters.DemandayCampaignId")]
        public Int32? CampaignId { get; set; }

        [FileUploadEditor(DisplayFileName = true), Required]
        public String FileName { get; set; }
    }
}
