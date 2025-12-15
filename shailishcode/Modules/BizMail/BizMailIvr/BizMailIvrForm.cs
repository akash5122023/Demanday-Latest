
namespace AdvanceCRM.BizMail.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("BizMail.BizMailIvr")]
    [BasedOnRow(typeof(BizMailIvrRow), CheckNames = true)]
    public class BizMailIvrForm
    {
        [ReadOnly(true), DefaultValue(6)]
        public Masters.BizMailRulesMaster Rule { get; set; }
        public Int32 BmListId { get; set; }
        [BSSwitchEditor]
        public Boolean Status { get; set; }
        [ReadOnly(true)]
        public Int32 CompanyId { get; set; }
        [TextAreaEditor(Rows = 3)]
        public String Description { get; set; }
        
    }
}