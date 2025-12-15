using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.HolidayType")]
    public enum HolidayTypeMaster
    {
        [Description("Weekly")]
        Weekly = 1,
        [Description("General")]
        General = 2
    }
}