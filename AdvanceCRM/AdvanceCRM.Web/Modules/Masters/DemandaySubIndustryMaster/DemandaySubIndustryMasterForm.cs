using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Forms
{
    [FormScript("Masters.DemandaySubIndustryMaster")]
    [BasedOnRow(typeof(DemandaySubIndustryMasterRow), CheckNames = true)]
    public class DemandaySubIndustryMasterForm
    {
        public String Name { get; set; }
    }
}