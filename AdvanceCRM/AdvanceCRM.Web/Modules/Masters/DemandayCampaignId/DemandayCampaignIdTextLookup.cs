using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Web;
using System;

namespace AdvanceCRM.Masters
{
    [LookupScript("Masters.DemandayCampaignIdByText", Permission = "?")]
    public class DemandayCampaignIdTextLookup : RowLookupScript<DemandayCampaignIdRow>
    {
        public DemandayCampaignIdTextLookup(ISqlConnections sqlConnections)
            : base(sqlConnections)
        {
            IdField = DemandayCampaignIdRow.Fields.CampaignId.PropertyName;
            TextField = DemandayCampaignIdRow.Fields.CampaignId.PropertyName;
            Expiration = TimeSpan.FromDays(-1);
        }
    }
}
