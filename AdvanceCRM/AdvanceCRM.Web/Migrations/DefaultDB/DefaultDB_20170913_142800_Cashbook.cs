using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170913142800)]
    public class DefaultDB_20170913_142800_Cashbook : Migration
    {
        public override void Up()
        {

            Create.Table("Cashbook")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("Type").AsInt32().NotNullable()
                .WithColumn("Head").AsInt32().NotNullable().ForeignKey("FK_Cashbook_AccountingHeadsId", "dbo", "AccountingHeads", "Id")
                .WithColumn("ContactsId").AsInt32().Nullable().ForeignKey("FK_Cashbook_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("InvoiceNo").AsString(50).Nullable()
                .WithColumn("CashIn").AsDouble().Nullable()
                .WithColumn("CashOut").AsDouble().Nullable()
                .WithColumn("Narration").AsString(300).Nullable()
                .WithColumn("BankId").AsInt32().Nullable().ForeignKey("FK_Cashbook_BankId", "dbo", "BankMaster", "Id")
                ;
        }

        public override void Down()
        {

        }
    }
}