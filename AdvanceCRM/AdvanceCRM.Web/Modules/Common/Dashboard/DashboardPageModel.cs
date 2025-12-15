
namespace AdvanceCRM.Common
{
    using Administration;
    using System.Collections.Generic;
    using System;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Tasks;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Services;
    using AdvanceCRM.Purchase;
    using AdvanceCRM.Sales;
    using AdvanceCRM.Accounting;
    

    public class DashboardPageModel
    {
        public int OpenEnq { get; set; }
        public int OpenQuot { get; set; }
        public int CustomerCount { get; set; }
        public int OpenTasks { get; set; }
        public int OpenCMS { get; set; }
        public int OpenAMC { get; set; }
        public int Opensale { get; set; }
        public int OpenPi { get; set; }
        public int EnqAmt { get; set; }
        public int QuotAmt { get; set; }
        public int SaleAmt  { get; set; }
        public int PIAmt { get; set; }
        public List<ContactsRow> Customer { get; set; }
        public List<UserRow> Users { get; set; }
        public List<EnquiryRow> EnqListChart { get; set; }
        public List<QuotationRow> QuotListChart { get; set; }

        public List<QuotationRow> QuotApprovalList { get; set; }
        public List<InvoiceRow> InvoiceApprovalList { get; set; }
        public List<SalesRow> SalesApprovalList { get; set; }
        public List<ChallanRow> ChallanApprovalList { get; set; }
        public List<PurchaseRow> PurchaseApprovalList { get; set; }
        public List<CashbookRow> CashbookApprovalList { get; set; }

        public List<TasksRow> Tasks { get; set; }

        public List<TasksRow> TasksOpen { get; set; }
        public List<TasksRow> TasksCompleted { get; set; }
        public List<TasksRow> ODTasks { get; set; }
        public List<CMSRow> CMS { get; set; }
        public List<CMSRow> CMSCompleted { get; set; }
        public List<CMSRow> ODCMS { get; set; }
        public List<AMCVisitPlannerRow> AMC { get; set; }

        public List<AMCRow> AMCED { get; set; }
        public List<AMCRow> AMCOD { get; set; }
        public List<AMCVisitPlannerRow> AMCCompleted { get; set; }
        public List<AMCVisitPlannerRow> ODAMC { get; set; }
        public List<SalesRow> SalesPaymentDue { get; set; }
        public List<EnquiryFollowupsRow> EnqFollowups { get; set; }
        public List<EnquiryFollowupsRow> EnqFollowupsCompleted { get; set; }
        public List<EnquiryFollowupsRow> ODEnqFollowups { get; set; }
        public List<QuotationFollowupsRow> QuotFollowups { get; set; }
        public List<QuotationFollowupsRow> QuotFollowupsCompleted { get; set; }
        public List<QuotationFollowupsRow> ODQuotFollowups { get; set; }
        public List<InvoiceFollowupsRow> InvoiceFollowups { get; set; }
        public List<InvoiceFollowupsRow> InvoiceFollowupsCompleted { get; set; }
        public List<InvoiceFollowupsRow> ODInvoiceFollowups { get; set; }
        public List<SalesFollowupsRow> SalesFollowups { get; set; }
        public List<SalesFollowupsRow> SalesFollowupsCompleted { get; set; }
        public List<SalesFollowupsRow> ODSalesFollowups { get; set; }
        public List<CMSFollowupsRow> CMSFollowups { get; set; }
        public List<CMSFollowupsRow> CMSFollowupsCompleted { get; set; }
        public List<CMSFollowupsRow> ODCMSFollowups { get; set; }
        public List<TeleCallingFollowupsRow> TCFollowups { get; set; }
        public List<TeleCallingFollowupsRow> TCCompleted { get; set; }
        public List<TeleCallingFollowupsRow> ODTCFollowups { get; set; }
        public List<EnquiryAppointmentsRow> EnquiryAppointmentList { get; set; }
        public List<QuotationAppointmentsRow> QuotationAppointmentList { get; set; }
        public List<TeleCallingAppointmentsRow> TeleAppointmentList { get; set; }
        public List<InvoiceAppointmentsRow> InvoiceAppointmentList { get; set; }
        public List<AMCVisitPlannerRow> AMCAppointmentList { get; set; }
        public int CountOfBirthDayWishes { get; set; }
        public int CountOfAniversaryWishes { get; set; }
        public int CountOfIncorporationWishes { get; set; }
        public Boolean StockData { get; set; }
    }
}