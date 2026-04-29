using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260429081500)]
    public class DefaultDB_20260429_081500_AddCampaignIdToDemandayTMEQuestionAnswers : Migration
    {
        public override void Up()
        {
            if (Schema.Table("DemandayTeleMarketingEnquiryQuestionAnswers").Exists() &&
                !Schema.Table("DemandayTeleMarketingEnquiryQuestionAnswers").Column("CampaignId").Exists())
            {
                Alter.Table("DemandayTeleMarketingEnquiryQuestionAnswers")
                    .AddColumn("CampaignId").AsInt32().Nullable()
                        .ForeignKey("FK_DemandayTeleMarketingEnquiryQuestionAnswers_CampaignId",
                            "dbo", "DemandayCampaignId", "Id");
            }
        }

        public override void Down()
        {
            if (Schema.Table("DemandayTeleMarketingEnquiryQuestionAnswers").Column("CampaignId").Exists())
            {
                Delete.ForeignKey("FK_DemandayTeleMarketingEnquiryQuestionAnswers_CampaignId")
                    .OnTable("DemandayTeleMarketingEnquiryQuestionAnswers");
                Delete.Column("CampaignId").FromTable("DemandayTeleMarketingEnquiryQuestionAnswers");
            }
        }
    }
}
