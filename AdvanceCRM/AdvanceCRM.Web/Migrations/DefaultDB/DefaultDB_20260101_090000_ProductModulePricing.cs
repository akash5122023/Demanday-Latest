using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260101090000)]
    public class DefaultDB_20260101_090000_ProductModulePricing : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("ProductModules").Column("Price").Exists())
            {
                Alter.Table("ProductModules")
                    .AddColumn("Price").AsDecimal(18, 2).Nullable();
            }

            if (!Schema.Table("ProductModules").Column("Currency").Exists())
            {
                Alter.Table("ProductModules")
                    .AddColumn("Currency").AsString(16).Nullable();
            }
        }

        public override void Down()
        {
            if (Schema.Table("ProductModules").Column("Currency").Exists())
            {
                Delete.Column("Currency").FromTable("ProductModules");
            }

            if (Schema.Table("ProductModules").Column("Price").Exists())
            {
                Delete.Column("Price").FromTable("ProductModules");
            }
        }
    }
}
