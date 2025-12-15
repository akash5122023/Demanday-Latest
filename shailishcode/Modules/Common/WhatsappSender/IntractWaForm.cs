using Serenity.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Common.Forms
{
    [FormScript("Common.IntractWa")]
    public class IntractWaForm
    {
        [DisplayName("Number"),Hidden]
        [Required]
        public string Number { get; set; }

        [DisplayName("Template")]
        [LookupEditor("Template.TemplateNameLookup")]
        public string Template { get; set; }

        [DisplayName("Variable Name"),Hidden]
        public string Variable { get; set; }

        [DisplayName("Image")]
        [ImageUploadEditor(FilenameFormat = "IntractTmpMsg/~", CopyToHistory = true)]
        public string Image { get; set; }
    }
}