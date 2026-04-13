using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260406133500)]
    public class DefaultDB_20260406_133500_AddCampaignAttachmenttoDemanday : Migration
    {
        public override void Up()
        {
            // DemandayEnquiry - Add CampaignId if not exists
            if (Schema.Table("DemandayEnquiry").Exists())
            {
                if (!Schema.Table("DemandayEnquiry").Column("CampaignId").Exists())
                    Alter.Table("DemandayEnquiry")
                        .AddColumn("CampaignId").AsString(15).Nullable();

            }

            // DemandayTeamLeader - Add Attachments and CampaignId if not exist
            if (Schema.Table("DemandayTeamLeader").Exists())
            {
                if (!Schema.Table("DemandayTeamLeader").Column("Attachments").Exists())
                    Alter.Table("DemandayTeamLeader")
                        .AddColumn("Attachments").AsString(500).Nullable();

                if (!Schema.Table("DemandayTeamLeader").Column("CampaignId").Exists())
                    Alter.Table("DemandayTeamLeader")
                        .AddColumn("CampaignId").AsString(15).Nullable();
            }

            // EnquiryContacts - Add CampaignId if not exists
            if (Schema.Table("EnquiryContacts").Exists())
            {
                if (!Schema.Table("EnquiryContacts").Column("CampaignId").Exists())
                    Alter.Table("EnquiryContacts")
                       .AddColumn("CampaignId").AsString(15).Nullable();
            }

            // DemandayTeleMarketingEnquiry - Add or Alter CampaignId
            if (Schema.Table("DemandayTeleMarketingEnquiry").Exists())
            {
                if (!Schema.Table("DemandayTeleMarketingEnquiry").Column("CampaignId").Exists())
                    Alter.Table("DemandayTeleMarketingEnquiry")
                        .AddColumn("CampaignId").AsString(50).Nullable();
                else
                    Alter.Table("DemandayTeleMarketingEnquiry")
                        .AlterColumn("CampaignId").AsString(50).Nullable();

                if (!Schema.Table("DemandayTeleMarketingEnquiry").Column("Attachments").Exists())
                    Alter.Table("DemandayTeleMarketingEnquiry")
                        .AddColumn("Attachments").AsString(500).Nullable();
            }

            // DemandayTeleMarketingTeamLeader - Add Attachments and CampaignId
            if (Schema.Table("DemandayTeleMarketingTeamLeader").Exists())
            {
                if (!Schema.Table("DemandayTeleMarketingTeamLeader").Column("Attachments").Exists())
                    Alter.Table("DemandayTeleMarketingTeamLeader")
                        .AddColumn("Attachments").AsString(500).Nullable();

                if (!Schema.Table("DemandayTeleMarketingTeamLeader").Column("CampaignId").Exists())
                    Alter.Table("DemandayTeleMarketingTeamLeader")
                        .AddColumn("CampaignId").AsString(50).Nullable();
                else
                    Alter.Table("DemandayTeleMarketingTeamLeader")
                        .AlterColumn("CampaignId").AsString(50).Nullable();
            }
            if (Schema.Table("DemandayTeleMarketingQualilty").Exists())
            {
                if (!Schema.Table("DemandayTeleMarketingQualilty").Column("Attachments").Exists())
                    Alter.Table("DemandayTeleMarketingQualilty")
                        .AddColumn("Attachments").AsString(500).Nullable();
            }
        }

        public override void Down()
        {
            // Rollback: Remove added columns
            if (Schema.Table("DemandayEnquiry").Exists() && Schema.Table("DemandayEnquiry").Column("CampaignId").Exists())
                Delete.Column("CampaignId").FromTable("DemandayEnquiry");

            if (Schema.Table("DemandayTeamLeader").Exists())
            {
                if (Schema.Table("DemandayTeamLeader").Column("Attachments").Exists())
                    Delete.Column("Attachments").FromTable("DemandayTeamLeader");
                if (Schema.Table("DemandayTeamLeader").Column("CampaignId").Exists())
                    Delete.Column("CampaignId").FromTable("DemandayTeamLeader");
            }

            if (Schema.Table("EnquiryContacts").Exists() && Schema.Table("EnquiryContacts").Column("CampaignId").Exists())
                Delete.Column("CampaignId").FromTable("EnquiryContacts");

            if (Schema.Table("DemandayTeleMarketingEnquiry").Exists() && Schema.Table("DemandayTeleMarketingEnquiry").Column("CampaignId").Exists())
                Delete.Column("CampaignId").FromTable("DemandayTeleMarketingEnquiry");

            if (Schema.Table("DemandayTeleMarketingTeamLeader").Exists())
            {
                if (Schema.Table("DemandayTeleMarketingTeamLeader").Column("Attachments").Exists())
                    Delete.Column("Attachments").FromTable("DemandayTeleMarketingTeamLeader");
                if (Schema.Table("DemandayTeleMarketingTeamLeader").Column("CampaignId").Exists())
                    Delete.Column("CampaignId").FromTable("DemandayTeleMarketingTeamLeader");
            }
        }
    }
}

