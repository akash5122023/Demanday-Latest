using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210501162100)]
    public class DefaultDB_20210501_162100_QuoDiv : Migration
    {
        public override void Up()
        {
            Alter.Table("QuotationProducts")
                  .AlterColumn("ProductsDivision").AsString(200).Nullable();
           


        }

        public override void Down()
        {

        }
    }
}