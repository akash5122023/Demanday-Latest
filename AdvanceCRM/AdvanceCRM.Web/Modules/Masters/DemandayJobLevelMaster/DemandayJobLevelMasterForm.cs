using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Forms
{
    [FormScript("Masters.DemandayJobLevelMaster")]
    [BasedOnRow(typeof(DemandayJobLevelMasterRow), CheckNames = true)]
    public class DemandayJobLevelMasterForm
    {
        public String Name { get; set; }
    }
}