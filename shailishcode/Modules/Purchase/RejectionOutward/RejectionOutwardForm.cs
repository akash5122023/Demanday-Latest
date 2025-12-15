using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Purchase.Forms
{
    [FormScript("Purchase.RejectionOutward")]
    [BasedOnRow(typeof(RejectionOutwardRow), CheckNames = true)]
    public class RejectionOutwardForm
    {
        [Category("General")]
        [OneThirdWidth(UntilNext = true)]
        [DefaultValue("1")]
        public DateTime Date { get; set; }
        public Int32 QcNumber { get; set; }
        public Int32 ProductId { get; set; }
        public Int32 QtyRejected { get; set; }
        public Int32 PurchaseFromId { get; set; }
        [DefaultValue("1")]
        public Int32 Status { get; set; }
        [DefaultValue("now")]
        public DateTime ClosingDate { get; set; }
        public Int32 BranchId { get; set; }
        public Boolean SentToSupplier { get; set; }
        public DateTime SentDate { get; set; }

        [Category("Additional Details")]
        [FullWidth]
        public String AdditionalInfo { get; set; }
        [FullWidth]
        public String Attachments { get; set; }

    }
}