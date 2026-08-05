using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    /// <summary>
    /// ToolkitTMEnquiry rows are now written when a TM Enquiry is moved to Team Leader, and that
    /// move deletes the enquiry. A NOT NULL foreign key to DemandayTeleMarketingEnquiry therefore
    /// cannot survive - it would block the very delete that creates the row. The link moves to the
    /// Team Leader record, which is what the enquiry becomes and what actually lives on.
    /// Existing values are enquiry keys under the old meaning, so they are cleared rather than
    /// silently re-read as Team Leader keys.
    /// </summary>
    [Migration(20261108000000)]
    public class DefaultDB_20261108_000000_ToolkitTMEnquiryTeamLeaderLink : Migration
    {
        public override void Up()
        {
            Delete.Index("IX_ToolkitTMEnquiry_DemandayTeleMarketingEnquiryId").OnTable("ToolkitTMEnquiry");
            Delete.ForeignKey("FK_ToolkitTMEnquiry_DemandayTeleMarketingEnquiryId").OnTable("ToolkitTMEnquiry");

            Rename.Column("DemandayTeleMarketingEnquiryId").OnTable("ToolkitTMEnquiry").To("TeamLeaderId");

            Alter.Column("TeamLeaderId").OnTable("ToolkitTMEnquiry").AsInt32().Nullable();

            Execute.Sql("UPDATE [dbo].[ToolkitTMEnquiry] SET [TeamLeaderId] = NULL;");

            Create.ForeignKey("FK_ToolkitTMEnquiry_TeamLeaderId")
                .FromTable("ToolkitTMEnquiry").ForeignColumn("TeamLeaderId")
                .ToTable("DemandayTeleMarketingTeamLeader").PrimaryColumn("Id");

            Create.Index("IX_ToolkitTMEnquiry_TeamLeaderId")
                .OnTable("ToolkitTMEnquiry")
                .OnColumn("TeamLeaderId")
                .Ascending();
        }

        public override void Down()
        {
            Delete.Index("IX_ToolkitTMEnquiry_TeamLeaderId").OnTable("ToolkitTMEnquiry");
            Delete.ForeignKey("FK_ToolkitTMEnquiry_TeamLeaderId").OnTable("ToolkitTMEnquiry");

            // Any surviving rows have no enquiry to point back at, so the column cannot be made
            // NOT NULL again; it is restored as a nullable reference only.
            Rename.Column("TeamLeaderId").OnTable("ToolkitTMEnquiry").To("DemandayTeleMarketingEnquiryId");

            Create.Index("IX_ToolkitTMEnquiry_DemandayTeleMarketingEnquiryId")
                .OnTable("ToolkitTMEnquiry")
                .OnColumn("DemandayTeleMarketingEnquiryId")
                .Ascending();
        }
    }
}
