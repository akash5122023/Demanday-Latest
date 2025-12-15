
namespace AdvanceCRM.Masters.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Masters.QuotationTermsMaster")]
    [BasedOnRow(typeof(QuotationTermsMasterRow), CheckNames = true)]
    public class QuotationTermsMasterForm
    {
        public String Terms { get; set; }
    }
}