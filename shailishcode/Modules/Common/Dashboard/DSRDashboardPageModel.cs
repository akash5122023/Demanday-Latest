
namespace AdvanceCRM.Common
{
    using Administration;
    using System.Collections.Generic;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Tasks;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Services;
    using AdvanceCRM.Sales;
    using System;

    public class DSRDashboardPageModel
    {
        public List<UserRow> Users { get; set; }
        public List<EnquiryRow> SourceWise { get; set; }
        public List<EnquiryRow> RepWise { get; set; }
        public int TotalEnq { get; set; }

        public int WonEnq { get; set; }
        public double? WonEnqAmt { get; set; }
        public int LostEnq { get; set; }
        public double? LostEnqAmt { get; set; }
        public double? EnqClosure { get; set; }
        public int totalQua { get; set; }

        public int WonQuo { get; set; }
        public double? WonQuoAmt { get; set; }
        public int LostQuo { get; set; }
        public double? LostQuoAmt { get; set; }
        public double? QuoClosure { get; set; }
        public int OpenSales { get; set; }
        public int CloseSales { get; set; }
        public int NewSales { get; set; }
        public int OverSales { get; set; }
        public double? OpenSalesAmt { get; set; }
        public double? CloseSalesAmt { get; set; }
        public double? NewSalesAmt { get; set; }
        public double? OverSalesAmt { get; set; }

        public int OpenPI { get; set; }
        public int ClosePI { get; set; }
        public int NewPI { get; set; }
        public int OverPI { get; set; }
        public double? OpenPIAmt { get; set; }
        public double? ClosePIAmt { get; set; }
        public double? NewPIAmt { get; set; }
        public double? OverPIAmt { get; set; }
        public int OpenTask { get; set; }
        public int CloseTask { get; set; }
        public int NewTask { get; set; }
        public int OverTask { get; set; }
        public int OpenCMS { get; set; }
        public int CloseCMS { get; set; }
        public int NewCMS { get; set; }
        public int OverCMS { get; set; }

        public List<EnquiryFollowupsRow> EnqFollowups { get; set; }
        public List<EnquiryFollowupsRow> ODEnqFollowups { get; set; }
        public List<QuotationFollowupsRow> QuotFollowups { get; set; }
        public List<QuotationFollowupsRow> ODQuotFollowups { get; set; }
        public List<InvoiceFollowupsRow> InvoiceFollowups { get; set; }
        public List<InvoiceFollowupsRow> ODInvoiceFollowups { get; set; }
        public List<CMSFollowupsRow> CMSFollowups { get; set; }
        public List<CMSFollowupsRow> ODCMSFollowups { get; set; }
        public List<TeleCallingFollowupsRow> TCFollowups { get; set; }
        public List<TeleCallingFollowupsRow> ODTCFollowups { get; set; }
    }
}