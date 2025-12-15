using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220929162900)]
    public class DefaultDB_20220929_162900_WatiContacts : Migration
    {
        public override void Up()
        {
            Create.Table("WatiContacts")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("WAID").AsString(50).Nullable()
                .WithColumn("FirtName").AsString(50).Nullable()
                .WithColumn("FullName").AsString(50).Nullable()
                .WithColumn("Phone").AsString(50).Nullable()
                .WithColumn("Source").AsString(50).Nullable()
                .WithColumn("Status").AsString(50).Nullable()
                .WithColumn("Created").AsDate().Nullable()
                ;
        }
        
        public override void Down()
        {

        }
    }
}