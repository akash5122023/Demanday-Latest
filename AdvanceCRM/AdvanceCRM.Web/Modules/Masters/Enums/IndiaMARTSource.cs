using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.IndiaMARTSource")]
    public enum IndiaMARTSource
    {
        [Description("Web Enquiry")]
        Web = 1,
        [Description("Buy Lead")]
        Buy = 2,
        [Description("Call Enquiry")]
        Call = 3,
    }
}