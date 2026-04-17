using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Toolkit.Columns
{
    [ColumnsScript("Toolkit.DemandaySpecs")]
    [BasedOnRow(typeof(DemandaySpecsRow), CheckNames = true)]
    public class DemandaySpecsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        public Int64 OrderId { get; set; }
        [EditLink]
        public String JobTitle { get; set; }
        public String JobLevel { get; set; }
        public String JobFunction { get; set; }
        public String Industry { get; set; }
        public String CompanyEmployeeSize { get; set; }
        public String AnnualRevenue { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
        public String Country { get; set; }
        public String Comments { get; set; }
        public String AdditionalNotes { get; set; }
        //public String MasterAccountAccountNumber { get; set; }
        //public String CampaignCampaignId { get; set; }
        public String OwnerUsername { get; set; }
    }
}