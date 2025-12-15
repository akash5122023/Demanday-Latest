using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20180409150200)]
    public class DefaultDB_20180409_150200_PurchaseOrder : Migration
    {
        public override void Up()
        {

            Create.Table("PurchaseOrder")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ContactsId").AsInt32().NotNullable().ForeignKey("FK_PurchaseOrder_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("Total").AsDouble().Nullable()
                .WithColumn("Description").AsString(2000).Nullable()
                .WithColumn("AdditionalInfo").AsString(2000).Nullable()
                .WithColumn("SourceId").AsInt32().Nullable().ForeignKey("FK_PurchaseOrder_SourceId", "dbo", "Source", "Id")
                .WithColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_PurchaseOrder_BranchId", "dbo", "Branch", "Id")
                .WithColumn("Terms").AsString(2000).Nullable()
                .WithColumn("OwnerId").AsInt32().Nullable().ForeignKey("FK_PurchaseOrder_OUserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedId").AsInt32().Nullable().ForeignKey("FK_PurchaseOrder_AUserId", "dbo", "Users", "UserId")
                ;
            
            Create.Table("PurchaseOrderProducts")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_PurchaseOrderProducts_ProductsId", "dbo", "Products", "Id")
                .WithColumn("Quantity").AsDouble().NotNullable().WithDefaultValue(1)
                .WithColumn("Price").AsDouble()
                .WithColumn("Discount").AsDouble().WithDefaultValue(0)
                .WithColumn("DiscountAmount").AsDouble().WithDefaultValue(0)
                .WithColumn("PurchaseOrderId").AsInt32().NotNullable().ForeignKey("FK_PurchaseOrderProducts_PurchaseOrderId", "dbo", "PurchaseOrder", "Id")
                .WithColumn("TaxType1").AsString(100).Nullable()
                .WithColumn("Percentage1").AsDouble().Nullable()
                .WithColumn("TaxType2").AsString(100).Nullable()
                .WithColumn("Percentage2").AsDouble().Nullable()
                .WithColumn("Unit").AsString(128).Nullable()
            ;
        }

        public override void Down()
        {

        }
    }
}