using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260528171200)]
    public class DefaultDB_20260528_171200_DemandayAddSubIndustry : Migration
    {
        private static readonly string[] Tables = new[]
        {
            "DemandayEnquiry",
            "DemandayTeamLeader",
            "DemandayQuality",
            "DemandayMIS",
            "DemandayContacts",
            "EnquiryContacts",
            "DemandayTeleMarketingEnquiry",
            "DemandayTeleMarketingTeamLeader",
            "DemandayTeleMarketingQualilty",
            "DemandayTeleMarketingMIS",
            "DemandayTeleMarketingContacts"
        };

        public override void Up()
        {
            foreach (var table in Tables)
            {
                if (Schema.Table(table).Exists() && !Schema.Table(table).Column("SubIndustry").Exists())
                {
                    Alter.Table(table)
                        .AddColumn("SubIndustry").AsString(100).Nullable();
                }
            }
        }

        public override void Down()
        {
            foreach (var table in Tables)
            {
                if (Schema.Table(table).Exists() && Schema.Table(table).Column("SubIndustry").Exists())
                {
                    Delete.Column("SubIndustry").FromTable(table);
                }
            }
        }
    }
}
