using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251013121200)]
    public class DefaultDB_20251013_121200_AddProductsIdToInventory : Migration
    {
        public override void Up()
        {
            Alter.Table("Inventory")
                  .AddColumn("ProductsId").AsInt32().Nullable();

            Create.ForeignKey("FK_Inventory_ProductsId")
           .FromTable("Inventory").ForeignColumn("ProductsId")
                  .ToTable("Products").PrimaryColumn("Id");
        }        
        public override void Down()
        {
            Delete.ForeignKey("FK_Inventory_ProductsId").OnTable("Inventory");
            Delete.Column("ProductsId").FromTable("Inventory");
        }
    }
}