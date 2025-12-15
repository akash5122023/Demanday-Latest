using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210601183100)]
    public class DefaultDB_20210601_183100_NoModify : Migration
    {
        public override void Up()
        {
            Alter.Table("Quotation")
           .AlterColumn("EnquiryN").AsString(100).Nullable().WithDefaultValue(0);

            Alter.Table("Sales")          
           .AlterColumn("QuotationN").AsString(100).Nullable().WithDefaultValue(0);

            Alter.Table("Invoice")
          .AlterColumn("QuotationN").AsString(100).Nullable().WithDefaultValue(0);


        }

        public override void Down()
        {

        }
    }
}