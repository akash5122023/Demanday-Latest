using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    // Admin loads the whole EnquiryContacts table (~1M rows, no OwnerId filter). The grid shows
    // OwnerUsername, so its List query LEFT JOINs Users; the derived COUNT(*) inherits that join.
    //
    // Making the FK trusted lets SQL Server eliminate that unnecessary LEFT JOIN from the COUNT
    // (the join adds nothing to a plain count), so COUNT(*) can run against the narrow
    // IX_EnquiryContacts_OwnerId index instead of joining 1M rows to Users. We only trust the FK
    // when there are no orphan rows, so this can never fail startup. Stats are refreshed so the
    // optimizer actually picks the narrow index for the count.
    [Migration(20260703121000)]
    public class DefaultDB_20260703_121000_EnquiryContactsTrustFkAndStats : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_EnquiryContacts_OwnerId_UserId')
   AND NOT EXISTS (
        SELECT 1
        FROM dbo.EnquiryContacts e
        WHERE e.OwnerId IS NOT NULL
          AND NOT EXISTS (SELECT 1 FROM dbo.Users u WHERE u.UserId = e.OwnerId))
BEGIN
    ALTER TABLE dbo.EnquiryContacts WITH CHECK CHECK CONSTRAINT FK_EnquiryContacts_OwnerId_UserId;
END;

UPDATE STATISTICS dbo.EnquiryContacts;
");
        }

        public override void Down()
        {
            // Non-reversible optimization; nothing to undo.
        }
    }
}
