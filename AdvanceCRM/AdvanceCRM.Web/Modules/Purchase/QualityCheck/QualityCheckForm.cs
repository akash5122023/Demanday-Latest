using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Purchase.Forms
{
    [FormScript("Purchase.QualityCheck")]
    [BasedOnRow(typeof(QualityCheckRow), CheckNames = true)]
    public class QualityCheckForm
    {
        [Category("General")]
        [OneThirdWidth(UntilNext = true)]
        public Int32 QcNumber { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Int32 PurchaseFromId { get; set; }

        //public String ProductName { get; set; }
        public Int32 ProductId { get; set; }
        [DefaultValue("now")]
        public DateTime QcDate { get; set; }
        public String InspectionCriteria { get; set; }
        public Int32 QtyInspected { get; set; }
        public Int32 QtyPassed { get; set; }
        public Int32 QtyRejected { get; set; }
        public String DepositionAction { get; set; }
        [Category("Additional Details")]
        [FullWidth]
        public String AdditionalInfo { get; set; }
        [FullWidth]
        public String Attachments { get; set; }
    }
}