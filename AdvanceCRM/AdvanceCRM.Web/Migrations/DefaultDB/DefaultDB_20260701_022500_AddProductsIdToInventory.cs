using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701022500)]
    public class DefaultDB_20260701_022500_AddProductsIdToInventory : Migration
    {
        public override void Up()
        {
            if (Schema.Table("Inventory").Exists() && !Schema.Table("Inventory").Column("ProductsId").Exists())
            {
                Alter.Table("Inventory")
                    .AddColumn("ProductsId").AsInt32().Nullable();

                Create.ForeignKey("FK_Inventory_ProductsId")
                    .FromTable("Inventory").ForeignColumn("ProductsId")
                    .ToTable("Products").PrimaryColumn("Id");
            }
        }

        public override void Down()
        {
            if (Schema.Table("Inventory").Column("ProductsId").Exists())
            {
                Delete.ForeignKey("FK_Inventory_ProductsId").OnTable("Inventory");
                Delete.Column("ProductsId").FromTable("Inventory");
            }
        }
    }
}
