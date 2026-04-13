using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260405093900)]
    public class DefaultDB_20260405_093900_DemandayToolkitMasters : Migration
    {
        public override void Up()
        {
            Create.Table("DemandayMasterAccount")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("AccountNumber").AsString(15).Nullable();

            Create.Table("DemandayCampaignId")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("CampaignId").AsString(15).Nullable()
                .WithColumn("DemandayMasterAccountId").AsInt32().Nullable().ForeignKey("FK_DemandayCampaignId_DemandayMasterAccountId", "dbo", "DemandayMasterAccount", "Id");
        }

        public override void Down()
        {
            Delete.Table("DemandayCampaignId");
            Delete.Table("DemandayMasterAccount");
        }
    }
}
