using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.WeekDays")]
    public enum WeekDaysMaster
    {
        [Description("Sunday")]
        Sunday = 1,
        [Description("Monday")]
        Monday = 2,
        [Description("Tuesday")]
        Tuesday = 3,
        [Description("Wednesday")]
        Wednesday = 4,
        [Description("Thursday")]
        Thursday = 5,
        [Description("Friday")]
        Friday = 6,
        [Description("Saturday")]
        Saturday = 7
    }
}