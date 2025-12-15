
namespace AdvanceCRM.BizMail.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("BizMail.CampaignBm")]
    [BasedOnRow(typeof(CampaignBmRow), CheckNames = true)]
    public class CampaignBmColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Campaignuid { get; set; }
        public String Name { get; set; }
        public Int32 Type { get; set; }
        public String FromName { get; set; }
        public String FromEmail { get; set; }
        public String Subject { get; set; }
        public String ReplyTo { get; set; }
        public String BmListListId { get; set; }
        public DateTime SendAt { get; set; }
        public String BmTemplateName { get; set; }
        public Int32 InlineCss { get; set; }
        public Int32 AutoPlaneTest { get; set; }
    }
}