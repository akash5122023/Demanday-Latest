using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.DNCContact.Columns
{
    [ColumnsScript("DNCContact.DncContacts")]
    [BasedOnRow(typeof(DncContactsRow), CheckNames = true)]
    public class DncContactsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink, DisplayName("Sr No"), AlignRight]
        public Int32? SrNo { get; set; }
        [EditLink]
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String DncStatus { get; set; }
        public String Number { get; set; }
        public String CampaignCampaignId { get; set; }
        public String MasterAccountAccountNumber { get; set; }
    }
}