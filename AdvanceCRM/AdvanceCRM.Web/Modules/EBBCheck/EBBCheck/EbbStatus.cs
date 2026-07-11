using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.EBBCheck
{
    [EnumKey("EBBCheck.EbbStatus")]
    public enum EbbStatus
    {
        [Description("Clear")]
        Clear = 1,
        [Description("Bounce")]
        Bounce = 2,
        [Description("50\\50")]
        FiftyFifty = 3,
        [Description("Waiting")]
        Waiting = 4
    }
}
