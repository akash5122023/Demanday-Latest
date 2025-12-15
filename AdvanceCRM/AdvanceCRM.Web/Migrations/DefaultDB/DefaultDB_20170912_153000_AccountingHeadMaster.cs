using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170912153000)]
    public class DefaultDB_20170912_153000_AccountingHeadMaster : Migration
    {
        public override void Up()
        {

            Create.Table("AccountingHeads")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Head").AsString(200).Nullable()
                .WithColumn("Type").AsInt32().NotNullable()
                ;

            Insert.IntoTable("AccountingHeads")
            //Deposit heads
            .Row(new
            {
                Head = "Sundry debtors",
                Type = 1
            })
            .Row(new
            {
                Head = "Bank to cashbook deposit",
                Type = 1
            })
            .Row(new
            {
                Head = "Capital",
                Type = 1
            })
            .Row(new
            {
                Head = "Commission received",
                Type = 1
            })
            .Row(new
            {
                Head = "Discount received",
                Type = 1
            })
            .Row(new
            {
                Head = "Interest received",
                Type = 1
            })
            .Row(new
            {
                Head = "Other incomes",
                Type = 1
            })
            .Row(new
            {
                Head = "Rent received",
                Type = 1
            })
            .Row(new
            {
                Head = "Salary received",
                Type = 1
            })
            .Row(new
            {
                Head = "Sundry income",
                Type = 1
            })
            
            //Expense heads
            .Row(new
            {
                Head = "Sundry creditors",
                Type = 2
            })
            .Row(new
            {
                Head = "Account charges",
                Type = 2
            })
            .Row(new
            {
                Head = "Profit withdrawal",
                Type = 2
            })
            .Row(new
            {
                Head = "Administrative expenses",
                Type = 2
            })
            .Row(new
            {
                Head = "Advertisment",
                Type = 2
            })
            .Row(new
            {
                Head = "Allowances Paid",
                Type = 2
            })
            .Row(new
            {
                Head = "Bad dates",
                Type = 2
            })
            .Row(new
            {
                Head = "Bank charges",
                Type = 2
            })
            .Row(new
            {
                Head = "Carriage inward",
                Type = 2
            })
            .Row(new
            {
                Head = "Cashbook to bank deposit",
                Type = 2
            })
            .Row(new
            {
                Head = "Cleaning charges",
                Type = 2
            })
            .Row(new
            {
                Head = "Custom duties",
                Type = 2
            })
            .Row(new
            {
                Head = "Depreciation",
                Type = 2
            })
            .Row(new
            {
                Head = "Discount allowed",
                Type = 2
            })
            .Row(new
            {
                Head = "Electricity bill",
                Type = 2
            })
            .Row(new
            {
                Head = "Freight",
                Type = 2
            })
            .Row(new
            {
                Head = "Gas, fuel and water",
                Type = 2
            })
            .Row(new
            {
                Head = "Income tax paid",
                Type = 2
            })
            .Row(new
            {
                Head = "Insurance paid",
                Type = 2
            })
            .Row(new
            {
                Head = "Interest on capital",
                Type = 2
            })
            .Row(new
            {
                Head = "Interest on loans",
                Type = 2
            })
            .Row(new
            {
                Head = "Office expenses",
                Type = 2
            })
            .Row(new
            {
                Head = "Other Expense",
                Type = 2
            })
            .Row(new
            {
                Head = "Printing and stationary",
                Type = 2
            })
            .Row(new
            {
                Head = "Rent",
                Type = 2
            })
            .Row(new
            {
                Head = "Repair and maintenance",
                Type = 2
            })
            .Row(new
            {
                Head = "Royalties",
                Type = 2
            })
            .Row(new
            {
                Head = "Salaries",
                Type = 2
            })
            .Row(new
            {
                Head = "Taxes",
                Type = 2
            })
            .Row(new
            {
                Head = "Telephone expenses",
                Type = 2
            })
            .Row(new
            {
                Head = "Trade",
                Type = 2
            })
            .Row(new
            {
                Head = "Transport",
                Type = 2
            })
            .Row(new
            {
                Head = "Travelling",
                Type = 2
            })
            .Row(new
            {
                Head = "Wages",
                Type = 2
            })
            .Row(new
            {
                Head = "Suspense account",
                Type = 2
            })

            //1 Deposit head
            .Row(new
            {
                Head = "Suspense account",
                Type = 1
            })
            ;
        }

        public override void Down()
        {

        }
    }
}