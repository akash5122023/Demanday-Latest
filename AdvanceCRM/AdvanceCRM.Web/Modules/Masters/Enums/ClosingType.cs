using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.ClosingType")]
    public enum ClosingTypeMaster
    {
        [Description("Won")]
        Won = 1,
        [Description("Lost")]
        Lost = 2,
        [Description("Revised")]
        Revised = 3
    }
}