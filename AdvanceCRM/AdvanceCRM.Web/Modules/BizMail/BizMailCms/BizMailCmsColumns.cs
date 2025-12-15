
namespace AdvanceCRM.BizMail.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("BizMail.BizMailCms")]
    [BasedOnRow(typeof(BizMailCmsRow), CheckNames = true)]
    public class BizMailCmsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public Masters.BizMailRulesMaster Rule { get; set; }
        public String BmListListId { get; set; }
        public Boolean Status { get; set; }
        public String CompanyName { get; set; }
        [EditLink]
        public String Description { get; set; }
    }
}