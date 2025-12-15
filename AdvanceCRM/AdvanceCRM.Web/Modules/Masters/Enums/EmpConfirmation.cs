using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.EmpConfirmation")]
    public enum EmpConfirmationMaster
    {
        [Description("Month")]
        Month = 1,
        [Description("Date")]
        Date = 2
    }
}