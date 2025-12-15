using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Products.Forms
{
    [FormScript("Products.BomProducts")]
    [BasedOnRow(typeof(BomProductsRow), CheckNames = true)]
    public class BomProductsForm
    {
        [Category("General")]
        [HalfWidth(UntilNext = true)]
        public Int32 ProductsId { get; set; }
        [LookupEditor("Products.ProductCodeLookup")]
        public String Code { get; set; }
        //[OneThirdWidth(UntilNext = true)]
        public Double Quantity { get; set; }
        public Int32 BomBranchId { get; set; }
        [ReadOnly(true)]
        public Double Mrp { get; set; }
        [ReadOnly(true)]
        public String Unit { get; set; }
        [ReadOnly(true)]
        public Double SellingPrice { get; set; }
        public Double Price { get; set; }
        public Boolean Inclusive { get; set; }
        [FullWidth]
        public String Description { get; set; }
        [Category("Discount")]
        [HalfWidth(UntilNext = true)]
        [DefaultValue("0"), DecimalEditor(MaxValue = "100")]
        public Double Discount { get; set; }
        [DefaultValue("0")]
        public Double DiscountAmount { get; set; }
        [Category("TAX")]
        public String TaxType1 { get; set; }
        [DefaultValue("0")]
        public Double Percentage1 { get; set; }
        public String TaxType2 { get; set; }
        [DefaultValue("0")]
        public Double Percentage2 { get; set; }
        [Category("Warranty")]
        public DateTime WarrantyStart { get; set; }
        public DateTime WarrantyEnd { get; set; }
        [Hidden]
        public Int32 BomId { get; set; }
    }
}