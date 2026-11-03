using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Forms
{
    [FormScript("Masters.ContactAccountTypeMaster")]
    [BasedOnRow(typeof(ContactAccountTypeMasterRow), CheckNames = true)]
    public class ContactAccountTypeMasterForm
    {
        public String Name { get; set; }
    }
}