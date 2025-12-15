
namespace AdvanceCRM.Purchase.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Purchase.PurchaseProducts")]
    [BasedOnRow(typeof(PurchaseProductsRow), CheckNames = true)]
    public class PurchaseProductsForm
    {
        [Category("General")]
      
        public Int32 ProductsId { get; set; }
        [HalfWidth]
        public String Serial { get; set; }
        [HalfWidth]
        public String Batch { get; set; }
        [HalfWidth]
        [DefaultValue("1")]
        public Double Quantity { get; set; }
        //public Double Mrp { get; set; }
        //public Double SellingPrice { get; set; }
        [HalfWidth]
        public Double Price { get; set; }
        [HalfWidth]
        public String Unit { get; set; }
        public Boolean Inclusive { get; set; }
        [HalfWidth]
        [DefaultValue("0"), DecimalEditor(MaxValue = "100")]
        public Double Discount { get; set; }
        [HalfWidth]
        [DefaultValue("0")]
        public Double DiscountAmount { get; set; }

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

        [Category("Tax")]
        [HalfWidth]
        public String TaxType1 { get; set; }
        [HalfWidth]
        [DefaultValue("0")]
        public Double Percentage1 { get; set; }
        [HalfWidth]
        public String TaxType2 { get; set; }
        [HalfWidth]
        [DefaultValue("0")]
        public Double Percentage2 { get; set; }
        [Category("Warranty")]
        [HalfWidth]
        public DateTime WarrantyStart { get; set; }
        [HalfWidth]
        public DateTime WarrantyEnd { get; set; }
        

        //public Boolean Sold { get; set; }
        //public Int32 PurchaseId { get; set; }
    }
}