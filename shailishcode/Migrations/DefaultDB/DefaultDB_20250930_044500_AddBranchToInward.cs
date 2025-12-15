using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250930044500)]
    public class DefaultDB_20250930_044500_AddBranchToInward : Migration
    {
        public override void Up()
        {
            Alter.Table("InwardProducts")
               .AddColumn("BranchId").AsInt32().NotNullable().WithDefaultValue(1); // or nullable if needed

            // Add foreign key to Branch table
            Create.ForeignKey("FK_InwardProducts_BranchId")
                .FromTable("InwardProducts").ForeignColumn("BranchId")
                .ToTable("Branch").PrimaryColumn("Id");
        }
        public override void Down()
        {
            Delete.ForeignKey("FK_InwardProducts_BranchId").OnTable("InwardProducts");
            Delete.Column("BranchId").FromTable("InwardProducts");
        }
    }
}