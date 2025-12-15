using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701012000)]
    public class DefaultDB_20260701_012000_Outward : Migration
    {
        public override void Up()
        {
            if (Schema.Table("Outward").Exists())
                return;

            if (!Schema.Table("Challan").Exists())
                return;

            Execute.Sql(@"
                SELECT *
                INTO Outward
                FROM Challan
                WHERE 1 = 0;
            ");

            Execute.Sql(@"
                ALTER TABLE Outward
                ADD CONSTRAINT PK_Outward PRIMARY KEY (Id);
            ");
        }

        public override void Down()
        {
            if (Schema.Table("Outward").Exists())
                Delete.Table("Outward");
        }
    }
}
