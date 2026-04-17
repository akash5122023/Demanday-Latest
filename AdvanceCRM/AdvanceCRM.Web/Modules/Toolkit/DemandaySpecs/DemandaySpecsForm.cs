using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Toolkit.Forms
{
    [FormScript("Toolkit.DemandaySpecs")]
    [BasedOnRow(typeof(DemandaySpecsRow), CheckNames = true)]
    public class DemandaySpecsForm
    {
        [Category("Specs Information")]
        [HalfWidth]
        public Int64 OrderId { get; set; }
        [HalfWidth]
        public String JobTitle { get; set; }
        [HalfWidth]
        public String JobLevel { get; set; }
        [HalfWidth]
        public String JobFunction { get; set; }
        [HalfWidth]
        public String Industry { get; set; }
        [HalfWidth]
        public String CompanyEmployeeSize { get; set; }
        [HalfWidth]
        public String AnnualRevenue { get; set; }
        [Category("Address Information")]
        [HalfWidth]
        public String Address { get; set; }
        [HalfWidth]
        public String City { get; set; }
        [HalfWidth]
        public String State { get; set; }
        [HalfWidth]
        public String ZipCode { get; set; }
        [HalfWidth]
        public String Country { get; set; }
        [FullWidth]
        public String Comments { get; set; }
        [FullWidth]
        public String AdditionalNotes { get; set; }
        //public Int32 MasterAccountId { get; set; }
        //public Int32 CampaignId { get; set; }
        [Category("Representatives")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }
    }
}