using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260625105000)]
    public class DefaultDB_20260625_105000_ChatMessageAttachment : Migration
    {
        public override void Up()
        {
            if (Schema.Table("ChatMessage").Exists() &&
                !Schema.Table("ChatMessage").Column("AttachmentPath").Exists())
            {
                Alter.Table("ChatMessage")
                    .AddColumn("AttachmentPath").AsString(500).Nullable()
                    .AddColumn("AttachmentName").AsString(255).Nullable()
                    .AddColumn("AttachmentType").AsString(20).Nullable();
            }
        }

        public override void Down()
        {
            if (Schema.Table("ChatMessage").Column("AttachmentPath").Exists())
            {
                Delete.Column("AttachmentPath")
                    .Column("AttachmentName")
                    .Column("AttachmentType")
                    .FromTable("ChatMessage");
            }
        }
    }
}
