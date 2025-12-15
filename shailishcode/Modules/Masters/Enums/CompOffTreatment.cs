using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.CompOffTreatment")]
    public enum CompOffTreatmentMaster
    {
        [Description("Both")]
        Both = 1,
        [Description("Set off against leave")]
        Setoffagainstleave = 2,
        [Description("Payout")]
        Payout = 3
    }
}