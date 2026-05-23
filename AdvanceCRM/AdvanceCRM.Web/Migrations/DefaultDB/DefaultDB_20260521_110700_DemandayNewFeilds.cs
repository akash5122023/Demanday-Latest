using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260521110700)]
    public class DefaultDB_20260521_110700_DemandayNewFeilds : Migration
    {
        public override void Up()
        {            
            
            if (Schema.Table("DemandayTeleMarketingEnquiry").Exists())
            {
                    Alter.Table("DemandayTeleMarketingEnquiry")
                    .AddColumn("ZoomInfoIndustry").AsString(100).Nullable()
                        .AddColumn("ZoomInfoEmployeeSize").AsString(100).Nullable()
                        .AddColumn("Date").AsDateTime().Nullable()
                        .AddColumn("Asset").AsString(100).Nullable()
                        .AddColumn("CallStatus").AsString(100).Nullable()
                        .AddColumn("AdditionalNotes").AsString(2000).Nullable();
            }
            if (Schema.Table("DemandayTeleMarketingTeamLeader").Exists())
            {
                Alter.Table("DemandayTeleMarketingTeamLeader")
                    .AddColumn("ZoomInfoIndustry").AsString(100).Nullable()
                        .AddColumn("ZoomInfoEmployeeSize").AsString(100).Nullable()
                        .AddColumn("Date").AsDateTime().Nullable()
                        .AddColumn("Asset").AsString(100).Nullable()
                        .AddColumn("CallStatus").AsString(100).Nullable()
                        .AddColumn("AdditionalNotes").AsString(2000).Nullable();
            }
            if (Schema.Table("DemandayTeleMarketingQualilty").Exists())
            {
                Alter.Table("DemandayTeleMarketingQualilty")
                    .AddColumn("ZoomInfoIndustry").AsString(100).Nullable()
                        .AddColumn("ZoomInfoEmployeeSize").AsString(100).Nullable()
                        .AddColumn("Date").AsDateTime().Nullable()
                        .AddColumn("Asset").AsString(100).Nullable()
                        .AddColumn("CallStatus").AsString(100).Nullable()
                        .AddColumn("AdditionalNotes").AsString(2000).Nullable();
            }
            if (Schema.Table("DemandayTeleMarketingMIS").Exists())
            {
                Alter.Table("DemandayTeleMarketingMIS")
                    .AddColumn("ZoomInfoIndustry").AsString(100).Nullable()
                        .AddColumn("ZoomInfoEmployeeSize").AsString(100).Nullable()
                        .AddColumn("Date").AsDateTime().Nullable()
                        .AddColumn("Asset").AsString(100).Nullable()
                        .AddColumn("CallStatus").AsString(100).Nullable()
                        .AddColumn("AdditionalNotes").AsString(2000).Nullable();
            }
            if (Schema.Table("DemandayTeleMarketingContacts").Exists())
            {
                Alter.Table("DemandayTeleMarketingContacts")
                    .AddColumn("ZoomInfoIndustry").AsString(100).Nullable()
                        .AddColumn("ZoomInfoEmployeeSize").AsString(100).Nullable()
                        .AddColumn("Date").AsDateTime().Nullable()
                        .AddColumn("Asset").AsString(100).Nullable()
                        .AddColumn("CallStatus").AsString(100).Nullable()
                        .AddColumn("AdditionalNotes").AsString(2000).Nullable();
            }

            ////////////////////

            if (Schema.Table("DemandayEnquiry").Exists())
            {
                Alter.Table("DemandayEnquiry")
                    .AddColumn("ZoomInfoIndustry").AsString(100).Nullable()
                        .AddColumn("ZoomInfoEmployeeSize").AsString(100).Nullable()
                    .AddColumn("Date").AsDateTime().Nullable();
            }
            if (Schema.Table("DemandayTeamLeader").Exists())
            {
                Alter.Table("DemandayTeamLeader")
                    .AddColumn("ZoomInfoIndustry").AsString(100).Nullable()
                        .AddColumn("ZoomInfoEmployeeSize").AsString(100).Nullable()
                    .AddColumn("Date").AsDateTime().Nullable();
            }
            if (Schema.Table("DemandayQuality").Exists())
            {
                Alter.Table("DemandayQuality")
                    .AddColumn("ZoomInfoIndustry").AsString(100).Nullable()
                        .AddColumn("ZoomInfoEmployeeSize").AsString(100).Nullable()
                    .AddColumn("Date").AsDateTime().Nullable();
            }
            if (Schema.Table("DemandayMIS").Exists())
            {
                Alter.Table("DemandayMIS")
                    .AddColumn("ZoomInfoIndustry").AsString(100).Nullable()
                    .AddColumn("ZoomInfoEmployeeSize").AsString(100).Nullable()
                    .AddColumn("Date").AsDateTime().Nullable();
            }
            if (Schema.Table("DemandayContacts").Exists())
            {
                Alter.Table("DemandayContacts")
                    .AddColumn("ZoomInfoIndustry").AsString(100).Nullable()
                    .AddColumn("ZoomInfoEmployeeSize").AsString(100).Nullable()
                    .AddColumn("Date").AsDateTime().Nullable();
            }
            if (Schema.Table("DemandayVerification").Exists())
            {
                Alter.Table("DemandayVerification")
                    .AddColumn("Date").AsDateTime().Nullable();
            }
        }

        public override void Down()
        {
        }
    }
}
