using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.EmpPFContributionRule")]
    public enum EmpPFContributionRuleMaster
    {
        [Description("Contribute on Restricted Wages")]
        ContributeRestrictedWages = 1,
        [Description("Contribute on Actual Wages")]
        ContributeActualWages = 2
    }
}