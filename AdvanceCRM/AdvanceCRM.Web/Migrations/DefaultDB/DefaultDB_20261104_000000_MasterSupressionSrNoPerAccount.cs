using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    // Master Suppression is imported account-wise, so a SrNo only means something inside one
    // Master Account: every account numbers its own rows from 1. The table-wide unique index
    // created by DefaultDB_20261024_000000_ToolkitSrNo made the second account's "1" collide
    // with the first account's "1", so it is replaced with a composite index.
    //
    // Only MasterSupression moves to per-account numbering; the other Tool Kit tables in that
    // migration keep their table-wide index.
    [Migration(20261104000000)]
    public class DefaultDB_20261104_000000_MasterSupressionSrNoPerAccount : Migration
    {
        public override void Up()
        {
            Execute.Sql("DROP INDEX IF EXISTS [UX_MasterSupression_SrNo] ON [dbo].[MasterSupression]");

            // The old table-wide index made same-account duplicates impossible, but a database
            // that never received it (or was edited by hand) can still hold them, and then
            // CREATE UNIQUE INDEX below would fail. Keep the earliest row of each clash and push
            // the rest past that account's highest SrNo. MasterAccountId may be null on legacy
            // rows; SQL Server treats those nulls as equal in a unique index, so they are grouped
            // as one account here rather than skipped.
            Execute.Sql(@"
;WITH clashing AS (
    SELECT [Id], [MasterAccountId],
           ROW_NUMBER() OVER (PARTITION BY [MasterAccountId], [SrNo] ORDER BY [Id]) AS rn
    FROM [dbo].[MasterSupression]
    WHERE [SrNo] IS NOT NULL
),
extras AS (
    SELECT [Id], [MasterAccountId],
           ROW_NUMBER() OVER (PARTITION BY [MasterAccountId] ORDER BY [Id]) AS seq
    FROM clashing
    WHERE rn > 1
)
UPDATE t
SET t.[SrNo] = e.seq + (
        SELECT ISNULL(MAX(m.[SrNo]), 0)
        FROM [dbo].[MasterSupression] m
        WHERE m.[SrNo] IS NOT NULL
          AND (m.[MasterAccountId] = e.[MasterAccountId]
               OR (m.[MasterAccountId] IS NULL AND e.[MasterAccountId] IS NULL))
    )
FROM [dbo].[MasterSupression] t
INNER JOIN extras e ON e.[Id] = t.[Id]");

            // Filtered, because SrNo is nullable and an ordinary unique index would allow only
            // one null row per account.
            Execute.Sql(
                "CREATE UNIQUE INDEX [UX_MasterSupression_MasterAccountId_SrNo] " +
                "ON [dbo].[MasterSupression] ([MasterAccountId], [SrNo]) " +
                "WHERE [SrNo] IS NOT NULL");
        }

        public override void Down()
        {
            Execute.Sql("DROP INDEX IF EXISTS [UX_MasterSupression_MasterAccountId_SrNo] " +
                "ON [dbo].[MasterSupression]");

            // Per-account numbering repeats across accounts, so the table-wide index cannot be
            // restored until every SrNo is unique again.
            Execute.Sql(@"
;WITH renumbered AS (
    SELECT [Id], ROW_NUMBER() OVER (ORDER BY [Id]) AS rn
    FROM [dbo].[MasterSupression]
    WHERE [SrNo] IS NOT NULL
)
UPDATE t
SET t.[SrNo] = r.rn
FROM [dbo].[MasterSupression] t
INNER JOIN renumbered r ON r.[Id] = t.[Id]");

            Execute.Sql(
                "CREATE UNIQUE INDEX [UX_MasterSupression_SrNo] ON [dbo].[MasterSupression] ([SrNo]) " +
                "WHERE [SrNo] IS NOT NULL");
        }
    }
}
