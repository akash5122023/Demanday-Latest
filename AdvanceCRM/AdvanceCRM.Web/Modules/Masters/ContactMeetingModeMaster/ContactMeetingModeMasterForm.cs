using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Forms
{
    [FormScript("Masters.ContactMeetingModeMaster")]
    [BasedOnRow(typeof(ContactMeetingModeMasterRow), CheckNames = true)]
    public class ContactMeetingModeMasterForm
    {
        public String Name { get; set; }
    }
}