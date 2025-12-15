
namespace AdvanceCRM.BizMail.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("BizMail.BizMailJustDial")]
    [BasedOnRow(typeof(BizMailJustDialRow), CheckNames = true)]
    public class BizMailJustDialForm
    {
        [ReadOnly(true), DefaultValue(7)]
        public Masters.BizMailRulesMaster Rule { get; set; }
        public Int32 BmListId { get; set; }
        [BSSwitchEditor]
        public Boolean Status { get; set; }
        [ReadOnly(true),DefaultValue(1)]
        public Int32 CompanyId { get; set; }
        [TextAreaEditor(Rows = 3)]
        public String Description { get; set; }
       
    }
}