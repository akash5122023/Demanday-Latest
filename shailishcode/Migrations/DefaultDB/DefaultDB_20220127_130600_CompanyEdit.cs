
using AdvanceCRM.Administration;
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using Serenity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220127130600)]
    public class DefaultDB_20220127_130600_CompanyEdit : Migration
    {

        public override void Up()
        {

            Alter.Table("CompanyDetails")
             .AddColumn("EnqStartNo").AsInt32().Nullable()
             .AddColumn("EnqEditNo").AsBoolean().WithDefaultValue(false)
             .AddColumn("QuoStartNo").AsInt32().Nullable()
             .AddColumn("QuoEditNo").AsBoolean().WithDefaultValue(false)
             .AddColumn("InvStartNo").AsInt32().Nullable()
             .AddColumn("InvEditNo").AsBoolean().WithDefaultValue(false)
             .AddColumn("DCStartNo").AsInt32().Nullable()
             .AddColumn("DCEditNo").AsBoolean().WithDefaultValue(false)
             .AddColumn("DealerInEnquiry").AsBoolean().WithDefaultValue(false)
             .AddColumn("DealerInQuotation").AsBoolean().WithDefaultValue(false)
             .AddColumn("DealerInSales").AsBoolean().WithDefaultValue(false)
             .AddColumn("DealerInInvoice").AsBoolean().WithDefaultValue(false)            
             .AddColumn("ProjectInCMS").AsBoolean().WithDefaultValue(false);

            Alter.Table("PurchaseOrder")
          .AddColumn("Lines").AsInt32().Nullable();

            Alter.Table("Enquiry")
             .AddColumn("DealerId").AsInt32().Nullable().ForeignKey("FK_EDealer_DealerId", "dbo", "Dealer", "Id");

            Alter.Table("Quotation")
            .AddColumn("DealerId").AsInt32().Nullable().ForeignKey("FK_QDealer_DealerId", "dbo", "Dealer", "Id");

            Alter.Table("Sales")
            .AddColumn("DealerId").AsInt32().Nullable().ForeignKey("FK_SDealer_DealerId", "dbo", "Dealer", "Id");

            Alter.Table("Invoice")
            .AddColumn("DealerId").AsInt32().Nullable().ForeignKey("FK_IDealer_DealerId", "dbo", "Dealer", "Id");

        }

        public override void Down()
        {
        }
    }
}