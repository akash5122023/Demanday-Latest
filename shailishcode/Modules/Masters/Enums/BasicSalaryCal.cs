using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.BasicSalaryCal")]
    public enum BasicSalaryCalMaster
    {
        [Description("CTC Based")]
        CTCBased = 1,
        [Description("Adhoc")]
        Adhoc = 2,
        [Description("Non CTC")]
        NonCTC = 3
    }
}