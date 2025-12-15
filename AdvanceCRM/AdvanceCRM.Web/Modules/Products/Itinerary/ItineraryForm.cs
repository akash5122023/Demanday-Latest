
namespace AdvanceCRM.Products.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Products.Itinerary")]
    [BasedOnRow(typeof(ItineraryRow), CheckNames = true)]
    public class ItineraryForm
    {
        [Category("General Information")]
        public String Headline { get; set; }

        [HalfWidth]
        public DateTime Date { get; set; }

         [Category("Travels")]
        [HalfWidth]
        public String From { get; set; }

        [HalfWidth]
        public String To { get; set; }
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



        // Days
        [Category("Days")]
        //[LookupEditor("YourModule.Days")] // replace with your actual lookup key
        public List<Int32> DaysId { get; set; }

        [HalfWidth]
        public Double Amount { get; set; }

        // Terms
        [Category("Terms / Conditions")]
        [TextAreaEditor(Rows = 5)]
        public String TermsAndConditions { get; set; }


    }
}