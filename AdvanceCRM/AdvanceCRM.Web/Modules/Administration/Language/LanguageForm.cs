
namespace AdvanceCRM.Administration.Forms
{
    using Serenity.ComponentModel;
    using System;

    [FormScript("Administration.Language")]
    [BasedOnRow(typeof(LanguageRow))]
    public class LanguageForm
    {
        public String LanguageId { get; set; }
        public String LanguageName { get; set; }
    }
}