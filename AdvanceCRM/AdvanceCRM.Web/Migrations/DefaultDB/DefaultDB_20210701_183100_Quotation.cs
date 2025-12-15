using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210701183100)]
    public class DefaultDB_20210701_183100_Quotation : Migration
    {
        public override void Up()
        {
            Alter.Table("CompanyDetails")
                .AddColumn("QuotationTotal").AsBoolean().WithDefaultValue(true);
        }

        public override void Down()
        {

        }
    }
}