using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Sales.Columns
{
    [ColumnsScript("Sales.OutwardProducts")]
    [BasedOnRow(typeof(OutwardProductsRow), CheckNames = true)]
    public class OutwardProductsColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        [EditLink, Width(120)]
        public String ProductsName { get; set; }
        [Width(80)]
        public String Serial { get; set; }
        [Width(80)]
        public String Batch { get; set; }
        public Double Quantity { get; set; }
        public Double Price { get; set; }
        public Double SellingPrice { get; set; }
        public Double Mrp { get; set; }
        public String Unit { get; set; }
        public Double Discount { get; set; }
        public Double DiscountAmount { get; set; }
    }
}