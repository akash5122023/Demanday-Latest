using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20261021000000)]
    public class DefaultDB_20261021_000000_EmailTeamModules : Migration
    {
        public override void Up()
        {
            Create.Table("EmailTeam")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("MasterAccountId").AsInt32().Nullable()
                    .ForeignKey("FK_EmailTeam_MasterAccountId_DemandayMasterAccount_Id", "dbo", "DemandayMasterAccount", "Id")
                .WithColumn("CampaignId").AsInt32().Nullable()
                    .ForeignKey("FK_EmailTeam_CampaignId_DemandayCampaignId_Id", "dbo", "DemandayCampaignId", "Id")
                .WithColumn("FirstName").AsString(100).Nullable()
                .WithColumn("LastName").AsString(100).Nullable()
                .WithColumn("Email").AsString(200).Nullable()
                .WithColumn("Status").AsInt32().Nullable()
                .WithColumn("OwnerId").AsInt32().Nullable()
                    .ForeignKey("FK_EmailTeam_OwnerId_Users_UserId", "dbo", "Users", "UserId")
                .WithColumn("DemandayQualityId").AsInt32().Nullable()
                    .ForeignKey("FK_EmailTeam_DemandayQualityId_DemandayQuality_Id", "dbo", "DemandayQuality", "Id");

            Create.Table("TeleMarketingEmailTeam")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("MasterAccountId").AsInt32().Nullable()
                    .ForeignKey("FK_TMEmailTeam_MasterAccountId_DemandayMasterAccount_Id", "dbo", "DemandayMasterAccount", "Id")
                .WithColumn("CampaignId").AsInt32().Nullable()
                    .ForeignKey("FK_TMEmailTeam_CampaignId_DemandayCampaignId_Id", "dbo", "DemandayCampaignId", "Id")
                .WithColumn("FirstName").AsString(100).Nullable()
                .WithColumn("LastName").AsString(100).Nullable()
                .WithColumn("Email").AsString(200).Nullable()
                .WithColumn("Status").AsInt32().Nullable()
                .WithColumn("OwnerId").AsInt32().Nullable()
                    .ForeignKey("FK_TMEmailTeam_OwnerId_Users_UserId", "dbo", "Users", "UserId")
                .WithColumn("DemandayTeleMarketingQualiltyId").AsInt32().Nullable()
                    .ForeignKey("FK_TMEmailTeam_TMQualiltyId_DemandayTeleMarketingQualilty_Id", "dbo", "DemandayTeleMarketingQualilty", "Id");

            // Mirror each Email Team module's Status back onto the Quality record it came from.
            Alter.Table("DemandayQuality")
                .AddColumn("EmailTeamStatus").AsInt32().Nullable();

            Alter.Table("DemandayTeleMarketingQualilty")
                .AddColumn("EmailTeamStatus").AsInt32().Nullable();
        }

        public override void Down()
        {
            // Both tables hold FKs into their Quality tables, so they go before the columns.
            Delete.Table("TeleMarketingEmailTeam");
            Delete.Table("EmailTeam");

            Delete.Column("EmailTeamStatus").FromTable("DemandayTeleMarketingQualilty");
            Delete.Column("EmailTeamStatus").FromTable("DemandayQuality");
        }
    }
}
