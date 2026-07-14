using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    // Adds the "Exclude Company" column to the Tool Kit > Specification (DemandaySpecs) table.
    // It sits between Annual Revenue and Address on the import sheet / grid.
    [Migration(20261101200000)]
    public class DefaultDB_20261101_200000_DemandaySpecsExcludeCompany : Migration
    {
        public override void Up()
        {
            Alter.Table("DemandaySpecs")
                .AddColumn("ExcludeCompany").AsString(4000).Nullable();
        }

        public override void Down()
        {
            Delete.Column("ExcludeCompany").FromTable("DemandaySpecs");
        }
    }
}
