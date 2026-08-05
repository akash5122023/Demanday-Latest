using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    /// <summary>
    /// ToolkitTMEnquiry.CampaignId was created as free text (copied verbatim from the
    /// TM Enquiry form, which stores the campaign's text value). Every other Tool Kit
    /// module keys its rows on DemandayCampaignId.Id, which is what the Verify Sheets
    /// page and the Account -> Campaign cascade filter by - so with a text column this
    /// sheet always came back empty. Convert it to the same numeric foreign key,
    /// carrying the existing text values over by matching them within their own account.
    /// </summary>
    [Migration(20261107000000)]
    public class DefaultDB_20261107_000000_ToolkitTMEnquiryCampaignFk : Migration
    {
        public override void Up()
        {
            Delete.Index("IX_ToolkitTMEnquiry_CampaignId").OnTable("ToolkitTMEnquiry");

            Rename.Column("CampaignId").OnTable("ToolkitTMEnquiry").To("CampaignIdText");

            Alter.Table("ToolkitTMEnquiry")
                .AddColumn("CampaignId").AsInt32().Nullable()
                    .ForeignKey("FK_ToolkitTMEnquiry_CampaignId", "dbo", "DemandayCampaignId", "Id");

            // Match on the account too: the same Campaign ID text may exist under more
            // than one Master Account, and a row must never be re-pointed at another
            // account's campaign.
            Execute.Sql(@"
UPDATE t
   SET t.[CampaignId] = c.[Id]
  FROM [dbo].[ToolkitTMEnquiry] t
 INNER JOIN [dbo].[DemandayCampaignId] c
    ON LTRIM(RTRIM(c.[CampaignId])) = LTRIM(RTRIM(t.[CampaignIdText]))
   AND c.[DemandayMasterAccountId] = t.[MasterAccountId]
 WHERE t.[CampaignIdText] IS NOT NULL;");

            Delete.Column("CampaignIdText").FromTable("ToolkitTMEnquiry");

            Create.Index("IX_ToolkitTMEnquiry_CampaignId")
                .OnTable("ToolkitTMEnquiry")
                .OnColumn("CampaignId")
                .Ascending();
        }

        public override void Down()
        {
            Delete.Index("IX_ToolkitTMEnquiry_CampaignId").OnTable("ToolkitTMEnquiry");
            Delete.ForeignKey("FK_ToolkitTMEnquiry_CampaignId").OnTable("ToolkitTMEnquiry");

            Rename.Column("CampaignId").OnTable("ToolkitTMEnquiry").To("CampaignRefId");

            Alter.Table("ToolkitTMEnquiry")
                .AddColumn("CampaignId").AsString(50).Nullable();

            Execute.Sql(@"
UPDATE t
   SET t.[CampaignId] = c.[CampaignId]
  FROM [dbo].[ToolkitTMEnquiry] t
 INNER JOIN [dbo].[DemandayCampaignId] c ON c.[Id] = t.[CampaignRefId];");

            Delete.Column("CampaignRefId").FromTable("ToolkitTMEnquiry");

            Create.Index("IX_ToolkitTMEnquiry_CampaignId")
                .OnTable("ToolkitTMEnquiry")
                .OnColumn("CampaignId")
                .Ascending();
        }
    }
}
