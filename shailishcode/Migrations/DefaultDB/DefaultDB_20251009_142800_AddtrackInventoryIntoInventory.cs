using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251009142800)]
    public class DefaultDB_20251009_142800_AddtrackInventoryIntoInventory : Migration
    {
        public override void Up()
        {           
            Alter.Table("Products")                
                .AddColumn("TrackInventory").AsBoolean().Nullable().WithDefaultValue(false)
                ;
        }
        public override void Down()
        {
            Delete.Column("TrackInventory");
        }
    }
}