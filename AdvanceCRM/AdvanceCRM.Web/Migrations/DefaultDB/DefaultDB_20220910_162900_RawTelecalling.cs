using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220910_162900)]
    public class DefaultDB_20220910_162900_RawTelecalling : Migration
    {
        public override void Up()
        {
            Alter.Table("RawTelecall")             
                
                .AlterColumn("Phone").AsString(20).Nullable()
                ;
        }

        public override void Down()
        {

        }
    }
}