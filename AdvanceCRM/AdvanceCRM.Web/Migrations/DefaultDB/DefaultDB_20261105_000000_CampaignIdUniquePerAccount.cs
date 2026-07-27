using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    // A Campaign ID belongs to one Master Account, so the same number may be used by two different
    // accounts, but never twice inside one account — a duplicate splits that campaign's data across
    // two master rows and shows the user two identical entries in every campaign dropdown.
    //
    // Existing duplicates are merged rather than dropped: everything pointing at a duplicate is
    // repointed to the earliest row of that group first, and only then are the extra rows deleted.
    [Migration(20261105000000)]
    public class DefaultDB_20261105_000000_CampaignIdUniquePerAccount : Migration
    {
        public override void Up()
        {
            // Padding would otherwise hide a duplicate from the index (" 79580" vs "79580").
            Execute.Sql(@"
UPDATE [dbo].[DemandayCampaignId]
SET [CampaignId] = LTRIM(RTRIM([CampaignId]))
WHERE [CampaignId] IS NOT NULL AND [CampaignId] <> LTRIM(RTRIM([CampaignId]))");

            Execute.Sql(@"
-- Every duplicate row mapped to the row of its group that survives (the earliest one).
-- DemandayMasterAccountId may be null on legacy rows; SQL Server treats those nulls as equal in a
-- unique index, so they are grouped as one account here rather than skipped.
IF OBJECT_ID('tempdb..#CampaignDupes') IS NOT NULL DROP TABLE #CampaignDupes;

SELECT d.[Id] AS DupId, k.KeepId
INTO #CampaignDupes
FROM [dbo].[DemandayCampaignId] d
INNER JOIN (
    SELECT [DemandayMasterAccountId], [CampaignId], MIN([Id]) AS KeepId
    FROM [dbo].[DemandayCampaignId]
    WHERE [CampaignId] IS NOT NULL
    GROUP BY [DemandayMasterAccountId], [CampaignId]
    HAVING COUNT(*) > 1
) k ON k.[CampaignId] = d.[CampaignId]
   AND (k.[DemandayMasterAccountId] = d.[DemandayMasterAccountId]
        OR (k.[DemandayMasterAccountId] IS NULL AND d.[DemandayMasterAccountId] IS NULL))
WHERE d.[Id] <> k.KeepId;

DECLARE @sql nvarchar(max) = N'';

-- The tables that store a campaign's key, whether or not the database carries a real foreign key
-- for it. Each is skipped when the table or column is not present in this database.
DECLARE @refs TABLE (TableName sysname, ColName sysname);
INSERT INTO @refs (TableName, ColName) VALUES
    ('DemandaySpecs', 'CampaignId'),
    ('ClientSupression', 'CampaignId'),
    ('DemandayCompetitor', 'CampaignId'),
    ('TalCampaign', 'CampaignId'),
    ('MasterSupression', 'CampaignId'),
    ('OpenCampaign', 'CampaignId'),
    ('DNCContacts', 'CampaignId'),
    ('EmailTeam', 'CampaignId'),
    ('TeleMarketingEmailTeam', 'CampaignId'),
    ('DemandayTeleMarketingEnquiryCampaignQuestions', 'CampaignId'),
    ('DemandayTeleMarketingEnquiryQuestionAnswers', 'CampaignId');

SELECT @sql = @sql + N'UPDATE t SET t.[' + ColName + N'] = d.KeepId FROM [dbo].[' + TableName +
       N'] t INNER JOIN #CampaignDupes d ON d.DupId = t.[' + ColName + N'];' + CHAR(10)
FROM @refs
WHERE OBJECT_ID(N'[dbo].' + QUOTENAME(TableName), 'U') IS NOT NULL
  AND COL_LENGTH(N'[dbo].' + QUOTENAME(TableName), ColName) IS NOT NULL;

-- Anything else the database itself declares as referencing this table, so a module added later
-- (or a column named differently) is repointed too. Repeating a table here is harmless.
SELECT @sql = @sql + N'UPDATE t SET t.[' + c.[name] + N'] = d.KeepId FROM ' +
       QUOTENAME(SCHEMA_NAME(pt.[schema_id])) + N'.' + QUOTENAME(pt.[name]) +
       N' t INNER JOIN #CampaignDupes d ON d.DupId = t.[' + c.[name] + N'];' + CHAR(10)
FROM sys.foreign_keys fk
INNER JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.[object_id]
INNER JOIN sys.tables pt ON pt.[object_id] = fk.parent_object_id
INNER JOIN sys.columns c ON c.[object_id] = fkc.parent_object_id AND c.column_id = fkc.parent_column_id
WHERE fk.referenced_object_id = OBJECT_ID(N'[dbo].[DemandayCampaignId]', 'U');

IF LEN(@sql) > 0 EXEC sp_executesql @sql;

DELETE cid
FROM [dbo].[DemandayCampaignId] cid
INNER JOIN #CampaignDupes d ON d.DupId = cid.[Id];

DROP TABLE #CampaignDupes;");

            // Filtered, because CampaignId is nullable and an ordinary unique index would allow only
            // one campaign without a number per account.
            Execute.Sql(
                "CREATE UNIQUE INDEX [UX_DemandayCampaignId_Account_CampaignId] " +
                "ON [dbo].[DemandayCampaignId] ([DemandayMasterAccountId], [CampaignId]) " +
                "WHERE [CampaignId] IS NOT NULL");
        }

        public override void Down()
        {
            // The merged rows are gone for good; only the constraint is lifted.
            Execute.Sql("DROP INDEX IF EXISTS [UX_DemandayCampaignId_Account_CampaignId] " +
                "ON [dbo].[DemandayCampaignId]");
        }
    }
}
