using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701015500)]
    public class DefaultDB_20260701_015500_InwardProductsBranch : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("InwardProducts").Column("BranchId").Exists())
            {
                Alter.Table("InwardProducts")
                    .AddColumn("BranchId").AsInt32().NotNullable().WithDefaultValue(1);

                Create.ForeignKey("FK_InwardProducts_BranchId")
                    .FromTable("InwardProducts").ForeignColumn("BranchId")
                    .ToTable("Branch").PrimaryColumn("Id");
            }
        }

        public override void Down()
        {
            if (Schema.Table("InwardProducts").Column("BranchId").Exists())
            {
                Delete.ForeignKey("FK_InwardProducts_BranchId").OnTable("InwardProducts");
                Delete.Column("BranchId").FromTable("InwardProducts");
            }
        }
    }
}
