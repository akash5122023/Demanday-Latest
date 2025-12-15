using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.Inlinecss")]
    public enum InlinecssMaster
    {
        [Description("Yes")]
        Yes = 1,
        [Description("No")]
        No = 2
    }
}