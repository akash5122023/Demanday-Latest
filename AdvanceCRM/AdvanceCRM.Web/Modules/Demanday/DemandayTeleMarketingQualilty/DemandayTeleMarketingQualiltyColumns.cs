using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Demanday.Columns
{
    [ColumnsScript("Demanday.DemandayTeleMarketingQualilty")]
    [BasedOnRow(typeof(DemandayTeleMarketingQualiltyRow), CheckNames = true)]
    public class DemandayTeleMarketingQualiltyColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Slot { get; set; }
        public String AgentsName { get; set; }
        public String CampaignId { get; set; }
        public String CompanyName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Title { get; set; }
        public String Email { get; set; }
        public String WorkPhone { get; set; }
        public String AlternativeNumber { get; set; }
        public String Street { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
        public String Country { get; set; }
        public String CompanyEmployeeSize { get; set; }
        public String Industry { get; set; }
        public String Revenue { get; set; }
        public String ProfileLink { get; set; }
        public String CompanyLink { get; set; }
        public String RevenueLink { get; set; }
        public String AddressLink { get; set; }
        public String PrimaryReason { get; set; }
        public String Category { get; set; }
        public String Comments { get; set; }
        public String QaStatus { get; set; }
        public String DeliveryStatus { get; set; }
        public String AgentName { get; set; }
        public String QaName { get; set; }
        public DateTime CallDate { get; set; }
        public DateTime DateAudited { get; set; }
        public DateTime DeliveryDate { get; set; }
        public String Source { get; set; }
        public String VerificationMode { get; set; }
        public String Asset1 { get; set; }
        public String Asset2 { get; set; }
        public String TlName { get; set; }
        public String Tenurity { get; set; }
        public String Code { get; set; }
        public String Link { get; set; }
        public String Md5 { get; set; }
        public String OwnerUsername { get; set; }
    }
}