
namespace AdvanceCRM.Enquiry.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;
    using AdvanceCRM.Masters;

    [ColumnsScript("Enquiry.Enquiry")]
    [BasedOnRow(typeof(EnquiryRow), CheckNames = true)]
    public class EnquiryColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, SortOrder(1, true)]
        public Int32 Id { get; set; }
        [EditLink, AlignRight, SortOrder(1, true)]
        public String EnquiryN { get; set; }
        [Hidden]
        public String EnquiryNo { get; set; }      
        public Int32 CompanyId { get; set; }
        [Width(120), EditLink, QuickFilter, QuickSearch]

        public String ContactsName { get; set; }
        [QuickSearch]
        public String ContactsPhone { get; set; }
        [QuickSearch]
        public String ContactsEmail { get; set; }
        public String ContactsAddress { get; set; }
        [QuickFilter, QuickSearch]
        public String ContactPersonName { get; set; }
        [QuickSearch]
        public String ContactPersonPhone { get; set; }
        public String ContactPersonEmail { get; set; }
        [QuickSearch]
        public String ContactPersonProject { get; set; }
        public String ContactPersonAddress { get; set; }
        [Hidden] 
        public Int32 DealerId { get; set; }

        [QuickFilter]
        public DateTime Date { get; set; }
        [QuickFilter]
        public DateTime ExpectedClosingDate { get; set; }
        [QuickFilter]
        public DateTime ClosingDate { get; set; }
        public Double Total { get; set; }
        [QuickFilter, QuickFilterOption("multiple", true)]
        public Masters.StatusMaster Status { get; set; }
        [QuickFilter]
        public Masters.ClosingTypeMaster ClosingType { get; set; }
        public String LostReason { get; set; }
        [QuickFilter]
        public Masters.EnquiryTypeMaster Type { get; set; }
        public String LastFollowup { get; set; }
       public String AdditionalInfo { get; set; }
        [QuickFilter]
        public String Source { get; set; }
        [QuickFilter, LookupEditor(typeof(StageRow), InplaceAdd = false, FilterField = "Type", FilterValue = Masters.StageTypeMaster.Enquiry)]
        public Int32 StageId { get; set; }
        public String Branch { get; set; }
        public Masters.WinPercentageMaster WinPercentage { get; set; }
        public String ContactsState { get; set; }
        public String ContactsCity { get; set; }
        public String ContactsArea { get; set; }
        [QuickFilter, DisplayName("CreatedBy")]
        public String OwnerDisplayName { get; set; }
        //[QuickFilter, TextAreaEditor(Rows = 1)]
        //public String AdditionalInfo { get; set; }
        //[QuickFilter, TextAreaEditor(Rows = 1)]
        //public String AdditionalInfo2 { get; set; }
        [QuickFilter,DisplayName("AssignedTo")]
        public String AssignedDisplayName { get; set; }
        [QuickFilter,Hidden]
        public Int32Field AssignedTeamsId { get; set; }
        [QuickFilter,Hidden]
        public Int32Field OwnerTeamsId { get; set; }
        public List<Int32> MultiAssignList { get; set; }
        [Hidden]
        public String ReferenceName { get; set; }
        [Hidden]
        public String ReferencePhone { get; set; }
    }
}