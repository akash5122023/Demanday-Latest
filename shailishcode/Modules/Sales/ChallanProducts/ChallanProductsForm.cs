
namespace AdvanceCRM.Sales.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Sales.ChallanProducts")]
    [BasedOnRow(typeof(ChallanProductsRow), CheckNames = true)]
    public class ChallanProductsForm
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
        [Category("Travels")]
        [HalfWidth]
        public String From { get; set; }
        [HalfWidth]
        public String To { get; set; }
        [HalfWidth]
        public DateTime? Date { get; set; }
        [Category("Hotels")]
        [HalfWidth]
        public String Destination { get; set; }
        [HalfWidth]
        public String HotelName { get; set; }
        [HalfWidth]
        public String Nights { get; set; }
        [HalfWidth]
        public String Adults { get; set; }
        [HalfWidth]
        public String Childrens { get; set; }

        [HalfWidth]
        public String MealPlan { get; set; }


        [Category("TAX")]
        [HalfWidth]
        public String TaxType1 { get; set; }
        [HalfWidth]
        public Double Percentage1 { get; set; }
        [HalfWidth]
        public String TaxType2 { get; set; }
        [HalfWidth]
        public Double Percentage2 { get; set; }
        public Double Discount { get; set; }
        public Double DiscountAmount { get; set; }
        [FullWidth]
        public String Description { get; set; }
        //public Int32 ChallanId { get; set; }
    }
}