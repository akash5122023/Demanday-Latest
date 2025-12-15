using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Products.Columns
{
    [ColumnsScript("Products.Inventory")]
    [BasedOnRow(typeof(InventoryRow), CheckNames = true)]
    public class InventoryColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink, Width(120), QuickFilter, QuickSearch]
        public String Name { get; set; }
        [QuickSearch]
        public String Code { get; set; }
        [QuickFilter]
        public String Branch { get; set; }
       
        [QuickSearch]
        public Double? Quantity { get; set; }
        [QuickSearch]
        public String Hsn { get; set; }
        [QuickFilter]
        public String DivisionProductsDivision { get; set; }
        public String UnitProductsUnit { get; set; }
        [QuickFilter]
        public String GroupProductsGroup { get; set; }
        public Double SellingPrice { get; set; }
        public Double Mrp { get; set; }
        public Double PurchasePrice { get; set; }
        public String Description { get; set; }
        public String TechSpecs { get; set; }
        public String TaxId1Name { get; set; }
        public String TaxId2Name { get; set; }
        public Double ChannelCustomerPrice { get; set; }
        public Double ResellerPrice { get; set; }
        public Double WholesalerPrice { get; set; }
        public Double DealerPrice { get; set; }
        public Double DistributorPrice { get; set; }
        public Double StockiestPrice { get; set; }
        public Double NationalDistributorPrice { get; set; }
        [Hidden]
        public Boolean RawMaterial { get; set; }

        //[Hidden]
        //public Int32? BomId { get; set; }

        [Hidden]
        public Double MinimumStock { get; set; }
        [Hidden]
        public Double MaximumStock { get; set; }


        [Hidden]
        public Int32? ProductsId { get; set; }
    }
}