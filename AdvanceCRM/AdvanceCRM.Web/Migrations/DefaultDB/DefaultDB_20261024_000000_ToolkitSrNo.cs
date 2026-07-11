using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    // Adds a user-facing serial number (SrNo) to every Tool Kit sub-module table. It is the
    // upsert key on import: a row whose SrNo already exists is updated instead of duplicated.
    // SrNo is globally unique per table but nullable, so a filtered unique index is used
    // (SQL Server allows only one NULL in an ordinary unique index; the filter lifts that).
    [Migration(20261024000000)]
    public class DefaultDB_20261024_000000_ToolkitSrNo : Migration
    {
        private static readonly string[] Tables =
        {
            "DemandaySpecs",
            "ClientSupression",
            "MasterSupression",
            "DemandayCompetitor",
            "TalCampaign",
            "OpenCampaign"
        };

        public override void Up()
        {
            foreach (var table in Tables)
            {
                Alter.Table(table).AddColumn("SrNo").AsInt32().Nullable();

                Execute.Sql(
                    $"CREATE UNIQUE INDEX [UX_{table}_SrNo] ON [dbo].[{table}] ([SrNo]) " +
                    "WHERE [SrNo] IS NOT NULL");
            }
        }

        public override void Down()
        {
            foreach (var table in Tables)
            {
                Execute.Sql($"DROP INDEX IF EXISTS [UX_{table}_SrNo] ON [dbo].[{table}]");
                Delete.Column("SrNo").FromTable(table);
            }
        }
    }
}
