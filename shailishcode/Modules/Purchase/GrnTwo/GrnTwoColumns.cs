using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Purchase.Columns
{
    [ColumnsScript("Purchase.GrnTwo")]
    [BasedOnRow(typeof(GrnTwoRow), CheckNames = true)]
    public class GrnTwoColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String ContactsName { get; set; }
        public DateTime GrnDate { get; set; }
        public Int32 GrnType { get; set; }
        public String Po { get; set; }
        public DateTime PoDate { get; set; }
        public String OwnerUsername { get; set; }
        public String AssignedUsername { get; set; }
        public Int32 Status { get; set; }
        public String Description { get; set; }
        public String InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}