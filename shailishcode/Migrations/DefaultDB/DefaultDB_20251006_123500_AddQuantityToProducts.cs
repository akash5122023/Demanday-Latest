using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251006123500)]
    public class DefaultDB_20251006_123500_AddQuantityToProducts : Migration
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