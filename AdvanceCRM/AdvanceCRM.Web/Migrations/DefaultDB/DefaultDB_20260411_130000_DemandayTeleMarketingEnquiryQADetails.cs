using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260411130000)]
    public class DefaultDB_20260411_130000_DemandayTeleMarketingEnquiryQADetails : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("DemandayTeleMarketingEnquiryQADetails").Exists())
            {
                Create.Table("DemandayTeleMarketingEnquiryQADetails")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                    .WithColumn("EnquiryId").AsInt32().NotNullable()
                        .ForeignKey("FK_DemandayTeleMarketingEnquiryQADetails_EnquiryId", "dbo", "DemandayTeleMarketingEnquiry", "Id")
                    .WithColumn("QuestionId").AsInt32().Nullable()
                        .ForeignKey("FK_DemandayTeleMarketingEnquiryQADetails_QuestionId", "dbo", "DemandayTeleMarketingEnquiryCampaignQuestions", "Id")
                    .WithColumn("AnswerId").AsInt32().Nullable()
                        .ForeignKey("FK_DemandayTeleMarketingEnquiryQADetails_AnswerId", "dbo", "DemandayTeleMarketingEnquiryQuestionAnswers", "Id");
            }
        }

        public override void Down()
        {
            Delete.Table("DemandayTeleMarketingEnquiryQADetails");
        }
    }
}
