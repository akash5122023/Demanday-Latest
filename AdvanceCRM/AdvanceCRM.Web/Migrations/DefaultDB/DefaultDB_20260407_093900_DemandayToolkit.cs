using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260407093900)]
    public class DefaultDB_20260407_093900_DemandayToolkit : Migration
    {
        public override void Up()
        {
            Create.Table("MasterSupression")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("CompanyName").AsString(200).Nullable()
                .WithColumn("FirstName").AsString(100).Nullable()
                .WithColumn("LastName").AsString(100).Nullable()
                .WithColumn("Email").AsString(200).Nullable()
                .WithColumn("Domain").AsString(50).Nullable()
                .WithColumn("Date").AsDateTime().Nullable()
                .WithColumn("MasterAccountId").AsInt32().Nullable().ForeignKey("FK_MasterSupression_MasterAccountId_UserId", "dbo", "DemandayMasterAccount", "Id")
                .WithColumn("CampaignId").AsInt32().Nullable().ForeignKey("FK_MasterSupression_CampaignId_UserId", "dbo", "DemandayCampaignId", "Id")
                .WithColumn("OwnerId").AsInt32().Nullable().ForeignKey("FK_MasterSupression_OwnerId_UserId", "dbo", "Users", "UserId");

            Create.Table("ClientSupression")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("CompanyName").AsString(200).Nullable()
                .WithColumn("FirstName").AsString(100).Nullable()
                .WithColumn("LastName").AsString(100).Nullable()
                .WithColumn("Email").AsString(200).Nullable()
                .WithColumn("Domain").AsString(50).Nullable()
                .WithColumn("Date").AsDateTime().Nullable()
                .WithColumn("MasterAccountId").AsInt32().Nullable().ForeignKey("FK_ClientSupression_MasterAccountId_UserId", "dbo", "DemandayMasterAccount", "Id")
                .WithColumn("CampaignId").AsInt32().Nullable().ForeignKey("FK_ClientSupression_CampaignId_UserId", "dbo", "DemandayCampaignId", "Id")
                .WithColumn("OwnerId").AsInt32().Nullable().ForeignKey("FK_ClientSupression_OwnerId_UserId", "dbo", "Users", "UserId");

            Create.Table("OpenCampaign")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Domain").AsString(100).Nullable()
                .WithColumn("DemandayUserId").AsInt32().Nullable().ForeignKey("FK_OpenCampaign_DemandayUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("TimeStamp").AsDateTime().Nullable()
                .WithColumn("MasterAccountId").AsInt32().Nullable().ForeignKey("FK_OpenCampaign_MasterAccountId_UserId", "dbo", "DemandayMasterAccount", "Id")
                .WithColumn("CampaignId").AsInt32().Nullable().ForeignKey("FK_OpenCampaign_CampaignId_UserId", "dbo", "DemandayCampaignId", "Id")
                .WithColumn("OwnerId").AsInt32().Nullable().ForeignKey("FK_OpenCampaign_OwnerId_UserId", "dbo", "Users", "UserId");

            Create.Table("TalCampaign")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("CompanyName").AsString(200).Nullable()
                .WithColumn("Domain").AsString(100).Nullable()
                .WithColumn("AgentsName").AsInt32().Nullable().ForeignKey("FK_TalCampaign_AgentsName_UserId", "dbo", "Users", "UserId")
                .WithColumn("Reason").AsString(100).Nullable()
                .WithColumn("CPC").AsInt64().Nullable()
                .WithColumn("MasterAccountId").AsInt32().Nullable().ForeignKey("FK_TalCampaign_MasterAccountId_UserId", "dbo", "DemandayMasterAccount", "Id")
                .WithColumn("CampaignId").AsInt32().Nullable().ForeignKey("FK_TalCampaign_CampaignId_UserId", "dbo", "DemandayCampaignId", "Id")
                .WithColumn("OwnerId").AsInt32().Nullable().ForeignKey("FK_TalCampaign_OwnerId_UserId", "dbo", "Users", "UserId");            

            Create.Table("DemandayCompetitor")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("CompanyName").AsString(200).Nullable()
                .WithColumn("Domain").AsString(50).Nullable()
                .WithColumn("CPC").AsInt64().Nullable()
                .WithColumn("MasterAccountId").AsInt32().Nullable().ForeignKey("FK_DemandayCompetitor_MasterAccountId_UserId", "dbo", "DemandayMasterAccount", "Id")
                .WithColumn("CampaignId").AsInt32().Nullable().ForeignKey("FK_DemandayCompetitor_CampaignId_UserId", "dbo", "DemandayCampaignId", "Id")
                .WithColumn("OwnerId").AsInt32().Nullable().ForeignKey("FK_DemandayCompetitor_OwnerId_UserId", "dbo", "Users", "UserId");

        }

        public override void Down()
        {
            Delete.Table("MasterSupression");
            Delete.Table("ClientSupression");
            Delete.Table("OpenCampaign");
            Delete.Table("TalCampaign");
            Delete.Table("DemandayCompetitor");
        }
    }
}
