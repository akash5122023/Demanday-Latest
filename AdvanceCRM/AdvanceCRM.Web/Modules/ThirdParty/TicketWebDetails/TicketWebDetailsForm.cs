
namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("ThirdParty.TicketWebDetails")]
    [BasedOnRow(typeof(TicketWebDetailsRow), CheckNames = true)]
    public class TicketWebDetailsForm
    {
        [HalfWidth(UntilNext = true)]
       
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public DateTime DateTime { get; set; }
        public String ProductName { get; set; }
        public DateTime PurchaseDate { get; set; }

        [FullWidth(UntilNext = true)]
        public String Address { get; set; }
        public String Requirement { get; set; }      
       
        public String ComplaintDetails { get; set; }
        public String Attachment { get; set; }
        public Boolean IsMoved { get; set; }
    }
}