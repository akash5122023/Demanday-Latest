using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20251006010200)]
    public class DefaultDB_20251006_010200_AddBranchToProducts : Migration
    {
        public override void Up()
        {
            Alter.Table("Products")
                 .AddColumn("BranchId").AsInt32().Nullable().WithDefaultValue(1)
                 .ForeignKey("FK_Products_BranchId", "Branch", "Id");
        }
        public override void Down()
        {
            Delete.Column("BranchId").FromTable("Products");
        }
    }
}