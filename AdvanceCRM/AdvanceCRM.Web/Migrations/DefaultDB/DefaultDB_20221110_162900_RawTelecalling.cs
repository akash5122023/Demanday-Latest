using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20221110162900)]
    public class DefaultDB_20221110_162900_RawTelecalling : Migration
    {
        public override void Up()
        {
            Alter.Table("RawTelecall")
                .AddColumn("Feedback").AsString(5000).Nullable()
                ;
        }

        public override void Down()
        {

        }
    }
}