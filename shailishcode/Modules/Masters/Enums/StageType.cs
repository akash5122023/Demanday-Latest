using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.StageType")]
    public enum StageTypeMaster
    {
        [Description("Enquiry")]
        Enquiry = 1,
        [Description("Quotation")]
        Quotation = 2,
        [Description("Invoice")]
        Invoice = 3,
        [Description("Service")]
        Service = 4,
        [Description("Manufacturing")]
        Manufacturing = 5
    }
}