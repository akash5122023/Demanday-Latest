using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Demanday.Forms
{
    [FormScript("Demanday.EnquiryContacts")]
    [BasedOnRow(typeof(EnquiryContactsRow), CheckNames = true)]
    public class EnquiryContactsForm
    {
        [Category("Accounts")]
        [HalfWidth]
        public string CompanyName { get; set; }
        [HalfWidth]
        public Int32? AccountTypeId { get; set; }
        [HalfWidth]
        public string Country { get; set; }
        [HalfWidth]
        public Int32? AccountStatusId { get; set; }

        [Category("RPC Contacts")]
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
        [HalfWidth]
        public string Industry { get; set; }
        [HalfWidth]
        public string CompanyEmployeeSize { get; set; }
        [HalfWidth]
        public string ProfileLink { get; set; }
        [HalfWidth]
        public string CompanyLink { get; set; }
        [HalfWidth]
        public Int32? OutreachModeId { get; set; }
        [HalfWidth]
        public Int32? DispositionId { get; set; }
        [FullWidth, TextAreaEditor(Rows = 3)]
        public string Comments { get; set; }

        [Category("L1 Meeting")]
        [HalfWidth, DateTimeEditor]
        public DateTime? L1Meeting { get; set; }
        [HalfWidth]
        public Int32? MeetingModeId { get; set; }
        [FullWidth, TextAreaEditor(Rows = 3)]
        public string MeetingStatus { get; set; }
        [HalfWidth, DateTimeEditor]
        public DateTime? FollowUpDate { get; set; }

        [Category("Representatives")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }

        // Not part of the Accounts / RPC Contacts layout - kept for reference, can be switched back on.
        //[Category("Campaign Information")]
        //[HalfWidth]
        //[LookupEditor(typeof(Masters.DemandayMasterAccountRow))]
        //public Int32? MasterAccountId { get; set; }
        //[HalfWidth]
        //[LookupEditor("Masters.DemandayCampaignIdByText", CascadeFrom = "MasterAccountId", CascadeField = "DemandayMasterAccountId")]
        //public string CampaignId { get; set; }
        //[HalfWidth]
        //public string SubIndustry { get; set; }
        //[HalfWidth]
        //public string Revenue { get; set; }
        //[FullWidth]
        //public string Street { get; set; }
        //[OneThirdWidth]
        //public string City { get; set; }
        //[OneThirdWidth]
        //public string State { get; set; }
        //[OneThirdWidth]
        //public string ZipCode { get; set; }
        //[HalfWidth]
        //public string RevenueLink { get; set; }
        //[HalfWidth]
        //public string AdressLink { get; set; }
        //public string Tenurity { get; set; }
        //[HalfWidth]
        //public string Code { get; set; }
        //[HalfWidth]
        //public string Md5 { get; set; }
    }
}
