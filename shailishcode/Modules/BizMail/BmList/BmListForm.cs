
namespace AdvanceCRM.BizMail.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("BizMail.BmList")]
    [BasedOnRow(typeof(BmListRow), CheckNames = true)]
    public class BmListForm
    {
        [ReadOnly(true)]
        public String ListId { get; set; }
        public String CompanyName { get; set; }
        public String Name { get; set; }
        public String City { get; set; }
        public String DisplayName { get; set; }

        public String From { get; set; }
        public String ReplyTo { get; set; }
        public String Description { get; set; }
        //[ReadOnly(true), DefaultValue(1), Hidden]
        //public Int32 CompanyId { get; set; }
    }
}