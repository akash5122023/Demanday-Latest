
namespace AdvanceCRM.Masters.Forms
{
    using Serenity.ComponentModel;
    using Serenity.Web;
    using System;

    [FormScript("Masters.CityExcelImport")]
    public class CityExcelImportForm
    {
        [FileUploadEditor(DisplayFileName = true), Required]
        public String FileName { get; set; }
    }
}
