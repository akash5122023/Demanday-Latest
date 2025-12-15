using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Demanday.Columns
{
    [ColumnsScript("Demanday.DemandayVerification")]
    [BasedOnRow(typeof(DemandayVerificationRow), CheckNames = true)]
    public class DemandayVerificationColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public Int32 SrNo { get; set; }
        [EditLink]
        public String AgentName { get; set; }
        public String CdqaComments { get; set; }
        public Int32 CampaignId { get; set; }
        public String CompanyName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Title { get; set; }
        public String Email { get; set; }
        public String WorkPhone { get; set; }
        public String Alternate01 { get; set; }
        public String Alternate02 { get; set; }
        public String ProfileLink { get; set; }
        [QuickFilter]
        public String OwnerUsername { get; set; }
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        //public Int32 SrNo { get; set; }
        //[EditLink]
        //public String AgentName { get; set; }
        //public String CdqaComments { get; set; }
        //public Int32 CampaignId { get; set; }
        //public String CompanyName { get; set; }
        //public String FirstName { get; set; }
        //public String LastName { get; set; }
        //public String Title { get; set; }
        //public String Email { get; set; }
        //public String WorkPhone { get; set; }
        //public String Alternate01 { get; set; }
        //public String Alternate02 { get; set; }
        //public String ProfileLink { get; set; }
        //public String OwnerUsername { get; set; }
    }
}