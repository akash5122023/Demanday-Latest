
namespace AdvanceCRM.Sales.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Sales.Challan")]
    [BasedOnRow(typeof(ChallanRow), CheckNames = true)]
    public class ChallanForm
    {
        [Category("Contact Details")]
        [HalfWidth]
        public Int32 ContactsId { get; set; }
        [Hidden]
        public Int32 ContactsContactType { get; set; }
        [Hidden]
        public String ContactsName { get; set; }
        [Hidden]
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
        [Category("Product Details")]
        [ChallanProductsEditor]
        public List<ChallanProductsRow> Products { get; set; }
        [OneThirdWidth]
        public Double PackagingCharges { get; set; }
        [OneThirdWidth]
        public Double FreightCharges { get; set; }
        [OneThirdWidth]
        public Double Advacne { get; set; }
        [Category("Challan Details")]
        [OneThirdWidth(UntilNext = true)]
        public Int32 ChallanNo { get; set; }
        [DefaultValue("now")]
        public DateTime Date { get; set; }
        [DefaultValue("1")]
        public Int32 Status { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public Int32 Type { get; set; }
        public Int32 SourceId { get; set; }
        public Int32 StageId { get; set; }        
        public Int32 BranchId { get; set; }
        [Category("Additional Details")]
        [HalfWidth]
        public Boolean InvoiceMade { get; set; }
        [HalfWidth]
        public Boolean OtherAddress { get; set; }
        [HalfWidth]
        public String DispatchDetails { get; set; }
        [HalfWidth]
        public String ShippingAddress { get; set; }
        public String AdditionalInfo { get; set; }
        public String Attachments { get; set; }
        [Category("Representatives")]
        [HalfWidth, ReadOnly(true)]
        public Int32 OwnerId { get; set; }
        [HalfWidth]
        public Int32 AssignedId { get; set; }
        public List<object> NoteList { get; set; }
    }
}