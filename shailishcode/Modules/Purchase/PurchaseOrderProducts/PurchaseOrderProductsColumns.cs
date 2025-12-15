
namespace AdvanceCRM.Purchase.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Purchase.PurchaseOrderProducts")]
    [BasedOnRow(typeof(PurchaseOrderProductsRow), CheckNames = true)]
    public class PurchaseOrderProductsColumns
    {
        [Width(140), EditLink]
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

        [Width(120), AlignCenter]
        public Double Quantity { get; set; }
        [Width(120), AlignRight]
        public Double Price { get; set; }
        [Width(120), AlignRight]
        public Double Discount { get; set; }
        [Width(120), AlignRight]
        public Double DiscountAmount { get; set; }
        [Width(120), AlignRight]
        public String Unit { get; set; }
    }
}