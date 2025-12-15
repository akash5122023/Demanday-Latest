using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250530120000)]
    public class DefaultDB_20250530_120000_CouponCodes : Migration
    {
        public override void Up()
        {
            Create.Table("CouponCodes")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Code").AsString(50).NotNullable()
                .WithColumn("DiscountType").AsString(10).NotNullable()
                .WithColumn("DiscountValue").AsDecimal(18, 2).NotNullable()
                .WithColumn("MaxUsageCount").AsInt32().Nullable()
                .WithColumn("UsedCount").AsInt32().NotNullable().WithDefaultValue(0)
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("ExpiryDate").AsDateTime().Nullable();

            Create.Index("UX_CouponCodes_Code")
                .OnTable("CouponCodes")
                .OnColumn("Code").Unique();


            Execute.Sql("ALTER TABLE CouponCodes ADD CONSTRAINT CK_CouponCodes_DiscountType CHECK (DiscountType IN ('Flat', 'Percent'));");
        }

        public override void Down()
        {
            Execute.Sql("ALTER TABLE CouponCodes DROP CONSTRAINT CK_CouponCodes_DiscountType;");
            Delete.Table("CouponCodes");

        }
    }
}
