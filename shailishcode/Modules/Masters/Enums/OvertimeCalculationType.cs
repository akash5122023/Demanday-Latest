using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.OvertimeCalculationType")]
    public enum OvertimeCalculationTypeMaster
    {
        [Description("Days")]
        Days = 1,
        [Description("Hours")]
        Hours = 2
    }
}