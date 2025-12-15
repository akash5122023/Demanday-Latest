using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.PayType")]
    public enum PayTypeMaster
    {
        [Description("Deductions")]
        Deductions = 1,
        [Description("Earnings")]
        Earnings = 2,
        [Description("OverTime")]
        OverTime = 3,
        [Description("Reimbursements")]
        Reimbursements = 4,
        [Description("Statutory Component")]
        StatutoryComponent = 5,
        [Description("Friday")]
        Friday = 6,
        [Description("Saturday")]
        Saturday = 7
    }
}