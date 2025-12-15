using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Sales.Forms
{
    [FormScript("Sales.Indent")]
    [BasedOnRow(typeof(IndentRow), CheckNames = true)]
    public class IndentForm
    {
        [Category("Contact Details")]
        [HalfWidth]
        public Int32 ContactsId { get; set; }
        [HalfWidth]
        public String ContactsPhone { get; set; }
        [HalfWidth]
        public DateTime Date { get; set; }
        [Category("Product Details")]
        [IndentProductsEditor]
        public List<IndentProductsRow> Products { get; set; }
        
        
        [OneThirdWidth(UntilNext = true)]
        public String InvoiceNo { get; set; }        
        public Int32 Status { get; set; }        
        public Int32 BranchId { get; set; }
        [FullWidth]
        public String AdditionalInfo { get; set; }
        [Category("Representative")]
        [HalfWidth, ReadOnly(true)]
        public Int32 OwnerId { get; set; }
        [HalfWidth]
        public Int32 AssignedId { get; set; }

    }
}