
namespace AdvanceCRM.Reports.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Reports.QuotationProducts")]
    [BasedOnRow(typeof(QuotationProductsRow), CheckNames = true)]
    public class QuotationProductsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public String QuotationContacts { get; set; }

        public String QuotationContactMail { get; set; }

        public String QuotationContactsMobile { get; set; }

        public String QuotationContactAddress { get; set; }

        public String QuotationSubContact { get; set; }

        public String QuotationSubContactMobile { get; set; }

        public String QuotationSubContactMail { get; set; }
        // public List<Int32> MultiAssignList { get; set; }
        [QuickFilter]
        public DateTime QuotationDate { get; set; }
        public Int32 QuotationStatus { get; set; }
        public Masters.EnquiryTypeMaster QuotationType { get; set; }
        //public StringField QuotationAdditionalInfo { get; set; }
        public String QuotationSource { get; set; }
        public String QuotationStage { get; set; }
        // public Int32 QuotationStageId { get; set; }
        public Int32 QuotationBranchId { get; set; }
        //[QuickFilter]
        public String QuotationOwner { get; set; }
        //[QuickFilter]
        public String QuotationAssigned { get; set; }
        public String QuotationReferenceName { get; set; }
        public String QuotationReferencePhone { get; set; }
        public Masters.ClosingTypeMaster QuotationClosingType { get; set; }
        public String QuotationLostReason { get; set; }
        [QuickFilter]
        public DateTime QuotationClosingDate { get; set; }
        public Int32 QuotationContactPersonId { get; set; }
       // public String QuotationAttachments { get; set; }
        public String QuotationQuotationN { get; set; }
        public Int32 QuotationQuotationNo { get; set; }
        public Int32 QuotationCompanyId { get; set; }
        [QuickFilter]
        public String ProductsName { get; set; }
        public Double Quantity { get; set; }
        public Double Mrp { get; set; }
        public Double SellingPrice { get; set; }
        public Double Price { get; set; }
        public Double Discount { get; set; }
        [EditLink]
        public String TaxType1 { get; set; }
        public Double Percentage1 { get; set; }
        public String TaxType2 { get; set; }
        public Double Percentage2 { get; set; }
        public String QuotationAdditionalInfo { get; set; }
        public Double DiscountAmount { get; set; }
        public String Description { get; set; }
        public String Unit { get; set; }
        public String Capacity { get; set; }
        public String ProductsDivision { get; set; }
    }
}