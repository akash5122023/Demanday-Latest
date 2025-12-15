using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.AddInfoType")]
    public enum AddInfoTypeMaster
    {
        [Description("Enquiry")]
        Enquiry = 1,
        [Description("Quotation")]
        Quotation = 2,
        [Description("Invoice")]
        Contact = 3,
        //[Description("Service")]
        //Service = 4,
        //[Description("Manufacturing")]
        //Manufacturing = 5
    }
}