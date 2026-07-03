using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Forms
{
    [FormScript("Masters.DemandayCountryMaster")]
    [BasedOnRow(typeof(DemandayCountryMasterRow), CheckNames = true)]
    public class DemandayCountryMasterForm
    {
        public String Name { get; set; }
    }
}