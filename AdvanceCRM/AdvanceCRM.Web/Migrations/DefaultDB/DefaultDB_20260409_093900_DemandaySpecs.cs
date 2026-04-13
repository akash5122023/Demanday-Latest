using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260409093900)]
    public class DefaultDB_20260409_093900_DemandaySpecs : Migration
    {
        public override void Up()
        {
            Create.Table("DemandaySpecs")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("OrderId").AsInt64().Nullable()
                .WithColumn("JobTitle").AsString(500).Nullable()
                .WithColumn("JobLevel").AsString(500).Nullable()
                .WithColumn("JobFunction").AsString(200).Nullable()
                .WithColumn("Industry").AsString(500).Nullable()
                .WithColumn("CompanyEmployeeSize").AsString(200).Nullable()
                .WithColumn("AnnualRevenue").AsString(200).Nullable()
                .WithColumn("Address").AsString(200).Nullable()
                .WithColumn("City").AsString(100).Nullable()
                .WithColumn("State").AsString(100).Nullable()
                .WithColumn("ZipCode").AsString(20).Nullable()
                .WithColumn("Country").AsString(100).Nullable()
                .WithColumn("Comments").AsString(10000).Nullable()
                .WithColumn("AdditionalNotes").AsString(10000).Nullable()
                .WithColumn("MasterAccountId").AsInt32().Nullable().ForeignKey("FK_DemandaySpecs_MasterAccountId_UserId", "dbo", "DemandayMasterAccount", "Id")
                .WithColumn("CampaignId").AsInt32().Nullable().ForeignKey("FK_DemandaySpecs_CampaignId_UserId", "dbo", "DemandayCampaignId", "Id")
                .WithColumn("OwnerId").AsInt32().Nullable().ForeignKey("FK_DemandaySpecs_OwnerId_UserId", "dbo", "Users", "UserId");     
        }

        public override void Down()
        {
            Delete.Table("DemandaySpecs");
        }
    }
}
