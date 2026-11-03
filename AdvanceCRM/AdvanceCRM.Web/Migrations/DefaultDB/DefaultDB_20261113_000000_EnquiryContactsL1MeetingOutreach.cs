using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    /// <summary>
    /// L1 Meeting section (meeting mode, status comments, follow-up date; the existing L1Meeting
    /// column is the Date of Meeting) and Mode of Outreach on RPC data. Both modes are dropdown masters.
    /// </summary>
    [Migration(20261113000000)]
    public class DefaultDB_20261113_000000_EnquiryContactsL1MeetingOutreach : Migration
    {
        private static readonly string[] Masters = { "ContactMeetingModeMaster", "ContactOutreachModeMaster" };

        public override void Up()
        {
            foreach (var t in Masters)
            {
                if (!Schema.Table(t).Exists())
                {
                    Create.Table(t)
                        .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                        .WithColumn("Name").AsString(200).Nullable();
                }
            }

            Seed("ContactMeetingModeMaster", "Virtual Meeting", "In-person Meeting");
            Seed("ContactOutreachModeMaster", "Digital", "Phone");

            Alter.Table("EnquiryContacts")
                .AddColumn("MeetingModeId").AsInt32().Nullable()
                    .ForeignKey("FK_EnquiryContacts_MeetingMode", "ContactMeetingModeMaster", "Id")
                .AddColumn("OutreachModeId").AsInt32().Nullable()
                    .ForeignKey("FK_EnquiryContacts_OutreachMode", "ContactOutreachModeMaster", "Id")
                .AddColumn("MeetingStatus").AsString(2000).Nullable()
                .AddColumn("FollowUpDate").AsDateTime().Nullable();
        }

        private void Seed(string table, params string[] values)
        {
            foreach (var v in values)
            {
                Execute.Sql(
                    "IF NOT EXISTS (SELECT 1 FROM dbo." + table + " WHERE Name = N'" + v + "') " +
                    "INSERT INTO dbo." + table + " (Name) VALUES (N'" + v + "');");
            }
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_EnquiryContacts_MeetingMode").OnTable("EnquiryContacts");
            Delete.ForeignKey("FK_EnquiryContacts_OutreachMode").OnTable("EnquiryContacts");
            Delete.Column("MeetingModeId").FromTable("EnquiryContacts");
            Delete.Column("OutreachModeId").FromTable("EnquiryContacts");
            Delete.Column("MeetingStatus").FromTable("EnquiryContacts");
            Delete.Column("FollowUpDate").FromTable("EnquiryContacts");
            foreach (var t in Masters)
                Delete.Table(t);
        }
    }
}
