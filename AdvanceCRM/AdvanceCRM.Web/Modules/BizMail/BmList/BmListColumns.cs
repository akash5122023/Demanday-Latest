
namespace AdvanceCRM.BizMail.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("BizMail.BmList")]
    [BasedOnRow(typeof(BmListRow), CheckNames = true)]
    public class BmListColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String ListId { get; set; }
        public String CompanyName { get; set; }
        public String Name { get; set; }
        public String City { get; set; }
        public String DisplayName { get; set; }
        public String Description { get; set; }
        public String From { get; set; }
        public String ReplyTo { get; set; }
    }
}