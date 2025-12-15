using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Sales.Columns
{
    [ColumnsScript("Sales.InwardProducts")]
    [BasedOnRow(typeof(InwardProductsRow), CheckNames = true)]
    public class InwardProductsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public Int32 ProductsId { get; set; }
        [EditLink]
        public String Serial { get; set; }
        public String Batch { get; set; }
        public Double Quantity { get; set; }
        public Double Price { get; set; }
        public Double SellingPrice { get; set; }
        public Double Mrp { get; set; }
        public Double Discount { get; set; }
        public Double DiscountAmount { get; set; }
        public Int32 InwardId { get; set; }
        public String Description { get; set; }
        public String Unit { get; set; }
        public String TaxType1 { get; set; }
        public Double Percentage1 { get; set; }
        public String TaxType2 { get; set; }
        public Double Percentage2 { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public DateTime Date { get; set; }
        public String Adults { get; set; }
        public String Childrens { get; set; }
        public String Destination { get; set; }
        public String Nights { get; set; }
        public String HotelName { get; set; }
        public String MealPlan { get; set; }
        public String Branch { get; set; }
    }
}