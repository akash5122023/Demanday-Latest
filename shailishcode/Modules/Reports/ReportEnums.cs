using Serenity.ComponentModel;
using System.ComponentModel;

namespace AdvanceCRM.Reports
{

    [EnumKey("Reports.CMSReportType")]
    public enum CMSReportType
    {
        [Description("Customerwise")]
        Customerwise = 1,

        //[Description("Representativewise")]
        //Representativewise = 7,

        //[Description("TeamWise")]
        //TeamWise = 9,
        [Description("ProjectWise")]
        ProjectWise = 10
    }

    [EnumKey("Reports.LeadsReportType")]
    public enum LeadsReportType
    {
        [Description("Customerwise")]
        Customerwise = 1,
        [Description("Divisionwise")]
        Divisionwise = 2,
        [Description("Statistics")]
        Statistics = 3,
        [Description("Lost Reasons")]
        LostReasons = 4,
        [Description("Mediawise")]
        Mediawise = 5,
        [Description("Productwise")]
        Productwise = 6,
        [Description("Representativewise")]
        Representativewise = 7,
        [Description("Detailed")]
        Detailed = 8,
        [Description("TeamWise")]
        TeamWise = 9
    }

    [EnumKey("Reports.StockReportType")]
    public enum StockReportType
    {
        [Description("Current Stock")]
        Current = 1,
        [Description("All-Branchwise")]
        AllBranchwise = 2,
        [Description("Branchwise")]
        Branchwise = 3,
        [Description("Divisionwise")]
        Divisionwise = 4,
        [Description("Groupwise")]
        Groupwise = 5,
        [Description("Reorder Stock")]
        Reorder = 6,
        [Description("All-Branch-DivisionWise")]
        AllBranchDivisionWise = 7,
        [Description("All-Branch-ProductWise")]
        AllBranchProductWise = 8,
        [Description("GRN-Report")]
        GRNReport = 9,
        [Description("Productwise-GRN-Report")]
        ProductwiseGRNReport = 10,
        [Description("Outward-Report")]
        OutwardReport = 11,
        [Description("Inward-Report")]
        InwardReport = 12,
        [Description("Productwise-Outward-Report")]
        ProductwiseOutwardReport = 13,
        [Description("Productwise-Inward-Report")]
        ProductwiseInwardReport = 14
    }

    [EnumKey("Reports.AttendanceReportType")]
    public enum AttendanceReportType
    {
        [Description("All Representativewise")]
        All = 1,
        [Description("Representativewise")]
        Representativewise = 2
    }

    [EnumKey("Reports.AccountingReportType")]
    public enum AccountingReportType
    {
        [Description("Cashbook")]
        Cashbook = 1,
        [Description("All Outstanding")]
        AllOutstanding = 2,
        [Description("Outstanding Balance")]
        OutstandingBalance = 3,
        [Description("All Supplier Outstanding")]
        AllSupplierOutstanding = 4,
        [Description("Supplier Outstanding Balance")]
        SupplierOutstandingBalance = 5,
        [Description("Ledger Balance")]
        LedgerBalance = 6,
        [Description("Contactwise Cashbook")]
        ContactwiseCashbook = 7
    }



    [EnumKey("Reports.SalesReportType")]
    public enum SalesReportType
    {
        [Description("Customerwise")]
        Customerwise = 1,
        [Description("Divisionwise")]
        Divisionwise = 2,
        [Description("Mediawise")]
        Mediawise = 3,
        [Description("Productwise")]
        Productwise = 4,
        [Description("Representativewise")]
        Representativewise = 5

    }




}