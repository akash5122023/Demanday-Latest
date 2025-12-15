
namespace AdvanceCRM.Quotation.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;

    [ColumnsScript("Quotation.Quotation")]
    [BasedOnRow(typeof(QuotationRow), CheckNames = true)]
    public class QuotationColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Hidden]
        public Int32 Id { get; set; }
        [Hidden]
        public Int32 DealerId { get; set; }
        public Int32 CompanyId { get; set; }
        [EditLink, AlignRight, SortOrder(1, true)]
        public Int32 QuotationN { get; set; }
        [Hidden]
        public Int32 QuotationNo { get; set; }
        [Width(120), EditLink, QuickFilter, QuickSearch]
        public String ContactsName { get; set; }
        [Hidden]
        public String ContactsEmail { get; set; }
        [QuickSearch]
        public String ContactsPhone { get; set; }
        public String ContactsAddress { get; set; }        
        [QuickFilter, QuickSearch]
        public String ContactPersonName { get; set; }
        [QuickSearch]
        public String ContactPersonPhone { get; set; }
        public String ContactPersonEmail { get; set; }
        [QuickSearch]
        public String ContactPersonProject { get; set; }
        public String ContactPersonAddress { get; set; }
        [QuickFilter]
        public DateTime Date { get; set; }
        [QuickFilter]
        public DateTime ExpectedClosingDate { get; set; }
        [QuickFilter]
        public DateTime ClosingDate { get; set; }
        public Double GrandTotal { get; set; }
        public Double PerDiscount { get; set; }
        public Double DiscountAmt { get; set; }
        public Double DisGrandTotal { get; set; }

        [QuickFilter, QuickFilterOption("multiple", true)]
        public Masters.StatusMaster Status { get; set; }
        [QuickFilter]
        public Masters.ClosingTypeMaster ClosingType { get; set; }
        [QuickFilter]
        public Masters.EnquiryTypeMaster Type { get; set; }
        public String LastFollowup { get; set; }
        //[QuickFilter, TextAreaEditor(Rows = 1)]
         public String AdditionalInfo { get; set; }
        //[QuickFilter, TextAreaEditor(Rows = 1)]
        //public String AdditionalInfo2 { get; set; }

        [QuickFilter]
        public String Source { get; set; }
        [QuickFilter]
        public String Stage { get; set; }
        public String Branch { get; set; }
        public Masters.WinPercentageMaster WinPercentage { get; set; }
        public String ContactsState { get; set; }
        public String ContactsCity { get; set; }
        public String ContactsArea { get; set; }
        [QuickFilter]
        public String OwnerUsername { get; set; }
        [QuickFilter]
        public String AssignedUsername { get; set; }

        public List<Int32> MultiAssignList { get; set; }
        [QuickSearch]
        public Int32 EnquiryNo { get; set; }
        public Int32 EnquiryDate { get; set; }
        [Hidden]
        public Boolean CurrencyConversion { get; set; }
        [Hidden]
        public Boolean Taxable { get; set; }
        [Hidden]
        public String ReferenceName { get; set; }
        [Hidden]
        public String ReferencePhone { get; set; }
        [Hidden]
        public String LostReason { get; set; }
        [QuickFilter,Hidden]
        public Int32Field OwnerTeamsId { get; set; }
        [QuickFilter, Hidden]
        public Int32Field AssignedTeamsId { get; set; }

        [QuickFilter]
        public String ApprovedByDisplayName { get; set; }
    }
}