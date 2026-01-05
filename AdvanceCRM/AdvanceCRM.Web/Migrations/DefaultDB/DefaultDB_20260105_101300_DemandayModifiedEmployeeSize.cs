using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260105105000)]
    public class DefaultDB_20260105_105000_DemandayModifiedEmployeeSize : Migration
    {
        public override void Up()
        {
            if (Schema.Table("DemandayEnquiry").Exists())
            {
                if (Schema.Table("DemandayEnquiry").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayEnquiry")
                        .AlterColumn("CompanyEmployeeSize").AsString(100).Nullable();
            }
            if (Schema.Table("DemandayTeamLeader").Exists())
            {
                if (Schema.Table("DemandayTeamLeader").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayTeamLeader")
                        .AlterColumn("CompanyEmployeeSize").AsString(100).Nullable();
            }           
            if (Schema.Table("DemandayQuality").Exists())
            {
                if (Schema.Table("DemandayQuality").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayQuality")
                        .AlterColumn("CompanyEmployeeSize").AsString(100).Nullable();         
            }
            if (Schema.Table("DemandayMIS").Exists())
            {
                if (Schema.Table("DemandayMIS").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayMIS")
                        .AlterColumn("CompanyEmployeeSize").AsString(100).Nullable();
            }
            if (Schema.Table("DemandayContacts").Exists())
            {
                if (Schema.Table("DemandayContacts").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayContacts")
                        .AlterColumn("CompanyEmployeeSize").AsString(100).Nullable();
            }
            if (Schema.Table("EnquiryContacts").Exists())
            {
                if (Schema.Table("EnquiryContacts").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("EnquiryContacts")
                        .AlterColumn("CompanyEmployeeSize").AsString(100).Nullable();
            }



            ///////////////////////////////////////////////////////////////
            if (Schema.Table("DemandayTeleMarketingEnquiry").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingEnquiry").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayTeleMarketingEnquiry")
                        .AlterColumn("CompanyEmployeeSize").AsString(100).Nullable();
            }
            if (Schema.Table("DemandayTeleMarketingTeamLeader").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingTeamLeader").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayTeleMarketingTeamLeader")
                        .AlterColumn("CompanyEmployeeSize").AsString(100).Nullable();
            }
            if (Schema.Table("DemandayTeleMarketingQualilty").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingQualilty").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayTeleMarketingQualilty")
                        .AlterColumn("CompanyEmployeeSize").AsString(100).Nullable();
            }

            if (Schema.Table("DemandayTeleMarketingMIS").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingMIS").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayTeleMarketingMIS")
                        .AlterColumn("CompanyEmployeeSize").AsString(100).Nullable();
            }
            if (Schema.Table("DemandayTeleMarketingContacts").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingContacts").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayTeleMarketingContacts")
                        .AlterColumn("CompanyEmployeeSize").AsString(100).Nullable();
            }                   
        }
        public override void Down()
        {
        }
    }
}

