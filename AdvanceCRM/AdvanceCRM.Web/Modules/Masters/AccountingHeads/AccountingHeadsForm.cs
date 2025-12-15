
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.AccountingHeads")]
    [BasedOnRow(typeof(AccountingHeadsRow), CheckNames = true)]
    public class AccountingHeadsForm
    {
        public String Head { get; set; }
        public Int32 Type { get; set; }
    }
}