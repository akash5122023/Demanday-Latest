
namespace AdvanceCRM.BizMail.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("BizMail.CampaignBm")]
    [BasedOnRow(typeof(CampaignBmRow), CheckNames = true)]
    public class CampaignBmForm
    {
        public String Campaignuid { get; set; }
        public String Name { get; set; }
        public Int32 Type { get; set; }
        public String FromName { get; set; }
        public String FromEmail { get; set; }
        public String Subject { get; set; }
        public String ReplyTo { get; set; }
        public Int32 BmListId { get; set; }
       
        public String ListId { get; set; }
        public DateTime SendAt { get; set; }
        public Int32 BmTemplateId { get; set; }
        public Int32 InlineCss { get; set; }
        public Int32 AutoPlaneTest { get; set; }
    }
}