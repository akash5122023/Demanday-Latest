
namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("ThirdParty.WoocommerceDetails")]
    [BasedOnRow(typeof(WoocommerceDetailsRow), CheckNames = true)]
    public class WoocommerceDetailsForm
    {
        public String WooId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Company { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Country { get; set; }
        public DateTime CreatedDate { get; set; }
        [TextAreaEditor(Rows = 4), FullWidth]
        public String Feedback { get; set; }
        public Boolean IsMoved { get; set; }
    }
}