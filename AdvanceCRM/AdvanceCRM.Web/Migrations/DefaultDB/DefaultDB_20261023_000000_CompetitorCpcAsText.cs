using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20261023000000)]
    public class DefaultDB_20261023_000000_CompetitorCpcAsText : Migration
    {
        public override void Up()
        {
            // CPC is entered as free text in the source sheets ("02 cpc", "$0.75", "5"), which a
            // bigint column silently dropped on import. Store it verbatim instead.
            // bigint -> nvarchar converts implicitly, so existing numbers survive as their text form.
            Alter.Table("DemandayCompetitor")
                .AlterColumn("CPC").AsString(50).Nullable();
        }

        public override void Down()
        {
            // Anything that is no longer a whole number cannot go back into a bigint.
            Execute.Sql(
                "UPDATE [dbo].[DemandayCompetitor] SET [CPC] = NULL " +
                "WHERE [CPC] IS NOT NULL AND TRY_CONVERT(bigint, [CPC]) IS NULL");

            Alter.Table("DemandayCompetitor")
                .AlterColumn("CPC").AsInt64().Nullable();
        }
    }
}
