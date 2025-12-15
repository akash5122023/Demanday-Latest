using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.GSTITCEligibilityType")]
    public enum GSTITCEligibilityTypeMaster
    {
        [Description("Inputs")]
        Inputs = 1,
        [Description("Capital goods")]
        Capitalgoods = 2,
        [Description("Input services")]
        Inputservices = 3,
        [Description("Ineligible")]
        Ineligible = 4
    }
}