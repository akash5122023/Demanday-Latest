using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260105133500)]
    public class DefaultDB_20260105_133500_DemandayModifiedRevenuee : Migration
    {
        public override void Up()
        {
            if (Schema.Table("DemandayEnquiry").Exists())
            {
                if (Schema.Table("DemandayEnquiry").Column("Revenue").Exists())
                    Alter.Table("DemandayEnquiry")
                        .AlterColumn("Revenue").AsString(50).Nullable();
            }
            if (Schema.Table("DemandayTeamLeader").Exists())
            {
                if (Schema.Table("DemandayTeamLeader").Column("Revenue").Exists())
                    Alter.Table("DemandayTeamLeader")
                        .AlterColumn("Revenue").AsString(50).Nullable();
            }           
            if (Schema.Table("DemandayQuality").Exists())
            {
                if (Schema.Table("DemandayQuality").Column("Revenue").Exists())
                    Alter.Table("DemandayQuality")
                        .AlterColumn("Revenue").AsString(50).Nullable();
            }
            if (Schema.Table("DemandayMIS").Exists())
            {
                if (Schema.Table("DemandayMIS").Column("Revenue").Exists())
                    Alter.Table("DemandayMIS")
                        .AlterColumn("Revenue").AsString(50).Nullable();
            }
            if (Schema.Table("DemandayContacts").Exists())
            {
                if (Schema.Table("DemandayContacts").Column("Revenue").Exists())
                    Alter.Table("DemandayContacts")
                        .AlterColumn("Revenue").AsString(50).Nullable();
            }
            if (Schema.Table("EnquiryContacts").Exists())
            {
                if (Schema.Table("EnquiryContacts").Column("Revenue").Exists())
                    Alter.Table("EnquiryContacts")
                       .AlterColumn("Revenue").AsString(50).Nullable();
            }



            ///////////////////////////////////////////////////////////////
            if (Schema.Table("DemandayTeleMarketingEnquiry").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingEnquiry").Column("Revenue").Exists())
                    Alter.Table("DemandayTeleMarketingEnquiry")
                        .AlterColumn("Revenue").AsString(50).Nullable();
            }
            if (Schema.Table("DemandayTeleMarketingTeamLeader").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingTeamLeader").Column("Revenue").Exists())
                    Alter.Table("DemandayTeleMarketingTeamLeader")
                        .AlterColumn("Revenue").AsString(50).Nullable();
            }
            if (Schema.Table("DemandayTeleMarketingQualilty").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingQualilty").Column("Revenue").Exists())
                    Alter.Table("DemandayTeleMarketingQualilty")
                        .AlterColumn("Revenue").AsString(50).Nullable();
            }

            if (Schema.Table("DemandayTeleMarketingMIS").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingMIS").Column("Revenue").Exists())
                    Alter.Table("DemandayTeleMarketingMIS")
                       .AlterColumn("Revenue").AsString(50).Nullable();
            }
            if (Schema.Table("DemandayTeleMarketingContacts").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingContacts").Column("Revenue").Exists())
                    Alter.Table("DemandayTeleMarketingContacts")
                        .AlterColumn("Revenue").AsString(50).Nullable();
            }                   
        }
        public override void Down()
        {
        }
    }
}

