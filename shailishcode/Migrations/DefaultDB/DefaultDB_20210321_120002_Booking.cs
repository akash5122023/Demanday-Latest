using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210321120002)]
    public class DefaultDB_20210321_120002_Booking : Migration
    {
        public override void Up()
        {
            this.CreateTableIfNotExists("BookingProducts", table => table
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_BPProducts_ProductsId", "dbo", "Products", "Id")
                .WithColumn("Quantity").AsDouble().NotNullable().WithDefaultValue(1)
                .WithColumn("MRP").AsDouble()
                .WithColumn("SellingPrice").AsDouble()
                .WithColumn("Price").AsDouble()
                .WithColumn("Discount").AsDouble().WithDefaultValue(0)
                .WithColumn("BookingId").AsInt32().NotNullable().ForeignKey("FK_BPBooking_BookingId", "dbo", "Booking", "Id")
                .WithColumn("Description").AsString(2000).Nullable()
                .WithColumn("Hypothecation").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("Accessories").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("RoadSideAssistance").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("AMC").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("ExtendedWarranty").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("Others").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("Concession").AsDouble().WithDefaultValue(0));

            this.CreateTableIfNotExists("BookingFollowups", table => table
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("FollowupNote").AsString(200).NotNullable()
                .WithColumn("Details").AsString(2000)
                .WithColumn("FollowupDate").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("BookingId").AsInt32().NotNullable().ForeignKey("FK_BFBooking_BookingId", "dbo", "Booking", "Id")
                .WithColumn("RepresentativeId").AsInt32().Nullable().ForeignKey("BK_BookingFollowups_UserId", "dbo", "Users", "UserId"));

        }

        public override void Down()
        {

        }
    }
}