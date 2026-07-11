namespace AdvanceCRM.Toolkit.Forms
{
    using Serenity.ComponentModel;
    using Serenity.Web;
    using System;
    using System.ComponentModel;

    [FormScript("Toolkit.MasterSupressionExcelImport")]
    public class MasterSupressionExcelImportForm
    {
        [LookupEditor("Masters.DemandayMasterAccount"), Required]
        [DisplayName("Master Account")]
        public Int32? MasterAccountId { get; set; }

        [FileUploadEditor(DisplayFileName = true), Required]
        public String FileName { get; set; }
    }
}
