using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251009154300)]
    public class DefaultDB_20251009_154300_AddtrackInventoryIntoInventory1 : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("Inventory").Column("TrackInventory").Exists())
            {
                Alter.Table("Inventory")
                .AddColumn("TrackInventory").AsBoolean().Nullable().WithDefaultValue(false)
                ;
            }
        }
        public override void Down()
        {
            Delete.Column("TrackInventory");
        }
    }
}