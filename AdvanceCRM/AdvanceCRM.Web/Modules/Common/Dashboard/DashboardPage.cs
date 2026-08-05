
namespace AdvanceCRM.Common.Pages
{
    using Serenity;
    using AdvanceCRM.Web.Helpers;
    using Serenity.Data;
    using Microsoft.AspNetCore.Mvc;
    using System;
    
    using Administration;
    using System.IO;
    using System.Net;
    using AdvanceCRM.Reports;
    using System.Data;
    using System.Linq;
    using Serenity.Services;
    using System.Net.Mail;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Products;
    using AdvanceCRM.Tasks;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Settings;
    using AdvanceCRM.Common;
    using AdvanceCRM.Services;
    using AdvanceCRM.Purchase;
    using AdvanceCRM.Sales;
    using AdvanceCRM.Demanday;
    using System.Collections.Generic;
    using AdvanceCRM.Accounting;
    using AdvanceCRM.Common.Calendar;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Authorization;
    [Route("Dashboard")]
    public class DashboardController : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IMemoryCache _cache;
        private readonly ILogger<CalendarController> _logger;
        private readonly IRequestContext Context;

        public DashboardController(ISqlConnections connections, IRequestContext context, IMemoryCache cache, ILogger<CalendarController> logger)
        {
            _connections = connections;
            _cache = cache;
            _logger = logger;
            Context = context ?? throw new ArgumentNullException(nameof(context));

        }
        /// <summary>
        /// Turns one MIS module's rows into the dashboard tiles + trend, worked out once for
        /// every time window the user can pick. All four are sent to the page together so
        /// switching between Daily and Yearly costs nothing.
        /// </summary>
        /// <param name="rows">QA Status, Comments, effective date and Master Account, per MIS record.</param>
        /// <param name="accountNames">Master Account key to Account Number.</param>
        private static MisDashboardStats BuildMisStats(
            IEnumerable<(string QaStatus, string Comments, DateTime? Date, int? MasterAccountId)> rows,
            IDictionary<int, string> accountNames)
        {
            // Materialised once - every window walks the same list.
            var list = rows.ToList();
            var today = DateTime.Today;
            var stats = new MisDashboardStats();

            var firstDay = today.AddDays(-29);
            stats.Periods.Add(BuildPeriod(list, accountNames, "daily", "Daily", "Last 30 days", 30,
                d => (d.Date - firstDay).Days,
                i => firstDay.AddDays(i).ToString("dd MMM")));

            var firstWeek = StartOfWeek(today).AddDays(-77);
            stats.Periods.Add(BuildPeriod(list, accountNames, "weekly", "Weekly", "Last 12 weeks", 12,
                d => (StartOfWeek(d) - firstWeek).Days / 7,
                i => firstWeek.AddDays(i * 7).ToString("dd MMM")));

            var firstMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-11);
            stats.Periods.Add(BuildPeriod(list, accountNames, "monthly", "Monthly", "Last 12 months", 12,
                d => (d.Year * 12 + d.Month) - (firstMonth.Year * 12 + firstMonth.Month),
                i => firstMonth.AddMonths(i).ToString("MMM yy")));

            var firstYear = today.Year - 4;
            stats.Periods.Add(BuildPeriod(list, accountNames, "yearly", "Yearly", "Last 5 years", 5,
                d => d.Year - firstYear,
                i => (firstYear + i).ToString()));

            // How much of the whole module each window covers - the Total Leads tile's percentage.
            if (list.Count > 0)
            {
                foreach (var period in stats.Periods)
                    period.ShareOfAll = Math.Round(period.TotalLeads * 100m / list.Count, 1);
            }

            return stats;
        }

        /// <summary>
        /// Fills one time window: <paramref name="bucketOf"/> places a row's date on the axis and
        /// <paramref name="labelOf"/> names each bucket. Only rows landing inside the window count,
        /// so the tiles and the chart always describe the same set of records.
        /// </summary>
        private static MisPeriodStats BuildPeriod(
            IEnumerable<(string QaStatus, string Comments, DateTime? Date, int? MasterAccountId)> rows,
            IDictionary<int, string> accountNames,
            string key, string title, string rangeLabel, int bucketCount,
            Func<DateTime, int> bucketOf, Func<int, string> labelOf)
        {
            var period = new MisPeriodStats { Key = key, Title = title, RangeLabel = rangeLabel };
            for (int i = 0; i < bucketCount; i++)
                period.ChartData.Add(new QualityChartDataPoint { Label = labelOf(i) });

            // Keyed by Master Account; -1 collects rows that carry no account at all, so the
            // report's Grand Totals still add up to the tiles above it.
            var byAccount = new Dictionary<int, MisAccountRow>();

            foreach (var row in rows)
            {
                // An undated MIS row cannot be placed in any window, so it is left out of all of them.
                if (!row.Date.HasValue)
                    continue;

                var idx = bucketOf(row.Date.Value);
                if (idx < 0 || idx >= bucketCount)
                    continue;

                period.TotalLeads++;

                var accountKey = row.MasterAccountId ?? -1;
                if (!byAccount.TryGetValue(accountKey, out var account))
                {
                    account = new MisAccountRow
                    {
                        AccountNumber = accountKey >= 0 && accountNames.TryGetValue(accountKey, out var name)
                            && !string.IsNullOrWhiteSpace(name) ? name : "(No account)"
                    };
                    byAccount[accountKey] = account;
                }
                account.GrandTotal++;

                if (string.Equals(row.QaStatus, "Qualified", StringComparison.Ordinal))
                {
                    period.QualifiedLeads++;
                    period.ChartData[idx].QualifiedCount++;
                    account.Qualified++;
                }
                else if (string.Equals(row.QaStatus, "Disqualified", StringComparison.Ordinal))
                {
                    period.DisqualifiedLeads++;
                    period.ChartData[idx].DisqualifiedCount++;
                    account.Disqualified++;
                }

                // "EBB" is matched case-sensitively on purpose - a comment reading "ebb" is not
                // the marker. This runs here rather than in SQL because the database collation is
                // case-insensitive and a LIKE would count both spellings.
                if (!string.IsNullOrEmpty(row.Comments) &&
                    row.Comments.Contains("EBB", StringComparison.Ordinal))
                    period.EbbCount++;
            }

            if (period.TotalLeads > 0)
            {
                period.QualifiedRate = Math.Round(period.QualifiedLeads * 100m / period.TotalLeads, 1);
                period.EbbRatio = Math.Round(period.EbbCount * 100m / period.TotalLeads, 1);
            }

            foreach (var account in byAccount.Values)
            {
                if (account.GrandTotal > 0)
                    account.QualifiedRate = Math.Round(account.Qualified * 100m / account.GrandTotal, 1);
            }

            period.Accounts = byAccount.Values
                .OrderByDescending(a => a.GrandTotal)
                .ThenBy(a => a.AccountNumber, StringComparer.OrdinalIgnoreCase)
                .ToList();

            return period;
        }

        /// <summary>Monday of the week the given date falls in.</summary>
        private static DateTime StartOfWeek(DateTime date)
        {
            var offset = ((int)date.DayOfWeek + 6) % 7;
            return date.Date.AddDays(-offset);
        }

        [Authorize, HttpGet, Route("~/")]
        public IActionResult Index()
        {
            var cachedModel = LocalCache.GetLocalStoreOnly("DashboardPageModel", TimeSpan.FromSeconds(1),
                UserRow.Fields.GenerationKey, () =>
                {
                    var model = new DashboardPageModel();

                    var e = EnquiryRow.Fields;
                    var q = QuotationRow.Fields;
                    var t = TasksRow.Fields;
                    var CMS = CMSRow.Fields;
                    var a = AMCVisitPlannerRow.Fields;
                    var amc = AMCRow.Fields;
                    var ef = EnquiryFollowupsRow.Fields;
                    var qf = QuotationFollowupsRow.Fields;
                    var c = ContactsRow.Fields;
                    var sc = SubContactsRow.Fields;
                    var u = UserRow.Fields;
                    var inv = InvoiceFollowupsRow.Fields;
                    var invo = InvoiceRow.Fields;
                    var sal = SalesFollowupsRow.Fields;
                    var sale = SalesRow.Fields;
                    var tel = TeleCallingFollowupsRow.Fields;
                    var CMSf = CMSFollowupsRow.Fields;
                    var p = ProductsRow.Fields;
                    var pp = PurchaseProductsRow.Fields;
                    var sp = SalesProductsRow.Fields;
                    var prp = PurchaseReturnProductsRow.Fields;
                    var srp = SalesReturnProductsRow.Fields;
                    var cp = ChallanProductsRow.Fields;
                    var ch = ChallanRow.Fields;
                    var pur = PurchaseRow.Fields;
                    var cash = CashbookRow.Fields;



                    var user = (UserDefinition)Context.User.ToUserDefinition();

                    using (var connection = _connections.NewFor<EnquiryRow>())
                    {
                        model.OpenEnq = connection.Count<EnquiryRow>(e.Status == 1 && e.AssignedId == user.UserId);
                        model.OpenQuot = connection.Count<QuotationRow>(q.Status == 1 && q.AssignedId == user.UserId);
                        model.CustomerCount = connection.Count<ContactsRow>(c.AssignedId == user.UserId);
                        model.OpenTasks = connection.Count<TasksRow>(t.StatusId ==1 && t.AssignedTo == user.UserId);
                        model.OpenCMS = connection.Count<CMSRow>(CMS.Status == 1 && CMS.AssignedTo == user.UserId);
                        model.OpenAMC = connection.Count<AMCVisitPlannerRow>(a.Status == 1 && a.AssignedTo == user.UserId);
                        model.Opensale = connection.Count<SalesRow>(sale.Status == 1 && sale.AssignedId == user.UserId);
                        model.OpenPi = connection.Count<InvoiceRow>(invo.Status == 1 && invo.AssignedId == user.UserId);

                        try
                        {
                            // Campaign Performance is MIS-only: each team's tiles, its trend and its
                            // Account Wise Report are built from that team's MIS module, nothing else.
                            // MIS stores only the account key, so its number is looked up here.
                            var accFields = Masters.DemandayMasterAccountRow.Fields;
                            var accountNames = connection.List<Masters.DemandayMasterAccountRow>(q => q
                                    .Select(accFields.Id)
                                    .Select(accFields.AccountNumber))
                                .Where(a => a.Id != null)
                                .GroupBy(a => a.Id.Value)
                                .ToDictionary(g => g.Key, g => g.First().AccountNumber);

                            var etMis = connection.List<DemandayMisRow>(q => q
                                .Select(DemandayMisRow.Fields.QaStatus)
                                .Select(DemandayMisRow.Fields.Comments)
                                .Select(DemandayMisRow.Fields.MasterAccountId)
                                .Select(DemandayMisRow.Fields.Date)
                                .Select(DemandayMisRow.Fields.DateAudited)
                                .Select(DemandayMisRow.Fields.CallDate)
                            );
                            model.EmailTeamMis = BuildMisStats(etMis.Select(r =>
                                (r.QaStatus, r.Comments, r.Date ?? r.DateAudited ?? r.CallDate, r.MasterAccountId)),
                                accountNames);

                            var tmMis = connection.List<DemandayTeleMarketingMISRow>(q => q
                                .Select(DemandayTeleMarketingMISRow.Fields.QaStatus)
                                .Select(DemandayTeleMarketingMISRow.Fields.Comments)
                                .Select(DemandayTeleMarketingMISRow.Fields.MasterAccountId)
                                .Select(DemandayTeleMarketingMISRow.Fields.Date)
                                .Select(DemandayTeleMarketingMISRow.Fields.DateAudited)
                                .Select(DemandayTeleMarketingMISRow.Fields.CallDate)
                            );
                            model.TeleMarketingMis = BuildMisStats(tmMis.Select(r =>
                                (r.QaStatus, r.Comments, r.Date ?? r.DateAudited ?? r.CallDate, r.MasterAccountId)),
                                accountNames);
                        }
                        catch { }

                        model.Customer = connection.List<ContactsRow>(f => f
                         .SelectTableFields()
                         .Select(c.Id)
                         .Select(c.Name)
                         );

                        var EnqAmtList = connection.List<EnquiryRow>(f => f
                          .SelectTableFields()
                          .Select(e.Id)
                          .Select(e.Total)
                          .Where(e.Total > 0)
                          .Where(e.Status==1)
                          .Where(e.AssignedId == user.UserId)
                        );

                        model.EnqAmt = 0;
                        foreach (var item in EnqAmtList)
                        {
                            //++model.amtcaselock;
                            model.EnqAmt += (Int32)item.Total;
                        }
                        //Quot
                        var QuotAmtList = connection.List<QuotationRow>(f => f
                          .SelectTableFields()
                          .Select(q.Id)
                          .Select(q.Total)
                          .Where(q.Total > 0)
                          .Where(q.Status == 1)
                          .Where(q.AssignedId == user.UserId)
                        );

                        model.QuotAmt = 0;
                        foreach (var item in QuotAmtList)
                        {
                            //++model.amtcaselock;
                            model.QuotAmt += (Int32)item.Total;
                        }

                        //pi
                        var PiAmtList = connection.List<InvoiceRow>(f => f
                          .SelectTableFields()
                          .Select(invo.Id)
                          .Select(invo.Total)
                          .Where(invo.Total > 0)
                          .Where(invo.Status == 1)
                          .Where(invo.AssignedId == user.UserId)
                        );

                        model.PIAmt = 0;
                        foreach (var item in PiAmtList)
                        {
                            //++model.amtcaselock;
                            model.PIAmt += (Int32)item.Total;
                        }
                        //pi
                        var saleAmtList = connection.List<SalesRow>(f => f
                          .SelectTableFields()
                          .Select(sale.Id)
                          .Select(sale.Total)
                          .Where(sale.Total > 0)
                          .Where(sale.Status == 1)
                          .Where(sale.AssignedId == user.UserId)
                        );

                        model.SaleAmt = 0;
                        foreach (var item in saleAmtList)
                        {
                            //++model.amtcaselock;
                            model.SaleAmt += (Int32)item.Total;
                        }

                        model.EnqFollowups = connection.List<EnquiryFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(ef.EnquiryId)
                         .Select(ef.EnquiryContactsId)
                         .Select(ef.FollowupNote)
                         .Select(ef.Details)
                         .Where(ef.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(ef.EnquiryAssignedId == user.UserId)
                         );

                        model.EnqFollowupsCompleted = connection.List<EnquiryFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(ef.EnquiryId)
                         .Select(ef.EnquiryContactsId)
                         .Select(ef.FollowupNote)
                         .Select(ef.Details)
                         .Where(ef.Status == 2)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(ef.EnquiryAssignedId == user.UserId)
                         );

                        model.QuotFollowups = connection.List<QuotationFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(qf.QuotationId)
                         .Select(qf.QuotationContactsId)
                         .Select(qf.FollowupNote)
                         .Select(qf.Details)
                         .Where(qf.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(qf.QuotationAssignedId == user.UserId)
                         );

                        model.QuotFollowupsCompleted = connection.List<QuotationFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(qf.QuotationId)
                         .Select(qf.QuotationContactsId)
                         .Select(qf.FollowupNote)
                         .Select(qf.Details)
                         .Where(qf.Status == 2)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(qf.QuotationAssignedId == user.UserId)
                         );


                        //model.QuotApprovalList = connection.List<QuotationRow>(g => g
                        // .SelectTableFields()
                        // .Select(q.Id)
                        // .Select(q.ContactsId)
                        // .Select(q.DisGrandTotal)
                        // .Where(q.Status == 2)
                        // .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                        // .Where(q.QuotationAssignedId == user.UserId)
                        // );


                        //model.QuotApprovalList = connection.List<QuotationRow>(g => g
                        //.SelectTableFields()
                        //.Select(q.Id)
                        //.Select(q.ContactsId)
                        //.Select(q.QuotationN)
                        //.Where(q.ApprovedBy.IsNull())
                        //);

                        //model.InvoiceApprovalList = connection.List<InvoiceRow>(g => g
                        //.SelectTableFields()
                        //.Select(invo.InvoiceNo)
                        //.Select(invo.ContactsId)
                        //.Select(invo.GrandTotal)
                        //.Where(invo.ApprovedBy.IsNull())
                        //);

                        //model.SalesApprovalList = connection.List<SalesRow>(g => g
                        //.SelectTableFields()
                        //.Select(sale.Id)
                        //.Select(sale.ContactsId)
                        //.Select(sale.GrandTotal)
                        //.Where(sale.ApprovedBy.IsNull())
                        //);

                        //model.ChallanApprovalList = connection.List<ChallanRow>(g => g
                        //.SelectTableFields()
                        //.Select(ch.Id)
                        //.Select(ch.ContactsId)
                        //.Select(ch.Date)
                        //.Where(ch.ApprovedBy.IsNull())
                        //);

                        //model.PurchaseApprovalList = connection.List<PurchaseRow>(g => g
                        //.SelectTableFields()
                        //.Select(pur.Id)
                        //.Select(pur.PurchaseFromId)
                        //.Select(pur.Total)
                        //.Where(pur.ApprovedBy.IsNull())
                        //);

                        //model.CashbookApprovalList = connection.List<CashbookRow>(g => g
                        //.SelectTableFields()
                        //.Select(cash.Id)
                        //.Select(cash.Type)
                        //.Select(cash.Date)
                        //.Where(cash.ApprovedBy.IsNull())
                        //);

                        model.ODEnqFollowups = connection.List<EnquiryFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(ef.EnquiryId)
                         .Select(ef.EnquiryContactsId)
                         .Select(ef.FollowupNote)
                         .Select(ef.Details)
                         .Where(ef.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(ef.EnquiryAssignedId == user.UserId)
                         );

                        model.ODQuotFollowups = connection.List<QuotationFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(qf.QuotationId)
                         .Select(qf.QuotationContactsId)
                         .Select(qf.FollowupNote)
                         .Select(qf.Details)
                         .Where(qf.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(qf.QuotationAssignedId == user.UserId)
                         );

                        model.Tasks = connection.List<TasksRow>(ts => ts
                         .SelectTableFields()
                         .Select(t.Id)
                         .Select(t.Task)
                         .Select(t.Details)
                         .Select(t.CreationDate)
                         .Select(t.ExpectedCompletion)
                         .Select(t.AssignedBy)
                         .Select(t.AssignedTo)
                         .Where(t.StatusId != 2)
                         .Where(new Criteria("CAST(CreationDate as DATE)<=" + DateTime.Now.ToSqlDate()))
                         .Where(new Criteria("CAST(ExpectedCompletion as DATE)>" + DateTime.Now.ToSqlDate()))
                         .Where(t.AssignedTo == user.UserId)
                         );

                        model.TasksOpen = connection.List<TasksRow>(ts => ts
                       .SelectTableFields()
                       .Select(t.Id)
                       .Select(t.Task)
                       .Select(t.Details)
                       .Select(t.CreationDate)
                       .Select(t.ExpectedCompletion)
                       .Select(t.AssignedBy)
                       .Select(t.AssignedTo)
                       .Where(t.StatusId != 2)
                       .Where(new Criteria("CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate()))
                       .Where(t.AssignedTo == user.UserId)
                       );

                        model.TasksCompleted = connection.List<TasksRow>(ts => ts
                         .SelectTableFields()
                         .Select(t.Id)
                         .Select(t.Task)
                         .Select(t.Details)
                         .Select(t.CreationDate)
                         .Select(t.ExpectedCompletion)
                         .Select(t.AssignedBy)
                         .Select(t.AssignedTo)
                         .Where(t.StatusId == 2)
                         .Where(new Criteria("CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(t.AssignedTo == user.UserId)
                         );

                        model.ODTasks = connection.List<TasksRow>(td => td
                         .SelectTableFields()
                         .Select(t.Id)
                         .Select(t.Task)
                         .Select(t.Details)
                         .Select(t.CreationDate)
                         .Select(t.ExpectedCompletion)
                         .Select(t.AssignedBy)
                         .Select(t.AssignedTo)
                         .Where(t.StatusId != 2)
                         .Where(new Criteria("CAST(ExpectedCompletion as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(t.AssignedTo == user.UserId)
                         );

                        model.CMS = connection.List<CMSRow>(ts => ts
                         .SelectTableFields()
                         .Select(CMS.Id)
                         .Select(CMS.ContactsName)
                         .Select(CMS.ContactsPhone)
                         .Select(CMS.Date)
                         .Select(CMS.ProductsName)
                         .Select(CMS.ComplaintComplaintType)
                         .Select(CMS.Instructions)
                         .Where(CMS.Status == 1)
                         .Where(new Criteria("CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(CMS.AssignedTo == user.UserId)
                         );

                        model.CMSCompleted = connection.List<CMSRow>(ts => ts
                         .SelectTableFields()
                         .Select(CMS.Id)
                         .Select(CMS.ContactsName)
                         .Select(CMS.ContactsPhone)
                         .Select(CMS.Date)
                         .Select(CMS.ProductsName)
                         .Select(CMS.ComplaintComplaintType)
                         .Select(CMS.Instructions)
                         .Where(CMS.Status == 2)
                         .Where(new Criteria("CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(CMS.AssignedTo == user.UserId)
                         );

                        model.ODCMS = connection.List<CMSRow>(td => td
                         .Select(CMS.Id)
                         .Select(CMS.ContactsName)
                         .Select(CMS.Date)
                         .Select(CMS.ProductsName)
                         .Select(CMS.ContactsPhone)
                         .Select(CMS.ComplaintComplaintType)
                         .Select(CMS.Instructions)
                         .Where(CMS.Status == 1)
                         .Where(new Criteria("CAST(ExpectedCompletion as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(CMS.AssignedTo == user.UserId)
                         );

                        model.InvoiceFollowups = connection.List<InvoiceFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(inv.InvoiceId)
                         .Select(inv.InvoiceContactsId)
                         .Select(inv.FollowupNote)
                         .Select(inv.Details)
                         .Where(inv.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(inv.InvoiceAssignedId == user.UserId)
                         );

                        model.InvoiceFollowupsCompleted = connection.List<InvoiceFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(inv.InvoiceId)
                         .Select(inv.InvoiceContactsId)
                         .Select(inv.FollowupNote)
                         .Select(inv.Details)
                         .Where(inv.Status == 2)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(inv.InvoiceAssignedId == user.UserId)
                         );

                        model.ODInvoiceFollowups = connection.List<InvoiceFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(inv.InvoiceId)
                         .Select(inv.InvoiceContactsId)
                         .Select(inv.FollowupNote)
                         .Select(inv.Details)
                         .Where(inv.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(inv.InvoiceAssignedId == user.UserId)
                         );

                        model.SalesFollowups = connection.List<SalesFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(sal.SalesId)
                         .Select(sal.SalesContactsId)
                         .Select(sal.FollowupNote)
                         .Select(sal.Details)
                         .Where(sal.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(sal.SalesAssignedId == user.UserId)
                         );

                        model.SalesFollowupsCompleted = connection.List<SalesFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(sal.SalesId)
                         .Select(sal.SalesContactsId)
                         .Select(sal.FollowupNote)
                         .Select(sal.Details)
                         .Where(sal.Status == 2)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(sal.SalesAssignedId == user.UserId)
                         );

                        model.ODSalesFollowups = connection.List<SalesFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(sal.SalesId)
                         .Select(sal.SalesContactsId)
                         .Select(sal.FollowupNote)
                         .Select(sal.Details)
                         .Where(sal.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(sal.SalesAssignedId == user.UserId)
                         );

                        model.SalesPaymentDue = connection.List<SalesRow>(f => f
                         .SelectTableFields()
                         .Select(sale.Id)
                         .Select(sale.Date)
                         .Select(sale.ContactsName)
                         .Select(sale.ContactsPhone)
                         .Select(sale.ContactsCreditDays)
                         .Select(sale.Type)
                         .Select(sale.Total)
                         .Where(sale.Status == 1)
                         .Where(sale.Type == 2)
                         .Where(sale.AssignedId == user.UserId)
                         );


                        //Stock check for reorder level
                        #region Stockdata
                        model.StockData = false;

                        Products = connection.List<ProductsRow>(q1 => q1
                         .SelectTableFields()
                         .Select(p.Name)
                         .Select(p.OpeningStock)
                         .Select(p.MinimumStock)
                         .Select(p.MaximumStock)
                         .Where(p.RawMaterial == 0)
                         );

                        PurchaseProducts = connection.List<PurchaseProductsRow>(q2 => q2
                                 .SelectTableFields()
                                 .Select(pp.ProductsName)
                                 .Select(pp.Quantity)
                                 .Select(pp.Price)
                                 );

                        SalesProducts = connection.List<SalesProductsRow>(q4 => q4
                         .SelectTableFields()
                         .Select(sp.ProductsName)
                         .Select(sp.Quantity)
                         .Select(sp.Price)
                         );

                        PurchaseReturnProducts = connection.List<PurchaseReturnProductsRow>(q5 => q5
                         .SelectTableFields()
                         .Select(prp.ProductsName)
                         .Select(prp.Quantity)
                         .Select(prp.Price)
                         );

                        SalesReturnProducts = connection.List<SalesReturnProductsRow>(q6 => q6
                         .SelectTableFields()
                         .Select(srp.ProductsName)
                         .Select(srp.Quantity)
                         .Select(srp.Price)
                         );

                        ChallanProducts = connection.List<ChallanProductsRow>(q7 => q7
                         .SelectTableFields()
                         .Select(cp.ProductsName)
                         .Select(cp.Quantity)
                         .Select(cp.Price)
                         .Where(cp.ChallanInvoiceMade != 1)
                         );

                        double pqty = 0; double sqty = 0; double prqty = 0; double srqty = 0; double cqty = 0; double qty = 0;

                        foreach (var item in Products)
                        {
                            pqty = (double)PurchaseProducts.Where(y => y.ProductsName == item.Name).Sum(x => x.Quantity);

                            sqty = (double)SalesProducts.Where(y => y.ProductsName == item.Name).Sum(x => x.Quantity);

                            prqty = (double)PurchaseReturnProducts.Where(y => y.ProductsName == item.Name).Sum(x => x.Quantity);

                            srqty = (double)SalesReturnProducts.Where(y => y.ProductsName == item.Name).Sum(x => x.Quantity);

                            cqty = (double)ChallanProducts.Where(y => y.ProductsName == item.Name).Sum(x => x.Quantity);

                            qty = pqty + srqty + item.OpeningStock.Value - (sqty + prqty + cqty);

                            if (qty < item.MinimumStock)
                            {
                                model.StockData = true;
                            }
                        }
                        #endregion Stockdata

                     model.AMCED = connection.List<AMCRow>(ts => ts
                    .SelectTableFields()
                    .Select(amc.Id)
                    .Select(amc.ContactsId)
                    .Select(amc.ContactsPhone)
                    .Where(amc.Status == 1)
                    .Where(new Criteria("CAST(EndDate as DATE)=" + DateTime.Now.ToSqlDate()))
                    .Where(amc.AssignedId == user.UserId)
                    );

                     model.AMCOD = connection.List<AMCRow>(ts => ts
                    .SelectTableFields()
                    .Select(amc.Id)
                    .Select(amc.ContactsId)
                    .Select(amc.ContactsPhone)
                    .Where(amc.Status == 1)
                    .Where(new Criteria("CAST(EndDate as DATE)<" + DateTime.Now.ToSqlDate()))
                    .Where(amc.AssignedId == user.UserId)
                    );

                        model.AMC = connection.List<AMCVisitPlannerRow>(ts => ts
                     .SelectTableFields()
                     .Select(a.Id)
                     .Select(a.AMCContactsId)
                     .Select(a.VisitDetails)
                     .Where(a.Status == 1)
                     .Where(new Criteria("CAST(VisitDate as DATE)=" + DateTime.Now.ToSqlDate()))
                     .Where(a.AssignedTo == user.UserId)
                     );

                        model.AMCCompleted = connection.List<AMCVisitPlannerRow>(ts => ts
                         .SelectTableFields()
                         .Select(a.Id)
                         .Select(a.AMCContactsId)
                         .Select(a.VisitDetails)
                         .Where(a.Status == 2)
                         .Where(new Criteria("CAST(VisitDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(a.AssignedTo == user.UserId)
                         );

                        model.ODAMC = connection.List<AMCVisitPlannerRow>(ts => ts
                         .SelectTableFields()
                         .Select(a.Id)
                         .Select(a.AMCContactsId)
                         .Select(a.VisitDetails)
                         .Where(a.Status == 1)
                         .Where(new Criteria("CAST(VisitDate as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(a.AssignedTo == user.UserId)
                         );

                        model.Users = connection.List<UserRow>(us => us
                         .SelectTableFields()
                         .Select(u.UserId)
                         .Select(u.Username)
                         );

                        model.EnqListChart = connection.List<EnquiryRow>(el => el
                         .SelectTableFields()
                         .Select(e.Id)
                         .Where(e.Date <= (DateTime.Now.Date))
                         .Where(e.Date > (DateTime.Now.Date.AddDays(-9)))
                         .Where(e.AssignedId == user.UserId)
                         );

                        model.QuotListChart = connection.List<QuotationRow>(ql => ql
                         .SelectTableFields()
                         .Select(q.Id)
                         .Where(q.Date <= (DateTime.Now.Date))
                         .Where(q.Date > (DateTime.Now.Date.AddDays(-9)))
                         .Where(q.AssignedId == user.UserId)
                         );


                        //TeleCalling Followups
                        model.TCFollowups = connection.List<TeleCallingFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(tel.TeleCallingId)
                         .Select(tel.TeleCallingContactsId)
                         .Select(tel.FollowupNote)
                         .Select(tel.Details)
                         .Where(tel.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(tel.RepresentativeId == user.UserId)
                         );

                        model.TCCompleted = connection.List<TeleCallingFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(tel.TeleCallingId)
                         .Select(tel.TeleCallingContactsId)
                         .Select(tel.FollowupNote)
                         .Select(tel.Details)
                         .Where(tel.Status == 2)
                         .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                         .Where(tel.RepresentativeId == user.UserId)
                         );

                        model.ODTCFollowups = connection.List<TeleCallingFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(tel.TeleCallingId)
                         .Select(tel.TeleCallingContactsId)
                         .Select(tel.FollowupNote)
                         .Select(tel.Details)
                         .Where(tel.Status == 1)
                         .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                         .Where(tel.RepresentativeId == user.UserId)
                         );

                        model.CMSFollowups = connection.List<CMSFollowupsRow>(f => f
                              .SelectTableFields()
                              .Select(CMSf.CMSId)
                              .Select(CMSf.CMSContactsId)
                              .Select(CMSf.FollowupNote)
                              .Select(CMSf.Details)
                              .Select(CMSf.ContactName)
                              .Select(CMSf.ContactPhone)
                              .Select(CMSf.ProductsName)
                              .Select(CMSf.ComplaintType)
                              .Where(CMSf.Status == 1)
                              .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                              .Where(CMSf.CMSAssignedTo == user.UserId)
                            );

                        model.CMSFollowupsCompleted = connection.List<CMSFollowupsRow>(f => f
                               .SelectTableFields()
                              .Select(CMSf.CMSId)
                              .Select(CMSf.CMSContactsId)
                              .Select(CMSf.FollowupNote)
                              .Select(CMSf.Details)
                              .Select(CMSf.ContactName)
                              .Select(CMSf.ContactPhone)
                              .Select(CMSf.ProductsName)
                              .Select(CMSf.ComplaintType)
                              .Where(CMSf.Status == 2)
                              .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                              .Where(CMSf.CMSAssignedTo == user.UserId)
                            );

                        model.ODCMSFollowups = connection.List<CMSFollowupsRow>(f => f
                               .SelectTableFields()
                               .Select(CMSf.CMSId)
                               .Select(CMSf.CMSContactsId)
                               .Select(CMSf.FollowupNote)
                               .Select(CMSf.Details)
                               .Select(CMSf.ContactName)
                               .Select(CMSf.ContactPhone)
                               .Select(CMSf.ProductsName)
                               .Select(CMSf.ComplaintType)
                               .Where(CMSf.Status == 1)
                               .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                               .Where(CMSf.CMSAssignedTo == user.UserId)
                             );

                    }

                    return model;
                });

            return View(MVC.Views.Common.Dashboard.DashboardIndex, cachedModel);
        }


        //public ActionResult SendMail(string subject, string tomail, string body)
        //{
        //    var User = new UserRow();

        //    using (var connection = _connections.NewFor<UserRow>())
        //    {
        //        var u = UserRow.Fields;
        //        User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
        //            .SelectTableFields()
        //            .Select(u.Host)
        //            .Select(u.Port)
        //            .Select(u.SSL)
        //            .Select(u.EmailId)
        //            .Select(u.EmailPassword));
        //    }

        //    string response;

        //    try
        //    {
        //        var message = new MailMessage();
        //        var m = new MailAddress(User.EmailId, User.EmailId);
        //        message.From = m;
        //        List<string> Receipent = tomail.Split(',').ToList();
        //        for (int i = 0; i < Receipent.Count; i++)
        //        {
        //            message.To.Add(Receipent.ElementAt(i));
        //        }

        //        message.Subject = subject;
        //        message.IsBodyHtml = true;
        //        message.Body = HttpUtility.UrlDecode(body, System.Text.Encoding.Default);
        //        response = EmailHelper.Send(message, User.EmailId, User.EmailPassword, User.SSL.Value, User.Host, User.Port.Value);
        //    }
        //    catch (Exception ex)
        //    {

        //        response = "Error\n\n" + ex.Message.ToString();
        //    }


        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}
        //[HttpPost, ServiceAuthorize, Route("~/Dashboard/SendMail")]
        //public ActionResult SendMail(string subject, string tomail, string body, List<string> attachments)

        //{
        //    var User = new UserRow();

        //    using (var connection = _connections.NewFor<UserRow>())
        //    {
        //        var u = UserRow.Fields;
        //        User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
        //            .SelectTableFields()
        //            .Select(u.Host)
        //            .Select(u.Port)
        //            .Select(u.SSL)
        //            .Select(u.EmailId)
        //            .Select(u.EmailPassword));
        //    }

        //    string response;

        //    try
        //    {
        //        var message = new MailMessage();
        //        var m = new MailAddress(User.EmailId, User.EmailId);
        //        message.From = m;
        //        List<string> Receipent = tomail.Split(',').ToList();
        //        for (int i = 0; i < Receipent.Count; i++)
        //        {
        //            message.To.Add(Receipent.ElementAt(i));
        //        }

        //        message.Subject = subject;
        //        message.IsBodyHtml = true;
        //        message.Body = HttpUtility.UrlDecode(body, System.Text.Encoding.Default);
        //        // ✅ Handle Attachments

        //        if (attachments != null && attachments.Count > 0)
        //        {
        //            foreach (var filePath in attachments)
        //            {

        //                string appDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "upload", "temporary", Path.GetFileName(filePath));

        //                if (System.IO.File.Exists(appDataPath)) // ✅ Check the correct absolute path
        //                {
        //                    string tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(filePath));

        //                    System.IO.File.Copy(appDataPath, tempFilePath, true); // ✅ Copy file to temp folder
        //                    message.Attachments.Add(new Attachment(tempFilePath));
        //                }
        //            }
        //        }

        //        response = EmailHelper.Send(message, User.EmailId, User.EmailPassword, User.SSL.Value, User.Host, User.Port.Value);
        //    }
        //    catch (Exception ex)
        //    {

        //        response = "Error\n\n" + ex.Message.ToString();
        //    }


        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost, ServiceAuthorize, Route("~/Dashboard/SendMail")]
        public JsonResult SendMail(string subject, string tomail, string body, List<string> attachments)

        {
            var User = new UserRow();

            using (var connection = _connections.NewFor<UserRow>())
            {
                var u = UserRow.Fields;
                User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                    .SelectTableFields()
                    .Select(u.Host)
                    .Select(u.Port)
                    .Select(u.SSL)
                    .Select(u.EmailId)
                    .Select(u.EmailPassword));
            }

            string response;

            try
            {
                var message = new MailMessage();
                var m = new MailAddress(User.EmailId, User.EmailId);
                message.From = m;
                List<string> Receipent = tomail.Split(',').ToList();
                //for (int i = 0; i < Receipent.Count; i++)
                //{
                //    message.To.Add(Receipent.ElementAt(i));
                //}

                if (Receipent.Count > 0)
                {
                    // Add the first recipient in "To"

                    message.To.Add(User.EmailId);

                    // Add remaining recipients in "BCC"
                    for (int i = 0; i < Receipent.Count; i++)
                    {
                        message.Bcc.Add(Receipent[i]);
                    }
                }

                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = WebUtility.UrlDecode(body);
                // ✅ Handle Attachments

                if (attachments != null && attachments.Count > 0)
                {
                    foreach (var filePath in attachments)
                    {

                        string appDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "upload", "temporary", Path.GetFileName(filePath));

                        if (System.IO.File.Exists(appDataPath)) // ✅ Check the correct absolute path
                        {
                            string tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(filePath));

                            System.IO.File.Copy(appDataPath, tempFilePath, true); // ✅ Copy file to temp folder
                            message.Attachments.Add(new Attachment(tempFilePath));
                        }
                    }
                }

                response = EmailHelper.Send(message, User.EmailId, User.EmailPassword, User.SSL.Value, User.Host, User.Port.Value);
            }
            catch (Exception ex)
            {

                response = "Error\n\n" + ex.Message.ToString();
            }


            return new JsonResult(response);
        }



        [HttpPost, Route("~/Dashboard/SendSMS")]
        public JsonResult SendSMS(SendSMSRequest request)
        {
            string response;
            try
            {
                response = SMSHelper.SendSMS(request.Phone, request.SMSType,request.TemplateID);
            }
            catch (Exception ex)
            {
                response = ex.Message.ToString();
            }
            return new JsonResult(response);
        }

        private List<ProductsRow> Products { get; set; }
        private List<PurchaseProductsRow> PurchaseProducts { get; set; }
        private List<SalesProductsRow> SalesProducts { get; set; }
        private List<PurchaseReturnProductsRow> PurchaseReturnProducts { get; set; }
        private List<SalesReturnProductsRow> SalesReturnProducts { get; set; }
        private List<ChallanProductsRow> ChallanProducts { get; set; }
    }
}
