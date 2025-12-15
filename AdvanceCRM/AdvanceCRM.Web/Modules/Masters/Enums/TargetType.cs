using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.TargetType")]
    public enum TargetTypeMaster
    {
        [Description("Enquiry")]
        Enquiry = 1,
        [Description("Quotation")]
        Quotation = 2,
        [Description("Sales")]
        Sales = 3,
        [Description("Calling")]
        Calling = 4
    }
}