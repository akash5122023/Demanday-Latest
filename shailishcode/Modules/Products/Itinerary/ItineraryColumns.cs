
namespace AdvanceCRM.Products.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Products.Itinerary")]
    [BasedOnRow(typeof(ItineraryRow), CheckNames = true)]
    public class ItineraryColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Headline { get; set; }
        public DateTime Date { get; set; }
       // public String DaysTitle { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public String Adults { get; set; }
        public String Childrens { get; set; }
        public String Destination { get; set; }
        public String Nights { get; set; }
        public String HotelName { get; set; }
        public String MealPlan { get; set; }
        public Double Amount { get; set; }
        public String TermsAndConditions { get; set; }
    }
}