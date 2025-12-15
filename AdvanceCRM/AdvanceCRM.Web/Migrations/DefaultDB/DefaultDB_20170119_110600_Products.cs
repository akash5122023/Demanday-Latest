using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170119110600)]
    public class DefaultDB_20170119_110600_Products : Migration
    {
        public override void Up()
        {
            Create.Table("Products")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(200).NotNullable()
                .WithColumn("Code").AsString(100).Nullable()
                .WithColumn("DivisionId").AsInt32().NotNullable().ForeignKey("FK_PDivision_DivisionId", "dbo", "ProductsDivision", "Id")
                .WithColumn("GroupId").AsInt32().Nullable().ForeignKey("FK_PGroup_GroupId", "dbo", "ProductsGroup", "Id")
                .WithColumn("SellingPrice").AsDouble().NotNullable().WithDefaultValue(0)
                .WithColumn("MRP").AsDouble().NotNullable().WithDefaultValue(0)
                .WithColumn("Description").AsString(800).NotNullable()
                .WithColumn("TaxId1").AsInt32().Nullable().ForeignKey("FK_PTax1_TaxId", "dbo", "Tax", "Id")
                .WithColumn("TaxId2").AsInt32().Nullable().ForeignKey("FK_PTax2_TaxId", "dbo", "Tax", "Id")
                .WithColumn("Image").AsString(500).Nullable()
                .WithColumn("TechSpecs").AsString(2000).Nullable()
                .WithColumn("HSN").AsString(100).Nullable()
                .WithColumn("ChannelCustomerPrice").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("ResellerPrice").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("WholesalerPrice").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("DealerPrice").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("DistributorPrice").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("StockiestPrice").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("NationalDistributorPrice").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("MinimumStock").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("MaximumStock").AsDouble().Nullable().WithDefaultValue(0)
                .WithColumn("RawMaterial").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("PurchasePrice").AsDouble().NotNullable().WithDefaultValue(0)
                .WithColumn("OpeningStock").AsDouble().NotNullable().WithDefaultValue(0)
                .WithColumn("UnitId").AsInt32().Nullable().ForeignKey("FK_PUnit_UnitId", "dbo", "ProductsUnit", "Id")
                ;
        }

        public override void Down()
        {

        }
    }
}