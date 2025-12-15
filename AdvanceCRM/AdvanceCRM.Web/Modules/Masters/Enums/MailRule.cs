using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.MailRule")]
    public enum MailRuleMaster
    {
        [Description("Status")]
        Status = 1,
        [Description("Stage")]
        Stage = 2,
        [Description("Source")]
        Source = 3,
        [Description("Type")]
        Type = 4,
        [Description("ClosingType")]
        ClosingType = 5
    }
}