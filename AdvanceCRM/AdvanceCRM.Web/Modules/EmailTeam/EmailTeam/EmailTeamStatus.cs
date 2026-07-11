using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.EmailTeam
{
    [EnumKey("EmailTeam.EmailTeamStatus")]
    public enum EmailTeamStatus
    {
        // Placeholder value. Rows created by Move-to-Quality start here; add the real
        // statuses below, then mirror them into Imports/ServerTypings/EmailTeam.EmailTeamStatus.ts.
        [Description("Pending")]
        Pending = 1
    }
}
