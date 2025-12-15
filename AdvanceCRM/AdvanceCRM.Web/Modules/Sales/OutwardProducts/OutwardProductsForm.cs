using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Sales.Forms
{
    [FormScript("Sales.OutwardProducts")]
    [BasedOnRow(typeof(OutwardProductsRow), CheckNames = true)]
    public class OutwardProductsForm
    {
        [Category("General")]
        [HalfWidth(UntilNext = true)]
        public Int32 ProductsId { get; set; }
        //[_Ext.AutoCompleteEditor(LookupKey = "Products.ProductCodeLookup")]
        [LookupEditor("Products.ProductCodeLookup")]
        public String Code { get; set; }
        [OneThirdWidth(UntilNext = true)]
        public String Serial { get; set; }
        public String Batch { get; set; }
        [DefaultValue("1")]
        public Double Quantity { get; set; }
        [ReadOnly(true)]
        public Double SellingPrice { get; set; }
        [ReadOnly(true)]
        public Double Mrp { get; set; }
        [ReadOnly(true)]
        public String Unit { get; set; }
        public Double Price { get; set; }
        public Double Discount { get; set; }
        public Double DiscountAmount { get; set; }

        public Int32 BranchId { get; set; }
        [FullWidth]
        public String Description { get; set; }
    }
}