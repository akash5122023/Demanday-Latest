using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    // The Demanday link columns (Profile/Company/Revenue/Address/Link) were created
    // as nvarchar(300). Imported source URLs (e.g. Google/ZoomInfo search links) routinely
    // exceed 300 characters, causing "String or binary data would be truncated" on import.
    // Widen all of these columns to nvarchar(2000) across every Demanday table.
    [Migration(20260602165500)]
    public class DefaultDB_20260602_165500_DemandayEnlargeLinkColumns : Migration
    {
        private const int NewSize = 2000;

        private static readonly string[] Tables = new[]
        {
            "DemandayEnquiry",
            "DemandayTeamLeader",
            "DemandayQuality",
            "DemandayMIS",
            "DemandayContacts",
            "DemandayVerification",
            "EnquiryContacts",
            "DemandayTeleMarketingEnquiry",
            "DemandayTeleMarketingTeamLeader",
            "DemandayTeleMarketingQualilty",
            "DemandayTeleMarketingMIS",
            "DemandayTeleMarketingContacts"
        };

        // Both spellings exist in the schema across tables ("AdressLink" vs "AddressLink"),
        // so probe for each column individually before altering it.
        private static readonly string[] Columns = new[]
        {
            "ProfileLink",
            "CompanyLink",
            "RevenueLink",
            "AdressLink",
            "AddressLink",
            "Link"
        };

        public override void Up()
        {
            foreach (var table in Tables)
            {
                if (!Schema.Table(table).Exists())
                    continue;

                foreach (var column in Columns)
                {
                    if (Schema.Table(table).Column(column).Exists())
                    {
                        Alter.Table(table)
                            .AlterColumn(column).AsString(NewSize).Nullable();
                    }
                }
            }
        }

        public override void Down()
        {
            foreach (var table in Tables)
            {
                if (!Schema.Table(table).Exists())
                    continue;

                foreach (var column in Columns)
                {
                    if (Schema.Table(table).Column(column).Exists())
                    {
                        Alter.Table(table)
                            .AlterColumn(column).AsString(300).Nullable();
                    }
                }
            }
        }
    }
}
