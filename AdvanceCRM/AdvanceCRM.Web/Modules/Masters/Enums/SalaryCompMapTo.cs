using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.SalaryCompMap")]
    public enum SalaryCompMapMaster
    {
        [Description("Leave Encashment")]
        LeaveEncashment = 1,
        [Description("Gratuity")]
        Gratuity = 2,
        [Description("Notice Pay")]
        NoticePay = 3,
        [Description("Unclaimed Reimbursement")]
        UnclaimedReimbursement = 4,
        [Description("IT HRA")]
        ITHRA = 5,
        [Description("PF Wages")]
        PFWages = 6,
        [Description("Sp. Allowance Component")]
        SpAllowanceComponent = 7,
        [Description("Other Income")]
        OtherIncome = 8,
        [Description("VPF")]
        VPF = 9,
        [Description("Notice Recovery")]
        NoticeRecovery = 10,
        [Description("Comp-Off")]
        CompOff = 11,
        [Description("Employer PF")]
        EmployerPF = 12,
        [Description("Employer ESI")]
        EmployerESI = 13,
        [Description("DA")]
        DA = 14,
        [Description("Leave Encashment Non taxable")]
        LeaveEncashmentNontaxable = 15
    }
}