using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250930012400)]
    public class DefaultDB_20250930_012400_Inward : Migration
    {
        public override void Up()
        {            
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
            Delete.Table("Inward");
        }
    }
}