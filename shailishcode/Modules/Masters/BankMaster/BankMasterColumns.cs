
namespace AdvanceCRM.Masters.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Masters.BankMaster")]
    [BasedOnRow(typeof(BankMasterRow), CheckNames = true)]
    public class BankMasterColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, SortOrder(1, true)]
        public Int32 Id { get; set; }
        [EditLink]
        public String BankName { get; set; }
        public String AccountNumber { get; set; }
        public String IFSC { get; set; }
        public String Type { get; set; }
        public String Branch { get; set; }
        public String AdditionalInfo { get; set; }
    }
}