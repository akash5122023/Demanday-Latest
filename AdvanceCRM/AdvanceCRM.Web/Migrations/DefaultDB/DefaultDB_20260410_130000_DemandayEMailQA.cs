using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260410130000)]
    public class DefaultDB_20260410_130000_DemandayEMailQA : Migration
    {
        public override void Up()
        {
            Alter.Table("DemandayTeleMarketingEnquiry")
                 .AddColumn("ETQuestionId").AsInt32().Nullable().ForeignKey("FK_DemandayTeleMarketingEnquiry_ETQuestionId", "dbo", "DemandayTeleMarketingEnquiryCampaignQuestions", "Id")
                 .AddColumn("ETAnswerId").AsInt32().Nullable().ForeignKey("FK_DemandayTeleMarketingEnquiry_ETAnswerId", "dbo", "DemandayTeleMarketingEnquiryQuestionAnswers", "Id");

            Alter.Table("DemandayTeleMarketingTeamLeader")
                .AddColumn("ETQuestionId").AsInt32().Nullable().ForeignKey("FK_DemandayTeleMarketingTeamLeader_ETQuestionId", "dbo", "DemandayTeleMarketingEnquiryCampaignQuestions", "Id")
                 .AddColumn("ETAnswerId").AsInt32().Nullable().ForeignKey("FK_DemandayTeleMarketingTeamLeader_ETAnswerId", "dbo", "DemandayTeleMarketingEnquiryQuestionAnswers", "Id");
            Alter.Table("DemandayTeleMarketingQualilty")
                .AddColumn("ETQuestionId").AsInt32().Nullable().ForeignKey("FK_DemandayTeleMarketingQualilty_ETQuestionId", "dbo", "DemandayTeleMarketingEnquiryCampaignQuestions", "Id")
                 .AddColumn("ETAnswerId").AsInt32().Nullable().ForeignKey("FK_DemandayTeleMarketingQualilty_ETAnswerId", "dbo", "DemandayTeleMarketingEnquiryQuestionAnswers", "Id");

            Alter.Table("DemandayTeleMarketingMIS")
                .AddColumn("ETQuestionId").AsInt32().Nullable().ForeignKey("FK_DemandayTeleMarketingMIS_ETQuestionId", "dbo", "DemandayTeleMarketingEnquiryCampaignQuestions", "Id")
                 .AddColumn("ETAnswerId").AsInt32().Nullable().ForeignKey("FK_DemandayTeleMarketingMIS_ETAnswerId", "dbo", "DemandayTeleMarketingEnquiryQuestionAnswers", "Id");
            
            Alter.Table("DemandayTeleMarketingContacts")
                .AddColumn("ETQuestionId").AsInt32().Nullable().ForeignKey("FK_DemandayTeleMarketingContacts_ETQuestionId", "dbo", "DemandayTeleMarketingEnquiryCampaignQuestions", "Id")
                 .AddColumn("ETAnswerId").AsInt32().Nullable().ForeignKey("FK_DemandayTeleMarketingContacts_ETAnswerId", "dbo", "DemandayTeleMarketingEnquiryQuestionAnswers", "Id");
        }

        public override void Down()
        {
        }
    }
}
