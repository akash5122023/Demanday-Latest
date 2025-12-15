using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20221007182900)]
    public class DefaultDB_20221007_182900_Products : Migration
    {
        public override void Up()
        {
            Alter.Table("Products")
                 .AlterColumn("Description").AsString(4000).Nullable();
            ;
        }
        
        public override void Down()
        {

        }
    }
}