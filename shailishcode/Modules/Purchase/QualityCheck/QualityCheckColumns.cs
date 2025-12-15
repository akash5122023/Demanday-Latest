using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using Serenity.Data.Mapping;

namespace AdvanceCRM.Purchase.Columns
{
    [ColumnsScript("Purchase.QualityCheck")]
    [BasedOnRow(typeof(QualityCheckRow), CheckNames = true)]
    public class QualityCheckColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public Int32 QcNumber { get; set; }
        public DateTime PurchaseDate { get; set; }
        [EditLink]
        [QuickSearch]
        public String ProductName1 { get; set; }

        public DateTime QcDate { get; set; }
        public String InspectionCriteria { get; set; }
        public Int32 QtyInspected { get; set; }
        public Int32 QtyPassed { get; set; }
        public Int32 QtyRejected { get; set; }
        public String DepositionAction { get; set; }
        public String AdditionalInfo { get; set; }
        public String Attachments { get; set; }
        public String PurchaseFromName { get; set; }
    }
}