
namespace AdvanceCRM.BizMail.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("BizMail.BizMailQuotation")]
    [BasedOnRow(typeof(BizMailQuotationRow), CheckNames = true)]
    public class BizMailQuotationColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public Masters.MailRuleMaster Rule { get; set; }
        public String BmListListId { get; set; }
      //  public String MwListListId { get; set; }
        public Boolean Status { get; set; }
        public String CompanyName { get; set; }
        [EditLink]
        public String Description { get; set; }
    }
}