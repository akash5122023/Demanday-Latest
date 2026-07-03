using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Forms
{
    [FormScript("Masters.DemandayEmployeeSizeMaster")]
    [BasedOnRow(typeof(DemandayEmployeeSizeMasterRow), CheckNames = true)]
    public class DemandayEmployeeSizeMasterForm
    {
        public String Name { get; set; }
    }
}