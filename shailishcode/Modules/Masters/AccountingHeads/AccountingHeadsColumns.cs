
namespace AdvanceCRM.Masters.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Masters.AccountingHeads")]
    [BasedOnRow(typeof(AccountingHeadsRow), CheckNames = true)]
    public class AccountingHeadsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Head { get; set; }
        public Masters.TransactionTypeMaster Type { get; set; }
    }
}