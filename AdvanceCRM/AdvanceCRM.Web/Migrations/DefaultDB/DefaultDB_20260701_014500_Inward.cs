using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701014500)]
    public class DefaultDB_20260701_014500_Inward : Migration
    {
        public override void Up()
        {
            if (Schema.Table("Inward").Exists())
                return;

            if (!Schema.Table("Outward").Exists())
                return;

            Execute.Sql(@"
                SELECT *
                INTO Inward
                FROM Outward
                WHERE 1 = 0;
            ");

            Execute.Sql(@"
                ALTER TABLE Inward
                ADD CONSTRAINT PK_Inward PRIMARY KEY (Id);
            ");
        }

        public override void Down()
        {
            if (Schema.Table("Inward").Exists())
                Delete.Table("Inward");
        }
    }
}
