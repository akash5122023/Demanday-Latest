using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20180604091200)]
    public class DefaultDB_20180604_091200_SalesReturn : Migration
    {
        public override void Up()
        {
            Create.Table("SalesReturn")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_SalesReturn_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("InvoiceNo").AsInt32().NotNullable()
                .WithColumn("InvoiceDate").AsDateTime().NotNullable()
                .WithColumn("Amount").AsDouble().Nullable()
                .WithColumn("Roundup").AsDouble().Nullable()
                .WithColumn("AdditionalInfo").AsString(200).Nullable()
                .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_SalesReturn_BranchId", "dbo", "Branch", "Id")
                .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_SalesReturnO_UserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_SalesReturnA_UserId", "dbo", "Users", "UserId")
                .WithColumn("Lines").AsInt32().Nullable()
                ;


            Create.Table("SalesReturnProducts")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_SalesReturnProducts_ProductsId", "dbo", "Products", "Id")
                .WithColumn("Serial").AsString(100).Nullable()
                .WithColumn("Batch").AsString(100).Nullable()
                .WithColumn("Quantity").AsDouble().NotNullable().WithDefaultValue(1)
                .WithColumn("Price").AsDouble()
                .WithColumn("TaxType1").AsString(100).Nullable()
                .WithColumn("Percentage1").AsDouble().Nullable()
                .WithColumn("TaxType2").AsString(100).Nullable()
                .WithColumn("Percentage2").AsDouble().Nullable()
                .WithColumn("Description").AsString(2000).Nullable()
                .WithColumn("SalesReturnId").AsInt32().NotNullable().ForeignKey("FK_SalesReturnProducts_SalesId", "dbo", "Sales", "Id")
                ;
        }

        public override void Down()
        {

        }
    }
}