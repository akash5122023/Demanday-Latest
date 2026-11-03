using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Forms
{
    [FormScript("Masters.ContactAccountStatusMaster")]
    [BasedOnRow(typeof(ContactAccountStatusMasterRow), CheckNames = true)]
    public class ContactAccountStatusMasterForm
    {
        public String Name { get; set; }
    }
}