using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Forms
{
    [FormScript("Masters.ContactDispositionMaster")]
    [BasedOnRow(typeof(ContactDispositionMasterRow), CheckNames = true)]
    public class ContactDispositionMasterForm
    {
        public String Name { get; set; }
    }
}