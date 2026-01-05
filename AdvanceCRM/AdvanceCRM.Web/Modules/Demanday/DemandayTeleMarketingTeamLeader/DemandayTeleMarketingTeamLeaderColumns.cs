using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Demanday.Columns
{
    [ColumnsScript("Demanday.DemandayTeleMarketingTeamLeader")]
    [BasedOnRow(typeof(DemandayTeleMarketingTeamLeaderRow), CheckNames = true)]
    public class DemandayTeleMarketingTeamLeaderColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        //public String TeleMarketingEnquiryCompanyName { get; set; }
        [EditLink]
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
        public String Tenurity { get; set; }
        public String Code { get; set; }
        public String Link { get; set; }
        public String Md5 { get; set; }
        public String OwnerUsername { get; set; }
    }
}