using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Toolkit.Columns
{
    [ColumnsScript("Toolkit.MasterSupression")]
    [BasedOnRow(typeof(MasterSupressionRow), CheckNames = true)]
    public class MasterSupressionColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink, DisplayName("Account Number")]
        public String MasterAccountAccountNumber { get; set; }
        [DisplayName("Campaign Id")]
        public String CampaignCampaignId { get; set; }
        public String CompanyName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Domain { get; set; }
        public DateTime Date { get; set; }
        public String OwnerUsername { get; set; }
    }
}