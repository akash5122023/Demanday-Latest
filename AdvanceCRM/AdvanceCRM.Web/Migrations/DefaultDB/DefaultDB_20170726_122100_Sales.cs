using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170726122100)]
    public class DefaultDB_20170726_122100_Sales : Migration
    {
        public override void Up()
        {
            Create.Table("Sales")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_Sales_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("Type").AsInt32().Nullable()
                .WithColumn("AdditionalInfo").AsString(200).Nullable()
                .WithColumn("SourceId").AsInt32().NotNullable().ForeignKey("FK_Sales_SourceId", "dbo", "Source", "Id")
                .WithColumn("StageId").AsInt32().NotNullable().ForeignKey("FK_Sales_StageId", "dbo", "Stage", "Id")
                .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_Sales_BranchId", "dbo", "Branch", "Id")
                .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_SalesO_UserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_SalesA_UserId", "dbo", "Users", "UserId")
                .WithColumn("OtherAddress").AsBoolean().Nullable()
                .WithColumn("ShippingAddress").AsString(1000).Nullable()
                .WithColumn("PackagingCharges").AsDouble().Nullable()
                .WithColumn("FreightCharges").AsDouble().Nullable()
                .WithColumn("Advacne").AsDouble().Nullable()
                .WithColumn("DueDate").AsDateTime().Nullable()
                .WithColumn("DispatchDetails").AsString(1000).Nullable()
                .WithColumn("Roundup").AsDouble().Nullable()
                .WithColumn("ContactPersonId").AsInt32().Nullable().ForeignKey("FK_Sales_SubContactsId", "dbo", "SubContacts", "Id")
                .WithColumn("Lines").AsInt32().Nullable()
                .WithColumn("InvoiceNo").AsInt32().Nullable()
                .WithColumn("ReverseCharge").AsBoolean().Nullable()
                .WithColumn("EcomType").AsInt32().Nullable()
                .WithColumn("InvoiceType").AsInt32().Nullable()
                .WithColumn("TrasportationId").AsInt32().Nullable().ForeignKey("FK_TrasportationId_Id", "dbo", "Transportation", "Id")
                .WithColumn("QuotationNo").AsInt32().Nullable()
                .WithColumn("QuotationDate").AsDateTime().Nullable()
                .WithColumn("Conversion").AsDouble().Nullable()
                .WithColumn("PurchaseOrderNo").AsString(1024).Nullable()
                ;

            Create.Table("SalesProducts")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_SalesProducts_ProductsId", "dbo", "Products", "Id")
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
                .WithColumn("SalesId").AsInt32().NotNullable().ForeignKey("FK_SalesProducts_SalesId", "dbo", "Sales", "Id")
                .WithColumn("Description").AsString(2000).Nullable()
                .WithColumn("Unit").AsString(128).Nullable()
            ;
        }

        public override void Down()
        {

        }
    }
}