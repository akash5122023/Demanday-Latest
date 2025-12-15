using Serenity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Reports
{
    public class LeadsReportRequest : ServiceRequest
    {
        public LeadsReportType? Type { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Int32? Representative { get; set; }
        public Int32? Contact { get; set; }
        public Int32? TeamsId { get; set; }
        public Int32? Project { get; set; }
    }

    public class StockReportRequest : ServiceRequest
    {
        public StockReportType? Type { get; set; }
        public Int32? Branch { get; set; }
        public Int32? Division { get; set; }
        public Int32? Group { get; set; }
        public Int32? Product { get; set; }
        public Int32? Status { get; set; }
        public Int32? PurchaseOrderNo { get; set; }
    }

    public class AttendanceReportRequest : ServiceRequest
    {
        public AttendanceReportType? Type { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public Int32? Representative { get; set; }
    }

    public class VisitReportRequest : ServiceRequest
    {
        public AttendanceReportType? Type { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public Int32? Representative { get; set; }
    }

    public class SignInReportRequest : ServiceRequest
    {
        public Int32? Representative { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

    public class AccountingReportRequest : ServiceRequest
    { 
        public AccountingReportType? Type { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Int32? Contact { get; set; }
        public Int32? Head { get; set; }
        public Int32? Employee { get; set; }
        public Int32? Project { get; set; }
        public Int32? Bank { get; set; }

        public Boolean? CashIn { get; set; }
        public Boolean? CashOut { get; set; }
    }
    public class CMSReportRequest : ServiceRequest
    {
        public CMSReportType? Type { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Int32? Representative { get; set; }
        public Int32? Contact { get; set; }
        public Int32? TeamsId { get; set; }
        public Int32? Project { get; set; }
    }


    public class SalesReportRequest : ServiceRequest
    {
        public SalesReportType? Type { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Int32? Representative { get; set; }
        public Int32? Contact { get; set; }
        public Int32? TeamsId { get; set; }
    }

}