using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.CalculationType")]
    public enum CalculationTypeMaster
    {
        [Description("Flat")]
        Flat = 1,
        [Description("Formula")]
        Formula = 2,
        [Description("Slabwise")]
        Slabwise = 3
    }
}