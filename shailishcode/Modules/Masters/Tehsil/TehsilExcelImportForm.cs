
namespace AdvanceCRM.Masters.Forms
{
    using Serenity.ComponentModel;
    using Serenity.Web;
    using System;

    [FormScript("Masters.TehsilExcelImport")]
    public class TehsilExcelImportForm
    {
        [FileUploadEditor(DisplayFileName = true), Required]
        public String FileName { get; set; }
    }
}
