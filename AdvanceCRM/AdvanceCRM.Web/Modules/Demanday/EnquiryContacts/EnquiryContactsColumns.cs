using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Demanday.Columns
{
    [ColumnsScript("Demanday.EnquiryContacts")]
    [BasedOnRow(typeof(EnquiryContactsRow), CheckNames = true)]
    public class EnquiryContactsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String CompanyName { get; set; }
        [QuickFilter]
        public String AccountTypeName { get; set; }
        public String Country { get; set; }
        [QuickFilter]
        public String AccountStatusName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        [QuickFilter]
        public String Title { get; set; }
        public String Email { get; set; }
        public String WorkPhone { get; set; }
        public String AlternativeNumber { get; set; }
        [QuickFilter]
        public String Industry { get; set; }
        [QuickFilter]
        public String CompanyEmployeeSize { get; set; }
        public String ProfileLink { get; set; }
        public String CompanyLink { get; set; }
        [QuickFilter]
        public String OutreachModeName { get; set; }
        [QuickFilter]
        public String DispositionName { get; set; }
        public DateTime? L1Meeting { get; set; }
        [QuickFilter]
        public String MeetingModeName { get; set; }
        public String MeetingStatus { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public String Comments { get; set; }
        [QuickFilter]
        public String OwnerUsername { get; set; }

        // Not part of the Accounts / RPC Contacts layout - kept for reference, can be switched back on.
        //public Int32? MasterAccountId { get; set; }
        //public String MasterAccountNo { get; set; }
        //public String CampaignId { get; set; }
        //public String Street { get; set; }
        //public String City { get; set; }
        //public String State { get; set; }
        //public String ZipCode { get; set; }
        //public String SubIndustry { get; set; }
        //public String Revenue { get; set; }
        //public String RevenueLink { get; set; }
        //public String EmailFormat { get; set; }
        //public String AdressLink { get; set; }
        //public String Tenurity { get; set; }
        //public String Code { get; set; }
        //public String Link { get; set; }
        //public String Md5 { get; set; }
    }
}
