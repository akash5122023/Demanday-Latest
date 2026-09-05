namespace AdvanceCRM.Toolkit.Forms
{
    using Serenity.ComponentModel;
    using Serenity.Web;
    using System;
    using System.ComponentModel;

    [FormScript("Toolkit.MasterSupressionExcelImport")]
    public class MasterSupressionExcelImportForm
    {
        // Optional: a row can instead name its own Account Number column, which is created
        // automatically if it doesn't already exist.
        [LookupEditor("Masters.DemandayMasterAccount")]
        [DisplayName("Master Account")]
        public Int32? MasterAccountId { get; set; }

        [FileUploadEditor(DisplayFileName = true), Required]
        public String FileName { get; set; }
    }
}
