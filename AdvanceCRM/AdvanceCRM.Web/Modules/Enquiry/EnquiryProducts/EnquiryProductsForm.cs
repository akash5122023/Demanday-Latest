
namespace AdvanceCRM.Enquiry.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using AdvanceCRM.Masters;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Enquiry.EnquiryProducts")]
    [BasedOnRow(typeof(EnquiryProductsRow), CheckNames = true)]
    public class EnquiryProductsForm
    {
        [Category("General")]
        [HalfWidth]
        public Int32 ProductsId { get; set; }
        [HalfWidth]

        public String Capacity { get; set; }
        [HalfWidth]
        [LookupEditor("Products.ProductCodeLookup")]

        public String Code { get; set; }
        [DefaultValue("1"), OneThirdWidth]
        public Double Quantity { get; set; }
        [ReadOnly(true), OneThirdWidth]
        public Double Mrp { get; set; }
        [OneThirdWidth]

        public String Unit { get; set; }
        [ReadOnly(true), OneThirdWidth]
        public Double SellingPrice { get; set; }
        [OneThirdWidth]
        public Double Price { get; set; }
        [OneThirdWidth]
        public Double Discount { get; set; }

        //public StringField FileAttachment;
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
        public String Nights { get; set; }
        [HalfWidth]
        public String Adults { get; set; }
        [HalfWidth]
        public String Childrens { get; set; }
        [HalfWidth]
        public String HotelName { get; set; }
        [HalfWidth]
        public String MealPlan { get; set; }

       
    }
}