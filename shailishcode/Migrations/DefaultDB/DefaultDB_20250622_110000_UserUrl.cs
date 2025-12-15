using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250622110000)]
    public class DefaultDB_20250622_110000_UserUrl : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("Url").AsString(300).Nullable();
        }
    }
}
