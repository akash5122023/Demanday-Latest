using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20201107183400)]
    public class DefaultDB_20201107_183400_IdentityColumnInPrint : Migration
    {

        public override void Up()
        {
            Alter.Table("Quotation").AddColumn("QuotationNo").AsInt32().Nullable();
            Alter.Table("Invoice").AddColumn("InvoiceNo").AsInt32().Nullable();
            Alter.Table("Challan").AddColumn("ChallanNo").AsInt32().Nullable();
            Alter.Table("PurchaseOrder").AddColumn("PurchaseOrderNo").AsInt32().Nullable();
            Alter.Table("AMC").AddColumn("AMCNo").AsInt32().Nullable();
            Alter.Table("CMS").AddColumn("CMSNo").AsInt32().Nullable();

            Execute.Sql("UPDATE Quotation SET QuotationNo = Id WHERE QuotationNo IS NULL");
            Execute.Sql("UPDATE Invoice SET InvoiceNo = Id WHERE InvoiceNo IS NULL");
            Execute.Sql("UPDATE Challan SET ChallanNo = Id WHERE ChallanNo IS NULL");
            Execute.Sql("UPDATE PurchaseOrder SET PurchaseOrderNo = Id WHERE PurchaseOrderNo IS NULL");
            Execute.Sql("UPDATE AMC SET AMCNo = Id WHERE AMCNo IS NULL");
            Execute.Sql("UPDATE CMS SET CMSNo = Id WHERE CMSNo IS NULL");

            if (Schema.Table("SalesReturnProducts").Constraint("FK_SalesReturnProducts_SalesId").Exists())
            {
                Delete.ForeignKey("FK_SalesReturnProducts_SalesId").OnTable("SalesReturnProducts");
                Create.ForeignKey("FK_SalesReturnProducts_SalesReturnId")
                    .FromTable("SalesReturnProducts").ForeignColumn("SalesReturnId")
                    .ToTable("SalesReturn").PrimaryColumn("Id");
            }
        }
        public override void Down()
        {
        }
    }
}