using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Masters
{
    [EnumKey("Masters.AccountingHeads")]
    public enum AccountingHeadsMaster
    {
        //TODO: For bank to bank deposit add another module and add one deopsit and expense entry in cashbook module
        [Description("Sundry debtors")]
        Sundrydebtors = 1,
        [Description("Bank to cashbook deposit")]
        Banktocashbookdeposit = 2,
        [Description("Capital")]
        Capital = 3,
        [Description("Commission received")]
        Commissionreceived = 4,
        [Description("Discount received")]
        Discountreceived = 5,
        [Description("Interest received")]
        Interestreceived = 6,
        [Description("Other incomes")]
        Otherincomes = 7,
        [Description("Rent received")]
        Rentreceived = 8,
        [Description("Salary received")]
        Salaryreceived = 9,
        [Description("Sundry income")]
        Sundryincome = 10,

        //Expenses
        [Description("Sundry creditors")]
        Sundrycreditors = 11,
        [Description("Account charges")]
        Accountcharges = 12,
        [Description("Profit withdrawal")]
        Profitwithdrawal = 13,
        [Description("Administrative expenses")]
        Administrativeexpenses = 14,
        [Description("Advertisment")]
        Advertisment = 15,
        [Description("Allowances Paid")]
        AllowancesPaid = 16,
        [Description("Bad dates")]
        Baddates = 17,
        [Description("Bank charges")]
        Bankcharges = 18,
        [Description("Carriage inward")]
        Carriageinward = 19,
        [Description("Cashbook to bank deposit")]
        Cashbooktobankdeposit = 20,
        [Description("Cleaning charges")]
        Cleaningcharges = 21,
        [Description("Custom duties")]
        Customduties = 22,
        [Description("Depreciation")]
        Depreciation = 23,
        [Description("Discount allowed")]
        Discountallowed = 24,
        [Description("Electricity bill")]
        Electricitybill = 25,
        [Description("Freight")]
        Freight = 26,
        [Description("Gas, fuel and water")]
        Gasfuelandwater = 27,
        [Description("Income tax paid")]
        Incometaxpaid = 28,
        [Description("Insurance paid")]
        Insurancepaid = 29,
        [Description("Interest on capital")]
        Interestoncapital = 30,
        [Description("Interest on loans")]
        Interestonloans = 31,
        [Description("Office expenses")]
        Officeexpenses = 32,
        [Description("Other Expense")]
        OtherExpense = 33,
        [Description("Printing and stationary")]
        Printingandstationary = 34,
        [Description("Rent")]
        Rent = 35,
        [Description("Repair and maintenance")]
        Repairandmaintenance = 36,
        [Description("Royalties")]
        Royalties = 37,
        [Description("Salaries")]
        Salaries = 38,
        [Description("Taxes")]
        Taxes = 39,
        [Description("Telephone expenses")]
        TelephoneExpenses = 40,
        [Description("Trade")]
        Trade = 41,
        [Description("Transport")]
        Transport = 42,
        [Description("Travelling")]
        Travelling = 43,
        [Description("Wages")]
        Wages = 44,
        //This head is common for both deposit and expense
        [Description("Suspense account")]
        Suspenseaccount = 45,
    }
}