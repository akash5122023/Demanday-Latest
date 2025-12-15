using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250930011500)]
    public class DefaultDB_20250930_011500_Outward : Migration
    {
        public override void Up()
        {            
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
            Delete.Table("Outward");
        }
    }
}