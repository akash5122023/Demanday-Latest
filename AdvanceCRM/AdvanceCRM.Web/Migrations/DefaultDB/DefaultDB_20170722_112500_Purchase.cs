using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170722112500)]
    public class DefaultDB_20170722_112500_Purchase : Migration
    {
        public override void Up()
        {
            Create.Table("Purchase")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Invoice No").AsString(50).NotNullable()
                .WithColumn("PurchaseFromId").AsInt32().NotNullable().ForeignKey("FK_Purchase_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("InvoiceDate").AsDateTime().NotNullable()
                .WithColumn("Total").AsDouble().Nullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("Type").AsInt32().Nullable()
                .WithColumn("AdditionalInfo").AsString(200).Nullable()
                .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_Purchase_BranchId", "dbo", "Branch", "Id")
                .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_PurchaseO_UserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_PurchaseA_UserId", "dbo", "Users", "UserId")
                .WithColumn("ReverseCharge").AsBoolean().Nullable()
                .WithColumn("InvoiceType").AsInt32().Nullable()
                .WithColumn("ITCEligibility").AsInt32().Nullable()
                ;

            Create.Table("PurchaseProducts").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_PurchaseProducts_ProductsId", "dbo", "Products", "Id")
                .WithColumn("Serial").AsString(100).Nullable()
                .WithColumn("Batch").AsString(100).Nullable()
                .WithColumn("Quantity").AsDouble().NotNullable().WithDefaultValue(1)
                .WithColumn("Price").AsDouble()
                .WithColumn("SellingPrice").AsDouble()
                .WithColumn("MRP").AsDouble()
                .WithColumn("Discount").AsDouble().WithDefaultValue(0)
                .WithColumn("DiscountAmount").AsDouble().WithDefaultValue(0)
                .WithColumn("TaxType1").AsString(100).Nullable()
                .WithColumn("Percentage1").AsDouble().Nullable()
                .WithColumn("TaxType2").AsString(100).Nullable()
                .WithColumn("Percentage2").AsDouble().Nullable()
                .WithColumn("WarrantyStart").AsDateTime().Nullable()
                .WithColumn("WarrantyEnd").AsDateTime().Nullable()
                .WithColumn("Sold").AsBoolean().Nullable()
                .WithColumn("PurchaseId").AsInt32().NotNullable().ForeignKey("FK_PurchaseProducts_PurchaseId", "dbo", "Purchase", "Id")
                .WithColumn("Unit").AsString(128).Nullable()
            ;
        }

        public override void Down()
        {

        }
    }
}