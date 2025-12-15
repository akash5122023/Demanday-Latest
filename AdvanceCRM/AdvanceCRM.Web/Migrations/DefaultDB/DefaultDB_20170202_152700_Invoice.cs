using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170202163700)]
    public class DefaultDB_20170202_152700_Invoice : Migration
    {
        public override void Up()
        {
            Create.Table("Invoice")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_IContacts_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("Type").AsInt32().Nullable()
                .WithColumn("AdditionalInfo").AsString(200).Nullable()
                .WithColumn("SourceId").AsInt32().NotNullable().ForeignKey("FK_ISource_SourceId", "dbo", "Source", "Id")
                .WithColumn("StageId").AsInt32().NotNullable().ForeignKey("FK_IStage_StageId", "dbo", "Stage", "Id")
                .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_IBranch_BranchId", "dbo", "Branch", "Id")
                .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_IOUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_IAUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("OtherAddress").AsBoolean().Nullable()
                .WithColumn("ShippingAddress").AsString(1000).Nullable()
                .WithColumn("PackagingCharges").AsDouble().Nullable()
                .WithColumn("FreightCharges").AsDouble().Nullable()
                .WithColumn("Advacne").AsDouble().Nullable()
                .WithColumn("DueDate").AsDateTime().Nullable()
                .WithColumn("DispatchDetails").AsString(1000).Nullable()
                .WithColumn("Roundup").AsDouble().Nullable()
                .WithColumn("Subject").AsString(1000).Nullable()
                .WithColumn("Reference").AsString(1000).Nullable()
                .WithColumn("ContactPersonId").AsInt32().Nullable().ForeignKey("FK_Invoice_SubContactsId", "dbo", "SubContacts", "Id")
                .WithColumn("Lines").AsInt32().Nullable()
                .WithColumn("QuotationNo").AsInt32().Nullable()
                .WithColumn("QuotationDate").AsDateTime().Nullable()
                .WithColumn("Conversion").AsDouble().Nullable()
                .WithColumn("PurchaseOrderNo").AsString(1024).Nullable()
            ;

            Create.Table("InvoiceProducts").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_IPProducts_ProductsId", "dbo", "Products", "Id")
                .WithColumn("Quantity").AsDouble().NotNullable().WithDefaultValue(1)
                .WithColumn("MRP").AsDouble()
                .WithColumn("SellingPrice").AsDouble()
                .WithColumn("Price").AsDouble()
                .WithColumn("Discount").AsDouble().WithDefaultValue(0)
                .WithColumn("TaxType1").AsString(100).Nullable()
                .WithColumn("Percentage1").AsDouble().Nullable()
                .WithColumn("TaxType2").AsString(100).Nullable()
                .WithColumn("Percentage2").AsDouble().Nullable()
                .WithColumn("WarrantyStart").AsDateTime().Nullable()
                .WithColumn("WarrantyEnd").AsDateTime().Nullable()
                .WithColumn("InvoiceId").AsInt32().NotNullable().ForeignKey("FK_IPInvoice_InvoiceId", "dbo", "Invoice", "Id")
                .WithColumn("DiscountAmount").AsDouble().WithDefaultValue(0)
                .WithColumn("Description").AsString(2000).Nullable()
                .WithColumn("Unit").AsString(128).Nullable()
            ;

            Create.Table("InvoiceFollowups")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("FollowupNote").AsString(200).NotNullable()
                .WithColumn("Details").AsString(200)
                .WithColumn("FollowupDate").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("InvoiceId").AsInt32().NotNullable().ForeignKey("FK_IFInvoice_InvoiceId", "dbo", "Invoice", "Id")
                .WithColumn("RepresentativeId").AsInt32().Nullable().ForeignKey("FK_InvoiceFollowups_UserId", "dbo", "Users", "UserId")
            ;
        }

        public override void Down()
        {

        }
    }
}