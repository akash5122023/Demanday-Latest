
namespace AdvanceCRM.Services.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Services.Ticket")]
    [BasedOnRow(typeof(TicketRow), CheckNames = true)]
    public class TicketForm
    {
        public String Name { get; set; }
        public String Phone { get; set; }
        public Int32 ProductsId { get; set; }
        public String ComplaintDetails { get; set; }
        public Int32 Priority { get; set; }

        public String AdditionalDetails { get; set; }
        //public DateTime Date { get; set; }
        public Int32 AssignedId { get; set; }
    }
}