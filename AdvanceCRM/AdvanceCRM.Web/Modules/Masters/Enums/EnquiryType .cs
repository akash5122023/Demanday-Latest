using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.EnquiryType")]
    public enum EnquiryTypeMaster
    {
        [Description("Hot")]
        Hot = 1,
        [Description("Warm")]
        Warm = 2,
        [Description("Cold")]
        Cold = 3
    }
}