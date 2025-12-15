
namespace AdvanceCRM.Enquiry.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using AdvanceCRM.Enquiry;
    

    [FormScript("Enquiry.Enquiry")]
    [BasedOnRow(typeof(EnquiryRow), CheckNames = true)]
    public class EnquiryForm
    {
        [Category("Contact Details")]
        [HalfWidth]
        public Int32 ContactsId { get; set; }
        [Hidden]
        public Int32 ContactsContactType { get; set; }
        [Hidden]
        public String ContactsName { get; set; }

        [Hidden]
        public String ContactsEmail { get; set; }
        [HalfWidth,ReadOnly(true)]
//[LookupEditor("Contacts.ContactPhoneLookup")]
        public String ContactsPhone { get; set; }
        [Hidden]
        public String ContactsWhatsapp { get; set; }
        [ReadOnly(true)]
        public String ContactsAddress { get; set; }
        [HalfWidth, FormCssClass("line-break-sm")]
        public Int32 ContactPersonId { get; set; }
        [Hidden]
        public String ContactPersonName { get; set; }
        [ReadOnly(true), HalfWidth]
        public String ContactPersonPhone { get; set; }
        [Hidden]
        public String ContactPersonWhatsapp { get; set; }
        [ReadOnly(true), HalfWidth]
        public String ContactPersonProject { get; set; }
        [ReadOnly(true)]
        public String ContactPersonAddress { get; set; }
        [Visible(false)]
        public Int32? ProjectId { get; set; }

        public Int32 DealerId { get; set; }
        [Category("Product Details")]
        public List<EnquiryProductsRow> Products { get; set; }
        [ReadOnly(true)]
        public Double Total { get; set; }
        [Category("Enquiry Details")]
        [OneThirdWidth,Hidden]
        public Int32 EnquiryNo { get; set; }
        [OneThirdWidth, ReadOnly(true)]
        public string EnquiryN { get; set; }

        [DefaultValue("now"), OneThirdWidth]
        public DateTime Date { get; set; }

        [OneThirdWidth]
        public DateTime ExpectedClosingDate { get; set; }

        [DefaultValue("1"), OneThirdWidth]
        public Masters.StatusMaster Status { get; set; }
        [OneThirdWidth, DefaultValue("now")]
        public DateTime ClosingDate { get; set; }
        [OneThirdWidth]
        public Int32 ClosingType { get; set; }
        [OneThirdWidth]
        public String LostReason { get; set; }
        [OneThirdWidth, DefaultValue(1)]
        public Int32 SourceId { get; set; }
        [OneThirdWidth, DefaultValue(1)]
        public Int32 StageId { get; set; }
        [OneThirdWidth, DefaultValue(1)]
        public Int32 Type { get; set; }
        [OneThirdWidth]
        public Int32 BranchId { get; set; }
        [OneThirdWidth]
        public Int32 WinPercentage { get; set; }
        [OneThirdWidth]
        public String ReferenceName { get; set; }
        [OneThirdWidth, MaskedEditor(Mask = "9999999999")]
        public String ReferencePhone { get; set; }
        public String AdditionalInfo { get; set; }
        public String AdditionalInfo2 { get; set; }

        public List<Int32> EnquiryAddinfoList { get; set; }
        public String Attachments { get; set; }
        [Category("Representatives")]
        [OneThirdWidth, ReadOnly(true)]
        public Int32 OwnerId { get; set; }

        
        [OneThirdWidth]
        public Int32 AssignedId { get; set; }

        [OneThirdWidth]
        public List<Int32> MultiAssignList { get; set; }
        [ReadOnly(true),DefaultValue(1),Hidden]
        public Int32 CompanyId { get; set; }
        public List<object> NoteList { get; set; }
        public List<object> Timeline { get; set; }
    }
}