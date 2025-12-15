using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220518110000)]
    public class DefaultDB_20220518_110000_WooComOrder : AutoReversingMigration
    {
        public override void Up()
        {


            Create.Table("WCOrderDetails").WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("WCOID").AsString(255).Nullable()
                .WithColumn("ParentID").AsString(255).Nullable()
                .WithColumn("Status").AsString(255).Nullable()
                .WithColumn("Currency").AsString(255).Nullable()
                .WithColumn("IncludedTax").AsString(255).Nullable()
                .WithColumn("DateCreated").AsString(255).Nullable()
                .WithColumn("DateModified").AsString(255).Nullable()
                .WithColumn("DiscountTotal").AsString(255).Nullable()
                .WithColumn("DiscountTax").AsString(255).Nullable()
                .WithColumn("ShippingTotal").AsString(255).Nullable()
                .WithColumn("ShipppingTax").AsString(255).Nullable()
                .WithColumn("CartTax").AsString(255).Nullable()
                .WithColumn("Total").AsString(255).Nullable()
                .WithColumn("TotalTax").AsString(255).Nullable()
                .WithColumn("CustomerID").AsString(255).Nullable()
                .WithColumn("OrderKey").AsString(255).Nullable()
                .WithColumn("BFirstName").AsString(255).Nullable()
                .WithColumn("BLastName").AsString(255).Nullable()
                .WithColumn("BCompany").AsString(255).Nullable()
               .WithColumn("BEmail").AsString(50).Nullable()
                .WithColumn("BPhone").AsString(20).Nullable()
                .WithColumn("BPostCode").AsString(20).Nullable()
                .WithColumn("BAddress1").AsString(500).Nullable()
                .WithColumn("BAddress2").AsString(500).Nullable()
                .WithColumn("BCity").AsString(255).Nullable()
                .WithColumn("BState").AsString(255).Nullable()
                 .WithColumn("BCountry").AsString(255).Nullable()
                  .WithColumn("SFirstName").AsString(255).Nullable()
                .WithColumn("SLastName").AsString(255).Nullable()
                .WithColumn("SCompany").AsString(255).Nullable()
                .WithColumn("SPhone").AsString(20).Nullable()
                .WithColumn("SPostCode").AsString(20).Nullable()
                .WithColumn("SAddress1").AsString(500).Nullable()
                .WithColumn("SAddress2").AsString(500).Nullable()
                .WithColumn("SCity").AsString(255).Nullable()
                .WithColumn("SState").AsString(255).Nullable()
                 .WithColumn("SCountry").AsString(255).Nullable()
                .WithColumn("PaymentMethod").AsString(255).Nullable()
                .WithColumn("TransactionID").AsString(255).Nullable()
                .WithColumn("CustomerIP").AsString(255).Nullable()
               .WithColumn("ItemId").AsString(50).Nullable()
                .WithColumn("ItemName").AsString(2000).Nullable()
                .WithColumn("ProductId").AsString(20).Nullable()
                .WithColumn("VariationId").AsString(50).Nullable()
                .WithColumn("Quantity").AsString(50).Nullable()
                .WithColumn("TaxClass").AsString(50).Nullable()
                .WithColumn("SubTotal").AsString(50).Nullable()
                 .WithColumn("SubTotaltax").AsString(50).Nullable()
                 .WithColumn("ItemTotal").AsString(50).Nullable()
                 .WithColumn("ItemTotaltax").AsString(50).Nullable()
                 .WithColumn("TaxRateCode").AsString(50).Nullable()
                 .WithColumn("TaxRateId").AsString(50).Nullable()
                 .WithColumn("TaxLabel").AsString(50).Nullable()
                 .WithColumn("TaxCompound").AsString(50).Nullable()
                 .WithColumn("TaxTotal").AsString(50).Nullable()
                 .WithColumn("TaxShippingTotal").AsString(50).Nullable()
                 .WithColumn("TaxRatePercent").AsString(50).Nullable()
                .WithColumn("IsMoved").AsBoolean().WithDefaultValue(false);


            
        }
    }
}