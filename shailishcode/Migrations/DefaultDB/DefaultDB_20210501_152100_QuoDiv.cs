using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210501152100)]
    public class DefaultDB_20210501_152100_QuoDiv : Migration
    {
        public override void Up()
        {
            Alter.Table("QuotationProducts")
                  .AddColumn("ProductsDivision").AsDateTime().Nullable();
           


        }

        public override void Down()
        {

        }
    }
}