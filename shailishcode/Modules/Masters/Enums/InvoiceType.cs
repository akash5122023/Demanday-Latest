using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.InvoiceType")]
    public enum InvoiceTypeMaster
    {
        [Description("Cash")]
        Cash = 1,
        [Description("Credit")]
        Credit = 2,
        [Description("Card")]
        Card = 3,
        [Description("App Transfer")]
        AppTransfer = 4
    }
}