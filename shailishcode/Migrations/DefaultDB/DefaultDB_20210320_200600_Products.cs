using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210320200600)]
    public class DefaultDB_20210320_200600_Products : Migration
    {
        public override void Up()
        {
            Alter.Table("Products")
                .AddColumn("ExtendedWarranty").AsDouble().Nullable().WithDefaultValue(0)
                .AddColumn("RSA").AsDouble().Nullable().WithDefaultValue(0);
        }

        public override void Down()
        {
           

        }
    }
}