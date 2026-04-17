using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260410120000)]
    public class DefaultDB_20260410_120000_DemandayEnquiryCampaignQuestions : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("DemandayTeleMarketingEnquiryCampaignQuestions").Exists())
            {
                Create.Table("DemandayTeleMarketingEnquiryCampaignQuestions")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                    .WithColumn("QuestionText").AsString(500).Nullable()
                    .WithColumn("CampaignId").AsInt32().Nullable().ForeignKey("FK_DemandayTeleMarketingEnquiryCampaignQuestions_CampaignId_UserId", "dbo", "DemandayCampaignId", "Id")
                    .WithColumn("OwnerId").AsInt32().Nullable().ForeignKey("FK_DemandayTeleMarketingEnquiryCampaignQuestions_OwnerId_UserId", "dbo", "Users", "UserId");
            }

            // Table for Demanday Tele Marketing Enquiry Question Answers
            if (!Schema.Table("DemandayTeleMarketingEnquiryQuestionAnswers").Exists())
            {
                Create.Table("DemandayTeleMarketingEnquiryQuestionAnswers")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                    .WithColumn("QuestionId").AsInt32().Nullable().ForeignKey("FK_DemandayTeleMarketingEnquiryQuestionAnswers_QuestionId", "dbo", "DemandayTeleMarketingEnquiryCampaignQuestions", "Id")
                    .WithColumn("AnswerText").AsString(500).Nullable()
                    .WithColumn("OwnerId").AsInt32().Nullable().ForeignKey("FK_DemandayTeleMarketingEnquiryQuestionAnswers_OwnerId_UserId", "dbo", "Users", "UserId");
            }
        }

        public override void Down()
        {  
            // Drop tables
            Delete.Table("DemandayTeleMarketingEnquiryQuestionAnswers");
            Delete.Table("DemandayTeleMarketingEnquiryCampaignQuestions");
        }
    }
}
