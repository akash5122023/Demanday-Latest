using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701013000)]
    public class DefaultDB_20260701_013000_OutwardProductRename : Migration
    {
        public override void Up()
        {
            if (Schema.Table("OutwardProducts").Column("ChallanId").Exists())
            {
                Rename.Column("ChallanId").OnTable("OutwardProducts").To("OutwardId");
            }
        }

        public override void Down()
        {
            if (Schema.Table("OutwardProducts").Column("OutwardId").Exists())
            {
                Rename.Column("OutwardId").OnTable("OutwardProducts").To("ChallanId");
            }
        }
    }
}
