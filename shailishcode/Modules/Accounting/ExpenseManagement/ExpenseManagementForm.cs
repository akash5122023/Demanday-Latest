
namespace AdvanceCRM.Accounting.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Accounting.ExpenseManagement")]
    [BasedOnRow(typeof(ExpenseManagementRow), CheckNames = true)]
    public class ExpenseManagementForm
    {
        public DateTime Date { get; set; }
        [ReadOnly(true)]
        public Int32 RepresentativeId { get; set; }
        public Int32 HeadId { get; set; }
        public Double Amount { get; set; }
        public String Attachment { get; set; }
        public String AdditionalInfo { get; set; }
        //public Int32 ApprovedBy { get; set; }
    }
}