using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170118215200)]
    public class DefaultDB_20170118_215200_ProductsMaster : Migration
    {
        public override void Up()
        {
            Create.Table("ProductsDivision")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsDivision").AsString(200).NotNullable();

            Create.Table("ProductsGroup")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsGroup").AsString(200).NotNullable();

            Create.Table("Tax")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Type").AsString(100).NotNullable()
                .WithColumn("Percentage").AsDouble().NotNullable();

            Create.Table("ProductsUnit").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ProductsUnit").AsString(128).NotNullable();
        }

        public override void Down()
        {

        }
    }
}