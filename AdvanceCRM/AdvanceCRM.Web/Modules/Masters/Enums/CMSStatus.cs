using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.CMSStatus")]
    public enum CMSStatusMaster
    {
        [Description("Open")]
        Open = 1,
        [Description("Closed")]
        Closed = 2,
        [Description("Analysis")]
        Analysis = 3,
        [Description("Action")]
        Action = 4,
        [Description("Verify")]
        Verify = 5
    }
}