
namespace AdvanceCRM.Reports.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Reports.EnquiryProducts")]
    [BasedOnRow(typeof(EnquiryProductsRow), CheckNames = true)]
    public class EnquiryProductsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        
        public String EnquiryContacts { get; set; }

        public String EnquiryContactMail { get; set; }

        public String EnquiryContactsMobile { get; set; }

        public String EnquiryContactAddress { get; set; }

        public String EnquirySubContact { get; set; }

        public String EnquirySubContactMobile { get; set; }

        public String EnquirySubContactMail { get; set; }
       // public List<Int32> MultiAssignList { get; set; }
        [QuickFilter]
        public DateTime EnquiryDate { get; set; }
        public Int32 EnquiryStatus { get; set; }
        public Masters.EnquiryTypeMaster EnquiryType { get; set; }
        //public StringField EnquiryAdditionalInfo { get; set; }
        public String EnquirySource { get; set; }
        public String EnquiryStage { get; set; }
       // public Int32 EnquiryStageId { get; set; }
        public Int32 EnquiryBranchId { get; set; }
        //[QuickFilter]
        public String EnquiryOwner { get; set; }
        //[QuickFilter]
        public String EnquiryAssigned { get; set; }
        public String EnquiryReferenceName { get; set; }
        public String EnquiryReferencePhone { get; set; }
        public Masters.ClosingTypeMaster EnquiryClosingType { get; set; }
        public String EnquiryLostReason { get; set; }
        [QuickFilter]
        public DateTime EnquiryClosingDate { get; set; }
        public Int32 EnquiryContactPersonId { get; set; }
        public String EnquiryAttachments { get; set; }
        public String EnquiryEnquiryN { get; set; }
        public Int32 EnquiryEnquiryNo { get; set; }
        public Int32 EnquiryCompanyId { get; set; }
        [QuickFilter]
        public String ProductsName { get; set; }
        public Double Quantity { get; set; }
        public Double Mrp { get; set; }
        public Double SellingPrice { get; set; }
        public Double Price { get; set; }
        public Double Discount { get; set; }
        public String EnquiryAdditionalInfo { get; set; }
        [EditLink]
        public String Description { get; set; }
        public String Capacity { get; set; }
    }
}