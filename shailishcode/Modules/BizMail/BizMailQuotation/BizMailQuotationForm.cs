
namespace AdvanceCRM.BizMail.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("BizMail.BizMailQuotation")]
    [BasedOnRow(typeof(BizMailQuotationRow), CheckNames = true)]
    public class BizMailQuotationForm
    {
        public Masters.MailRuleMaster Rule { get; set; }
        public Masters.StatusMaster QuotationStatus { get; set; }
        public Int32 ClosingType { get; set; }
        public Int32 SourceId { get; set; }
        public Int32 StageId { get; set; }
        public Int32 Type { get; set; }
        public Int32 BmListId { get; set; }
        [BSSwitchEditor]
        public Boolean Status { get; set; }
        [ReadOnly(true),DefaultValue(1)]
        public Int32 CompanyId { get; set; }
        [TextAreaEditor(Rows = 3)]
        public String Description { get; set; }
       
    }
}