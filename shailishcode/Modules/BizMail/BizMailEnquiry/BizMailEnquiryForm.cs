
namespace AdvanceCRM.BizMail.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("BizMail.BizMailEnquiry")]
    [BasedOnRow(typeof(BizMailEnquiryRow), CheckNames = true)]
    public class BizMailEnquiryForm
    {
        public Masters.MailRuleMaster Rule { get; set; }

        public Masters.StatusMaster EnquiryStatus { get; set; }
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
        //public Int32 Rule { get; set; }
        //public Int32 BmListId { get; set; }
        //public Boolean Status { get; set; }
        //public Int32 CompanyId { get; set; }
        //public String Description { get; set; }
        //public Int32 EnquiryStatus { get; set; }
        //public Int32 Type { get; set; }
        //public Int32 SourceId { get; set; }
        //public Int32 StageId { get; set; }
        //public Int32 ClosingType { get; set; }
    }
}