using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251221121300)]
    public class DefaultDB_20251221_121300_DemandayAddedFeilds : Migration
    {
        public override void Up()
        {
            if (Schema.Table("DemandayEnquiry").Exists())
            {
                if (Schema.Table("DemandayEnquiry").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayEnquiry")
                        .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();
                else
                    Alter.Table("DemandayEnquiry")
                        .AddColumn("CompanyEmployeeSize").AsInt64().Nullable();

                if (!Schema.Table("DemandayEnquiry").Column("EmailFormat").Exists())
                    Alter.Table("DemandayEnquiry")
                        .AddColumn("EmailFormat").AsString(100).Nullable();
            }
            if (Schema.Table("DemandayTeamLeader").Exists())
            {
                if (Schema.Table("DemandayTeamLeader").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayTeamLeader")
                        .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();
                else
                    Alter.Table("DemandayTeamLeader")
                        .AddColumn("CompanyEmployeeSize").AsInt64().Nullable();

                if (!Schema.Table("DemandayTeamLeader").Column("EmailFormat").Exists())
                    Alter.Table("DemandayTeamLeader")
                        .AddColumn("EmailFormat").AsString(100).Nullable();
            }

            //Alter.Table("DemandayTeamLeader")
            //    .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable()
            //    .AddColumn("EmailFormat").AsString(100).Nullable();
            ////////////////////////////////////////////////////////////
            ///

            if (Schema.Table("DemandayQuality").Exists())
            {
                if (Schema.Table("DemandayQuality").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayQuality")
                        .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();
                else
                    Alter.Table("DemandayQuality")
                        .AddColumn("CompanyEmployeeSize").AsInt64().Nullable();               
            }

            //Alter.Table("DemandayQuality")
            //    .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();
            if (Schema.Table("DemandayMIS").Exists())
            {
                if (Schema.Table("DemandayMIS").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayMIS")
                        .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();
                else
                    Alter.Table("DemandayMIS")
                        .AddColumn("CompanyEmployeeSize").AsInt64().Nullable();
            }
            //Alter.Table("DemandayMIS")
            //    .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();
            if (Schema.Table("DemandayContacts").Exists())
            {
                if (Schema.Table("DemandayContacts").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayContacts")
                        .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();
                else
                    Alter.Table("DemandayContacts")
                        .AddColumn("CompanyEmployeeSize").AsInt64().Nullable();
            }
            //Alter.Table("DemandayContacts")
            //    .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();
            if (Schema.Table("EnquiryContacts").Exists())
            {
                if (Schema.Table("EnquiryContacts").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("EnquiryContacts")
                        .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();
                else
                    Alter.Table("EnquiryContacts")
                        .AddColumn("CompanyEmployeeSize").AsInt64().Nullable();
            }
            //Alter.Table("EnquiryContacts")
            //    .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();



            ///////////////////////////////////////////////////////////////
            if (Schema.Table("DemandayTeleMarketingEnquiry").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingEnquiry").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayTeleMarketingEnquiry")
                        .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();
                else
                    Alter.Table("DemandayTeleMarketingEnquiry")
                        .AddColumn("CompanyEmployeeSize").AsInt64().Nullable();

                if (!Schema.Table("DemandayTeleMarketingEnquiry").Column("EmailFormat").Exists())
                    Alter.Table("DemandayTeleMarketingEnquiry")
                        .AddColumn("EmailFormat").AsString(100).Nullable();
            }
            //Alter.Table("DemandayTeleMarketingEnquiry")
            //    .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable()
            //    .AddColumn("EmailFormat").AsString(100).Nullable();
            if (Schema.Table("DemandayTeleMarketingTeamLeader").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingTeamLeader").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayTeleMarketingTeamLeader")
                        .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();
                else
                    Alter.Table("DemandayTeleMarketingTeamLeader")
                        .AddColumn("CompanyEmployeeSize").AsInt64().Nullable();

                if (!Schema.Table("DemandayTeleMarketingTeamLeader").Column("EmailFormat").Exists())
                    Alter.Table("DemandayTeleMarketingTeamLeader")
                        .AddColumn("EmailFormat").AsString(100).Nullable();
            }
            //Alter.Table("DemandayTeleMarketingTeamLeader")
            //    .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable()
            //    .AddColumn("EmailFormat").AsString(100).Nullable();
            if (Schema.Table("DemandayTeleMarketingQualilty").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingQualilty").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayTeleMarketingQualilty")
                        .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();
                else
                    Alter.Table("DemandayTeleMarketingQualilty")
                        .AddColumn("CompanyEmployeeSize").AsInt64().Nullable();

                if (!Schema.Table("DemandayTeleMarketingQualilty").Column("EmailFormat").Exists())
                    Alter.Table("DemandayTeleMarketingQualilty")
                        .AddColumn("EmailFormat").AsString(100).Nullable();
            }
            //Alter.Table("DemandayTeleMarketingQualilty")
            //    .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable()
            //    .AddColumn("EmailFormat").AsString(100).Nullable();
            ///////////////////////////////////////////////////////////

            if (Schema.Table("DemandayTeleMarketingMIS").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingMIS").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayTeleMarketingMIS")
                        .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();
                else
                    Alter.Table("DemandayTeleMarketingMIS")
                        .AddColumn("CompanyEmployeeSize").AsInt64().Nullable();
            }

            //Alter.Table("DemandayTeleMarketingMIS")
            //    .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();
            if (Schema.Table("DemandayTeleMarketingContacts").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingContacts").Column("CompanyEmployeeSize").Exists())
                    Alter.Table("DemandayTeleMarketingContacts")
                        .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();
                else
                    Alter.Table("DemandayTeleMarketingContacts")
                        .AddColumn("CompanyEmployeeSize").AsInt64().Nullable();
            }
            //Alter.Table("DemandayTeleMarketingContacts")
            //    .AlterColumn("CompanyEmployeeSize").AsInt64().Nullable();

           

        }

        public override void Down()
        {
            //Delete.Table("DemandayEnquiry");
            //Delete.Table("DemandayTeamLeader");
            //Delete.Table("DemandayQuality");
            //Delete.Table("DemandayMIS");
            //Delete.Table("DemandayContacts");
            //Delete.Table("EnquiryContacts");
            //Delete.Table("DemandayVerification");
        }
    }
}
