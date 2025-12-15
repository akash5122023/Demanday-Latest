using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170120163700)]
    public class DefaultDB_20170120_163700_Quotation : Migration
    {
        public override void Up()
        {
            Create.Table("Quotation")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_QContacts_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("Type").AsInt32().Nullable()
                .WithColumn("AdditionalInfo").AsString(5000).Nullable()
                .WithColumn("SourceId").AsInt32().NotNullable().ForeignKey("FK_QSource_SourceId", "dbo", "Source", "Id")
                .WithColumn("StageId").AsInt32().NotNullable().ForeignKey("FK_QStage_StageId", "dbo", "Stage", "Id")
                .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_QBranch_BranchId", "dbo", "Branch", "Id")
                .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_QOUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_QAUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("ReferenceName").AsString(100).Nullable()
                .WithColumn("ReferencePhone").AsString(50).Nullable()
                .WithColumn("ClosingType").AsInt32().Nullable()
                .WithColumn("LostReason").AsString(450).Nullable()
                .WithColumn("Subject").AsString(500).Nullable()
                .WithColumn("Reference").AsString(500).Nullable()
                .WithColumn("Attachment").AsString(500).Nullable()
                .WithColumn("Lines").AsInt32().Nullable()
                .WithColumn("ContactPersonId").AsInt32().Nullable().ForeignKey("FK_Quotation_SubContactsId", "dbo", "SubContacts", "Id")
                .WithColumn("ClosingDate").AsDateTime().Nullable()
                .WithColumn("EnquiryNo").AsInt32().Nullable()
                .WithColumn("EnquiryDate").AsDateTime().Nullable()
                .WithColumn("Conversion").AsDouble().Nullable()
                ;

            Create.Table("QuotationFollowups")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("FollowupNote").AsString(200).NotNullable()
                .WithColumn("Details").AsString(200)
                .WithColumn("FollowupDate").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("QuotationId").AsInt32().NotNullable().ForeignKey("FK_QFQuotation_QuotationId", "dbo", "Quotation", "Id")
                .WithColumn("RepresentativeId").AsInt32().Nullable().ForeignKey("FK_QuotationFollowups_UserId", "dbo", "Users", "UserId")
                ;

            Create.Table("QuotationProducts").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_QPProducts_ProductsId", "dbo", "Products", "Id")
                .WithColumn("Quantity").AsDouble().NotNullable().WithDefaultValue(1)
                .WithColumn("MRP").AsDouble()
                .WithColumn("SellingPrice").AsDouble()
                .WithColumn("Price").AsDouble()
                .WithColumn("Discount").AsDouble().WithDefaultValue(0)
                .WithColumn("TaxType1").AsString(100).Nullable()
                .WithColumn("Percentage1").AsDouble().Nullable()
                .WithColumn("TaxType2").AsString(100).Nullable()
                .WithColumn("Percentage2").AsDouble().Nullable()
                .WithColumn("QuotationId").AsInt32().NotNullable().ForeignKey("FK_QPQuotation_QuotationId", "dbo", "Quotation", "Id")
                .WithColumn("DiscountAmount").AsDouble().WithDefaultValue(0)
                .WithColumn("Description").AsString(2000).Nullable()
                .WithColumn("Unit").AsString(128).Nullable()
                ;
        }

        public override void Down()
        {

        }
    }
}