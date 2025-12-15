using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210315230000)]
    public class DefaultDB_20210315_230000_IndiaMartConfig : Migration
    {
        public override void Up()
        {
            

            Alter.Table("IndiaMART")
                .AddColumn("SMobileNumber").AsString(20).Nullable()
                .AddColumn("SAPIKey").AsString(100).Nullable()
                ;

            

            
        }

        public override void Down()
        {

        }
    }
}