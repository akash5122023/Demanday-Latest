
namespace AdvanceCRM.BizMail.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("BizMail.BizMailFacebook")]
    [BasedOnRow(typeof(BizMailFacebookRow), CheckNames = true)]
    public class BizMailFacebookForm
    {
        [ReadOnly(true), DefaultValue(3)]
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