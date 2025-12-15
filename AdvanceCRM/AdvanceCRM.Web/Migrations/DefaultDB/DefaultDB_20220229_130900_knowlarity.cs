using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220229130900)]
    public class DefaultDB_20220229_130900_knowlarity : Migration
    {
        public override void Up()
        {
            Alter.Table("KnowlarityDetails")
                .AlterColumn("CMIUID").AsString(100).Nullable();
        }

        public override void Down()
        {

        }
    }
}