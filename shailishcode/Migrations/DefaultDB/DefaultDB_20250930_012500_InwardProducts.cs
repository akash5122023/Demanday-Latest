using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250930012500)]
    public class DefaultDB_20250930_012500_InwardProducts : Migration
    {
        public override void Up()
        {
            Alter.Table("Inward")
                .AddColumn("OutwardId").AsInt32().Nullable();
            
            Execute.Sql(@"
                SELECT *
                INTO InwardProducts
                FROM OutwardProducts
                WHERE 1 = 0;
            ");            
            Execute.Sql(@"
                ALTER TABLE InwardProducts
                ADD CONSTRAINT PK_InwardProducts PRIMARY KEY (Id);
            ");
            Rename.Column("OutwardId").OnTable("InwardProducts").To("InwardId");
        }
        public override void Down()
        {
            Delete.Table("OutwardProducts");
        }
    }
}
