using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170119195800)]
    public class DefaultDB_20170119_195800_Enquiry : Migration
    {
        public override void Up()
        {
            Create.Table("Enquiry")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_EContacts_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("Type").AsInt32().Nullable()
                .WithColumn("AdditionalInfo").AsString(5000).Nullable()
                .WithColumn("SourceId").AsInt32().NotNullable().ForeignKey("FK_ESource_SourceId", "dbo", "Source", "Id")
                .WithColumn("StageId").AsInt32().NotNullable().ForeignKey("FK_EStage_StageId", "dbo", "Stage", "Id")
                .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_EBranch_BranchId", "dbo", "Branch", "Id")
                .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_EOUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_EAUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("ReferenceName").AsString(100).Nullable()
                .WithColumn("ReferencePhone").AsString(50).Nullable()
                .WithColumn("ClosingType").AsInt32().Nullable()
                .WithColumn("LostReason").AsString(450).Nullable()
                .WithColumn("ClosingDate").AsDateTime().Nullable()
                .WithColumn("ContactPersonId").AsInt32().Nullable().ForeignKey("FK_Enquiry_SubContactsId", "dbo", "SubContacts", "Id")
                ;

            Create.Table("EnquiryProducts").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_EPProducts_ProductsId", "dbo", "Products", "Id")
                .WithColumn("Quantity").AsDouble().NotNullable().WithDefaultValue(1)
                .WithColumn("MRP").AsDouble()
                .WithColumn("SellingPrice").AsDouble()
                .WithColumn("Price").AsDouble()
                .WithColumn("Discount").AsDouble().WithDefaultValue(0)
                .WithColumn("EnquiryId").AsInt32().NotNullable().ForeignKey("FK_EPEnquiry_EnquiryId", "dbo", "Enquiry", "Id")
                .WithColumn("Description").AsString(2000).Nullable()
            ;

            Create.Table("EnquiryFollowups")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("FollowupNote").AsString(200).NotNullable()
                .WithColumn("Details").AsString(2000)
                .WithColumn("FollowupDate").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("EnquiryId").AsInt32().NotNullable().ForeignKey("FK_EFEnquiry_EnquiryId", "dbo", "Enquiry", "Id")
                .WithColumn("RepresentativeId").AsInt32().Nullable().ForeignKey("FK_EnquiryFollowups_UserId", "dbo", "Users", "UserId")
            ;

        }

        public override void Down()
        {

        }
    }
}