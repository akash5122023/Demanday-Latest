
namespace AdvanceCRM.Template.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Template.QuickMailTemplate")]
    [BasedOnRow(typeof(QuickMailTemplateRow), CheckNames = true)]
    public class QuickMailTemplateForm
    {
        //public String Subject { get; set; }
        //public String Message { get; set; }
        //public String Attachments { get; set; }
        [DisplayName("Name")]
        [Required, FullWidth]
        public String Name { get; set; }
        [DisplayName("Subject")]
        [Required, FullWidth]
        public String Subject { get; set; }

        [DisplayName("Message")]
        [Required, HtmlContentEditor, FullWidth]
        public String Message { get; set; }

        [DisplayName("Attachment")]
        [MultipleImageUploadEditor(FilenameFormat = "QuickMail/~", CopyToHistory = true, AllowNonImage = true)]
        public String Attachments { get; set; }
    }
}