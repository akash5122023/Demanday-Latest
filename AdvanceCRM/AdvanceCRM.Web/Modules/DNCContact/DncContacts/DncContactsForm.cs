using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.DNCContact.Forms
{
    [FormScript("DNCContact.DncContacts")]
    [BasedOnRow(typeof(DncContactsRow), CheckNames = true)]
    public class DncContactsForm
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String DncStatus { get; set; }
        public String Number { get; set; }
        public Int32 CampaignId { get; set; }
        public Int32 MasterAccountId { get; set; }
    }
}