using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251007171800)]
    public class DefaultDB_20251007_171800_RemoveQuantityFromProducts : Migration
    {
        public override void Up()
        {
            if (Schema.Table("Products").Column("Quantity").Exists())
            {
                Delete.Column("Quantity").FromTable("Products");
            }
        }
        public override void Down()
        {
            Alter.Table("Products")
                .AddColumn("Quantity").AsDouble().Nullable().WithDefaultValue(0);
        }
    }
}