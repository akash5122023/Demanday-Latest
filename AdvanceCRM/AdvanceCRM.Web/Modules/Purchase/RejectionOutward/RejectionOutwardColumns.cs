using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Purchase.Columns
{
    [ColumnsScript("Purchase.RejectionOutward")]
    [BasedOnRow(typeof(RejectionOutwardRow), CheckNames = true)]
    public class RejectionOutwardColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public DateTime Date { get; set; }
        public Int32 QcNumber { get; set; }
        public String ProductName { get; set; }
        public Int32 QtyRejected { get; set; }
        public String PurchaseFromName { get; set; }
        public Int32 Status { get; set; }
        public String Branch { get; set; }
        [EditLink]
        public String AdditionalInfo { get; set; }
        public String Attachments { get; set; }
        public Boolean SentToSupplier { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}