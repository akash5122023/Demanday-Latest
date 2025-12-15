using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.GSTEcomType")]
    public enum GSTEcomTypeMaster
    {
        [Description("E")]
        E = 1,
        [Description("OE")]
        OE = 2
    }
}