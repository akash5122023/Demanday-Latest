using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.TeleMarketingEmailTeam
{
    [EnumKey("TeleMarketingEmailTeam.TeleMarketingEmailTeamStatus")]
    public enum TeleMarketingEmailTeamStatus
    {
        // Placeholder value. Rows created by Move-to-Quality start here; add the real statuses
        // below, then mirror them into Imports/ServerTypings/TeleMarketingEmailTeam.TeleMarketingEmailTeamStatus.ts.
        [Description("Pending")]
        Pending = 1
    }
}
