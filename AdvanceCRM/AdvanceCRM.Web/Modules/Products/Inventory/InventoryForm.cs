using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Products.Forms
{
    [FormScript("Products.Inventory")]
    [BasedOnRow(typeof(InventoryRow), CheckNames = true)]
    public class InventoryForm
    {
        [Tab("General")]
        public String Name { get; set; }
        [HalfWidth]
        public String Code { get; set; }
        [HalfWidth]
        public String Hsn { get; set; }
        [HalfWidth]
        public Int32 DivisionId { get; set; }
        [HalfWidth]
        public Int32 UnitId { get; set; }
        [HalfWidth]
        public Int32 GroupId { get; set; }
       
        [HalfWidth]
        public Boolean RawMaterial { get; set; }

        [Hidden]
        public Int32? ProductsId { get; set; }
        [HalfWidth, ReadOnly(true)]
        public Double Quantity { get; set; }
        [HalfWidth, ReadOnly(true)]
        public Int32? BranchId { get; set; }
        public String Description { get; set; }
        [Category("Pricing")]
        [OneThirdWidth, DefaultValue("0")]
        public Double SellingPrice { get; set; }
        [OneThirdWidth, DefaultValue("0")]
        public Double Mrp { get; set; }
        [OneThirdWidth, DefaultValue("0")]
        public Double PurchasePrice { get; set; }
        [Category("Tax")]
        [HalfWidth]
        public Int32 TaxId1 { get; set; }
        [HalfWidth]
        public Int32 TaxId2 { get; set; }
        [Tab("Additional Details")]
        [Category("Channel Pricing")]
        [HalfWidth]
        public Double ChannelCustomerPrice { get; set; }
        [HalfWidth]
        public Double ResellerPrice { get; set; }
        [HalfWidth]
        public Double WholesalerPrice { get; set; }
        [HalfWidth]
        public Double DealerPrice { get; set; }
        [HalfWidth]
        public Double DistributorPrice { get; set; }
        [HalfWidth]
        public Double StockiestPrice { get; set; }
        [HalfWidth]
        public Double NationalDistributorPrice { get; set; }
        [Category("Reorder")]
        [HalfWidth]
        public Double MinimumStock { get; set; }
        [HalfWidth]
        public Double MaximumStock { get; set; }
        [Category("Tech Specs")]
        public String TechSpecs { get; set; }
        [Category("Images")]
        public String Image { get; set; }
    }
}