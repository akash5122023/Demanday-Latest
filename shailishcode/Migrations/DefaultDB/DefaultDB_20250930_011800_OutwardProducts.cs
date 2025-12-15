using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250930011800)]
    public class DefaultDB_20250930_011800_OutwardProducts : Migration
    {
        public override void Up()
        {
            Rename.Column("ChallanId").OnTable("OutwardProducts").To("OutwardId");
        }

        public override void Down()
        {
            Rename.Column("OutwardId").OnTable("OutwardProducts").To("ChallanId");
        }
    }
}
