using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251001120000)]
    public class DefaultDB_20251001_120000_ProductPlans : Migration
    {
        public override void Up()
        {
            Create.Table("ProductPlans")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("PricePerUser").AsDecimal(18, 2).NotNullable()
                .WithColumn("TrialDays").AsInt32().NotNullable().WithDefaultValue(15)
                .WithColumn("UserLimit").AsInt32().NotNullable().WithDefaultValue(3)
                .WithColumn("Currency").AsString(10).NotNullable().WithDefaultValue("INR")
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("SortOrder").AsInt32().Nullable()
                .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("CreatedBy").AsString(50).Nullable()
                .WithColumn("ModifiedOn").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString(50).Nullable();

            Create.Index("IX_ProductPlans_Name")
                .OnTable("ProductPlans")
                .OnColumn("Name").Ascending().WithOptions().Unique();

            Insert.IntoTable("ProductPlans").Row(new
            {
                Name = "Starter ERP",
                PricePerUser = 1000m,
                TrialDays = 15,
                UserLimit = 3,
                Currency = "INR",
                IsActive = true,
                SortOrder = 1,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "system"
            });

            Insert.IntoTable("ProductPlans").Row(new
            {
                Name = "ERP + Basic Accounting",
                PricePerUser = 1800m,
                TrialDays = 15,
                UserLimit = 3,
                Currency = "INR",
                IsActive = true,
                SortOrder = 2,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "system"
            });

            Insert.IntoTable("ProductPlans").Row(new
            {
                Name = "BizPlus All-in-One",
                PricePerUser = 2500m,
                TrialDays = 15,
                UserLimit = 3,
                Currency = "INR",
                IsActive = true,
                SortOrder = 3,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "system"
            });
        }

        public override void Down()
        {
            Delete.Table("ProductPlans");
        }
    }
}
