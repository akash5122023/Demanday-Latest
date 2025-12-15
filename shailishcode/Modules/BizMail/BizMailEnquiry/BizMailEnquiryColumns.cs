
namespace AdvanceCRM.BizMail.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("BizMail.BizMailEnquiry")]
    [BasedOnRow(typeof(BizMailEnquiryRow), CheckNames = true)]
    public class BizMailEnquiryColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public Masters.MailRuleMaster Rule { get; set; }
        public String BmListListId { get; set; }
        public Boolean Status { get; set; }
        public String CompanyName { get; set; }
        [EditLink]
        public String Description { get; set; }
        public Int32 EnquiryStatus { get; set; }
        public Int32 Type { get; set; }
        public String Source { get; set; }
        public String Stage { get; set; }
        public Int32 ClosingType { get; set; }
    }
}