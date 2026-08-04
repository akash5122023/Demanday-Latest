using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260804150000)]
    public class DefaultDB_20260804_150000_CreateTMEnquiryTable : Migration
    {
        public override void Up()
        {
            Create.Table("TMEnquiry")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("MasterAccountId").AsInt32().Nullable().ForeignKey("FK_TMEnquiry_MasterAccountId", "dbo", "DemandayMasterAccount", "Id")
                .WithColumn("CampaignId").AsString(50).Nullable()
                .WithColumn("FirstName").AsString(100).Nullable()
                .WithColumn("LastName").AsString(100).Nullable()
                .WithColumn("Email").AsString(200).Nullable()
                .WithColumn("CompanyName").AsString(200).Nullable()
                .WithColumn("Timestamp").AsDateTime().Nullable()
                .WithColumn("DemandayTeleMarketingEnquiryId").AsInt32().NotNullable().ForeignKey("FK_TMEnquiry_DemandayTeleMarketingEnquiryId", "dbo", "DemandayTeleMarketingEnquiry", "Id")
                .WithColumn("CreatedOn").AsDateTime().WithDefaultValue(SystemMethods.CurrentDateTime)
                .WithColumn("CreatedBy").AsString(int.MaxValue).Nullable()
                .WithColumn("UpdatedOn").AsDateTime().Nullable()
                .WithColumn("UpdatedBy").AsString(int.MaxValue).Nullable();

            Create.Index("IX_TMEnquiry_DemandayTeleMarketingEnquiryId")
                .OnTable("TMEnquiry")
                .OnColumn("DemandayTeleMarketingEnquiryId")
                .Ascending();

            Create.Index("IX_TMEnquiry_MasterAccountId")
                .OnTable("TMEnquiry")
                .OnColumn("MasterAccountId")
                .Ascending();

            Create.Index("IX_TMEnquiry_CampaignId")
                .OnTable("TMEnquiry")
                .OnColumn("CampaignId")
                .Ascending();
        }

        public override void Down()
        {
            Delete.Table("TMEnquiry");
        }
    }
}
