using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250930011600)]
    public class DefaultDB_20250930_011600_OutwardProducts : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"
                SELECT *
                INTO OutwardProducts
                FROM ChallanProducts
                WHERE 1 = 0;
            ");
            Execute.Sql(@"
                ALTER TABLE OutwardProducts
                ADD CONSTRAINT PK_OutwardProducts PRIMARY KEY (Id);
            ");
        }
        public override void Down()
        {
            Delete.Table("OutwardProducts");
        }
    }
}
