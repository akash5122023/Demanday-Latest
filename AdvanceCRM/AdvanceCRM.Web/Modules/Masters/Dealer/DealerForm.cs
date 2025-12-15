
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.Dealer")]
    [BasedOnRow(typeof(DealerRow), CheckNames = true)]
    public class DealerForm
    {
        public String DealerName { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        [HalfWidth]
        public Int32 Country { get; set; }
        [HalfWidth]
        public Int32 StateId { get; set; }
        [HalfWidth]
        public Int32 CityId { get; set; }
        [HalfWidth]

        public String Pin { get; set; }
       
        public String AdditionalInfo { get; set; }
    }
}