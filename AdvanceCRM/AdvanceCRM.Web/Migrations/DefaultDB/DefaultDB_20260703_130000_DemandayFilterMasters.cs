using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    // Master tables that back the DemandayContacts filter dropdowns. Each holds the manageable list
    // of values for one field (Country, Job Level, Job Function, Sub Industry, Employee Size).
    // Seeded once from the distinct values already present in DemandayContacts (and the fixed
    // employee-size buckets), after which admins maintain them from the Masters menu.
    [Migration(20260703130000)]
    public class DefaultDB_20260703_130000_DemandayFilterMasters : Migration
    {
        private static readonly string[] Tables =
        {
            "DemandayCountryMaster",
            "DemandayJobLevelMaster",
            "DemandayJobFunctionMaster",
            "DemandaySubIndustryMaster",
            "DemandayEmployeeSizeMaster"
        };

        public override void Up()
        {
            foreach (var t in Tables)
            {
                if (!Schema.Table(t).Exists())
                {
                    Create.Table(t)
                        .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                        .WithColumn("Name").AsString(200).Nullable();
                }
            }

            // Seed from the distinct values already in DemandayContacts.
            // NB: some columns are mapped to different physical names ([Column(...)] on the row).
            SeedFromColumn("DemandayCountryMaster", "Country");
            SeedFromColumn("DemandayJobLevelMaster", "Job_Level");
            SeedFromColumn("DemandayJobFunctionMaster", "Job_Function_Role");
            SeedFromColumn("DemandaySubIndustryMaster", "SubIndustry");

            // Employee size: fixed buckets.
            foreach (var v in new[] { "0-1", "2-10", "11-50", "51-200", "201-500",
                                      "501-1000", "1001-5000", "5001-10000", "10001+" })
            {
                Execute.Sql(
                    "IF NOT EXISTS (SELECT 1 FROM dbo.DemandayEmployeeSizeMaster WHERE Name = N'" + v + "') " +
                    "INSERT INTO dbo.DemandayEmployeeSizeMaster (Name) VALUES (N'" + v + "');");
            }
        }

        private void SeedFromColumn(string table, string sourceColumn)
        {
            Execute.Sql(
                "INSERT INTO dbo.[" + table + "] (Name) " +
                "SELECT DISTINCT LTRIM(RTRIM(s.[" + sourceColumn + "])) " +
                "FROM dbo.DemandayContacts s " +
                "WHERE s.[" + sourceColumn + "] IS NOT NULL " +
                "  AND LTRIM(RTRIM(s.[" + sourceColumn + "])) <> '' " +
                "  AND NOT EXISTS (SELECT 1 FROM dbo.[" + table + "] m " +
                "                  WHERE m.Name = LTRIM(RTRIM(s.[" + sourceColumn + "])));");
        }

        public override void Down()
        {
            foreach (var t in Tables)
            {
                if (Schema.Table(t).Exists())
                    Delete.Table(t);
            }
        }
    }
}
