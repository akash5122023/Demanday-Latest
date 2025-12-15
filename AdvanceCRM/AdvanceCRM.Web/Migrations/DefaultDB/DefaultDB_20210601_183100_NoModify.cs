using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210601183000)]
    public class DefaultDB_20210601_183000_NoModify : Migration
    {
        public override void Up()
        {
            Alter.Table("Quotation")
           .AddColumn("EnquiryN").AsString(100).NotNullable().WithDefaultValue(0);

            Alter.Table("Sales")          
           .AddColumn("QuotationN").AsString(100).NotNullable().WithDefaultValue(0);

            Alter.Table("Invoice")
          .AddColumn("QuotationN").AsString(100).NotNullable().WithDefaultValue(0);


        }

        public override void Down()
        {

        }
    }
}