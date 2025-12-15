using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210505183000)]
    public class DefaultDB_20210505_183000_InvoiceSales : Migration
    {
        public override void Up()
        {
           
            Alter.Table("Sales")          
           .AddColumn("InvoiceN").AsString(100).NotNullable().WithDefaultValue(0);

            Alter.Table("Invoice")
          .AddColumn("InvoiceN").AsString(100).NotNullable().WithDefaultValue(0);


        }

        public override void Down()
        {

        }
    }
}