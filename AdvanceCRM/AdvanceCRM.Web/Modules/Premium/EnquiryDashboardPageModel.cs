
namespace AdvanceCRM.Modules
{
    using AdvanceCRM.Administration;
    
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Premium;

    public class EnquiryDashboardPageModel
    {
        public List<EnquiryRow> EnquiryStages { get; set; }
        public List<EnquiryRow> EnquiryWonLost { get; set; }
        public List<EnquiryRow> EnquirySource { get; set; }
        public List<EnquiryRow> CitywiseEnquiry { get; set; }
        public List<CityRow> City { get; set; }
        public List<BranchRow> Branch { get; set; }
        public List<EnquiryRow> BranchwiseEnquiry { get; set; }
        public List<EnquiryProductsRow> ProductwiseDivisionEnquiry { get; set; }
        public List<EnquiryProductsRow> MostEnquiryProduct { get; set; }
        public List<EnquiryProductsRow> LeastEnquiryProduct { get; set; }
        public List<EnquiryProductsRow> GroupMostEnquiryProduct { get; set; }
        public List<EnquiryProductsRow> GroupLeastEnquiryProduct { get; set; }
        public Int32 EnquiryStatusOpen { get; set; }
        public Int32 EnquiryStatusClosed { get; set; }
        public Int32 EnquiryStatusPending { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public List<UserRow> Users { get; set; }
        public String tStr { get; set; }
        public Int32 HotEnquiry { get; set; }
        public Int32 WarmEnquiry { get; set; }
        public Int32 ColdEnquiry { get; set; }
        public Int32 TotalEnquiryTypes { get; set; }
        public Int32 HotEndvalue { get; set; }
        public Int32 WarmEndvalue { get; set; }
        public Int32 ColdEndvalue { get; set; }
        public Int32 EnquiryWon { get; set; }
        public Int32 EnquiryLost { get; set; }
        public List<ProductsDivisionRow> DivisionList { get; set; }
        public List<EnquiryRow> EnquiryAnalysisList { get; set; }
        public List<TargetSettingRow> TargetEnquiry { get; set; }
        public List<EnquiryRow> TargetEnquiryAchieved { get; set; }
        public Double EnquiryTargetCount { get; set; }
        public Double EnquiryTargetAmount { get; set; }
        public Double RequiredTargetCount { get; set; }
        public Double RequiredTargetAmount { get; set; }
        public Double AchievedTargetCount { get; set; }
        public Double AchievedTargetAmount { get; set; }



    }
}