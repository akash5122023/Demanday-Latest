
namespace AdvanceCRM.BizMail.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("BizMail.BmCampaign")]
    [BasedOnRow(typeof(BmCampaignRow), CheckNames = true)]
    public class BmCampaignForm
    {
        public String CampaignId { get; set; }
        public String Name { get; set; }
        public String Status { get; set; }
    }
}