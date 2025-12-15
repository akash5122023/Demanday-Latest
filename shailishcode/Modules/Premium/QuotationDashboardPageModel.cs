using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System;
using AdvanceCRM.Masters;
using AdvanceCRM.Quotation;
using AdvanceCRM.Premium;
using AdvanceCRM.Administration;

namespace AdvanceCRM.Modules
{
    public class QuotationDashboardPageModel
    {
        public List<QuotationRow> QuotationStages { get; set; }
        public List<QuotationRow> QuotationWonLost { get; set; }
        public List<QuotationRow> QuotationSource { get; set; }
        public List<QuotationProductsRow> ProductwiseDivisionQuotation { get; set; }
        public List<QuotationProductsRow> MostQuotationProduct { get; set; }
        public List<QuotationProductsRow> LeastQuotationProduct { get; set; }
        public List<QuotationProductsRow> GroupMostQuotationProduct { get; set; }
        public List<QuotationProductsRow> GroupLeastQuotationProduct { get; set; }
        public List<ProductsDivisionRow> DivisionList { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public Int32 QuotationStatusOpen { get; set; }
        public Int32 QuotationStatusClosed { get; set; }
        public Int32 QuotationStatusPending { get; set; }
        public Int32 HotQuotation { get; set; }
        public Int32 WarmQuotation { get; set; }
        public Int32 ColdQuotation { get; set; }
        public Int32 TotalQuotationTypes { get; set; }
        public Int32 HotEndvalue { get; set; }
        public Int32 WarmEndvalue { get; set; }
        public Int32 ColdEndvalue { get; set; }
        public Int32 QuotationWon { get; set; }
        public Int32 QuotationLost { get; set; }
        public List<CityRow> City { get; set; }
        public List<BranchRow> Branch { get; set; }
        public List<QuotationRow> QuotationAnalysisList { get; set; }
        public List<QuotationRow> CitywiseQuotationList { get; set; }
        public List<QuotationRow> BranchwiseQuotationList { get; set; }
        public List<TargetSettingRow> TargetQuotation { get; set; }
        public List<QuotationRow> TargetQuotationAchieved { get; set; }
        public Double QuotationTargetCount { get; set; }
        public Double QuotationTargetAmount { get; set; }
        public Double RequiredTargetCount { get; set; }
        public Double RequiredTargetAmount { get; set; }
        public Double AchievedTargetCount { get; set; }
        public Double AchievedTargetAmount { get; set; }

    }
}