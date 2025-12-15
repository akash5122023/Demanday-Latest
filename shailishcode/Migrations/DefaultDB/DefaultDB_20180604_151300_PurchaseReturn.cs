using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20180604151300)]
    public class DefaultDB_20180604_151300_PurchaseReturn : Migration
    {
        public override void Up()
        {
            Create.Table("PurchaseReturn")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_PurchaseReturn_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("InvoiceNo").AsInt32().NotNullable()
                .WithColumn("InvoiceDate").AsDateTime().NotNullable()
                .WithColumn("Amount").AsDouble().Nullable()
                .WithColumn("Roundup").AsDouble().Nullable()
                .WithColumn("AdditionalInfo").AsString(200).Nullable()
                .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_PurchaseReturn_BranchId", "dbo", "Branch", "Id")
                .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_PurchaseReturnO_UserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_PurchaseReturnA_UserId", "dbo", "Users", "UserId")
                .WithColumn("Lines").AsInt32().Nullable()
                ;


            Create.Table("PurchaseReturnProducts")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_PurchaseReturnProducts_ProductsId", "dbo", "Products", "Id")
                .WithColumn("Serial").AsString(100).Nullable()
                .WithColumn("Batch").AsString(100).Nullable()
                .WithColumn("Quantity").AsDouble().NotNullable().WithDefaultValue(1)
                .WithColumn("Price").AsDouble()
                .WithColumn("TaxType1").AsString(100).Nullable()
                .WithColumn("Percentage1").AsDouble().Nullable()
                .WithColumn("TaxType2").AsString(100).Nullable()
                .WithColumn("Percentage2").AsDouble().Nullable()
                .WithColumn("Description").AsString(2000).Nullable()
                .WithColumn("PurchaseReturnId").AsInt32().NotNullable().ForeignKey("FK_PurchaseReturnProducts_PurchaseReturnId", "dbo", "PurchaseReturn", "Id")
                ;
        }

        public override void Down()
        {

        }
    }
}