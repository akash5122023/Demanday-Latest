
namespace AdvanceCRM.Services.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Services.CMS")]
    [BasedOnRow(typeof(CMSRow), CheckNames = true)]
    public class CMSForm
    {
        [Category("General")]
        [HalfWidth]
        public Int32 ProjectId { get; set; }
        [HalfWidth(UntilNext = true)]
        public DateTime Date { get; set; }
        [DisplayName("TicketNo"), ReadOnly(true)]
        public String Cmsn { get; set; }
        [DisplayName("Serial/Licence Key")]
        public String SerialNo { get; set; }
        [Hidden]
        public Int32 CMSNo { get; set; }

        [Category("Basic")]
        public Int32 ComplaintId { get; set; }
        public String Instructions { get; set; }

        [Category("Customer")]
        public Int32 ContactsId { get; set; }
        [Hidden]
        public String ContactsName { get; set; }
        public String ContactsPhone { get; set; }
        [FullWidth]
        public String ContactsAddress { get; set; }
        [Hidden]
        public String ContactsWhatsapp { get; set; }


        [Category("Dealer/Partner")]
        [HalfWidth(UntilNext = true)]
        public Int32 DealerId { get; set; }

        public String DealerPhone { get; set; }
        public String DealerEmail { get; set; }
        [Category("Employee/SalesPerson")]
        public Int32 EmployeeId { get; set; }
        public String EmployeePhone { get; set; }
        public String EmployeeEmail { get; set; }

        [Category("Complaint Details")]
        [OneThirdWidth(UntilNext = true)]
        public Int32 ProductsId { get; set; }
        public Int32 Qty { get; set; }
        public DateTime PurchaseDate { get; set; }
        public String InvoiceNo { get; set; }
        public Int32 Priority { get; set; }
        [DefaultValue("1")]
        public Int32 Status { get; set; }
        public Int32 Category { get; set; }
        public Int32 StageId { get; set; }
        public Int32 BranchId { get; set; }
        public Double Amount { get; set; }
        public DateTime ExpectedCompletion { get; set; }


        public String Representative { get; set; }
        public DateTime CompletionDate { get; set; }
        [FullWidth]
        public String Feedback { get; set; }

        public String AdditionalInfo { get; set; }
        public String Image { get; set; }
        [Category("Products")]
        public List<CMSProductsRow> Products { get; set; }

        [Category("Actions")]
        public Int32 InvestigationBy { get; set; }
        public String Observation { get; set; }
        public Int32 ActionBy { get; set; }
        public String Action { get; set; }
        public Int32 SupervisedBy { get; set; }
        public String Comments { get; set; }
        [Category("Representative")]
        [HalfWidth]
        public Int32 AssignedBy { get; set; }
        [HalfWidth]
        public Int32 AssignedTo { get; set; }
        public List<object> NoteList { get; set; }
        public List<object> Timeline { get; set; }
    }
}