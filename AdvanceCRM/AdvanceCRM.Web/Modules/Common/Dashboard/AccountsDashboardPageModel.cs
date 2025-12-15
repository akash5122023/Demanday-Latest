using AdvanceCRM.Contacts;
using AdvanceCRM.Enquiry;
using AdvanceCRM.Quotation;
using AdvanceCRM.Sales;
using AdvanceCRM.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdvanceCRM.Common
{
    public class AccountsDashboardPageModel
    {
        public string ListId { get; set; }
        public List<ContactsRow> ContactList { get; set; }
        public List<EnquiryRow> TotalEnquiryList { get; set; }
        public List<EnquiryRow> EnquiryWonList { get; set; }
        public List<EnquiryRow> EnquiryLostList { get; set; }
        public List<EnquiryRow> EnquiryStatusOpenList { get; set; }
        public List<EnquiryRow> EnquiryStatusClosedList { get; set; }
        public List<EnquiryRow> EnquiryStatusPendingList { get; set; }
        public List<QuotationRow> TotalQuotationList { get; set; }
        public List<QuotationRow> QuotationWonList { get; set; }
        public List<QuotationRow> QuotationLostList { get; set; }
        public List<QuotationRow> QuotationStatusOpenList { get; set; }
        public List<QuotationRow> QuotationStatusClosedList { get; set; }
        public List<QuotationRow> QuotationStatusPendingList { get; set; }
        public List<SalesRow> TotalSalesList { get; set; }
        public List<SalesRow> SalesStatusOpenList { get; set; }
        public List<SalesRow> SalesStatusClosedList { get; set; }
        public List<SalesRow> SalesStatusPendingList { get; set; }
        public List<SalesRow> SalesCashList { get; set; }
        public List<SalesRow> SalesCreditList { get; set; }
        public List<CMSRow> TotalCMSList { get; set; }
        public List<CMSRow> CMSStatusOpenList { get; set; }
        public List<CMSRow> CMSStatusClosedList { get; set; }
        public List<CMSRow> CMSStatusPendingList { get; set; }
        public List<AMCVisitPlannerRow> TotalAMCList { get; set; }
        public List<AMCVisitPlannerRow> AMCVisitsOpenList { get; set; }
        public List<AMCVisitPlannerRow> AMCVisitsClosedList { get; set; }
        public List<AMCVisitPlannerRow> AMCVisitsPendingList { get; set; }
        public Int32 EnquiryWon { get; set; }
        public Int32 EnquiryLost { get; set; }
        public Int32 TotalEnquiry { get; set; }
        public Int32 EnquiryStatusOpen { get; set; }
        public Int32 EnquiryStatusClosed { get; set; }
        public Int32 EnquiryStatusPending { get; set; }
        public Int32 QuotationWon { get; set; }
        public Int32 QuotationLost { get; set; }
        public Int32 TotalQuotation { get; set; }
        public Int32 QuotationStatusOpen { get; set; }
        public Int32 QuotationStatusClosed { get; set; }
        public Int32 QuotationStatusPending { get; set; }
        public Int32 TotalSales { get; set; }
        public Int32 SalesStatusOpen { get; set; }
        public Int32 SalesStatusClosed { get; set; }
        public Int32 SalesStatusPending { get; set; }
        public Int32 SalesCashCount { get; set; }
        public Int32 SalesCreditCount { get; set; }
    }
}