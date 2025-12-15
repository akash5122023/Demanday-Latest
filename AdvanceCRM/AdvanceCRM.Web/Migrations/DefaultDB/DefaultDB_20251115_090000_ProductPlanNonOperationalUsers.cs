using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251115090000)]
    public class DefaultDB_20251115_090000_ProductPlanNonOperationalUsers : Migration
    {
        public override void Up()
        {
            Alter.Table("ProductPlans")
                .AddColumn("NonOperationalUsers").AsInt32().NotNullable().WithDefaultValue(0);
        }

        public override void Down()
        {
            Delete.Column("NonOperationalUsers").FromTable("ProductPlans");
        }
    }
}
