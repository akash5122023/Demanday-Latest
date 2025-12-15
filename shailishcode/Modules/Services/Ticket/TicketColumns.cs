
namespace AdvanceCRM.Services.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;

    [ColumnsScript("Services.Ticket")]
    [BasedOnRow(typeof(TicketRow), CheckNames = true)]
    public class TicketColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink, QuickFilter]
        public DateTime Date { get; set; }
            
        [EditLink, QuickSearch]
        public String Name { get; set; }
        [QuickSearch]
        public String Phone { get; set; }
        [QuickFilter]
        public String ProductsName { get; set; }
        public String ComplaintDetails { get; set; }
        [QuickFilter]
        public Int32 Priority { get; set; }
        [QuickFilter]
        public String AssignedUsername { get; set; }
    }
}