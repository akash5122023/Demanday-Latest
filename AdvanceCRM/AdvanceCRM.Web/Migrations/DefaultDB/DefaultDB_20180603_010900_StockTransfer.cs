using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20180603010900)]
    public class DefaultDB_20180603_010900_StockTransfer : Migration
    {
        public override void Up()
        {

            Create.Table("StockTransfer")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("FromBranchId").AsInt32().NotNullable().ForeignKey("FK_StockTransfer_FromBranchId", "dbo", "Branch", "Id")
                .WithColumn("ToBranchId").AsInt32().NotNullable().ForeignKey("FK_StockTransfer_ToBranchId", "dbo", "Branch", "Id")
                .WithColumn("TotalQty").AsDouble().Nullable()
                .WithColumn("Amount").AsDouble().Nullable()
                .WithColumn("AdditionalInfo").AsString(2000).Nullable()
                .WithColumn("RepresentativeId").AsInt32().NotNullable().ForeignKey("FK_StockTransfer_UserId", "dbo", "Users", "UserId")
                ;

            Create.Table("StockTransferProducts")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsId").AsInt32().NotNullable().ForeignKey("FK_StockTransferProducts_ProductsId", "dbo", "Products", "Id")
                .WithColumn("Quantity").AsDouble().NotNullable().WithDefaultValue(1)
                .WithColumn("TransferPrice").AsDouble().Nullable()
                .WithColumn("TaxType1").AsString(100).Nullable()
                .WithColumn("Percentage1").AsDouble().Nullable()
                .WithColumn("TaxType2").AsString(100).Nullable()
                .WithColumn("Percentage2").AsDouble().Nullable()
                .WithColumn("StockTransferId").AsInt32().NotNullable().ForeignKey("FK_StockTransfer_Id", "dbo", "StockTransfer", "Id")
                ;
        }

        public override void Down()
        {

        }
    }
}