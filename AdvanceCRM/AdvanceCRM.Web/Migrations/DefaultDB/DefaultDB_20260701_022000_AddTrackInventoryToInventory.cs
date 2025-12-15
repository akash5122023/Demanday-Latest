using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701022000)]
    public class DefaultDB_20260701_022000_AddTrackInventoryToInventory : Migration
    {
        public override void Up()
        {
            if (Schema.Table("Inventory").Exists() && !Schema.Table("Inventory").Column("TrackInventory").Exists())
            {
                Alter.Table("Inventory")
                    .AddColumn("TrackInventory").AsBoolean().Nullable().WithDefaultValue(false);
            }
        }

        public override void Down()
        {
            if (Schema.Table("Inventory").Column("TrackInventory").Exists())
                Delete.Column("TrackInventory").FromTable("Inventory");
        }
    }
}
