using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.AttendanceType")]
    public enum AttendanceTypeMaster
    {
        [Description("Present")]
        Present = 1,
        [Description("Absent")]
        Absent = 2,
       [Description("HalfDay")]
        HalfDay = 3
    }
}