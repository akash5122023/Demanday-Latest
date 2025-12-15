
namespace AdvanceCRM.Modules.Premium
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    using System;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Sales;
    using AdvanceCRM.Premium;
    using AdvanceCRM.Administration;

    public class SalesDashboardPageModel
    {
        public List<SalesRow> SalesSource { get; set; }
        public List<SalesRow> SalesWonLost { get; set; }
        public List<SalesProductsRow> ProductwiseDivisionSales { get; set; }
        public List<ProductsDivisionRow> DivisionList { get; set; }
        public List<SalesProductsRow> MostSalesProduct { get; set; }
        public List<SalesProductsRow> GroupMostSalesProduct { get; set; }
        public List<SalesRow> SalesAnalysisList { get; set; }
        public List<SalesRow> SalesAnalysisYearlyList { get; set; }
        public List<SalesRow> LastthreeMonthsSalesAnalysisList { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public Int32 SalesStatusOpen { get; set; }
        public Int32 SalesStatusClosed { get; set; }
        public Int32 SalesStatusPending { get; set; }
        public Int32 SalesWon { get; set; }
        public Int32 SalesLost { get; set; }
        public Int32 TotalSalesTypes { get; set; }
        public List<CityRow> City { get; set; }
        public List<BranchRow> Branch { get; set; }
        public List<SalesRow> CitywiseSalesList { get; set; }
        public List<SalesRow> BranchwiseSalesList { get; set; }
        public List<TargetSettingRow> TargetSales { get; set; }
        public List<SalesRow> TargetSalesAchieved { get; set; }
        public Double SalesTargetCount { get; set; }
        public Double SalesTargetAmount { get; set; }
        public Double RequiredTargetCount { get; set; }
        public Double RequiredTargetAmount { get; set; }
        public Double AchievedTargetCount { get; set; }
        public Double AchievedTargetAmount { get; set; }

    }
}





