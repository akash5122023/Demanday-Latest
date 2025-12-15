using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250930044000)]
    public class DefaultDB_20250930_044000_AddBranchToChallan : Migration
    {
        public override void Up()
        {
            Alter.Table("OutwardProducts")
               .AddColumn("BranchId").AsInt32().NotNullable().WithDefaultValue(1); // or nullable if needed

            // Add foreign key to Branch table
            Create.ForeignKey("FK_OutwardProducts_BranchId")
                .FromTable("OutwardProducts").ForeignColumn("BranchId")
                .ToTable("Branch").PrimaryColumn("Id");
        }
        public override void Down()
        {
            Delete.ForeignKey("FK_OutwardProducts_BranchId").OnTable("OutwardProducts");
            Delete.Column("BranchId").FromTable("OutwardProducts");
        }
    }
}