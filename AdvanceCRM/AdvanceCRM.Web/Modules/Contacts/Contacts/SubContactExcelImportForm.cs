
namespace AdvanceCRM.Contacts.Forms
{
    using Serenity.ComponentModel;
    using Serenity.Web;
    using System;


    [FormScript("Contacts.SubContactExcelImport")]
    public class SubContactExcelImportForm
    {
        [FileUploadEditor(DisplayFileName = true), Required]
        public String FileName { get; set; }
    }
}
