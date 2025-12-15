
namespace AdvanceCRM.Masters.Forms
{
    using Serenity.ComponentModel;
    using Serenity.Web;
    using System;

    [FormScript("Masters.VillageExcelImport")]
    public class VillageExcelImportForm
    {
        [FileUploadEditor(DisplayFileName = true), Required]
        public String FileName { get; set; }
    }
}
