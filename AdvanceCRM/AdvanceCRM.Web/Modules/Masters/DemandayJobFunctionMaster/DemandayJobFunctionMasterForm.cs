using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Forms
{
    [FormScript("Masters.DemandayJobFunctionMaster")]
    [BasedOnRow(typeof(DemandayJobFunctionMasterRow), CheckNames = true)]
    public class DemandayJobFunctionMasterForm
    {
        public String Name { get; set; }
    }
}