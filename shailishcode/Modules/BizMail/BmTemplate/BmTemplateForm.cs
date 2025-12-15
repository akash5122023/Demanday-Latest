
namespace AdvanceCRM.BizMail.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Default.BmTemplate")]
    [BasedOnRow(typeof(BmTemplateRow), CheckNames = true)]
    public class BmTemplateForm
    {
        [ReadOnly(true)]
        public String TemplateUid { get; set; }
        public String Name { get; set; }
        //[HtmlContentEditor]
        public String Content { get; set; }
        [Hidden]
        public Int32 InlineCss { get; set; }
    }
}