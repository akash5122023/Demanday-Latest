using FluentMigrator;
using System;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260804110000)]
    public class DefaultDB_20260804110000_AddMasterAccountId : AutoReversingMigration
    {
        public override void Up()
        {
            // Regular Demanday modules
            if (!Schema.Table("DemandayEnquiry").Column("MasterAccountId").Exists())
            {
                Alter.Table("DemandayEnquiry").AddColumn("MasterAccountId").AsInt32().Nullable();
            }

            if (!Schema.Table("DemandayTeamLeader").Column("MasterAccountId").Exists())
            {
                Alter.Table("DemandayTeamLeader").AddColumn("MasterAccountId").AsInt32().Nullable();
            }

            if (!Schema.Table("DemandayContacts").Column("MasterAccountId").Exists())
            {
                Alter.Table("DemandayContacts").AddColumn("MasterAccountId").AsInt32().Nullable();
            }

            if (!Schema.Table("DemandayQuality").Column("MasterAccountId").Exists())
            {
                Alter.Table("DemandayQuality").AddColumn("MasterAccountId").AsInt32().Nullable();
            }

            if (!Schema.Table("DemandayMis").Column("MasterAccountId").Exists())
            {
                Alter.Table("DemandayMis").AddColumn("MasterAccountId").AsInt32().Nullable();
            }

            // TeleMarketing modules
            if (!Schema.Table("DemandayTeleMarketingEnquiry").Column("MasterAccountId").Exists())
            {
                Alter.Table("DemandayTeleMarketingEnquiry").AddColumn("MasterAccountId").AsInt32().Nullable();
            }

            if (!Schema.Table("DemandayTeleMarketingMIS").Column("MasterAccountId").Exists())
            {
                Alter.Table("DemandayTeleMarketingMIS").AddColumn("MasterAccountId").AsInt32().Nullable();
            }

            if (!Schema.Table("DemandayTeleMarketingQualilty").Column("MasterAccountId").Exists())
            {
                Alter.Table("DemandayTeleMarketingQualilty").AddColumn("MasterAccountId").AsInt32().Nullable();
            }

            if (!Schema.Table("DemandayTeleMarketingContacts").Column("MasterAccountId").Exists())
            {
                Alter.Table("DemandayTeleMarketingContacts").AddColumn("MasterAccountId").AsInt32().Nullable();
            }

            if (!Schema.Table("DemandayTeleMarketingTeamLeader").Column("MasterAccountId").Exists())
            {
                Alter.Table("DemandayTeleMarketingTeamLeader").AddColumn("MasterAccountId").AsInt32().Nullable();
            }

            // Other modules
            if (!Schema.Table("EnquiryContacts").Column("MasterAccountId").Exists())
            {
                Alter.Table("EnquiryContacts").AddColumn("MasterAccountId").AsInt32().Nullable();
            }
        }
    }
}
