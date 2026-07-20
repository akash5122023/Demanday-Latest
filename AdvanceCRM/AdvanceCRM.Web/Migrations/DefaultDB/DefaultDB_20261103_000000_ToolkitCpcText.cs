using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    // CPC values in source sheets mix text and numbers (e.g. "CPC 1225358", "02 cpc", "$0.75").
    // TalCampaign.CPC was a bigint so anything non-numeric was rejected; widen both modules to
    // a roomy nvarchar. bigint -> nvarchar converts implicitly, so existing numbers are preserved.
    [Migration(20261103000000)]
    public class DefaultDB_20261103_000000_ToolkitCpcText : Migration
    {
        public override void Up()
        {
            Execute.Sql("ALTER TABLE [dbo].[TalCampaign] ALTER COLUMN [CPC] NVARCHAR(200) NULL;");
            Execute.Sql("ALTER TABLE [dbo].[DemandayCompetitor] ALTER COLUMN [CPC] NVARCHAR(200) NULL;");
        }

        public override void Down()
        {
            // Only numeric-looking values can go back to bigint; the rest would be lost.
            Execute.Sql("UPDATE [dbo].[TalCampaign] SET [CPC] = NULL WHERE ISNUMERIC([CPC]) = 0;");
            Execute.Sql("ALTER TABLE [dbo].[TalCampaign] ALTER COLUMN [CPC] BIGINT NULL;");
            Execute.Sql("ALTER TABLE [dbo].[DemandayCompetitor] ALTER COLUMN [CPC] NVARCHAR(50) NULL;");
        }
    }
}
