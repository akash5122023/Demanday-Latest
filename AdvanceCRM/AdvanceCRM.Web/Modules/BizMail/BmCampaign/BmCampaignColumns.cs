
namespace AdvanceCRM.BizMail.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("BizMail.BmCampaign")]
    [BasedOnRow(typeof(BmCampaignRow), CheckNames = true)]
    public class BmCampaignColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String CampaignId { get; set; }
        public String Name { get; set; }
        public String Status { get; set; }
    }
}