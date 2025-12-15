
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

    [ColumnsScript("Services.CMS")]
    [BasedOnRow(typeof(CMSRow), CheckNames = true)]
    public class CMSColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Hidden]
        public Int32 Id { get; set; }
        [EditLink, AlignRight, DisplayName("TicketNo"), QuickSearch, QuickFilter]

        public String Cmsn { get; set; }
        [Hidden]
        public Int32 CMSNo { get; set; }

        [QuickFilter]
        public String Project { get; set; }
        [EditLink, Width(120), QuickFilter, QuickSearch]
        public String ContactsName { get; set; }
        [EditLink, QuickSearch]
        public String ContactsPhone { get; set; }
        public String ContactsAddress { get; set; }
        [QuickFilter]
        public DateTime Date { get; set; }
        [QuickFilter]
        public DateTime ExpectedCompletion { get; set; }
        public DateTime CompletionDate { get; set; }
        public String Branch { get; set; }
        [QuickFilter, QuickFilterOption("multiple", true)]
        public Masters.CMSStatusMaster Status { get; set; }
        [QuickFilter]
        public Masters.PriorityMaster Priority { get; set; }
        [QuickFilter]
        public String ComplaintComplaintType { get; set; }
        [QuickFilter]
        public Int32 Category { get; set; }
        [QuickFilter]
        public String EmployeeName { get; set; }
        [QuickFilter, DisplayName("DealerName")]
        public String DealerDealerName { get; set; }
        [QuickFilter]
        public DateTime PurchaseDate { get; set; }
        [QuickFilter]
        public String InvoiceNo { get; set; }
        //[QuickFilter]
        //public String Name { get; set; }
        [QuickFilter]
        public String Stage { get; set; }
        [QuickSearch]
        public String ProductsName { get; set; }
        [QuickSearch]
        public String SerialNo { get; set; }
        public Double Amount { get; set; }
        [QuickFilter]
        public String AssignedByUsername { get; set; }
        [QuickFilter]
        public String AssignedToUsername { get; set; }
        [QuickFilter]
        public String InvestigationByUsername { get; set; }
        [QuickFilter]
        public String ActionByUsername { get; set; }
        [QuickFilter]
        public String SupervisedByUsername { get; set; }
        public String Instructions { get; set; }
        public String Feedback { get; set; }
        public String AdditionalInfo { get; set; }
        [Hidden]
        public String Observation { get; set; }
        [Hidden]
        public String Action { get; set; }
        [Hidden]
        public String Comments { get; set; }
        [Hidden]
        public Int32 TicketNo { get; set; }

        public Int32 Qty { get; set; }
        public String Representative { get; set; }
    }
}