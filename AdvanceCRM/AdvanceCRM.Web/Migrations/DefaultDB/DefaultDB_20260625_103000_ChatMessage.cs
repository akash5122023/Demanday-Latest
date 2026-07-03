using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260625103000)]
    public class DefaultDB_20260625_103000_ChatMessage : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("ChatMessage").Exists())
            {
                Create.Table("ChatMessage")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("SenderId").AsInt32().NotNullable().ForeignKey("FK_ChatMessage_SenderId_UserId", "dbo", "Users", "UserId")
                    .WithColumn("ReceiverId").AsInt32().NotNullable().ForeignKey("FK_ChatMessage_ReceiverId_UserId", "dbo", "Users", "UserId")
                    .WithColumn("Message").AsCustom("nvarchar(max)").Nullable()
                    .WithColumn("SentDate").AsDateTime().Nullable()
                    .WithColumn("IsRead").AsBoolean().NotNullable().WithDefaultValue(0);

                Create.Index("IX_ChatMessage_Receiver_Read")
                    .OnTable("ChatMessage")
                    .OnColumn("ReceiverId").Ascending()
                    .OnColumn("IsRead").Ascending();
            }
        }

        public override void Down()
        {
            Delete.Table("ChatMessage");
        }
    }
}
