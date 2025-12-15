using FluentMigrator;
namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250930011700)]
    public class DefaultDB_20250930_011700_ChallanOutwardProducts : Migration
    {
        public override void Up()
        {

            Delete.ForeignKey("FK_ChallanProducts_ProductsId").OnTable("ChallanProducts");

            Create.ForeignKey("FK_ChallanProducts_ProductsId")
                .FromTable("ChallanProducts").ForeignColumn("ProductsId")
                .ToTable("Products").PrimaryColumn("Id");           

            Create.ForeignKey("FK_Outwardroducts_ProductsId").FromTable("OutwardProducts")
                .ForeignColumn("ProductsId").ToTable("Products").PrimaryColumn("Id");
        }
        public override void Down()
        {
            Delete.ForeignKey("FK_ChallanProducts_ProductsId").OnTable("ChallanProducts");

            Create.ForeignKey("FK_ChallanProducts_ProductsId")
                .FromTable("ChallanProducts").ForeignColumn("ProductsId")
                .ToTable("Products").PrimaryColumn("Id");

            Create.ForeignKey("FK_Outwardroducts_ProductsId").FromTable("OutwardProducts")
                .ForeignColumn("ProductsId").ToTable("Products").PrimaryColumn("Id");           

        }
    }
}