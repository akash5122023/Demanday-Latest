using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.CutOffRule")]
    public enum CutOffRuleMaster
    {
        [Description("Attendance and Payroll")]
        AttendanceandPayroll = 1,
        [Description("AttendanceOnly")]
        AttendanceOnly = 2
    }
}