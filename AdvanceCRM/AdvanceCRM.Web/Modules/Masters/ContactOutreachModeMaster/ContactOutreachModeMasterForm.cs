using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Forms
{
    [FormScript("Masters.ContactOutreachModeMaster")]
    [BasedOnRow(typeof(ContactOutreachModeMasterRow), CheckNames = true)]
    public class ContactOutreachModeMasterForm
    {
        public String Name { get; set; }
    }
}