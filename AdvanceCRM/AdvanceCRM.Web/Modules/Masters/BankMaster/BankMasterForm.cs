
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.BankMaster")]
    [BasedOnRow(typeof(BankMasterRow), CheckNames = true)]
    public class BankMasterForm
    {
        public String BankName { get; set; }
        public String AccountNumber { get; set; }
        public String IFSC { get; set; }
        public String Type { get; set; }
        public String Branch { get; set; }
        public String AdditionalInfo { get; set; }
    }
}