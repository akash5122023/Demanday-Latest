using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.GrnType")]
    public enum GrnTypeMaster
    {
        [Description("With PO")]
        WithPO = 1,
        [Description("Without PO")]
        WithoutPO = 2
    }
}
