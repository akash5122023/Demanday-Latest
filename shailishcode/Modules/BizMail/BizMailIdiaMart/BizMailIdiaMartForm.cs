
namespace AdvanceCRM.BizMail.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("BizMail.BizMailIdiaMart")]
    [BasedOnRow(typeof(BizMailIdiaMartRow), CheckNames = true)]
    public class BizMailIdiaMartForm
    {
        [ReadOnly(true), DefaultValue(4)]
        public Masters.BizMailRulesMaster Rule { get; set; }
        public Int32 BmListId { get; set; }
        [BSSwitchEditor]
        public Boolean Status { get; set; }
        [ReadOnly(true)]
        public Int32 CompanyId { get; set; }
        [TextAreaEditor(Rows = 3)]
        public String Description { get; set; }
        //public Int32 Rule { get; set; }
        //public Int32 BmListId { get; set; }
        //public Boolean Status { get; set; }
        //public Int32 CompanyId { get; set; }
        //public String Description { get; set; }
    }
}