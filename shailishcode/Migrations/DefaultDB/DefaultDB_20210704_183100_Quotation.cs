using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210704183100)]
    public class DefaultDB_20210704_183100_Quotation : Migration
    {
        public override void Up()
        {
            Alter.Table("CompanyDetails").AlterColumn("QuotationTotal").AsBoolean().Nullable().WithDefaultValue(true);
        }

        public override void Down()
        {

        }
    }
}