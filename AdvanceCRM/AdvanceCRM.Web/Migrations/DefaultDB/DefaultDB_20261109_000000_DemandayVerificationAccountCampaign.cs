using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    /// <summary>
    /// Verification rows now carry a Master Account, so the module can be filtered, exported and
    /// imported by account / campaign like the rest of Demanday.
    ///
    /// CampaignID held the campaign's own number as typed into the sheet (79580), not a key. It
    /// becomes a foreign key to DemandayCampaignId - the same move ToolkitTMEnquiry made - because
    /// that is what the Account -> Campaign cascade filters on. Existing numbers are carried over
    /// where they identify exactly one campaign, and that campaign's account is filled in with
    /// them. A number used under more than one account cannot be resolved without knowing the
    /// account, so those rows are left empty for the user to pick rather than being linked to the
    /// wrong account's campaign.
    /// </summary>
    [Migration(20261109000000)]
    public class DefaultDB_20261109_000000_DemandayVerificationAccountCampaign : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("DemandayVerification").Exists())
                return;

            // Park the old numbers under a different name so the new key column can take the
            // CampaignId name - matching by value first, then dropping what is left.
            Rename.Column("CampaignID").OnTable("DemandayVerification").To("CampaignIdText");

            Alter.Table("DemandayVerification")
                .AddColumn("MasterAccountId").AsInt32().Nullable()
                    .ForeignKey("FK_DemandayVerification_MasterAccountId", "dbo", "DemandayMasterAccount", "Id")
                .AddColumn("CampaignId").AsInt32().Nullable()
                    .ForeignKey("FK_DemandayVerification_CampaignId", "dbo", "DemandayCampaignId", "Id");

            // Only codes that belong to a single campaign are carried over (Matches = 1); anything
            // shared between accounts stays empty rather than guessing an account.
            Execute.Sql(@"
WITH UniqueCampaign AS (
    SELECT LTRIM(RTRIM([CampaignId])) AS Code,
           MIN([Id]) AS CampaignKey,
           MIN([DemandayMasterAccountId]) AS AccountKey,
           COUNT(*) AS Matches
      FROM [dbo].[DemandayCampaignId]
     WHERE [CampaignId] IS NOT NULL
     GROUP BY LTRIM(RTRIM([CampaignId]))
)
UPDATE v
   SET v.[CampaignId] = u.CampaignKey,
       v.[MasterAccountId] = u.AccountKey
  FROM [dbo].[DemandayVerification] v
 INNER JOIN UniqueCampaign u
    ON u.Code = LTRIM(RTRIM(CONVERT(nvarchar(50), v.[CampaignIdText])))
 WHERE u.Matches = 1
   AND v.[CampaignIdText] IS NOT NULL;");

            Delete.Column("CampaignIdText").FromTable("DemandayVerification");

            Create.Index("IX_DemandayVerification_MasterAccountId")
                .OnTable("DemandayVerification").OnColumn("MasterAccountId").Ascending();
            Create.Index("IX_DemandayVerification_CampaignId")
                .OnTable("DemandayVerification").OnColumn("CampaignId").Ascending();
        }

        public override void Down()
        {
            if (!Schema.Table("DemandayVerification").Exists())
                return;

            Delete.Index("IX_DemandayVerification_CampaignId").OnTable("DemandayVerification");
            Delete.Index("IX_DemandayVerification_MasterAccountId").OnTable("DemandayVerification");
            Delete.ForeignKey("FK_DemandayVerification_CampaignId").OnTable("DemandayVerification");
            Delete.ForeignKey("FK_DemandayVerification_MasterAccountId").OnTable("DemandayVerification");

            Rename.Column("CampaignId").OnTable("DemandayVerification").To("CampaignRefId");

            Alter.Table("DemandayVerification")
                .AddColumn("CampaignID").AsInt32().Nullable();

            // Back to the plain number the column used to hold. A campaign code that is not a
            // whole number has no representation here, so it comes back empty.
            Execute.Sql(@"
UPDATE v
   SET v.[CampaignID] = TRY_CONVERT(int, c.[CampaignId])
  FROM [dbo].[DemandayVerification] v
 INNER JOIN [dbo].[DemandayCampaignId] c ON c.[Id] = v.[CampaignRefId];");

            Delete.Column("CampaignRefId").FromTable("DemandayVerification");
            Delete.Column("MasterAccountId").FromTable("DemandayVerification");
        }
    }
}
