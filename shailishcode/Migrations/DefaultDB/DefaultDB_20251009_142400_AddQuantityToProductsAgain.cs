using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251009142400)]
    public class DefaultDB_20251009_142400_AddQuantityToProductsAgain : Migration
    {
        public override void Up()
        {
            Alter.Table("Products")
                 .AddColumn("Quantity").AsDouble().Nullable().WithDefaultValue(0);
        }
        public override void Down()
        {
            Delete.Column("Quantity").FromTable("Products");
        }
    }
}