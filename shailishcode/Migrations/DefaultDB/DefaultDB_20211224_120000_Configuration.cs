using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20211224120000)]
    public class DefaultDB_20211224_120000_Configuration : Migration
    {
        public override void Up()
        {
            Create.Table("BulkMailConfig")
               .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Host").AsString(200).Nullable()
               .WithColumn("Port").AsInt32().Nullable()
               .WithColumn("SSL").AsBoolean().Nullable()
               .WithColumn("EmailId").AsString(200).Nullable()
               .WithColumn("EmailPassword").AsString(200).Nullable()
           ;
           
        }

        public override void Down()
        {

        }
    }
}