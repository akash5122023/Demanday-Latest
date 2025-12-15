using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Demanday.Forms
{
    [FormScript("Demanday.DemandayTeleMarketingQualilty")]
    [BasedOnRow(typeof(DemandayTeleMarketingQualiltyRow), CheckNames = true)]
    public class DemandayTeleMarketingQualiltyForm
    {
        [Category("Account Information")]
        //[HalfWidth]
        //public string AccountID { get; set; }
        [HalfWidth]
        public string Slot { get; set; }

        [Category("Contact Information")]
        [HalfWidth]
        public string FirstName { get; set; }
        [HalfWidth]
        public string LastName { get; set; }
        [HalfWidth]
        public string Title { get; set; }
        [HalfWidth]
        public string Email { get; set; }
        [HalfWidth]
        public string WorkPhone { get; set; }
        [HalfWidth]
        public string AlternativeNumber { get; set; }

        [Category("Company Information")]
        [HalfWidth]
        public string CompanyName { get; set; }
        [HalfWidth]
        public string Industry { get; set; }
        [HalfWidth]
        public decimal? Revenue { get; set; }

        [HalfWidth, DisplayName("Employee Size")]
        public int? CompanyEmployeeSize { get; set; }

        [Category("Address Information")]
        [FullWidth]
        public string Street { get; set; }
        [OneThirdWidth]
        public string City { get; set; }
        [OneThirdWidth]
        public string State { get; set; }
        [OneThirdWidth]
        public string ZipCode { get; set; }
        [HalfWidth]
        public string Country { get; set; }

        [Category("Links & References")]
        [HalfWidth]
        public string ProfileLink { get; set; }
        [HalfWidth]
        public string CompanyLink { get; set; }
        [HalfWidth]
        public string RevenueLink { get; set; }
        [HalfWidth]
        public string AddressLink { get; set; }
        //[HalfWidth]
        //public string EmailFormat { get; set; }
        [HalfWidth]
        public string Link { get; set; }

        [Category("Status & Dates")]
        [OneThirdWidth]
        public string QaStatus { get; set; }
        [OneThirdWidth]
        public string DeliveryStatus { get; set; }
        [OneThirdWidth]
        public string Category { get; set; }
        [OneThirdWidth, DateTimeEditor]
        public DateTime? CallDate { get; set; }
        [OneThirdWidth, DateTimeEditor]
        public DateTime? DateAudited { get; set; }
        [OneThirdWidth, DateTimeEditor]
        public DateTime? DeliveryDate { get; set; }

        [Category("Team & Representatives")]
        [OneThirdWidth]
        public string AgentName { get; set; }
        [OneThirdWidth]
        public string QaName { get; set; }
        [OneThirdWidth]
        public string TlName { get; set; }

        [Category("Additional Details")]
        [HalfWidth]
        public string PrimaryReason { get; set; }
        [FullWidth, TextAreaEditor(Rows = 3)]
        public string Comments { get; set; }
        [HalfWidth]
        public string Source { get; set; }
        [HalfWidth]
        public string VerificationMode { get; set; }
        [HalfWidth]
        public string Asset1 { get; set; }
        [HalfWidth]
        public string Asset2 { get; set; }
        [HalfWidth]
        public string Tenurity { get; set; }
        [HalfWidth]
        public string Code { get; set; }
        [HalfWidth]
        public string Md5 { get; set; }
        [Category("Representative")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }
    }
}