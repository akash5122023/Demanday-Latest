using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Purchase.Forms
{
    [FormScript("Purchase.GrnTwo")]
    [BasedOnRow(typeof(GrnTwoRow), CheckNames = true)]
    public class GrnTwoForm
    {
        [Category("Contact Details")]
        [HalfWidth]
        public Int32 ContactsId { get; set; }

        [HalfWidth, ReadOnly(true)]
        public string ContactsPhone { get; set; }

        [Category("GRN Details")]
        [DefaultValue("now"), OneThirdWidth]
        public DateTime GrnDate { get; set; }

        [OneThirdWidth(UntilNext = true)]
        public Int32 GrnType { get; set; }
        public String Po { get; set; }
        public DateTime PoDate { get; set; }
        public Int32 Status { get; set; }
        public String InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }

        [Category("Product Details")]
        [GrnProductsTwoEditor]
        [FullWidth]
        public List<GrnProductsTwoRow> Products { get; set; }

        [FullWidth]
        public String Description { get; set; }

        [HalfWidth, ReadOnly(true)]
        public Int32 OwnerId { get; set; }

        [HalfWidth]
        public Int32 AssignedId { get; set; }

    }
}