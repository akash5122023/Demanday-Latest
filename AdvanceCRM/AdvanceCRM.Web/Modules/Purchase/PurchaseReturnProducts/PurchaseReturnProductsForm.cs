
namespace AdvanceCRM.Purchase.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Purchase.PurchaseReturnProducts")]
    [BasedOnRow(typeof(PurchaseReturnProductsRow), CheckNames = true)]
    public class PurchaseReturnProductsForm
    {
        [Category("General")]
        public Int32 ProductsId { get; set; }
        [HalfWidth]
        public String Serial { get; set; }
        [HalfWidth]
        public String Batch { get; set; }
        [HalfWidth]
        public Double Quantity { get; set; }
        [HalfWidth]
        public Double Price { get; set; }
        public String Description { get; set; }

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
        public Double Percentage1 { get; set; }
        [HalfWidth]
        public String TaxType2 { get; set; }
        [HalfWidth]
        public Double Percentage2 { get; set; }
        
        
        //public Int32 PurchaseReturnId { get; set; }
          }
}