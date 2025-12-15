using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.ContactType")]
    public enum ContactTypeMaster
    {
        [Description("Customer")]
        Customer = 1,
        [Description("Vendor")]
        Vendor = 2
    }
}