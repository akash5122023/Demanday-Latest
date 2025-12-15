using Serenity.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Common.Forms
{
    [FormScript("Common.QuickMail")]
    public class QuickMailForm
    {
        [DisplayName("Email To")]
        [Required, FullWidth]
        public String EmailTo { get; set; }

        [DisplayName("Templates")]
        [LookupEditor("Template.QuickMailTemplate"), FullWidth]
        public Int32? TemplateId { get; set; }

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