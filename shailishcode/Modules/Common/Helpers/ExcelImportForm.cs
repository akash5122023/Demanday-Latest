
namespace AdvanceCRM.Common.Forms
{
    using Serenity.ComponentModel;
    using Serenity.Web;
    using System;

    [FormScript("Common.ExcelImport")]
    public class ExcelImportForm
    {
        [FileUploadEditor(DisplayFileName = true), Required]
        public String FileName { get; set; }
    }
}
