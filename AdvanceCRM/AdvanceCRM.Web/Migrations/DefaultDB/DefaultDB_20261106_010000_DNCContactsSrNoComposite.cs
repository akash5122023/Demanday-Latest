using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20261106010000)]
    public class DefaultDB_20261106_010000_DNCContactsSrNoComposite : Migration
    {
        public override void Up()
        {
            // The original UX_DNCContacts_SrNo index was keyed on SrNo alone, which prevented the
            // same Sr No from being reused across different campaigns. DNC Contacts are scoped per
            // campaign (like MasterSuppression is scoped per Master Account), so the uniqueness
            // constraint must be (CampaignId, SrNo) — not SrNo alone.
            Execute.Sql(@"
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_DNCContacts_SrNo' AND object_id = OBJECT_ID('dbo.DNCContacts'))
    DROP INDEX [UX_DNCContacts_SrNo] ON [dbo].[DNCContacts];

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_DNCContacts_Campaign_SrNo' AND object_id = OBJECT_ID('dbo.DNCContacts'))
    CREATE UNIQUE INDEX [UX_DNCContacts_Campaign_SrNo]
        ON [dbo].[DNCContacts] ([CampaignId], [SrNo])
        WHERE [SrNo] IS NOT NULL AND [CampaignId] IS NOT NULL;
");
        }

        public override void Down()
        {
            Execute.Sql(@"
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_DNCContacts_Campaign_SrNo' AND object_id = OBJECT_ID('dbo.DNCContacts'))
    DROP INDEX [UX_DNCContacts_Campaign_SrNo] ON [dbo].[DNCContacts];

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_DNCContacts_SrNo' AND object_id = OBJECT_ID('dbo.DNCContacts'))
    CREATE UNIQUE INDEX [UX_DNCContacts_SrNo]
        ON [dbo].[DNCContacts] ([SrNo])
        WHERE [SrNo] IS NOT NULL;
");
        }
    }
}
