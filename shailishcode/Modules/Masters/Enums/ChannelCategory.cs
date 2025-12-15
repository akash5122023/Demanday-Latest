using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.ChannelCategory")]
    public enum ChannelCategory
    {
        [Description("Direct Customer")]
        DirectCustomer = 1,
        [Description("Customer(From Channel)")]
        CustomerFromChannel = 2,
        [Description("Reseller")]
        Reseller = 3,
        [Description("Wholesaler")]
        Wholesaler = 4,
        [Description("Dealer")]
        Dealer = 5,
        [Description("Distributor")]
        Distributor = 6,
        [Description("Stockist")]
        Stockist = 7,
        [Description("National Distributor")]
        NationalDistributor = 8
    }
}