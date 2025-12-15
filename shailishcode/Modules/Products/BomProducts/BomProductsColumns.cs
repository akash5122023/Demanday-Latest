using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using AdvanceCRM.Products;

namespace AdvanceCRM.Products.Columns
{
    [ColumnsScript("Products.BomProducts")]
    [BasedOnRow(typeof(BomProductsRow), CheckNames = true)]
    public class BomProductsColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        //public Int32 ProductsId { get; set; }
        [EditLink, Width(120)]
        public String ProductsName { get; set; }
        [Width(80)]
        public Double Quantity { get; set; }
        [Width(80)]
        public Double Mrp { get; set; }
        [Width(80)]
        public String Unit { get; set; }

        [Width(120)]
        public Double SellingPrice { get; set; }
        [Width(80)]
        public Double Price { get; set; }
        [Width(80)]
        public Double Discount { get; set; }
        [Width(80)]
        public Double DiscountAmount { get; set; }

        [Width(80)]
        public String TaxType1 { get; set; }

        [Width(80)]
        public String TaxType2 { get; set; }
        [Width(80)]
        public Double Percentage1 { get; set; }
        [Width(80)]
        public Double Percentage2 { get; set; }
        [Width(120)]
        public DateTime WarrantyStart { get; set; }
        [Width(120)]
        public DateTime WarrantyEnd { get; set; }
        [Width(100)]
        public Decimal LineTotal { get; set; }
        public Decimal GrandTotal { get; set; }
    }
}