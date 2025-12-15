using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210502183000)]
    public class DefaultDB_20210502_183000_ComAddtional : Migration
    {
        public override void Up()
        {
            Alter.Table("CompanyDetails")
                .AddColumn("CapacityInProducts").AsBoolean().Nullable()
                .AddColumn("YearInPrefix").AsInt32().Nullable();
        }

        public override void Down()
        {

        }
    }
}