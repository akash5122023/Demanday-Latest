using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.Status")]
    public enum StatusMaster
    {
        [Description("Open")]
        Open = 1,
        [Description("Closed")]
        Closed = 2,
        [Description("Parked")]
        Pending = 3,
        [Description("Expired")]
        Expired = 4
    }
}