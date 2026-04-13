using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Toolkit.Columns
{
    [ColumnsScript("Toolkit.DemandayCompetitor")]
    [BasedOnRow(typeof(DemandayCompetitorRow), CheckNames = true)]
    public class DemandayCompetitorColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String CompanyName { get; set; }
        public String Domain { get; set; }
        public Int64 Cpc { get; set; }
        public String MasterAccountAccountNumber { get; set; }
        public String CampaignCampaignId { get; set; }
        public String OwnerUsername { get; set; }
    }
}