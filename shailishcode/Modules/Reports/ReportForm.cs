
using AdvanceCRM.Administration;
using AdvanceCRM.Contacts;
using AdvanceCRM.Employee;
using AdvanceCRM.Masters;
using AdvanceCRM.Products;
using AdvanceCRM.Purchase;
using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace AdvanceCRM.Reports.Forms
{
    [FormScript("Reports.LeadsReportForm")]
    public class LeadsReportForm { 


        [OneThirdWidth(UntilNext = true)]
        [DisplayName("Report Type"), Required]
        public LeadsReportType? Type { get; set; }
        [DisplayName("Date From"), Required]
        public DateTime DateFrom { get; set; }
        [DisplayName("Date To"), Required]
        public DateTime DateTo { get; set; }
        [DisplayName("Representative"), Required]
        [Administration.UserEditor]
        public Int32? Representative { get; set; }
        [DisplayName("Contact"), Required]
        [LookupEditor(typeof(ContactsRow))]
        public Int32? Contact { get; set; }
        //[LookupEditor(typeof(TeamsRow))]
        //public Int32? TeamsId { get;  set; }
        //[DisplayName("Project"), Required]
        //[LookupEditor(typeof(ProjectRow))]
        //public Int32? Project { get; set; }

    }

    [FormScript("Reports.StockReportForm")]
    public class StockReportForm
    {
        [OneThirdWidth(UntilNext = true)]
        [DisplayName("Report Type"), Required]
        public StockReportType? Type { get; set; }
        [DisplayName("Branch"), Required]
        [LookupEditor("Administration.BranchLookup")]
        public Int32? Branch { get; set; }
        [DisplayName("Division"), Required]
        [LookupEditor(typeof(ProductsDivisionRow))]
        public Int32? Division { get; set; }
        [DisplayName("Group"), Required]
        [LookupEditor(typeof(ProductsGroupRow))]
        public Int32? Group { get; set; }

        [DisplayName("Product"), Required]
        [LookupEditor(typeof(ProductsRow))]
        public Int32? Product { get; set; }

        [DisplayName("Status")]
        public Masters.StatusMaster? Status
        {
            get { return (Masters.StatusMaster?)StatusValue; }
            set { StatusValue = (Int32?)value; }
        }

        [Ignore]
        [LookupEditor(typeof(GrnTwoRow))]
        public Int32? StatusValue { get; set; }

        [DisplayName("PO Number")]
        //[LookupEditor(typeof(PurchaseOrderRow))]
        public Int32? PurchaseOrderNo { get; set; }


    }

    [FormScript("Reports.AttendanceReportForm")]
    public class AttendanceReportForm
    {
        [OneThirdWidth(UntilNext = true)]
        [DisplayName("Report Type"), Required]
        public AttendanceReportType? Type { get; set; }
       
        [DisplayName("Date From"), Required]
        public DateTime DateFrom { get; set; }
        [DisplayName("Date To"), Required]
        public DateTime DateTo { get; set; }

        [OneThirdWidth(UntilNext = true)]
        [DisplayName("Representative"), Required]
        [Administration.UserEditor]
        public Int32? Representative { get; set; }
    }
    [FormScript("Reports.VisitReportForm")]
    public class VisitReportForm
    {
        [OneThirdWidth(UntilNext = true)]
        [DisplayName("Report Type"), Required]
        public AttendanceReportType? Type { get; set; }

        [DisplayName("Date From"), Required]
        public DateTime DateFrom { get; set; }
        [DisplayName("Date To"), Required]
        public DateTime DateTo { get; set; }

        [OneThirdWidth(UntilNext = true)]
        [DisplayName("Representative"), Required]
        [Administration.UserEditor]
        public Int32? Representative { get; set; }
    }


    [FormScript("Reports.SignInReportForm")]
    public class SignInReportForm
    {
        [OneThirdWidth(UntilNext = true)]
        [DisplayName("Representative"), Required]
        [Administration.UserEditor]
        public Int32? Representative { get; set; }
     
        [DisplayName("Date From"), Required]
        public DateTime DateFrom { get; set; }
        [DisplayName("Date To"), Required]
        public DateTime DateTo { get; set; }
    }

    [FormScript("Reports.AccountingReportForm")]
    public class AccountingReportForm
    {
        [OneThirdWidth(UntilNext = true)]
        [DisplayName("Report Type"), Required]
        public AccountingReportType? Type { get; set; }
        [DisplayName("Date From"), Required]
        public DateTime DateFrom { get; set; }
        [DisplayName("Date To"), Required]
        public DateTime DateTo { get; set; }
        [DisplayName("Contact"), Required]
        [LookupEditor(typeof(ContactsRow))]
        public Int32? Contact { get; set; }
        [DisplayName("Head"), Required]
        [LookupEditor(typeof(AccountingHeadsRow))]
        public Int32? Head { get; set; }
        [DisplayName("Employee"), Required]
        [LookupEditor(typeof(EmployeeRow))]
        public Int32? Employee { get; set; }
        [DisplayName("Project"), Required]
        [LookupEditor(typeof(ProjectRow))]
        public Int32? Project { get; set; }
        [LookupEditor(typeof(BankMasterRow))]
        public Int32? Bank { get; set; }
        [DisplayName("CashIn")]
        [BSSwitchEditor]
        public Boolean? CashIn { get; set; }
        [DisplayName("CashOut")]
        [BSSwitchEditor]
        public Boolean? CashOut { get; set; }
    }

    [FormScript("Reports.CMSReportForm")]
    public class CMSReportForm
    {


        [OneThirdWidth(UntilNext = true)]
        [DisplayName("Report Type"), Required]
        public CMSReportType? Type { get; set; }
        [DisplayName("Date From"), Required]
        public DateTime DateFrom { get; set; }
        [DisplayName("Date To"), Required]
        public DateTime DateTo { get; set; }
        [DisplayName("Representative"), Required]
        [Administration.UserEditor]
        public Int32? Representative { get; set; }
        [DisplayName("Contact"), Required]
        [LookupEditor(typeof(ContactsRow))]
        public Int32? Contact { get; set; }
        //[LookupEditor(typeof(TeamsRow))]
        //public Int32? TeamsId { get; set; }
        //[DisplayName("Project"), Required]
        //[LookupEditor(typeof(ProjectRow))]
        //public Int32? Project { get; set; }
    }


    [FormScript("Reports.SalesReportForm")]
    public class SalesReportForm
    {


        [OneThirdWidth(UntilNext = true)]
        [DisplayName("Report Type"), Required]
        public SalesReportType? Type { get; set; }
        [DisplayName("Date From"), Required]
        public DateTime DateFrom { get; set; }
        [DisplayName("Date To"), Required]
        public DateTime DateTo { get; set; }
        [DisplayName("Representative"), Required]
        [Administration.UserEditor]
        public Int32? Representative { get; set; }
        [DisplayName("Contact"), Required]
        [LookupEditor(typeof(ContactsRow))]
        public Int32? Contact { get; set; }
        //[LookupEditor(typeof(TeamsRow))]
        //public Int32? TeamsId { get; set; }
    }

}