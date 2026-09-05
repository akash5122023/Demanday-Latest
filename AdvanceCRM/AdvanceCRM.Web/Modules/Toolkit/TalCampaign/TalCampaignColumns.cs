using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Toolkit.Columns
{
    [ColumnsScript("Toolkit.TalCampaign")]
    [BasedOnRow(typeof(TalCampaignRow), CheckNames = true)]
    public class TalCampaignColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [DisplayName("Sr No"), AlignRight]
        public Int32 SrNo { get; set; }
        [EditLink]
        public String CompanyName { get; set; }
        public String Domain { get; set; }
        public String Cpc { get; set; }
        public String AgentDisplayName { get; set; }
        public String Reason { get; set; }
        [DisplayName("Master Account ID")]
        public String MasterAccountAccountNumber { get; set; }
        [DisplayName("Campaign ID")]
        public String CampaignIdValue { get; set; }
        public String OwnerUsername { get; set; }
    }
}