using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701021000)]
    public class DefaultDB_20260701_021000_AddTrackInventoryToProducts : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("Products").Column("TrackInventory").Exists())
            {
                Alter.Table("Products")
                    .AddColumn("TrackInventory").AsBoolean().Nullable().WithDefaultValue(false);
            }
        }

        public override void Down()
        {
            if (Schema.Table("Products").Column("TrackInventory").Exists())
                Delete.Column("TrackInventory").FromTable("Products");
        }
    }
}
