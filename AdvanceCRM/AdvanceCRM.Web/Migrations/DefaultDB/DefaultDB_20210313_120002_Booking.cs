using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210313120002)]
    public class DefaultDB_20210313_120002_Booking : Migration
    {
        public override void Up()
        {
            Create.Table("Financier").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("FinancierName").AsString(200).NotNullable();

            Create.Table("Booking")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_BContacts_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("Type").AsInt32().Nullable()
                .WithColumn("PurchaseType").AsInt32().NotNullable()
                .WithColumn("FinancierId").AsInt32().ForeignKey("FK_Financier_FinancierId","dbo","Financier","Id")
                .WithColumn("FinanceType").AsInt32().Nullable()
                .WithColumn("RecieptNo").AsString(500).Nullable()
                .WithColumn("HiriseBookingNo").AsString(500).Nullable()
                .WithColumn("AdditionalInfo").AsString(5000).Nullable()
                 .WithColumn("ExpectedDeliveryDate").AsDateTime().Nullable()
                .WithColumn("SourceId").AsInt32().NotNullable().ForeignKey("FK_BSource_SourceId", "dbo", "Source", "Id")
                .WithColumn("StageId").AsInt32().NotNullable().ForeignKey("FK_BStage_StageId", "dbo", "Stage", "Id")
                .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_BBranch_BranchId", "dbo", "Branch", "Id")
                .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_BOUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_BAUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("ReferenceName").AsString(100).Nullable()
                .WithColumn("ReferencePhone").AsString(50).Nullable()
                .WithColumn("ClosingType").AsInt32().Nullable()
                .WithColumn("LostReason").AsString(450).Nullable()
                .WithColumn("ClosingDate").AsDateTime().Nullable()
                .WithColumn("ContactPersonId").AsInt32().Nullable().ForeignKey("FK_Booking_SubContactsId", "dbo", "SubContacts", "Id")
                ;

            Create.Table("BookingProducts").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
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
               .WithColumn("Concession").AsDouble().WithDefaultValue(0);
            ;

            Create.Table("BookingFollowups")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("FollowupNote").AsString(200).NotNullable()
                .WithColumn("Details").AsString(2000)
                .WithColumn("FollowupDate").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("BookingId").AsInt32().NotNullable().ForeignKey("FK_BFBooking_BookingId", "dbo", "Booking", "Id")
                .WithColumn("RepresentativeId").AsInt32().Nullable().ForeignKey("BK_BookingFollowups_UserId", "dbo", "Users", "UserId")
            ;

        }

        public override void Down()
        {

        }
    }
}