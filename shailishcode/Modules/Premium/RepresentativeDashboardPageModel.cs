using AdvanceCRM.Administration;

using AdvanceCRM.Enquiry;
using AdvanceCRM.Masters;
using AdvanceCRM.Premium;
using AdvanceCRM.Quotation;
using AdvanceCRM.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdvanceCRM.Modules.Premium
{
    public class RepresentativeDashboardPageModel
    {
        public string ListId { get; set; }
        public string ListName { get; set; }
        public List<UserRow> RepresentativeList { get; set; }
        public List<EnquiryRow> EnquiryStages { get; set; }
        public List<QuotationRow> QuotationStages { get; set; }
        public List<EnquiryProductsRow> GroupMostEnquiryProduct { get; set; }
        public List<EnquiryProductsRow> MostEnquiryProduct { get; set; }
        public List<SalesProductsRow> MostSalesProduct { get; set; }
        public List<SalesProductsRow> GroupMostSalesProduct { get; set; }
        public Int32 EnquiryStatusOpen { get; set; }
        public Int32 EnquiryStatusClosed { get; set; }
        public Int32 EnquiryStatusPending { get; set; }
        public Int32 QuotationStatusOpen { get; set; }
        public Int32 QuotationStatusClosed { get; set; }
        public Int32 QuotationStatusPending { get; set; }
        public Int32 SalesStatusOpen { get; set; }
        public Int32 SalesStatusClosed { get; set; }
        public Int32 SalesStatusPending { get; set; }
        public Int32 TotalEnquiryQuantiy { get; set; }
        public Int32 TotalQuotationQuantity { get; set; }
        public Int32 TotalSalesQuantity { get; set; }
        public Int32 EnquiryRatioVal { get; set; }
        public Int32 QuotationRatioVal { get; set; }
        public Int32 QuotationWon { get; set; }
        public List<EnquiryRow> TotalEnquiryAmount { get; set; }
        public List<QuotationRow> TotalQuotationAmount { get; set; }
        public List<SalesRow> TotalSalesAmount { get; set; }
        public Int32 TotalEnquiry { get; set; }
        public Int32 TotalQuotation { get; set; }
        public Int32 EnquiryWon { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM/DD/YYYY}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public List<CityRow> QuotCity { get; set; }
        public List<BranchRow> QuotBranch { get; set; }
        public List<QuotationRow> CitywiseQuotationList { get; set; }
        public List<QuotationRow> BranchwiseQuotationList { get; set; }
        public List<EnquiryRow> CitywiseEnquiry { get; set; }
        public List<CityRow> EnqCity { get; set; }
        public List<BranchRow> EnqBranch { get; set; }
        public List<EnquiryRow> BranchwiseEnquiry { get; set; }
        public List<CityRow> SalCity { get; set; }
        public List<BranchRow> SalBranch { get; set; }
        public List<SalesRow> CitywiseSalesList { get; set; }
        public List<SalesRow> BranchwiseSalesList { get; set; }

        //Representative Enquiry Animated Guage
        public List<TargetSettingRow> TargetEnquiry { get; set; }
        public List<EnquiryRow> TargetEnquiryAchieved { get; set; }
        public Double EnquiryTargetCount { get; set; }
        public Double EnquiryTargetAmount { get; set; }
        public Double Required_Target_Count_Enq { get; set; }
        public Double Required_Target_Amount_Enq { get; set; }
        public Double Achieved_Target_Count_Enq { get; set; }
        public Double Achieved_Target_Amount_Enq { get; set; }

        //Representative Quotation Animated Guage
        public List<TargetSettingRow> TargetQuotation { get; set; }
        public List<QuotationRow> TargetQuotationAchieved { get; set; }
        public Double QuotationTargetCount { get; set; }
        public Double QuotationTargetAmount { get; set; }
        public Double Required_Target_Count_Quot { get; set; }
        public Double Required_Target_Amount_Quot { get; set; }
        public Double Achieved_Target_Count_Quot { get; set; }
        public Double Achieved_Target_Amount_Quot { get; set; }

        //Representative Sales Animated Guage
        public List<TargetSettingRow> TargetSales { get; set; }
        public List<SalesRow> TargetSalesAchieved { get; set; }
        public Double SalesTargetCount { get; set; }
        public Double SalesTargetAmount { get; set; }
        public Double Required_Target_Count_Sal { get; set; }
        public Double Required_Target_Amount_Sal { get; set; }
        public Double Achieved_Target_Count_Sal { get; set; }
        public Double Achieved_Target_Amount_Sal { get; set; }

    }
}