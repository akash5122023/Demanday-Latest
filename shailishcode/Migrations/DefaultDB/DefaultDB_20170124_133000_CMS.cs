using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170124133000)]
	public class DefaultDB_20170124_133000_CMS : Migration
	{
		public override void Up()
		{
			Create.Table("ComplaintType")
				   .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
				   .WithColumn("ComplaintType").AsString(150).Nullable()
				   ;

			Create.Table("CMS")
				.WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
				.WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_CMSContacts_ContactsId", "dbo", "Contacts", "Id")
				.WithColumn("Date").AsDateTime().NotNullable()
				.WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_CMS_ProductsId", "dbo", "Products", "Id")
				.WithColumn("SerialNo").AsString(70).Nullable()
				.WithColumn("ComplaintId").AsInt32().NotNullable().ForeignKey("FK_CMSComplaint_ComplaintId", "dbo", "ComplaintType", "Id")
				.WithColumn("Category").AsInt32().NotNullable()
				.WithColumn("Amount").AsDouble().Nullable()
				.WithColumn("ExpectedCompletion").AsDateTime().Nullable()
				.WithColumn("AssignedBy").AsInt32().NotNullable().ForeignKey("FK_CMSABUserId_UserId", "dbo", "Users", "UserId")
				.WithColumn("AssignedTo").AsInt32().NotNullable().ForeignKey("FK_CMSATUserId_UserId", "dbo", "Users", "UserId")
				.WithColumn("Instructions").AsString(450).Nullable()
				.WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_CMSBranch_BranchId", "dbo", "Branch", "Id")
				.WithColumn("Status").AsInt32().NotNullable()
				.WithColumn("CompletionDate").AsDateTime().Nullable()
				.WithColumn("Feedback").AsString(450).Nullable()
				.WithColumn("AdditionalInfo").AsString(2000).Nullable()
				.WithColumn("Image").AsString(500).Nullable()
				.WithColumn("Phone").AsString(50).Nullable()
				.WithColumn("Address").AsString(100).Nullable()
				.WithColumn("StageId").AsInt32().Nullable().ForeignKey("FK_CMSStage_StageId", "dbo", "Stage", "Id")
				.WithColumn("Priority").AsInt32().Nullable()
				.WithColumn("Attachment").AsString(500).Nullable()
				.WithColumn("PMRClosed").AsBoolean().Nullable().WithColumn("InvestigationBy").AsInt32().Nullable().ForeignKey("FK_CMSIBUserId_UserId", "dbo", "Users", "UserId")
				.WithColumn("ActionBy").AsInt32().Nullable().ForeignKey("FK_CMSACBUserId_UserId", "dbo", "Users", "UserId")
				.WithColumn("SupervisedBy").AsInt32().Nullable().ForeignKey("FK_CMSSBUserId_UserId", "dbo", "Users", "UserId")
				.WithColumn("Observation").AsString(250).Nullable()
				.WithColumn("Action").AsString(250).Nullable()
				.WithColumn("Comments").AsString(250).Nullable()
				;

			Create.Table("CMSProductsNew").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
				.WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_CMSProductsNew_ProductsId", "dbo", "Products", "Id")
				.WithColumn("Quantity").AsDouble().NotNullable().WithDefaultValue(1)
				.WithColumn("CMSId").AsInt32().NotNullable().ForeignKey("FK_CMSProductsNew_CMSId", "dbo", "CMS", "Id")
				.WithColumn("Price").AsDouble().NotNullable().WithDefaultValue(0)
				;

			Create.Table("CMSFollowups")
			.WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
			.WithColumn("FollowupNote").AsString(500).NotNullable()
			.WithColumn("Details").AsString(500)
			.WithColumn("FollowupDate").AsDateTime().NotNullable()
			.WithColumn("Status").AsInt32().NotNullable()
			.WithColumn("RepresentativeId").AsInt32().Nullable().ForeignKey("FK_CMSFollowps_UserId", "dbo", "Users", "UserId")
			.WithColumn("CMSId").AsInt32().NotNullable().ForeignKey("FK_CMSFollowups_CMSId", "dbo", "CMS", "Id")
			;
		}

		public override void Down()
		{

		}
	}
}