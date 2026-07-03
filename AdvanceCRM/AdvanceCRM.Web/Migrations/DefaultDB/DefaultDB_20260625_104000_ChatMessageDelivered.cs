using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260625104000)]
    public class DefaultDB_20260625_104000_ChatMessageDelivered : Migration
    {
        public override void Up()
        {
            if (Schema.Table("ChatMessage").Exists() &&
                !Schema.Table("ChatMessage").Column("IsDelivered").Exists())
            {
                Alter.Table("ChatMessage")
                    .AddColumn("IsDelivered").AsBoolean().NotNullable().WithDefaultValue(0);

                // Existing messages that were already read are obviously delivered too.
                Execute.Sql("UPDATE [dbo].[ChatMessage] SET [IsDelivered] = 1 WHERE [IsRead] = 1");
            }
        }

        public override void Down()
        {
            if (Schema.Table("ChatMessage").Column("IsDelivered").Exists())
                Delete.Column("IsDelivered").FromTable("ChatMessage");
        }
    }
}
