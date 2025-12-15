using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.WinPercentage")]
    public enum WinPercentageMaster
    {
        [Description("25%")]
        TwentyFivePercent = 1,
        [Description("50%")]
        FiftyPercent = 2,
        [Description("75%")]
        SeventyFivePercent = 3,
        [Description("95%")]
        NintyFivePercent = 4
    }
}