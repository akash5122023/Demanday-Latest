using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251201090000)]
    public class DefaultDB_20251201_090000_ProductPlanBadges : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("ProductPlans").Column("BadgeLabel").Exists())
            {
                Alter.Table("ProductPlans")
                    .AddColumn("BadgeLabel").AsInt32().NotNullable().WithDefaultValue(0);
            }

            if (!Schema.Table("ProductPlans").Column("BadgeHighlight").Exists())
            {
                Alter.Table("ProductPlans")
                    .AddColumn("BadgeHighlight").AsBoolean().NotNullable().WithDefaultValue(false);
            }
        }

        public override void Down()
        {
            if (Schema.Table("ProductPlans").Column("BadgeHighlight").Exists())
                Delete.Column("BadgeHighlight").FromTable("ProductPlans");

            if (Schema.Table("ProductPlans").Column("BadgeLabel").Exists())
                Delete.Column("BadgeLabel").FromTable("ProductPlans");
        }
    }
}
