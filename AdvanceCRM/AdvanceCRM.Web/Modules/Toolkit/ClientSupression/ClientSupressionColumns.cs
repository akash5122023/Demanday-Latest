using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Toolkit.Columns
{
    [ColumnsScript("Toolkit.ClientSupression")]
    [BasedOnRow(typeof(ClientSupressionRow), CheckNames = true)]
    public class ClientSupressionColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [DisplayName("Sr No"), AlignRight]
        public Int32 SrNo { get; set; }
        [EditLink, DisplayName("Campaign ID")]
        public String CampaignCampaignId { get; set; }
        [DisplayName("Master Account ID")]
        public String MasterAccountAccountNumber { get; set; }
        public String CompanyName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Domain { get; set; }
        public DateTime Date { get; set; }
        public String OwnerUsername { get; set; }
    }
}