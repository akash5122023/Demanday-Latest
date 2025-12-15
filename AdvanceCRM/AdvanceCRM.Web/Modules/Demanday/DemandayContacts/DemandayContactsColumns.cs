using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Demanday.Columns
{
    [ColumnsScript("Demanday.DemandayContacts")]
    [BasedOnRow(typeof(DemandayContactsRow), CheckNames = true)]
    public class DemandayContactsColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        //[EditLink]
        //public String AgentsName { get; set; }
        //public String TlName { get; set; }
        //public String CampaignId { get; set; }
        //public String CompanyName { get; set; }
        //public String FirstName { get; set; }
        //public String LastName { get; set; }
        //public String Domain { get; set; }
        //public String JobLevel { get; set; }
        //public String JobFunctionRole { get; set; }
        //public String Title { get; set; }
        //public String Email { get; set; }
        //public String WorkPhone { get; set; }
        //public String AlternativeNumber { get; set; }
        //public String Street { get; set; }
        //public String City { get; set; }
        //public String State { get; set; }
        //public String ZipCode { get; set; }
        //public String Country { get; set; }
        //public String Continents { get; set; }
        //public Int32 CompanyEmployeeSize { get; set; }
        //public String Industry { get; set; }
        //public Decimal Revenue { get; set; }
        //public String ProfileLink { get; set; }
        //public String CompanyLink { get; set; }
        //public String RevenueLink { get; set; }
        //public String ProspectUrl { get; set; }
        //public String EmailFormat { get; set; }
        //public String AdressLink { get; set; }
        //public String Tenurity { get; set; }
        //public String Code { get; set; }
        //public String Link { get; set; }
        //public String Md5 { get; set; }
        //public String Slot { get; set; }
        //public String PrimaryReason { get; set; }
        //public String Category { get; set; }
        //public String Comments { get; set; }
        //public String QaStatus { get; set; }
        //public String DeliveryStatus { get; set; }
        //public String AgentName { get; set; }
        //public String QaName { get; set; }
        //public DateTime CallDate { get; set; }
        //public DateTime DateAudited { get; set; }
        //public DateTime DeliveryDate { get; set; }
        //public String Source { get; set; }
        //public String VerificationMode { get; set; }
        //public String Asset1 { get; set; }
        //public String Asset2 { get; set; }
        //public String OwnerUsername { get; set; }
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public string Slot { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Domain { get; set; }
        public string Title { get; set; }
        public string JobLevel { get; set; }
        public string JobFunctionRole { get; set; }
        public string Email { get; set; }
        public string WorkPhone { get; set; }
        public string AlternativeNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Continents { get; set; }
        public string Industry { get; set; }
        public decimal? Revenue { get; set; }
        public string ProfileLink { get; set; }
        public string CompanyLink { get; set; }
        public string RevenueLink { get; set; }

        public int CompanyEmployeeSize { get; set; }
        public string EmailFormat { get; set; }
        public string AdressLink { get; set; }
        public string ProspectUrl { get; set; }
        public string PrimaryReason { get; set; }
        public string Category { get; set; }
        public string Comments { get; set; }
        public string QaStatus { get; set; }
        public string DeliveryStatus { get; set; }
        public string AgentName { get; set; }
        public string QaName { get; set; }
        public System.DateTime? CallDate { get; set; }
        public System.DateTime? DateAudited { get; set; }
        public System.DateTime? DeliveryDate { get; set; }
        public string Source { get; set; }
        public string VerificationMode { get; set; }
        public string Asset1 { get; set; }
        public string Asset2 { get; set; }
        public string TlName { get; set; }
        public string Tenurity { get; set; }
        public string Code { get; set; }
        public string Link { get; set; }
        public string Md5 { get; set; }
        [QuickFilter]
        public String OwnerUsername { get; set; }
    }
}