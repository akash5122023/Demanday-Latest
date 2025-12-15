
namespace AdvanceCRM.Accounting.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;

    [ColumnsScript("Accounting.ExpenseManagement")]
    [BasedOnRow(typeof(ExpenseManagementRow), CheckNames = true)]
    public class ExpenseManagementColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, SortOrder(1, true)]
        public Int32 Id { get; set; }
        [EditLink, QuickFilter]
        public DateTime Date { get; set; }
        [EditLink, QuickFilter]
        public String RepresentativeDisplayName { get; set; }
        [EditLink, QuickFilter, QuickSearch]
        public String Head { get; set; }
        public Double Amount { get; set; }
        //public String Attachment { get; set; }
        public String AdditionalInfo { get; set; }
        [QuickFilter]
        public String ApprovedByDisplayName { get; set; }
    }
}