
namespace AdvanceCRM.Masters.Forms
{
    using Serenity.ComponentModel;
    using System;

    [FormScript("Masters.ExcelImportQuestionsAnswers")]
    public class ExcelImportQuestionsAnswersForm
    {
        [FileUploadEditor(DisplayFileName = true), Required]
        public String FileName { get; set; }
    }
}
