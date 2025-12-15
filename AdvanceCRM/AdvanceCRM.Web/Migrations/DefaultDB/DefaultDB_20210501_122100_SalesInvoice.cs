using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210501122100)]
    public class DefaultDB_20210501_122100_SalesInvoice : Migration
    {
        public override void Up()
        {
            Alter.Table("Sales")
                  .AddColumn("PurchaseOrderDate").AsDateTime().Nullable();
            Alter.Table("Invoice")
                 .AddColumn("PurchaseOrderDate").AsDateTime().Nullable();


        }

        public override void Down()
        {

        }
    }
}