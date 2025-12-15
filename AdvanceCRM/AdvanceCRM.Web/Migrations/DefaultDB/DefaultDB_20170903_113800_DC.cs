using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170903113800)]
    public class DefaultDB_20170903_113800_Challan : Migration
    {
        public override void Up()
        {
            Create.Table("Challan")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_DC_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("OtherAddress").AsBoolean().Nullable()
                .WithColumn("ShippingAddress").AsString(1000).Nullable()
                .WithColumn("PackagingCharges").AsDouble().Nullable()
                .WithColumn("FreightCharges").AsDouble().Nullable()
                .WithColumn("Advacne").AsDouble().Nullable()
                .WithColumn("DueDate").AsDateTime().Nullable()
                .WithColumn("DispatchDetails").AsString(1000).Nullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("Type").AsInt32().Nullable()
                .WithColumn("AdditionalInfo").AsString(200).Nullable()
                .WithColumn("SourceId").AsInt32().NotNullable().ForeignKey("FK_DC_SourceId", "dbo", "Source", "Id")
                .WithColumn("StageId").AsInt32().NotNullable().ForeignKey("FK_DC_StageId", "dbo", "Stage", "Id")
                .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_DC_BranchId", "dbo", "Branch", "Id")
                .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_DCO_UserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_DCA_UserId", "dbo", "Users", "UserId")
                .WithColumn("Total").AsDouble().Nullable()
                .WithColumn("InvoiceMade").AsBoolean().Nullable()
                .WithColumn("ContactPersonId").AsInt32().Nullable().ForeignKey("FK_Challan_SubContactsId", "dbo", "SubContacts", "Id")
                .WithColumn("QuotationNo").AsInt32().Nullable()
                .WithColumn("QuotationDate").AsDateTime().Nullable()
                ;

            Create.Table("ChallanProducts")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_ChallanProducts_ProductsId", "dbo", "Products", "Id")
                .WithColumn("Serial").AsString(100).Nullable()
                .WithColumn("Batch").AsString(100).Nullable()
                .WithColumn("Quantity").AsDouble().NotNullable().WithDefaultValue(1)
                .WithColumn("Price").AsDouble()
                .WithColumn("SellingPrice").AsDouble()
                .WithColumn("MRP").AsDouble()
                .WithColumn("Discount").AsDouble().WithDefaultValue(0)
                .WithColumn("DiscountAmount").AsDouble().WithDefaultValue(0)
                .WithColumn("ChallanId").AsInt32().NotNullable().ForeignKey("FK_ChallanProducts_ChallanId", "dbo", "Challan", "Id")
                .WithColumn("Description").AsString(2000).Nullable()
                .WithColumn("Unit").AsString(128).Nullable()
            ;
        }

        public override void Down()
        {

        }
    }
}