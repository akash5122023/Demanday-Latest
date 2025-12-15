
namespace AdvanceCRM.Sales.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Sales.InvoiceProducts")]
    [BasedOnRow(typeof(InvoiceProductsRow), CheckNames = true)]
    public class InvoiceProductsColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink, Width(120)]
        public String ProductsName { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public DateTime? Date { get; set; }
        public String Destination { get; set; }
        public String Nights { get; set; }
        public String Adults { get; set; }
        public String Childrens { get; set; }
        public String HotelName { get; set; }
        public String MealPlan { get; set; }

        [HalfWidth]
        public String Capacity { get; set; }
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
        public Double Percentage1 { get; set; }
        [Width(80)]
        public String TaxType2 { get; set; }
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