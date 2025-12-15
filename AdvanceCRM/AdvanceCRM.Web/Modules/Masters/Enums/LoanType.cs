using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.LoanType")]
    public enum LoanTypeMaster
    {
        [Description("Personal loan")]
        Personalloan = 1,
        [Description("Credit card")]
        Creditcardloan = 2,
        [Description("Home loan")]
        Housingloan = 3,
        [Description("Business loan")]
        Businessloan = 4,
    }
}