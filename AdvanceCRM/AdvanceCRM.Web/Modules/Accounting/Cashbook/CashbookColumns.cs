
namespace AdvanceCRM.Accounting.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;

    [ColumnsScript("Accounting.Cashbook")]
    [BasedOnRow(typeof(CashbookRow), CheckNames = true)]
    public class CashbookColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, SortOrder(1, true)]
        public Int32 Id { get; set; }
        [DefaultValue("now"), EditLink]
        public DateTime Date { get; set; }
        [EditLink, QuickFilter]
        public Masters.TransactionTypeMaster Type { get; set; }
        [Width(140), QuickFilter]
        public String Head1 { get; set; }
        [EditLink, Width(200), QuickFilter, QuickSearch]
        public String ContactsName { get; set; }

        [EditLink, QuickFilter]
        public String RepresentativeDisplayName { get; set; }
        [EditLink, Width(200), QuickFilter, QuickSearch]
        public String ProjectName { get; set; }
        [EditLink, Width(200), QuickFilter, QuickSearch]
        public String EmployeeName { get; set; }
        [QuickSearch]
        public String InvoiceNo { get; set; }
        public String Purpose { get; set; }
        public Double ProjectAmtIn { get; set; }
        public Double CashIn { get; set; }
        public Double CashOut { get; set; }
        [QuickSearch]
        public String Narration { get; set; }
        [QuickSearch]
        public String BankBankName { get; set; }

        [QuickFilter]
        public String ApprovedByDisplayName { get; set; }


    }
}