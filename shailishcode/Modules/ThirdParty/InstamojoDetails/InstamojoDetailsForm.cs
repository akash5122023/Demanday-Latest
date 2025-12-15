
namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("ThirdParty.InstamojoDetails")]
    [BasedOnRow(typeof(InstamojoDetailsRow), CheckNames = true)]
    public class InstamojoDetailsForm
    {
        public String InstaId { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public String Purpose { get; set; }
        public String PaymentMode { get; set; }
        public String Status { get; set; }
        public DateTime PayoutDate { get; set; }
        public Boolean IsMoved { get; set; }
    }
}