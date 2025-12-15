
namespace AdvanceCRM.BizMail.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("BizMail.BmTemplate")]
    [BasedOnRow(typeof(BmTemplateRow), CheckNames = true)]
    public class BmTemplateColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String TemplateUid { get; set; }
        [EditLink]
        public String Name { get; set; }
        public String Content { get; set; }
        public Int32 InlineCss { get; set; }
    }
}