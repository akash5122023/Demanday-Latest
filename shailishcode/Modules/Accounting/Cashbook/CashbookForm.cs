
namespace AdvanceCRM.Accounting.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Accounting.Cashbook")]
    [BasedOnRow(typeof(CashbookRow), CheckNames = true)]
    public class CashbookForm
    {
        public DateTime Date { get; set; }
        [ReadOnly(true)]
        public Int32 RepresentativeId { get; set; }
        public Int32 Type { get; set; }
        [QuickFilter]
        public Int32 Head { get; set; }
        [QuickFilter]
        public Int32 ProjectId { get; set; }
        [QuickFilter]
        public Int32 ContactsId { get; set; }
        [QuickFilter]
        public Int32 EmployeeId { get; set; }
       
        public String InvoiceNo { get; set; }

        
        public String Purpose { get; set; }
        public Boolean IsCashIn { get; set; }
        public Double ProjectAmtIn { get; set; }
        public Double CashIn { get; set; }
        
        public Double CashOut { get; set; }
        public String Narration { get; set; }
        public Int32 BankId { get; set; }
    }
}