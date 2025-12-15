using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220929182900)]
    public class DefaultDB_20220929_182900_WatiContacts : Migration
    {
        public override void Up()
        {
            Alter.Table("WatiContacts")
                 .AddColumn("IsMoved").AsBoolean().WithDefaultValue(false);
            ;
        }
        
        public override void Down()
        {

        }
    }
}