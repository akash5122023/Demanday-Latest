using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701014000)]
    public class DefaultDB_20260701_014000_OutwardProductsBranch : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("OutwardProducts").Column("BranchId").Exists())
            {
                Alter.Table("OutwardProducts")
                    .AddColumn("BranchId").AsInt32().NotNullable().WithDefaultValue(1);

                Create.ForeignKey("FK_OutwardProducts_BranchId")
                    .FromTable("OutwardProducts").ForeignColumn("BranchId")
                    .ToTable("Branch").PrimaryColumn("Id");
            }
        }

        public override void Down()
        {
            if (Schema.Table("OutwardProducts").Column("BranchId").Exists())
            {
                Delete.ForeignKey("FK_OutwardProducts_BranchId").OnTable("OutwardProducts");
                Delete.Column("BranchId").FromTable("OutwardProducts");
            }
        }
    }
}
