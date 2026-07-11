using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Toolkit.Columns
{
    [ColumnsScript("Toolkit.OpenCampaign")]
    [BasedOnRow(typeof(OpenCampaignRow), CheckNames = true)]
    public class OpenCampaignColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [DisplayName("Sr No"), AlignRight]
        public Int32 SrNo { get; set; }
        [EditLink]
        public Int32 CampaignId { get; set; }
        public String Domain { get; set; }
        public String DemandayUserUsername { get; set; }
        public DateTime TimeStamp { get; set; }
        public String MasterAccountAccountNumber { get; set; }
        public String CampaignIdValue { get; set; }
        public String OwnerUsername { get; set; }
    }
}