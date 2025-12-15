using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250405105200)]
    public class DefaultDB_20250405_105200_POTerms : Migration
    {
        public override void Up()
        {

            Create.Table("PurchaseOrderTerms").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("TermsId").AsInt32().NotNullable().ForeignKey("FK_POQuotationTermsMaster_QuotationTermsMastersId", "dbo", "QuotationTermsMaster", "Id")
                .WithColumn("PurchaseOrderId").AsInt32().NotNullable().ForeignKey("FK_POPurchaseOrder_PurchaseOrderId", "dbo", "PurchaseOrder", "Id")
                ;
        }

        public override void Down()
        {

        }
    }
}