
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

        // The dashboard is driven by the MIS modules only - one block per team.
        public MisDashboardStats EmailTeamMis { get; set; } = new MisDashboardStats();
        public MisDashboardStats TeleMarketingMis { get; set; } = new MisDashboardStats();
    }

    /// <summary>
    /// One team's MIS figures, worked out once per time window (Daily / Weekly / Monthly /
    /// Yearly) so the dashboard can switch between them without going back to the server.
    /// Everything here comes from that team's MIS module.
    /// </summary>
    public class MisDashboardStats
    {
        public List<MisPeriodStats> Periods { get; set; } = new List<MisPeriodStats>();
    }

    /// <summary>The tiles and the trend for one time window.</summary>
    public class MisPeriodStats
    {
        /// <summary>daily | weekly | monthly | yearly.</summary>
        public string Key { get; set; }
        /// <summary>Button caption, e.g. "Monthly".</summary>
        public string Title { get; set; }
        /// <summary>What the window covers, e.g. "Last 12 months".</summary>
        public string RangeLabel { get; set; }

        /// <summary>MIS rows dated inside this window.</summary>
        public int TotalLeads { get; set; }
        /// <summary>Rows whose QA Status is exactly "Qualified".</summary>
        public int QualifiedLeads { get; set; }
        /// <summary>Rows whose QA Status is exactly "Disqualified".</summary>
        public int DisqualifiedLeads { get; set; }
        /// <summary>Rows whose Comments carry "EBB" (matched case-sensitively).</summary>
        public int EbbCount { get; set; }
        /// <summary>Qualified rows as a percentage of the window's rows.</summary>
        public decimal QualifiedRate { get; set; }
        /// <summary>EBB rows as a percentage of the window's rows.</summary>
        public decimal EbbRatio { get; set; }
        /// <summary>This window's rows as a percentage of every row in the MIS module.</summary>
        public decimal ShareOfAll { get; set; }

        public List<QualityChartDataPoint> ChartData { get; set; } = new List<QualityChartDataPoint>();

        /// <summary>Per Master Account breakdown for this window, biggest Grand Total first.</summary>
        public List<MisAccountRow> Accounts { get; set; } = new List<MisAccountRow>();
    }

    /// <summary>One Master Account's line in the Account Wise Report.</summary>
    public class MisAccountRow
    {
        /// <summary>The account's Account Number, or a placeholder when the MIS row has none.</summary>
        public string AccountNumber { get; set; }
        public int Qualified { get; set; }
        public int Disqualified { get; set; }
        /// <summary>Every MIS row for this account in the window, whatever its QA Status.</summary>
        public int GrandTotal { get; set; }
        /// <summary>Qualified as a percentage of this account's Grand Total.</summary>
        public decimal QualifiedRate { get; set; }
    }

    public class QualityChartDataPoint
    {
        /// <summary>X-axis caption for the bucket: a day, a week start, a month or a year.</summary>
        public string Label { get; set; }
        public int QualifiedCount { get; set; }
        public int DisqualifiedCount { get; set; }
    }
}