using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Forms
{
    [FormScript("Masters.DemandayMasterAccount")]
    [BasedOnRow(typeof(DemandayMasterAccountRow), CheckNames = true)]
    public class DemandayMasterAccountForm
    {
        public String AccountNumber { get; set; }
    }
}