using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220927162900)]
    public class DefaultDB_20220927_162900_Wati : Migration
    {
        public override void Up()
        {
            Create.Table("WatiConfig")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("URL").AsString(200).Nullable()
                .WithColumn("Token").AsString(1000).Nullable();
        }
        
        public override void Down()
        {

        }
    }
}