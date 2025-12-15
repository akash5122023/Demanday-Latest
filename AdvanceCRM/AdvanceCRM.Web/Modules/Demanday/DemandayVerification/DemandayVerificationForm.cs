using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Demanday.Forms
{
    [FormScript("Demanday.DemandayVerification")]
    [BasedOnRow(typeof(DemandayVerificationRow), CheckNames = true)]
    public class DemandayVerificationForm
    {
        [Category("📌 Lead Information")]
        [HalfWidth]
        public String AgentName { get; set; }

        [HalfWidth, TextAreaEditor(Rows = 3)]
        public String CdqaComments { get; set; }

        //[HalfWidth, LookupEditor(typeof(CampaignRow))]
        //public Int32 CampaignId { get; set; }

        [HalfWidth]
        public String CompanyName { get; set; }

        [Category("👤 Customer Details")]
        [HalfWidth]
        public String FirstName { get; set; }

        [HalfWidth]
        public String LastName { get; set; }

        [HalfWidth]
        public String Title { get; set; }

        [HalfWidth, EmailEditor]
        public String Email { get; set; }

        [Category("📞 Contact Numbers")]
        [HalfWidth]
        public String WorkPhone { get; set; }

        [HalfWidth]
        public String Alternate01 { get; set; }

        [HalfWidth]
        public String Alternate02 { get; set; }

        [Category("🌐 Other Details")]
        [HalfWidth]
        public String ProfileLink { get; set; }

        [Category("Representative")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }
        //public Int32 SrNo { get; set; }
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
        //public Int32 OwnerId { get; set; }
    }
}