
namespace AdvanceCRM.Purchase.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Purchase.PurchaseReturnProducts")]
    [BasedOnRow(typeof(PurchaseReturnProductsRow), CheckNames = true)]
    public class PurchaseReturnProductsColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink]
        public String ProductsName { get; set; }
        //public String Serial { get; set; }
        //public String Batch { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public DateTime? Date { get; set; }
        public String Destination { get; set; }
        public String Nights { get; set; }
        public String Adults { get; set; }
        public String Childrens { get; set; }
        public String HotelName { get; set; }
        public String MealPlan { get; set; }
        [EditLink]
        public Double Quantity { get; set; }
        public Double Price { get; set; }
        public String TaxType1 { get; set; }
        public Double Percentage1 { get; set; }
        public String TaxType2 { get; set; }
        public Double Percentage2 { get; set; }
        public String Description { get; set; }
        [Width(100)]
        public Decimal LineTotal { get; set; }
        [Width(100)]
        public Decimal GrandTotal { get; set; }
        //public Int32 PurchaseReturnId { get; set; }
    }
}