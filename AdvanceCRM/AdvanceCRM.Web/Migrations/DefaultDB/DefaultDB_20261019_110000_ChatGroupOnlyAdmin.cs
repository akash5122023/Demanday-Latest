using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20261019110000)]
    public class DefaultDB_20261019_110000_ChatGroupOnlyAdmin : Migration
    {
        public override void Up()
        {
            if (Schema.Table("ChatGroup").Exists() &&
                !Schema.Table("ChatGroup").Column("OnlyAdminsCanSend").Exists())
            {
                Alter.Table("ChatGroup")
                    .AddColumn("OnlyAdminsCanSend").AsBoolean().NotNullable().WithDefaultValue(0);
            }
        }

        public override void Down()
        {
            if (Schema.Table("ChatGroup").Column("OnlyAdminsCanSend").Exists())
                Delete.Column("OnlyAdminsCanSend").FromTable("ChatGroup");
        }
    }
}
