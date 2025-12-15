using _Ext;
using AdvanceCRM.Administration;
using AdvanceCRM.Contacts;
using Serenity.Data;
using Serenity.Reporting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using AdvanceCRM.Purchase;
using AdvanceCRM.Sales;
using AdvanceCRM.Accounting;
using Serenity.Extensions.DependencyInjection;
using Serenity.Services;

namespace AdvanceCRM.Reports
{
    [Report("Reports.AccountingReport")]
    [ReportDesign(MVC.Views.Reports.Accounting.AccountingReport)]
    public class AccountingReport : ListReportBase, IReport
    {
        public new AccountingReportRequest Request { get; set; }

        public AccountingReport(IRequestContext context, ISqlConnections connections)
            : base(context, connections)
        {
        }

        public AccountingReport()
            : this(Dependency.Resolve<IRequestContext>(), Dependency.Resolve<ISqlConnections>())
        {
        }

        public object GetData()
        {
            using (var connection = SqlConnections.NewFor<CashbookRow>())
            {
                return new AccountingReportModel(connection, Request);
            }
        }
    }

    public class AccountingReportModel : ListReportModelBase
    {
        public new AccountingReportRequest Request { get; set; }
        public CashbookRow Head { get; set; }

        public CashbookRow EmployeeName { get; set; }

        public CashbookRow BankName { get; set; }
        public CashbookRow CashIn { get; set; }
        public CashbookRow ProjectName { get; set; }
        public List<CashbookRow> Cash { get; set; }
        public List<CashbookRow> OpeningBal { get; set; }
        public List<SalesRow> Sales { get; set; }
        public List<SalesRow> SalesOpeningBal { get; set; }
        public List<PurchaseRow> Purchase { get; set; }
        public List<PurchaseRow> PurchaseOpeningBal { get; set; }
        public List<ContactsRow> Contacts { get; set; }
        public ContactsRow Customer { get; set; }
        public CompanyDetailsRow Company { get; set; }

        public AccountingReportModel(IDbConnection connection, AccountingReportRequest request)
        {
            Request = request;
            var p = ContactsRow.Fields;
            var m = CashbookRow.Fields;
            var c = SalesRow.Fields;
            var d = PurchaseRow.Fields;

            if (Request.Type == AccountingReportType.AllOutstanding)
            {
                Cash = connection.List<CashbookRow>(q => q
                 .SelectTableFields()
                 .Select(m.Date)
                 .Select(m.CashIn)
                 .Select(m.CashOut)
                 .Select(m.ContactsName)
                 .Where(m.Type == 1)
                 .Where(m.Head == 1)
                 );

                Sales = connection.List<SalesRow>(q => q
                 .SelectTableFields()
                 .Select(c.Date)
                 .Select(c.Total)
                 .Select(c.GrandTotal)
                 .Select(c.ContactsName)
                 .Select(c.ContactsAddress)
                 .Select(c.ContactsPhone)
                 .Select(c.ContactsEmail)
                 .Select(c.ContactsDebtorsOpening)
                 .Select(c.ContactsCreditorsOpening)
                 .Where(c.Type == 2)
                 );

                Contacts = connection.List<ContactsRow>(q => q
                    .SelectTableFields()
                    .Select(p.Id)
                    .Select(p.Name)
                    .Select(p.Address)
                    .Select(p.Phone)
                    .Select(p.Email)
                    );
            }
            else if (Request.Type == AccountingReportType.Cashbook)
            {
                if (Request.Bank.HasValue)
                {
                    BankName = connection.TryFirst<CashbookRow>(q => q
                            .SelectTableFields()
                            .Select(m.BankBankName)
                            .Where(m.BankId == Request.Bank.Value)
                            );
                }

                Cash = connection.List<CashbookRow>(q => q
                 .SelectTableFields()
                 .Select(m.Date)
                 .Select(m.Type)
                 .Select(m.Head)
                 .Select(m.Head1)
                 .Select(m.ContactsName)
                 .Select(m.ContactsAddress)
                 .Select(m.InvoiceNo)
                 .Select(m.CashIn)
                 .Select(m.CashOut)
                 .Select(m.Narration)
                 .Select(m.BankBankName)
                 .Where(m.Date >= Request.DateFrom)
                 .Where(m.Date <= Request.DateTo)
                 );
                if (BankName != null)
                {
                    Cash = Cash.Where(m => m.BankBankName == BankName.BankBankName).ToList();
                }
                if (Request.CashIn == true)
                {
                    Cash = Cash.Where(m => m.CashIn != null).ToList();
                }
                if (Request.CashOut == true)
                {
                    Cash = Cash.Where(m => m.CashOut != null).ToList();
                }

                OpeningBal = connection.List<CashbookRow>(q => q
                 .SelectTableFields()
                 .Select(m.Date)
                 .Select(m.CashIn)
                 .Select(m.CashOut)
                 .Where(m.Date < Request.DateFrom)
                 );
            }
            else if (Request.Type == AccountingReportType.LedgerBalance)
            {
                Head = connection.TryFirst<CashbookRow>(q => q
                    .SelectTableFields()
                    .Select(m.Head1)
                    .Where(m.Head == Request.Head.Value)
                    );

                //Cash = connection.List<CashbookRow>(q => q
                // .SelectTableFields()
                // .Select(m.Date)
                // .Select(m.CashIn)
                // .Select(m.CashOut)
                // .Select(m.Head)
                // .Select(m.Head1)
                // .Select(m.Narration)
                // .Where(m.Date >= Request.DateFrom)
                // .Where(m.Date <= Request.DateTo)
                // .Where(m.Head1 == Head.Head1)
                // );
                if (Head != null && Head.Head1 == "Salaries" || Head.Head1 == "Employee")
                {

                    EmployeeName = connection.TryFirst<CashbookRow>(q => q
                            .SelectTableFields()
                            .Select(m.EmployeeName)
                            .Where(m.EmployeeId == Request.Employee.Value)
                            );
                    if (Request.Bank.HasValue)
                    {
                        BankName = connection.TryFirst<CashbookRow>(q => q
                                .SelectTableFields()
                                .Select(m.BankBankName)
                                .Where(m.BankId == Request.Bank.Value)
                                );
                    }

                    Cash = connection.List<CashbookRow>(q => q
                     .SelectTableFields()
                     .Select(m.Date)
                     .Select(m.CashIn)
                     .Select(m.CashOut)
                     .Select(m.Head)
                     .Select(m.Head1)
                     .Select(m.Narration)
                     .Select(m.BankBankName)
                     .Where(m.Date >= Request.DateFrom)
                     .Where(m.Date <= Request.DateTo)
                     .Where(m.Head1 == Head.Head1)
                     .Where(m.EmployeeName == EmployeeName.EmployeeName)
                     //.Where(m.BankBankName == BankName.BankBankName)
                     );
                    if (BankName != null)
                    {
                        Cash = Cash.Where(m => m.BankBankName == BankName.BankBankName).ToList();
                    }
                    OpeningBal = connection.List<CashbookRow>(q => q
                   .SelectTableFields()
                   .Select(m.Date)
                   .Select(m.CashIn)
                   .Select(m.CashOut)
                   .Select(m.Head)
                   .Select(m.Head1)
                   .Select(m.BankBankName)
                   .Where(m.Date < Request.DateFrom)
                   .Where(m.Head1 == Head.Head1)
                   .Where(m.EmployeeName == EmployeeName.EmployeeName)
                   );
                }

                else if (Head != null && Head.Head1 == "Project Cost" || Head.Head1 == "Projects")
                {

                    ProjectName = connection.TryFirst<CashbookRow>(q => q
                        .SelectTableFields()
                        .Select(m.ProjectName)
                        .Where(m.ProjectId == Request.Project.Value)
                        );
                    if (Request.Employee.HasValue)
                    {
                        EmployeeName = connection.TryFirst<CashbookRow>(q => q
                               .SelectTableFields()
                               .Select(m.EmployeeName)
                               .Where(m.EmployeeId == Request.Employee.Value)
                               );
                    }
                    //if (ProjectName != null && EmployeeName != null)
                    //{
                    Cash = connection.List<CashbookRow>(q => q
                     .SelectTableFields()
                     .Select(m.Date)
                     .Select(m.ProjectAmtIn)
                     .Select(m.Purpose)
                     .Select(m.EmployeeName)
                     .Select(m.CashIn)
                     .Select(m.CashOut)
                     .Select(m.Head)
                     .Select(m.Head1)
                     .Select(m.Narration)
                     .Select(m.BankBankName)
                     .Where(m.Date >= Request.DateFrom)
                     .Where(m.Date <= Request.DateTo)
                     //.Where(m.Head1 == Head.Head1)
                     .Where(m.ProjectName == ProjectName.ProjectName)
                     //.Where(m.EmployeeName == EmployeeName.EmployeeName)
                     );

                    //if (ProjectName != null)
                    //{
                    //    Cash = Cash.Where(m => m.ProjectName == ProjectName.ProjectName).ToList();
                    //}
                    if (EmployeeName != null)
                    {
                        Cash = Cash.Where(m => m.EmployeeName == EmployeeName.EmployeeName).ToList();
                    }
                    OpeningBal = connection.List<CashbookRow>(q => q
                       .SelectTableFields()
                       .Select(m.Date)
                       .Select(m.CashIn)
                       .Select(m.EmployeeName)
                       .Select(m.Purpose)
                       .Select(m.CashOut)
                       .Select(m.Head)
                       .Select(m.Head1)
                       .Select(m.BankBankName)
                       .Where(m.Date < Request.DateFrom)
                        //.Where(m.Head1 == Head.Head1)
                        .Where(m.ProjectName == ProjectName.ProjectName)
                       //.Where(m.EmployeeName == EmployeeName.EmployeeName)
                       );
                    if (EmployeeName != null)
                    {
                        OpeningBal = OpeningBal.Where(m => m.EmployeeName == EmployeeName.EmployeeName).ToList();
                    }
                    //}
                    //else
                    //{
                    // Throw an exception if ProjectName or EmployeeName is null
                    //    throw new Exception("Project name or employee name cannot be null. Please provide both values.");

                    //}
                }

                else
                {
                    Cash = connection.List<CashbookRow>(q => q
                     .SelectTableFields()
                     .Select(m.Date)
                     .Select(m.CashIn)
                     .Select(m.CashOut)
                     .Select(m.Head)
                     .Select(m.Head1)
                     .Select(m.Narration)
                     .Select(m.BankBankName)
                     .Where(m.Date >= Request.DateFrom)
                     .Where(m.Date <= Request.DateTo)
                     .Where(m.Head1 == Head.Head1)
                     );
                    OpeningBal = connection.List<CashbookRow>(q => q
                .SelectTableFields()
                .Select(m.Date)
                .Select(m.CashIn)
                .Select(m.CashOut)
                .Select(m.Head)
                .Select(m.Head1)
                .Select(m.BankBankName)
                .Where(m.Date < Request.DateFrom)
                .Where(m.Head1 == Head.Head1)
                );
                }
                //OpeningBal = connection.List<CashbookRow>(q => q
                // .SelectTableFields()
                // .Select(m.Date)
                // .Select(m.CashIn)
                // .Select(m.CashOut)
                // .Select(m.Head)
                // .Select(m.Head1)
                // .Where(m.Date < Request.DateFrom)
                // .Where(m.Head1 == Head.Head1)
                // );
            }
            else if (Request.Type == Reports.AccountingReportType.OutstandingBalance)
            {
                Cash = connection.List<CashbookRow>(q => q
                 .SelectTableFields()
                 .Select(m.Date)
                 .Select(m.CashIn)
                 .Select(m.ContactsName)
                 .Select(m.InvoiceNo)
                 .Select(m.Narration)
                 .Where(m.Date >= Request.DateFrom)
                 .Where(m.Date <= Request.DateTo)
                 .Where(m.ContactsId == Request.Contact.Value)
                 .Where(m.Type == 1)
                 .Where(m.Head == 1)
                 );

                OpeningBal = connection.List<CashbookRow>(q => q
                 .SelectTableFields()
                 .Select(m.Date)
                 .Select(m.CashIn)
                 .Select(m.ContactsName)
                 .Select(m.InvoiceNo)
                 .Select(m.Narration)
                 .Where(m.Date < Request.DateFrom)
                 .Where(m.ContactsId == Request.Contact.Value)
                 .Where(m.Type == 1)
                 .Where(m.Head == 1)
                 );

                Sales = connection.List<SalesRow>(q => q
                 .SelectTableFields()
                 .Select(c.InvoiceN)
                // .Select(c.Narration)
                 .Select(c.Date)
                 .Select(c.Total)
                 .Select(c.GrandTotal)
                 .Select(c.ContactsName)
                 .Select(c.ContactsCreditorsOpening)
                 .Select(c.ContactsDebtorsOpening)
                 .Where(c.Date >= Request.DateFrom)
                 .Where(c.Date <= Request.DateTo)
                 .Where(c.ContactsId == Request.Contact.Value)
                 .Where(c.Type == 2)
                 );

                SalesOpeningBal = connection.List<SalesRow>(q => q
                 .SelectTableFields()
                  .Select(c.InvoiceN)
                 // .Select(c.Narration)
                 .Select(c.Date)
                 .Select(c.Total)
                 .Select(c.GrandTotal)
                 .Select(c.ContactsName)
                 .Where(c.Date < Request.DateFrom)
                 .Where(c.ContactsId == Request.Contact.Value)
                 .Where(c.Type == 2)
                 );

                Customer = connection.TryById<Contacts.ContactsRow>(Request.Contact.Value, q => q
                    .SelectTableFields()
                    .Select(p.Name)
                    );
            }
            else if (Request.Type == Reports.AccountingReportType.AllSupplierOutstanding)
            {
                Cash = connection.List<CashbookRow>(q => q
                  .SelectTableFields()
                  .Select(m.Date)
                  .Select(m.CashIn)
                  .Select(m.ContactsName)
                  .Where(m.Type == 2)
                  .Where(m.Head == 11)
                  );

                Purchase = connection.List<PurchaseRow>(q => q
                 .SelectTableFields()
                 .Select(d.InvoiceDate)
                 .Select(d.Total)
                 .Select(d.PurchaseFromName)
                 .Select(d.PurchaseFromCreditorsOpening)
                 .Where(d.Type == 2)
                 );

                Contacts = connection.List<ContactsRow>(q => q
                    .SelectTableFields()
                    .Select(p.Id)
                    .Select(p.Name)
                    .Select(p.Address)
                    );
            }
            else if (Request.Type == Reports.AccountingReportType.SupplierOutstandingBalance)
            {
                Cash = connection.List<CashbookRow>(q => q
                 .SelectTableFields()
                 .Select(m.Date)
                 .Select(m.CashIn)
                 .Select(m.ContactsName)
                 .Where(m.Date >= Request.DateFrom)
                 .Where(m.Date <= Request.DateTo)
                 .Where(m.ContactsId == Request.Contact.Value)
                 .Where(m.Type == 2)
                 .Where(m.Head == 11)
                 );

                OpeningBal = connection.List<CashbookRow>(q => q
                 .SelectTableFields()
                 .Select(m.Date)
                 .Select(m.CashIn)
                 .Select(m.ContactsName)
                 .Where(m.Date < Request.DateFrom)
                 .Where(m.ContactsId == Request.Contact.Value)
                 .Where(m.Type == 2)
                 .Where(m.Head == 11)
                 );

                Purchase = connection.List<PurchaseRow>(q => q
                 .SelectTableFields()
                 .Select(d.InvoiceDate)
                 .Select(d.Total)
                 .Select(d.PurchaseFromName)
                 .Select(d.PurchaseFromCreditorsOpening)
                 .Where(d.InvoiceDate >= Request.DateFrom)
                 .Where(d.InvoiceDate <= Request.DateTo)
                 .Where(d.PurchaseFromId == Request.Contact.Value)
                 .Where(d.Type == 2)
                 );

                PurchaseOpeningBal = connection.List<PurchaseRow>(q => q
                 .SelectTableFields()
                 .Select(d.InvoiceDate)
                 .Select(d.Total)
                 .Select(d.PurchaseFromName)
                 .Where(d.InvoiceDate < Request.DateFrom)
                 .Where(d.PurchaseFromId == Request.Contact.Value)
                 .Where(d.Type == 2)
                 );

                Customer = connection.TryById<Contacts.ContactsRow>(Request.Contact.Value, q => q
                    .SelectTableFields()
                    .Select(p.Name)
                    );
            }





            else if (Request.Type == AccountingReportType.ContactwiseCashbook)
            {
                //if (Request.Bank.HasValue)
                //{
                //    BankName = connection.TryFirst<CashbookRow>(q => q
                //            .SelectTableFields()
                //            .Select(m.BankBankName)
                //            .Where(m.BankId == Request.Bank.Value)
                //            );
                //}

                Cash = connection.List<CashbookRow>(q => q
                 .SelectTableFields()
                 .Select(m.Date)
                 .Select(m.Type)
                 .Select(m.Head)
                 .Select(m.Head1)
                 .Select(m.ContactsName)
                 .Select(m.ContactsAddress)
                 .Select(m.InvoiceNo)
                 .Select(m.CashIn)
                 .Select(m.CashOut)
                 .Select(m.Narration)
                 .Select(m.BankBankName)
                 .Where(m.Date >= Request.DateFrom)
                 .Where(m.Date <= Request.DateTo)
                 );
                if (BankName != null)
                {
                    Cash = Cash.Where(m => m.BankBankName == BankName.BankBankName).ToList();
                }
                if (Request.CashIn == true)
                {
                    Cash = Cash.Where(m => m.CashIn != null).ToList();
                }
                if (Request.CashOut == true)
                {
                    Cash = Cash.Where(m => m.CashOut != null).ToList();
                }

                OpeningBal = connection.List<CashbookRow>(q => q
                 .SelectTableFields()
                 .Select(m.Date)
                 .Select(m.CashIn)
                 .Select(m.CashOut)
                 .Where(m.Date < Request.DateFrom)
                 );
           

             Customer = connection.TryById<Contacts.ContactsRow>(Request.Contact.Value, q => q
                    .SelectTableFields()
                    .Select(p.Name)
                    );
            }





            var cmp = CompanyDetailsRow.Fields;
            Company = connection.TryById<CompanyDetailsRow>(1, q => q
                .SelectTableFields()
                .Select(cmp.Name)
                .Select(cmp.Slogan)
                .Select(cmp.Address)
                .Select(cmp.Phone)
                .Select(cmp.Logo)
                .Select(cmp.LogoHeight)
                .Select(cmp.LogoWidth)
                );
        }
    }
}