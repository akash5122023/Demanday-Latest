using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701023000)]
    public class DefaultDB_20260701_023000_AddBranchIdToInventory : Migration
    {
        public override void Up()
        {
            if (Schema.Table("Inventory").Exists() && !Schema.Table("Inventory").Column("BranchId").Exists())
            {
                Alter.Table("Inventory")
                    .AddColumn("BranchId").AsInt32().Nullable();

                if (Schema.Table("Branch").Exists())
                {
                    Create.ForeignKey("FK_Inventory_BranchId")
                        .FromTable("Inventory").ForeignColumn("BranchId")
                        .ToTable("Branch").PrimaryColumn("Id");
                }
            }
        }

        public override void Down()
        {
            if (Schema.Table("Inventory").Column("BranchId").Exists())
            {
                Delete.ForeignKey("FK_Inventory_BranchId").OnTable("Inventory");
                Delete.Column("BranchId").FromTable("Inventory");
            }
        }
    }
}
