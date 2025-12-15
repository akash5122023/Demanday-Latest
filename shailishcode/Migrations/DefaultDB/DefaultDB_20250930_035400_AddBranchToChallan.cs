using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250930035400)]
    public class DefaultDB_20250930_035400_AddBranchToChallan : Migration
    {
        public override void Up()
        {
            Alter.Table("ChallanProducts")
               .AddColumn("BranchId").AsInt32().NotNullable().WithDefaultValue(1); // or nullable if needed

            // Add foreign key to Branch table
            Create.ForeignKey("FK_ChallanProducts_BranchId")
                .FromTable("ChallanProducts").ForeignColumn("BranchId")
                .ToTable("Branch").PrimaryColumn("Id");
        }
        public override void Down()
        {
            Delete.ForeignKey("FK_ChallanProducts_BranchId").OnTable("ChallanProducts");
            Delete.Column("BranchId").FromTable("ChallanProducts");
        }
    }
}