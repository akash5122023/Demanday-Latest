using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.TransactionType")]
    public enum TransactionTypeMaster
    {
        [Description("Deposit")]
        Deposit = 1,
        [Description("Expense")]
        Expense = 2
    }
}