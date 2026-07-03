using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    // EnquiryContacts (ET Contacts) holds ~1M rows. A non-admin user's grid load filters by
    // WHERE OwnerId = <user>. There was a foreign key on OwnerId but no index backing it, so
    // every load scanned the whole table. A nonclustered index on OwnerId turns that into a
    // seek and makes per-user loads fast.
    [Migration(20260703120000)]
    public class DefaultDB_20260703_120000_EnquiryContactsOwnerIdIndex : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("EnquiryContacts").Index("IX_EnquiryContacts_OwnerId").Exists())
            {
                Create.Index("IX_EnquiryContacts_OwnerId")
                    .OnTable("EnquiryContacts")
                    .OnColumn("OwnerId").Ascending();
            }
        }

        public override void Down()
        {
            if (Schema.Table("EnquiryContacts").Index("IX_EnquiryContacts_OwnerId").Exists())
            {
                Delete.Index("IX_EnquiryContacts_OwnerId").OnTable("EnquiryContacts");
            }
        }
    }
}
