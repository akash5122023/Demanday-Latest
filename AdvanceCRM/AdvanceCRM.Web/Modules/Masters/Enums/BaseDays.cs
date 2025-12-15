using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.BaseDays")]
    public enum BaseDaysMaster
    {
        [Description("30 Days")]
        ThirtyDays = 1,
        [Description("31 Days")]
        ThirtyOneDays = 2,
        [Description("Actual Days in a Month")]
        DaysinMonth = 3,
        [Description("Base Days")]
        BaseDays = 4
    }
}