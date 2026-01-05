using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Demanday.Forms
{
    [FormScript("Demanday.DemandayTeleMarketingTeamLeader")]
    [BasedOnRow(typeof(DemandayTeleMarketingTeamLeaderRow), CheckNames = true)]
    public class DemandayTeleMarketingTeamLeaderForm
    {
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
        public string CompanyEmployeeSize { get; set; }

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
        [HalfWidth]
public string EmailFormat { get; set; }


        [Category("Additional Details")]
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