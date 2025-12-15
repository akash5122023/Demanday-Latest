using AdvanceCRM.Migrations;
using FluentMigrator;
using System.Data;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251002120000)]
    public class DefaultDB_20251002_120000_DefaultFeatures : Migration
    {
        public override void Up()
        {
            var productPlansExists = Schema.Table("ProductPlans").Exists();

            var defaultFeaturesExists = Schema.Table("DefaultFeatures").Exists();
            if (!defaultFeaturesExists)
            {
                this.CreateTableIfNotExists("DefaultFeatures", table => table
                    .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                    .WithColumn("Name").AsString(150).NotNullable());

                Create.Index("IX_DefaultFeatures_Name")
                    .OnTable("DefaultFeatures")
                    .OnColumn("Name").Ascending().WithOptions().Unique();

                SeedDefaultFeatures();
            }
            else
            {
                if (!Schema.Table("DefaultFeatures").Index("IX_DefaultFeatures_Name").Exists())
                {
                    Create.Index("IX_DefaultFeatures_Name")
                        .OnTable("DefaultFeatures")
                        .OnColumn("Name").Ascending().WithOptions().Unique();
                }

                SeedDefaultFeatures();
            }

            var planFeaturesExists = Schema.Table("ProductPlanDefaultFeatures").Exists();
            if (!planFeaturesExists)
            {
                this.CreateTableIfNotExists("ProductPlanDefaultFeatures", table => table
                    .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                    .WithColumn("PlanId").AsInt32().NotNullable()
                    .WithColumn("FeatureId").AsInt32().NotNullable());

                Create.Index("UQ_ProductPlanDefaultFeatures_Plan_Feature")
                    .OnTable("ProductPlanDefaultFeatures")
                    .OnColumn("PlanId").Ascending()
                    .OnColumn("FeatureId").Ascending()
                    .WithOptions().Unique();
            }
            else if (!Schema.Table("ProductPlanDefaultFeatures").Index("UQ_ProductPlanDefaultFeatures_Plan_Feature").Exists())
            {
                Create.Index("UQ_ProductPlanDefaultFeatures_Plan_Feature")
                    .OnTable("ProductPlanDefaultFeatures")
                    .OnColumn("PlanId").Ascending()
                    .OnColumn("FeatureId").Ascending()
                    .WithOptions().Unique();
            }

            if (!Schema.Table("ProductPlanDefaultFeatures").Constraint("FK_ProductPlanDefaultFeatures_FeatureId_DefaultFeatures_Id").Exists())
            {
                Create.ForeignKey("FK_ProductPlanDefaultFeatures_FeatureId_DefaultFeatures_Id")
                    .FromTable("ProductPlanDefaultFeatures").ForeignColumn("FeatureId")
                    .ToTable("DefaultFeatures").PrimaryColumn("Id")
                    .OnDelete(Rule.Cascade);
            }

            if (productPlansExists && !Schema.Table("ProductPlanDefaultFeatures").Constraint("FK_ProductPlanDefaultFeatures_PlanId_ProductPlans_Id").Exists())
            {
                Create.ForeignKey("FK_ProductPlanDefaultFeatures_PlanId_ProductPlans_Id")
                    .FromTable("ProductPlanDefaultFeatures").ForeignColumn("PlanId")
                    .ToTable("ProductPlans").PrimaryColumn("Id")
                    .OnDelete(Rule.Cascade);
            }
        }

        private void SeedDefaultFeatures()
        {
            Execute.Sql(@"
IF NOT EXISTS (SELECT 1 FROM DefaultFeatures WHERE Name = 'Facebook')
    INSERT INTO DefaultFeatures (Name) VALUES ('Facebook');

IF NOT EXISTS (SELECT 1 FROM DefaultFeatures WHERE Name = 'WhatsApp')
    INSERT INTO DefaultFeatures (Name) VALUES ('WhatsApp');

IF NOT EXISTS (SELECT 1 FROM DefaultFeatures WHERE Name = 'Email')
    INSERT INTO DefaultFeatures (Name) VALUES ('Email');
");
        }

        public override void Down()
        {
            if (Schema.Table("ProductPlanDefaultFeatures").Exists())
            {
                if (Schema.Table("ProductPlanDefaultFeatures").Constraint("FK_ProductPlanDefaultFeatures_PlanId_ProductPlans_Id").Exists())
                    Delete.ForeignKey("FK_ProductPlanDefaultFeatures_PlanId_ProductPlans_Id").OnTable("ProductPlanDefaultFeatures");

                if (Schema.Table("ProductPlanDefaultFeatures").Constraint("FK_ProductPlanDefaultFeatures_FeatureId_DefaultFeatures_Id").Exists())
                    Delete.ForeignKey("FK_ProductPlanDefaultFeatures_FeatureId_DefaultFeatures_Id").OnTable("ProductPlanDefaultFeatures");
                Delete.Table("ProductPlanDefaultFeatures");
            }

            if (Schema.Table("DefaultFeatures").Exists())
                Delete.Table("DefaultFeatures");
        }
    }
}
