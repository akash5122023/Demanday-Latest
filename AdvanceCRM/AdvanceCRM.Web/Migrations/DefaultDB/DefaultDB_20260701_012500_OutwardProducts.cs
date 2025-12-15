using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701012500)]
    public class DefaultDB_20260701_012500_OutwardProducts : Migration
    {
        public override void Up()
        {
            if (Schema.Table("OutwardProducts").Exists())
                return;

            if (!Schema.Table("ChallanProducts").Exists())
                return;

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
            if (Schema.Table("OutwardProducts").Exists())
                Delete.Table("OutwardProducts");
        }
    }
}
