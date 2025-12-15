namespace AdvanceCRM.Sales
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;


    [ColumnsScript("Sales.Indent")]
    [BasedOnRow(typeof(IndentRow), CheckNames = true)]
    public class IndentColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public String ContactsName { get; set; }
        public DateTime Date { get; set; }
        public Int32 Status { get; set; }
        [EditLink]
        public String AdditionalInfo { get; set; }
        public String InvoiceNo { get; set; }
        public String Branch { get; set; }
        public String OwnerUsername { get; set; }
        public String AssignedUsername { get; set; }
        public String Subject { get; set; }
        public String Reference { get; set; }
        public String ContactPersonName { get; set; }
        public Int32 Lines { get; set; }
    }
}