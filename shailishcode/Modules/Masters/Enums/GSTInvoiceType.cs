using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.GSTInvoiceType")]
    public enum GSTInvoiceTypeMaster
    {
        [Description("Regular")]
        Regular = 1,
        [Description("SEZ supplies with payment")]
        SEZsupplieswithpayment = 2,
        [Description("SEZ supplies without payment")]
        SEZsupplieswithoutpayment = 3,
        [Description("Deemed Exp")]
        DeemedExp = 4
    }
}