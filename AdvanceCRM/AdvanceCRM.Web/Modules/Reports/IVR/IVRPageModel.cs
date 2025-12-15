
using AdvanceCRM.Masters;
using AdvanceCRM.Enquiry;
using AdvanceCRM.ThirdParty;
using AdvanceCRM.Settings;
using AdvanceCRM.Administration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdvanceCRM.Modules.Reports.IVR
{
    public class IVRPageModel
    {
        public List<KnowlarityDetailsRow> IVRList { get; set; }
        public List<KnowlarityDetailsRow> calldurationlist { get; set; }
        public List<CompanyDetailsRow> CompanyList { get; set; }
        public List<EnquiryRow> EnquiryList { get; set; }
        public List<IVRConfigurationRow> IVRConfig { get; set; }
        public List<KnowlarityAgentsRow> Agentslist { get; set; }
        
        public string Agent { get; set; }
        public string AgentNo { get; set; }

        public int totalcalls { get; set; }

        public double avgcallduration { get; set; }
        public int totalenquiry { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }



    }


}