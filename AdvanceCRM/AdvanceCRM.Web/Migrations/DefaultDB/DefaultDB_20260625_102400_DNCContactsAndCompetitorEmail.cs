using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260625102400)]
    public class DefaultDB_20260625_102400_DNCContactsAndCompetitorEmail : Migration
    {
        public override void Up()
        {
            Alter.Table("DemandayCompetitor")
                .AddColumn("Email").AsString(200).Nullable();

            Create.Table("DNCContacts")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FirstName").AsString(100).Nullable()
                .WithColumn("LastName").AsString(100).Nullable()
                .WithColumn("Email").AsString(200).Nullable()
                .WithColumn("DNCStatus").AsString(50).Nullable()
                .WithColumn("Number").AsString(50).Nullable()
                .WithColumn("CampaignId").AsInt32().Nullable().ForeignKey("FK_DNCContacts_CampaignId_UserId", "dbo", "DemandayCampaignId", "Id")
                .WithColumn("MasterAccountId").AsInt32().Nullable().ForeignKey("FK_DNCContacts_MasterAccountId_UserId", "dbo", "DemandayMasterAccount", "Id");
        }

        public override void Down()
        {
            Delete.Table("DNCContacts");
            Delete.Column("Email").FromTable("DemandayCompetitor");
        }
    }
}
