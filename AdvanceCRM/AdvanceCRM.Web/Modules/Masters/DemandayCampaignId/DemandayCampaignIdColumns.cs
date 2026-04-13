using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Columns
{
    [ColumnsScript("Masters.DemandayCampaignId")]
    [BasedOnRow(typeof(DemandayCampaignIdRow), CheckNames = true)]
    public class DemandayCampaignIdColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String CampaignId { get; set; }
        public String DemandayMasterAccountAccountNumber { get; set; }
    }
}