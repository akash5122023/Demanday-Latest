
namespace AdvanceCRM.Purchase.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Purchase.PurchaseOrderProducts")]
    [BasedOnRow(typeof(PurchaseOrderProductsRow), CheckNames = true)]
    public class PurchaseOrderProductsForm
    {
        [Category("General")]
        public Int32 ProductsId { get; set; }
        [DefaultValue("1")]
        [HalfWidth]
        public Double Quantity { get; set; }
        [HalfWidth]
        public Double Price { get; set; }
        [HalfWidth]
        public Boolean Inclusive { get; set; }
        [HalfWidth]
        [DefaultValue("0"), DecimalEditor(MaxValue = "100")]
        public Double Discount { get; set; }
        [HalfWidth]
        [DefaultValue("0")]
        public Double DiscountAmount { get; set; }
        public String Unit { get; set; }



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
        

    }
}