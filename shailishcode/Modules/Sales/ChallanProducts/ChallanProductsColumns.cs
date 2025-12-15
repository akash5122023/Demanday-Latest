
namespace AdvanceCRM.Sales.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Sales.ChallanProducts")]
    [BasedOnRow(typeof(ChallanProductsRow), CheckNames = true)]
    public class ChallanProductsColumns
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
        //public Int32 ChallanId { get; set; }
    }
}