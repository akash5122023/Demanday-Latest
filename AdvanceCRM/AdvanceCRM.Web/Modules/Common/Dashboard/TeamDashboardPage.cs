
namespace AdvanceCRM.Common.Pages
{
    using Serenity;
using AdvanceCRM.Web.Helpers;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System.Threading.Tasks;
    using System;
    
    using Administration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Tasks;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Quotation;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;
    using AdvanceCRM.Services;
    using AdvanceCRM.Sales;
    using AdvanceCRM.Enquiry.Repositories;
    using Serenity.Services;
    using AdvanceCRM.Masters;
    using Microsoft.AspNetCore.Authorization;

    [Route("TeamDashboard")]
    public class TeamDashboardController : Controller
    {
        // Dependency injected memory cache and SQL connections
        private readonly IMemoryCache _cache;
        private readonly ISqlConnections _connections;
       

        public TeamDashboardController(IMemoryCache cache, ISqlConnections connections, IRequestContext context)
        {
            _cache = cache; // constructor injection
            _connections = connections;
            
        }

        [Authorize, HttpGet, Route("~/TeamDashboard")]
        public async Task<ActionResult> Index([FromServices] IRequestContext context)
        {
            var result = await Get(context);
            if (result.Result is OkObjectResult ok && ok.Value is TeamDashboardPageModel model)
                return View(MVC.Views.Common.Dashboard.TeamDashboardIndex, model);

            return View(MVC.Views.Common.Dashboard.TeamDashboardIndex, new TeamDashboardPageModel());
        }

        [HttpGet("api")]
        public Task<ActionResult<TeamDashboardPageModel>> Get(IRequestContext context)
        {
            // Use  cache retrieval
            var cachedModel = _cache.GetOrCreate(
                "TeamDashboardPageModel:" + UserRow.Fields.GenerationKey,  entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                    var model = new TeamDashboardPageModel();

                    var e = EnquiryRow.Fields;
                    var q = QuotationRow.Fields;
                    var t = TasksRow.Fields;
                    var cms = CMSRow.Fields;
                    var a = AMCVisitPlannerRow.Fields;
                    var ef = EnquiryFollowupsRow.Fields;
                    var qf = QuotationFollowupsRow.Fields;
                    var c = ContactsRow.Fields;
                    var u = UserRow.Fields;
                    var tms = TeamsRow.Fields;
                    var inv = InvoiceFollowupsRow.Fields;
                    var sal = SalesFollowupsRow.Fields;
                    var sale = SalesRow.Fields;
                    var invo = InvoiceRow.Fields;
                    var tel = TeleCallingFollowupsRow.Fields;
                    // Retrieve current user id from HttpContext
                    var userId = Convert.ToInt32(context.User.GetIdentifier());
                    var od = UserRow.Fields;
                    var cmsf = CMSFollowupsRow.Fields;

                    using (var connection = _connections.NewFor<UserRow>())
                    {
                        //Getting hierarchy list
                        Users1 = connection.List<UserRow>(uq => uq
                        .SelectTableFields()
                        .Select(od.UserId)
                        .Select(od.Username)
                        .Where(od.UpperLevel == userId));

                        Users2 = connection.List<UserRow>(uq => uq
                            .SelectTableFields()
                            .Select(od.UserId)
                            .Select(od.Username)
                            .Where(od.UpperLevel2 == userId));

                        Users3 = connection.List<UserRow>(uq => uq
                            .SelectTableFields()
                            .Select(od.UserId)
                            .Select(od.Username)
                            .Where(od.UpperLevel3 == userId));

                        Users4 = connection.List<UserRow>(uq => uq
                            .SelectTableFields()
                            .Select(od.UserId)
                            .Select(od.Username)
                            .Where(od.UpperLevel4 == userId));

                        Users5 =  connection.List<UserRow>(uq => uq
                            .SelectTableFields()
                            .Select(od.UserId)
                            .Select(od.Username)
                            .Where(od.UpperLevel5 == userId));

                        CurrentUser = connection.TryById<UserRow>(userId, uq => uq
                            .SelectTableFields()
                            .Select(od.UserId)
                            .Select(od.Username));

                        var str1 = ""; var str2 = ""; var str3 = ""; var str4 = ""; var str5 = "";

                        int i = 0;
                        foreach (var item in Users1)
                        {
                            if (i == 0)
                                str1 = "AssignedId = " + item.UserId.Value;
                            else
                                str1 = str1 + " OR AssignedId = " + item.UserId.Value;

                            i++;
                        }

                        foreach (var item in Users2)
                        {
                            if (i == 0)
                                str2 = "AssignedId = " + item.UserId.Value;
                            else
                                str2 = str2 + " OR AssignedId = " + item.UserId.Value;

                            i++;
                        }

                        foreach (var item in Users3)
                        {
                            if (i == 0)
                                str3 = "AssignedId = " + item.UserId.Value;
                            else
                                str3 = str3 + " OR AssignedId = " + item.UserId.Value;

                            i++;
                        }

                        foreach (var item in Users4)
                        {
                            if (i == 0)
                                str4 = "AssignedId = " + item.UserId.Value;
                            else
                                str4 = str4 + " OR AssignedId = " + item.UserId.Value;

                            i++;
                        }

                        foreach (var item in Users5)
                        {
                            if (i == 0)
                                str5 = "AssignedId = " + item.UserId.Value;
                            else
                                str5 = str5 + " OR AssignedId = " + item.UserId.Value;

                            i++;
                        }

                        string fstr = "";

                        fstr = str1 + str2 + str3 + str4 + str5;

                        if (fstr.Trim() != "")
                        {
                            fstr = " OR " + fstr;
                        }


                        //listing
                        string temp_str = "Status = 1 AND (AssignedId = " + userId.ToString() + fstr + ")";
                        model.OpenEnq = connection.Count<EnquiryRow>(new Criteria(temp_str));

                        temp_str = "Status = 1 AND (AssignedId = " + userId.ToString() + fstr + ")";
                        model.OpenQuot = connection.Count<QuotationRow>(new Criteria(temp_str));

                        temp_str = "(AssignedId = " + userId.ToString() + fstr + ")";
                        model.CustomerCount = connection.Count<ContactsRow>(new Criteria(temp_str));

                        temp_str = "StatusId < 6 AND (AssignedId = " + userId.ToString() + fstr + ")";
                        string tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        model.OpenTasks = connection.Count<TasksRow>(new Criteria(tstr));

                        temp_str = "Status = 1 AND (AssignedId = " + userId.ToString() + fstr + ")";
                        tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        model.OpenCMS = connection.Count<CMSRow>(new Criteria(tstr));
                        model.OpenAMC = connection.Count<AMCVisitPlannerRow>(new Criteria(tstr));

                        temp_str = "Status = 1 AND (AssignedId = " + userId.ToString() + fstr + ")";
                        //tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        model.Opensale = connection.Count<SalesRow>(new Criteria(temp_str));
                        model.OpenPi = connection.Count<InvoiceRow>(new Criteria(temp_str));

                        temp_str = ef.Status + " = 1 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";




                        model.EnqFollowups = connection.List<EnquiryFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(ef.EnquiryId)
                         .Select(ef.EnquiryContactsId)
                         .Select(ef.FollowupNote)
                         .Select(ef.Details)
                         .Select(ef.EnquiryAssignedId)
                         .Where(new Criteria("(" + temp_str + ")"))
                         );

                        temp_str = ef.Status + " = 2 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        model.EnqFollowupsCompleted = connection.List<EnquiryFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(ef.EnquiryId)
                         .Select(ef.EnquiryContactsId)
                         .Select(ef.FollowupNote)
                         .Select(ef.Details)
                         .Select(ef.EnquiryAssignedId)
                         .Where(new Criteria("(" + temp_str + ")"))
                         );

                        temp_str = e.Status + " = 1  AND (AssignedId = " + userId.ToString() + fstr + ")";
                        //tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        var EnqAmtList = connection.List<EnquiryRow>(f => f
                         .SelectTableFields()
                         .Select(e.Id)
                         .Select(e.Total)
                         .Where(e.Total > 0)
                         .Where(new Criteria("(" + temp_str + ")"))
                       );

                        model.EnqAmt = 0;
                        foreach (var item in EnqAmtList)
                        {
                            //++model.amtcaselock;
                            model.EnqAmt += (Int32)item.Total;
                        }
                        //Quot
                        temp_str = q.Status + " = 1  AND (AssignedId = " + userId.ToString() + fstr + ")";
                       // tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        var QuotAmtList = connection.List<QuotationRow>(f => f
                          .SelectTableFields()
                          .Select(q.Id)
                          .Select(q.Total)
                          .Where(q.Total > 0)
                          .Where(new Criteria("(" + temp_str + ")"))
                        );

                        model.QuotAmt = 0;
                        foreach (var item in QuotAmtList)
                        {
                            //++model.amtcaselock;
                            model.QuotAmt += (Int32)item.Total;
                        }

                        //pi
                        temp_str = invo.Status + " = 1  AND (AssignedId = " + userId.ToString() + fstr + ")";
                     //   tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        var PiAmtList = connection.List<InvoiceRow>(f => f
                          .SelectTableFields()
                          .Select(invo.Id)
                          .Select(invo.Total)
                          .Where(invo.Total > 0)
                          .Where(new Criteria("(" + temp_str + ")"))
                        );

                        model.PIAmt = 0;
                        foreach (var item in PiAmtList)
                        {
                            //++model.amtcaselock;
                            model.PIAmt += (Int32)item.Total;
                        }
                        //pi
                        temp_str = sale.Status + " = 1  AND (AssignedId = " + userId.ToString() + fstr + ")";
                       // tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        var saleAmtList = connection.List<SalesRow>(f => f
                          .SelectTableFields()
                          .Select(sale.Id)
                          .Select(sale.Total)
                          .Where(sale.Total > 0)
                          .Where(new Criteria("(" + temp_str + ")"))
                        );

                        model.SaleAmt = 0;
                        foreach (var item in saleAmtList)
                        {
                            //++model.amtcaselock;
                            model.SaleAmt += (Int32)item.Total;
                        }

                        model.Customer = connection.List<ContactsRow>(f => f
                         .SelectTableFields()
                         .Select(c.Id)
                         .Select(c.Name)
                         );

                        temp_str = qf.Status + " = 1 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        model.QuotFollowups = connection.List<QuotationFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(qf.QuotationId)
                         .Select(qf.QuotationContactsId)
                         .Select(qf.FollowupNote)
                         .Select(qf.Details)
                         .Select(qf.QuotationAssignedId)
                         .Where(new Criteria("(" + temp_str + ")"))
                         );

                        temp_str = qf.Status + " = 2 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        model.QuotFollowupsCompleted = connection.List<QuotationFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(qf.QuotationId)
                         .Select(qf.QuotationContactsId)
                         .Select(qf.FollowupNote)
                         .Select(qf.Details)
                         .Select(qf.QuotationAssignedId)
                         .Where(new Criteria("(" + temp_str + ")"))
                         );

                        temp_str = ef.Status + " = 1 AND " + "CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        model.ODEnqFollowups = connection.List<EnquiryFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(ef.EnquiryId)
                         .Select(ef.EnquiryContactsId)
                         .Select(ef.FollowupNote)
                         .Select(ef.Details)
                         .Select(ef.Status)
                         .Select(ef.EnquiryAssignedId)
                         .Where(new Criteria("(" + temp_str + ")"))
                         );

                        temp_str = qf.Status + " = 1 AND " + "CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        model.ODQuotFollowups = connection.List<QuotationFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(qf.QuotationId)
                         .Select(qf.QuotationContactsId)
                         .Select(qf.FollowupNote)
                         .Select(qf.Details)
                         .Select(qf.QuotationAssignedId)
                         .Where(new Criteria("(" + temp_str + ")"))
                         );

                        //Telecalling
                        temp_str = tel.Status + " = 1 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (RepresentativeId= " + userId.ToString() + fstr + ")";
                        tstr = temp_str.Replace("AssignedId", "RepresentativeId");
                        model.TCFollowups = connection.List<TeleCallingFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(tel.TeleCallingId)
                         .Select(tel.TeleCallingContactsId)
                         .Select(tel.FollowupNote)
                         .Select(tel.Details)
                         .Select(tel.TeleCallingAssignedTo)
                         .Where(tel.TeleCallingAssignedTo.IsNotNull())
                         .Where(new Criteria("(" + tstr + ")"))
                         );

                        temp_str = tel.Status + " = 2 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (RepresentativeId = " + userId.ToString() + fstr + ")";
                        tstr = temp_str.Replace("AssignedId", "RepresentativeId");
                        model.TCCompleted = connection.List<TeleCallingFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(tel.TeleCallingId)
                         .Select(tel.TeleCallingContactsId)
                         .Select(tel.FollowupNote)
                         .Select(tel.Details)
                         .Select(tel.TeleCallingAssignedTo)
                         .Where(tel.TeleCallingAssignedTo.IsNotNull())
                         .Where(new Criteria("(" + tstr + ")"))
                         );

                        temp_str = tel.Status + " = 1 AND " + "CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate() + " AND (RepresentativeId = " + userId.ToString() + fstr + ")";
                        tstr = temp_str.Replace("AssignedId", "RepresentativeId");
                        model.ODTCFollowups = connection.List<TeleCallingFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(tel.TeleCallingId)
                         .Select(tel.TeleCallingContactsId)
                         .Select(tel.FollowupNote)
                         .Select(tel.Details)
                         .Select(tel.TeleCallingAssignedTo)
                         .Where(tel.TeleCallingAssignedTo.IsNotNull())
                         .Where(new Criteria("(" + tstr + ")"))
                         );


                        temp_str = t.StatusId + " != 2 AND " + "CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        model.Tasks = connection.List<TasksRow>(ts => ts
                         .SelectTableFields()
                         .Select(t.Id)
                         .Select(t.Task)
                         .Select(t.Details)
                         .Select(t.CreationDate)
                         .Select(t.ExpectedCompletion)
                         .Select(t.AssignedBy)
                         .Select(t.AssignedTo)
                         .Where(new Criteria("(" + tstr + ")"))
                         );

                        temp_str = t.StatusId + " = 2 AND " + "CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        model.TasksCompleted = connection.List<TasksRow>(ts => ts
                         .SelectTableFields()
                         .Select(t.Id)
                         .Select(t.Task)
                         .Select(t.Details)
                         .Select(t.CreationDate)
                         .Select(t.ExpectedCompletion)
                         .Select(t.AssignedBy)
                         .Select(t.AssignedTo)
                         .Where(new Criteria("(" + tstr + ")"))
                         );

                        temp_str = t.StatusId + " != 2 AND " + "CAST(ExpectedCompletion as DATE)<" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        model.ODTasks = connection.List<TasksRow>(td => td
                         .SelectTableFields()
                         .Select(t.Id)
                         .Select(t.Task)
                         .Select(t.Details)
                         .Select(t.CreationDate)
                         .Select(t.ExpectedCompletion)
                         .Select(t.AssignedBy)
                         .Select(t.AssignedTo)
                         .Where(new Criteria("(" + tstr + ")"))
                         );

                        temp_str = cms.Status + " = 1 AND " + "CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        model.CMS = connection.List<CMSRow>(ts => ts
                         .SelectTableFields()
                         .Select(cms.Id)
                         .Select(cms.ContactsName)
                         .Select(cms.ContactsPhone)
                         .Select(cms.Date)
                         .Select(cms.ProductsName)
                         .Select(cms.ComplaintComplaintType)
                         .Select(cms.Instructions)
                         .Select(cms.AssignedTo)
                         .Where(new Criteria("(" + tstr + ")"))
                         );

                        model.CMSFollowups = connection.List<CMSFollowupsRow>(f => f
                              .SelectTableFields()
                              .Select(cmsf.CMSId)
                              .Select(cmsf.CMSContactsId)
                              .Select(cmsf.FollowupNote)
                              .Select(cmsf.Details)
                              .Select(cmsf.ContactName)
                              .Select(cmsf.ContactPhone)
                              .Select(cmsf.ProductsName)
                              .Select(cmsf.ComplaintType)
                              .Select(cmsf.CMSAssignedTo)
                              .Where(new Criteria("(" + tstr + ")"))
                            );

                        temp_str = cms.Status + " = 2 AND " + "CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        model.CMSCompleted = connection.List<CMSRow>(ts => ts
                         .SelectTableFields()
                         .Select(cms.Id)
                         .Select(cms.ContactsName)
                         .Select(cms.ContactsPhone)
                         .Select(cms.Date)
                         .Select(cms.ProductsName)
                         .Select(cms.ComplaintComplaintType)
                         .Select(cms.Instructions)
                         .Select(cms.AssignedTo)
                         .Where(new Criteria("(" + tstr + ")"))
                         );

                        model.CMSFollowupsCompleted = connection.List<CMSFollowupsRow>(f => f
                               .SelectTableFields()
                              .Select(cmsf.CMSId)
                              .Select(cmsf.CMSContactsId)
                              .Select(cmsf.FollowupNote)
                              .Select(cmsf.Details)
                              .Select(cmsf.ContactName)
                              .Select(cmsf.ContactPhone)
                              .Select(cmsf.ProductsName)
                              .Select(cmsf.ComplaintType)
                              .Select(cmsf.CMSAssignedTo)
                              .Where(new Criteria("(" + tstr + ")"))
                            );


                        temp_str = cms.Status + " = 1 AND " + "CAST(ExpectedCompletion as DATE)<" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        model.ODCMS = connection.List<CMSRow>(td => td
                         .Select(cms.Id)
                         .Select(cms.ContactsName)
                         .Select(cms.Date)
                         .Select(cms.ProductsName)
                         .Select(cms.ContactsPhone)
                         .Select(cms.ComplaintComplaintType)
                         .Select(cms.Instructions)
                         .Select(cms.AssignedTo)
                         .Where(new Criteria("(" + tstr + ")"))
                         );

                        model.ODCMSFollowups = connection.List<CMSFollowupsRow>(f => f
                              .SelectTableFields()
                              .Select(cmsf.CMSId)
                              .Select(cmsf.CMSContactsId)
                              .Select(cmsf.FollowupNote)
                              .Select(cmsf.Details)
                              .Select(cmsf.ContactName)
                              .Select(cmsf.ContactPhone)
                              .Select(cmsf.ProductsName)
                              .Select(cmsf.ComplaintType)
                              .Select(cmsf.CMSAssignedTo)
                              .Where(new Criteria("(" + tstr + ")"))
                            );

                        temp_str = inv.Status + " = 1 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        //tstr = temp_str.Replace("AssignedId", "InvoiceAssignedId");
                        model.InvoiceFollowups = connection.List<InvoiceFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(inv.InvoiceId)
                         .Select(inv.InvoiceContactsId)
                         .Select(inv.FollowupNote)
                         .Select(inv.Details)
                         .Select(inv.InvoiceAssignedId)
                         .Where(new Criteria("(" + temp_str + ")"))
                         );

                        temp_str = inv.Status + " = 2 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        //tstr = temp_str.Replace("AssignedId", "InvoiceAssignedId");
                        model.InvoiceFollowupsCompleted = connection.List<InvoiceFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(inv.InvoiceId)
                         .Select(inv.InvoiceContactsId)
                         .Select(inv.FollowupNote)
                         .Select(inv.Details)
                         .Select(inv.InvoiceAssignedId)
                         .Where(new Criteria("(" + temp_str + ")"))
                         );

                        temp_str = inv.Status + " = 1 AND " + "CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        //tstr = temp_str.Replace("AssignedId", "InvoiceAssignedId");
                        model.ODInvoiceFollowups = connection.List<InvoiceFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(inv.InvoiceId)
                         .Select(inv.InvoiceContactsId)
                         .Select(inv.FollowupNote)
                         .Select(inv.Details)
                         .Select(inv.InvoiceAssignedId)
                         .Where(new Criteria("(" + temp_str + ")"))
                         ); 
                        
                        temp_str = sal.Status + " = 1 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        //tstr = temp_str.Replace("AssignedId", "SalesAssignedId");
                        model.SalesFollowups = connection.List<SalesFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(sal.SalesId)
                         .Select(sal.SalesContactsId)
                         .Select(sal.FollowupNote)
                         .Select(sal.Details)
                         .Select(sal.SalesAssignedId)
                         .Where(new Criteria("(" + temp_str + ")"))
                         );

                        temp_str = sal.Status + " = 2 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        //tstr = temp_str.Replace("AssignedId", "SalesAssignedId");
                        model.SalesFollowupsCompleted = connection.List<SalesFollowupsRow>(g => g
                         .SelectTableFields()
                         .Select(sal.SalesId)
                         .Select(sal.SalesContactsId)
                         .Select(sal.FollowupNote)
                         .Select(sal.Details)
                         .Select(sal.SalesAssignedId)
                         .Where(new Criteria("(" + temp_str + ")"))
                         );

                        temp_str = sal.Status + " = 1 AND " + "CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        //tstr = temp_str.Replace("AssignedId", "SalesAssignedId");
                        model.ODSalesFollowups = connection.List<SalesFollowupsRow>(f => f
                         .SelectTableFields()
                         .Select(sal.SalesId)
                         .Select(sal.SalesContactsId)
                         .Select(sal.FollowupNote)
                         .Select(sal.Details)
                         .Select(sal.SalesAssignedId)
                         .Where(new Criteria("(" + temp_str + ")"))
                         );

                        temp_str = a.Status + " = 1 AND " + "CAST(VisitDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        model.AMC = connection.List<AMCVisitPlannerRow>(ts => ts
                         .SelectTableFields()
                         .Select(a.Id)
                         .Select(a.AMCContactsId)
                         .Select(a.VisitDetails)
                         .Select(a.AssignedTo)
                         .Where(new Criteria("(" + tstr + ")"))
                         );

                        temp_str = a.Status + " = 2 AND " + "CAST(VisitDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        model.AMCCompleted = connection.List<AMCVisitPlannerRow>(ts => ts
                         .SelectTableFields()
                         .Select(a.Id)
                         .Select(a.AMCContactsId)
                         .Select(a.VisitDetails)
                         .Select(a.AssignedTo)
                         .Where(new Criteria("(" + tstr + ")"))
                         );

                        temp_str = a.Status + " = 1 AND " + "CAST(VisitDate as DATE)<" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                        tstr = temp_str.Replace("AssignedId", "AssignedTo");
                        model.ODAMC = connection.List<AMCVisitPlannerRow>(ts => ts
                         .SelectTableFields()
                         .Select(a.Id)
                         .Select(a.AMCContactsId)
                         .Select(a.VisitDetails)
                         .Select(a.AssignedTo)
                         .Where(new Criteria("(" + tstr + ")"))
                         );

                        model.Users = connection.List<UserRow>(us => us
                         .SelectTableFields()
                         .Select(u.UserId)
                         .Select(u.Username)
                         );

                        //model.Teams = connection.List<TeamsRow>(tm => tm
                        //  .SelectTableFields()
                        //  .Select(tms.Id)
                        //  .Select(tms.Team)
                        //  .Select(tms.UserId)
                        //  .Select(tms.UserUsername));

                        temp_str = e.Date + "<='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "' AND " + e.Date + ">'" + DateTime.Now.Date.AddDays(-9).ToString("yyyy-MM-dd") + "' AND (AssignedId = " + userId.ToString() + fstr + ")";
                        model.EnqListChart = connection.List<EnquiryRow>(el => el
                         .SelectTableFields()
                         .Select(e.Id)
                         .Where(new Criteria("(" + temp_str + ")"))
                         );

                        temp_str = q.Date + "<='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "' AND " + q.Date + ">'" + DateTime.Now.Date.AddDays(-9).ToString("yyyy-MM-dd") + "' AND (AssignedId = " + userId.ToString() + fstr + ")";
                        model.QuotListChart = connection.List<QuotationRow>(ql => ql
                         .SelectTableFields()
                         .Select(q.Id)
                         .Where(new Criteria("(" + temp_str + ")"))
                         );
                    }
                    return model;
                });

            // Return cached dashboard model as JSON
            return Task.FromResult<ActionResult<TeamDashboardPageModel>>(Ok(cachedModel));
        }

        [HttpGet("GetData")]
        public Task<ActionResult<TeamDashboardPageModel>> GetData(IRequestContext context,[FromQuery] string users, [FromQuery] int status)
        {
            var cacheKey = $"TeamDashboardData:{context.User.GetIdentifier()}:{users}:{status}";
            var cachedModel = _cache.GetOrCreate(cacheKey,  entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1);

                var model = new TeamDashboardPageModel();
                JArray userlist = JArray.Parse(users);

                try
                {
                    var e = EnquiryRow.Fields;
                var q = QuotationRow.Fields;
                var t = TasksRow.Fields;
                var cms = CMSRow.Fields;
                var a = AMCVisitPlannerRow.Fields;
                var ef = EnquiryFollowupsRow.Fields;
                var qf = QuotationFollowupsRow.Fields;
                var c = ContactsRow.Fields;
                var u = UserRow.Fields;
                var invo = InvoiceRow.Fields;
                var sale = SalesRow.Fields;
                var inv = InvoiceFollowupsRow.Fields;
                var tel = TeleCallingFollowupsRow.Fields;
                // Current logged in user id
                var userId = Convert.ToInt32(context.User.GetIdentifier());
                var od = UserRow.Fields;
                var cmsf = CMSFollowupsRow.Fields;

                using (var connection = _connections.NewFor<EnquiryRow>())
                {
                    var fstr = "";

                    if (userlist.Count > 0)
                    {
                        int i = 0;
                        foreach (var item in userlist)
                        {
                            if (i == 0)
                                fstr = "AssignedId = " + item.ToString();
                            else
                                fstr = fstr + " OR AssignedId = " + item.ToString();

                            i++;
                        }
                    }
                    else
                    {
                        Users1 = connection.List<UserRow>(uq => uq
                        .SelectTableFields()
                        .Select(od.UserId)
                        .Select(od.Username)
                        .Where(od.UpperLevel == userId));

                        Users2 = connection.List<UserRow>(uq => uq
                            .SelectTableFields()
                            .Select(od.UserId)
                            .Select(od.Username)
                            .Where(od.UpperLevel2 == userId));

                        Users3 = connection.List<UserRow>(uq => uq
                            .SelectTableFields()
                            .Select(od.UserId)
                            .Select(od.Username)
                            .Where(od.UpperLevel3 == userId));

                        Users4 = connection.List<UserRow>(uq => uq
                            .SelectTableFields()
                            .Select(od.UserId)
                            .Select(od.Username)
                            .Where(od.UpperLevel4 == userId));

                        Users5 = connection.List<UserRow>(uq => uq
                            .SelectTableFields()
                            .Select(od.UserId)
                            .Select(od.Username)
                            .Where(od.UpperLevel5 == userId));

                        CurrentUser = connection.TryById<UserRow>(userId, uq => uq
                            .SelectTableFields()
                            .Select(od.UserId)
                            .Select(od.Username));

                        var str1 = ""; var str2 = ""; var str3 = ""; var str4 = ""; var str5 = "";

                        int i = 0;
                        foreach (var item in Users1)
                        {
                            if (i == 0)
                                str1 = "AssignedId = " + item.UserId.Value;
                            else
                                str1 = str1 + " OR AssignedId = " + item.UserId.Value;

                            i++;
                        }

                        foreach (var item in Users2)
                        {
                            if (i == 0)
                                str2 = "AssignedId = " + item.UserId.Value;
                            else
                                str2 = str2 + " OR AssignedId = " + item.UserId.Value;

                            i++;
                        }

                        foreach (var item in Users3)
                        {
                            if (i == 0)
                                str3 = "AssignedId = " + item.UserId.Value;
                            else
                                str3 = str3 + " OR AssignedId = " + item.UserId.Value;

                            i++;
                        }

                        foreach (var item in Users4)
                        {
                            if (i == 0)
                                str4 = "AssignedId = " + item.UserId.Value;
                            else
                                str4 = str4 + " OR AssignedId = " + item.UserId.Value;

                            i++;
                        }

                        foreach (var item in Users5)
                        {
                            if (i == 0)
                                str5 = "AssignedId = " + item.UserId.Value;
                            else
                                str5 = str5 + " OR AssignedId = " + item.UserId.Value;

                            i++;
                            if (model.Users.FirstOrDefault(x => x.UserId == item.UserId) == null)
                                model.Users.Add(item);
                        }

                        fstr = str1 + str2 + str3 + str4 + str5;
                    }

                    if (fstr.Trim() != "")
                    {
                        fstr = " OR " + fstr;
                    }

                    //listing
                    string temp_str = "Status = "+status+" AND (AssignedId = " + userId.ToString() + fstr + ")";
                    model.OpenEnq = connection.Count<EnquiryRow>(new Criteria(temp_str));

                    temp_str = "Status = " + status + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    model.OpenQuot = connection.Count<QuotationRow>(new Criteria(temp_str));

                    temp_str = "(AssignedId = " + userId.ToString() + fstr + ")";
                    model.CustomerCount = connection.Count<ContactsRow>(new Criteria(temp_str));

                    temp_str = "StatusId < 6 AND (AssignedId = " + userId.ToString() + fstr + ")";
                    string tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    model.OpenTasks = connection.Count<TasksRow>(new Criteria(tstr));

                    temp_str = "Status = " + status + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    model.OpenCMS = connection.Count<CMSRow>(new Criteria(tstr));
                    model.OpenAMC = connection.Count<AMCVisitPlannerRow>(new Criteria(tstr));

                    temp_str = "Status = " + status + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    //tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    model.Opensale = connection.Count<SalesRow>(new Criteria(temp_str));
                    model.OpenPi = connection.Count<InvoiceRow>(new Criteria(temp_str));


                    temp_str = e.Status + " = " + status + "  AND (AssignedId = " + userId.ToString() + fstr + ")";
                    //tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    var EnqAmtList = connection.List<EnquiryRow>(f => f
                     .SelectTableFields()
                     .Select(e.Id)
                     .Select(e.Total)
                     .Where(e.Total > 0)
                     .Where(new Criteria("(" + temp_str + ")"))
                   );

                    model.EnqAmt = 0;
                    foreach (var item in EnqAmtList)
                    {
                        //++model.amtcaselock;
                        model.EnqAmt += (Int32)item.Total;
                    }
                    //Quot
                    temp_str = q.Status + " = " + status + "  AND (AssignedId = " + userId.ToString() + fstr + ")";
                    // tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    var QuotAmtList = connection.List<QuotationRow>(f => f
                      .SelectTableFields()
                      .Select(q.Id)
                      .Select(q.Total)
                      .Where(q.Total > 0)
                      .Where(new Criteria("(" + temp_str + ")"))
                    );

                    model.QuotAmt = 0;
                    foreach (var item in QuotAmtList)
                    {
                        //++model.amtcaselock;
                        model.QuotAmt += (Int32)item.Total;
                    }

                    //pi
                    temp_str = invo.Status + " = " + status + "  AND (AssignedId = " + userId.ToString() + fstr + ")";
                    //   tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    var PiAmtList = connection.List<InvoiceRow>(f => f
                      .SelectTableFields()
                      .Select(invo.Id)
                      .Select(invo.Total)
                      .Where(invo.Total > 0)
                      .Where(new Criteria("(" + temp_str + ")"))
                    );

                    model.PIAmt = 0;
                    foreach (var item in PiAmtList)
                    {
                        //++model.amtcaselock;
                        model.PIAmt += (Int32)item.Total;
                    }
                    //pi
                    temp_str = sale.Status + " = " + status + "  AND (AssignedId = " + userId.ToString() + fstr + ")";
                    // tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    var saleAmtList = connection.List<SalesRow>(f => f
                      .SelectTableFields()
                      .Select(sale.Id)
                      .Select(sale.Total)
                      .Where(sale.Total > 0)
                      .Where(new Criteria("(" + temp_str + ")"))
                    );

                    model.SaleAmt = 0;
                    foreach (var item in saleAmtList)
                    {
                        //++model.amtcaselock;
                        model.SaleAmt += (Int32)item.Total;
                    }



                    temp_str = ef.Status + " = 1 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    model.EnqFollowups = connection.List<EnquiryFollowupsRow>(f => f
                     .SelectTableFields()
                     .Select(ef.EnquiryId)
                     .Select(ef.EnquiryContactsId)
                     .Select(ef.FollowupNote)
                     .Select(ef.Details)
                     .Select(ef.EnquiryAssignedId)
                     .Where(new Criteria("(" + temp_str + ")"))
                     );

                    temp_str = ef.Status + " = 2 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    model.EnqFollowupsCompleted = connection.List<EnquiryFollowupsRow>(f => f
                     .SelectTableFields()
                     .Select(ef.EnquiryId)
                     .Select(ef.EnquiryContactsId)
                     .Select(ef.FollowupNote)
                     .Select(ef.Details)
                     .Select(ef.EnquiryAssignedId)
                     .Where(new Criteria("(" + temp_str + ")"))
                     );

                    model.Customer = connection.List<ContactsRow>(f => f
                     .SelectTableFields()
                     .Select(c.Id)
                     .Select(c.Name)
                     );

                    temp_str = qf.Status + " = 1 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    model.QuotFollowups = connection.List<QuotationFollowupsRow>(g => g
                     .SelectTableFields()
                     .Select(qf.QuotationId)
                     .Select(qf.QuotationContactsId)
                     .Select(qf.FollowupNote)
                     .Select(qf.Details)
                     .Select(qf.QuotationAssignedId)
                     .Where(new Criteria("(" + temp_str + ")"))
                     );

                    temp_str = qf.Status + " = 2 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    model.QuotFollowupsCompleted = connection.List<QuotationFollowupsRow>(g => g
                     .SelectTableFields()
                     .Select(qf.QuotationId)
                     .Select(qf.QuotationContactsId)
                     .Select(qf.FollowupNote)
                     .Select(qf.Details)
                     .Select(qf.QuotationAssignedId)
                     .Where(new Criteria("(" + temp_str + ")"))
                     );

                    temp_str = ef.Status + " = 1 AND " + "CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    model.ODEnqFollowups = connection.List<EnquiryFollowupsRow>(f => f
                     .SelectTableFields()
                     .Select(ef.EnquiryId)
                     .Select(ef.EnquiryContactsId)
                     .Select(ef.FollowupNote)
                     .Select(ef.Details)
                     .Select(ef.Status)
                     .Select(ef.EnquiryAssignedId)
                     .Where(new Criteria("(" + temp_str + ")"))
                     );

                    temp_str = qf.Status + " = 1 AND " + "CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    model.ODQuotFollowups = connection.List<QuotationFollowupsRow>(g => g
                     .SelectTableFields()
                     .Select(qf.QuotationId)
                     .Select(qf.QuotationContactsId)
                     .Select(qf.FollowupNote)
                     .Select(qf.Details)
                     .Select(qf.QuotationAssignedId)
                     .Where(new Criteria("(" + temp_str + ")"))
                     );

                    //Telecalling
                    temp_str = tel.Status + " = 1 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (RepresentativeId= " + userId.ToString() + fstr + ")";
                    tstr = temp_str.Replace("AssignedId", "RepresentativeId");
                    model.TCFollowups = connection.List<TeleCallingFollowupsRow>(g => g
                     .SelectTableFields()
                     .Select(tel.TeleCallingId)
                     .Select(tel.TeleCallingContactsId)
                     .Select(tel.FollowupNote)
                     .Select(tel.Details)
                     .Select(tel.TeleCallingAssignedTo)
                     .Where(tel.TeleCallingAssignedTo.IsNotNull())
                     .Where(new Criteria("(" + tstr + ")"))
                     );

                    temp_str = tel.Status + " = 2 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (RepresentativeId = " + userId.ToString() + fstr + ")";
                    tstr = temp_str.Replace("AssignedId", "RepresentativeId");
                    model.TCCompleted = connection.List<TeleCallingFollowupsRow>(g => g
                     .SelectTableFields()
                     .Select(tel.TeleCallingId)
                     .Select(tel.TeleCallingContactsId)
                     .Select(tel.FollowupNote)
                     .Select(tel.Details)
                     .Select(tel.TeleCallingAssignedTo)
                     .Where(tel.TeleCallingAssignedTo.IsNotNull())
                     .Where(new Criteria("(" + tstr + ")"))
                     );

                    temp_str = tel.Status + " = 1 AND " + "CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate() + " AND (RepresentativeId = " + userId.ToString() + fstr + ")";
                    tstr = temp_str.Replace("AssignedId", "RepresentativeId");
                    model.ODTCFollowups = connection.List<TeleCallingFollowupsRow>(f => f
                     .SelectTableFields()
                     .Select(tel.TeleCallingId)
                     .Select(tel.TeleCallingContactsId)
                     .Select(tel.FollowupNote)
                     .Select(tel.Details)
                     .Select(tel.TeleCallingAssignedTo)
                     .Where(tel.TeleCallingAssignedTo.IsNotNull())
                     .Where(new Criteria("(" + tstr + ")"))
                     );


                    temp_str = t.StatusId + " < 6 AND " + "CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    model.Tasks = connection.List<TasksRow>(ts => ts
                     .SelectTableFields()
                     .Select(t.Id)
                     .Select(t.Task)
                     .Select(t.Details)
                     .Select(t.CreationDate)
                     .Select(t.ExpectedCompletion)
                     .Select(t.AssignedBy)
                     .Select(t.AssignedTo)
                     .Where(new Criteria("(" + tstr + ")"))
                     );

                    temp_str = t.StatusId + " = 6 AND " + "CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    model.TasksCompleted = connection.List<TasksRow>(ts => ts
                     .SelectTableFields()
                     .Select(t.Id)
                     .Select(t.Task)
                     .Select(t.Details)
                     .Select(t.CreationDate)
                     .Select(t.ExpectedCompletion)
                     .Select(t.AssignedBy)
                     .Select(t.AssignedTo)
                     .Where(new Criteria("(" + tstr + ")"))
                     );

                    temp_str = t.StatusId + " < 6 AND " + "CAST(ExpectedCompletion as DATE)<" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    model.ODTasks = connection.List<TasksRow>(td => td
                     .SelectTableFields()
                     .Select(t.Id)
                     .Select(t.Task)
                     .Select(t.Details)
                     .Select(t.CreationDate)
                     .Select(t.ExpectedCompletion)
                     .Select(t.AssignedBy)
                     .Select(t.AssignedTo)
                     .Where(new Criteria("(" + tstr + ")"))
                     );

                    temp_str = cms.Status + " = 1 AND " + "CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    model.CMS = connection.List<CMSRow>(ts => ts
                     .SelectTableFields()
                     .Select(cms.Id)
                     .Select(cms.ContactsName)
                     .Select(cms.ContactsPhone)
                     .Select(cms.Date)
                     .Select(cms.ProductsName)
                     .Select(cms.ComplaintComplaintType)
                     .Select(cms.Instructions)
                     .Select(cms.AssignedTo)
                     .Where(new Criteria("(" + tstr + ")"))
                     );

                    model.CMSFollowups = connection.List<CMSFollowupsRow>(f => f
                          .SelectTableFields()
                          .Select(cmsf.CMSId)
                          .Select(cmsf.CMSContactsId)
                          .Select(cmsf.FollowupNote)
                          .Select(cmsf.Details)
                          .Select(cmsf.ContactName)
                          .Select(cmsf.ContactPhone)
                          .Select(cmsf.ProductsName)
                          .Select(cmsf.ComplaintType)
                          .Select(cmsf.CMSAssignedTo)
                          .Where(new Criteria("(" + tstr + ")"))
                        );

                    temp_str = cms.Status + " = 2 AND " + "CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    model.CMSCompleted = connection.List<CMSRow>(ts => ts
                     .SelectTableFields()
                     .Select(cms.Id)
                     .Select(cms.ContactsName)
                     .Select(cms.ContactsPhone)
                     .Select(cms.Date)
                     .Select(cms.ProductsName)
                     .Select(cms.ComplaintComplaintType)
                     .Select(cms.Instructions)
                     .Select(cms.AssignedTo)
                     .Where(new Criteria("(" + tstr + ")"))
                     );

                    model.CMSFollowupsCompleted = connection.List<CMSFollowupsRow>(f => f
                           .SelectTableFields()
                          .Select(cmsf.CMSId)
                          .Select(cmsf.CMSContactsId)
                          .Select(cmsf.FollowupNote)
                          .Select(cmsf.Details)
                          .Select(cmsf.ContactName)
                          .Select(cmsf.ContactPhone)
                          .Select(cmsf.ProductsName)
                          .Select(cmsf.ComplaintType)
                          .Select(cmsf.CMSAssignedTo)
                          .Where(new Criteria("(" + tstr + ")"))
                        );


                    temp_str = cms.Status + " = 1 AND " + "CAST(ExpectedCompletion as DATE)<" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    model.ODCMS = connection.List<CMSRow>(td => td
                     .Select(cms.Id)
                     .Select(cms.ContactsName)
                     .Select(cms.Date)
                     .Select(cms.ProductsName)
                     .Select(cms.ContactsPhone)
                     .Select(cms.ComplaintComplaintType)
                     .Select(cms.Instructions)
                     .Select(cms.AssignedTo)
                     .Where(new Criteria("(" + tstr + ")"))
                     );

                    model.ODCMSFollowups = connection.List<CMSFollowupsRow>(f => f
                          .SelectTableFields()
                          .Select(cmsf.CMSId)
                          .Select(cmsf.CMSContactsId)
                          .Select(cmsf.FollowupNote)
                          .Select(cmsf.Details)
                          .Select(cmsf.ContactName)
                          .Select(cmsf.ContactPhone)
                          .Select(cmsf.ProductsName)
                          .Select(cmsf.ComplaintType)
                          .Select(cmsf.CMSAssignedTo)
                          .Where(new Criteria("(" + tstr + ")"))
                        );

                    temp_str = inv.Status + " = 1 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    //tstr = temp_str.Replace("AssignedId", "InvoiceAssignedId");
                    model.InvoiceFollowups = connection.List<InvoiceFollowupsRow>(g => g
                     .SelectTableFields()
                     .Select(inv.InvoiceId)
                     .Select(inv.InvoiceContactsId)
                     .Select(inv.FollowupNote)
                     .Select(inv.Details)
                     .Select(inv.InvoiceAssignedId)
                     .Where(new Criteria("(" + temp_str + ")"))
                     );

                    temp_str = inv.Status + " = 2 AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    //tstr = temp_str.Replace("AssignedId", "InvoiceAssignedId");
                    model.InvoiceFollowupsCompleted = connection.List<InvoiceFollowupsRow>(g => g
                     .SelectTableFields()
                     .Select(inv.InvoiceId)
                     .Select(inv.InvoiceContactsId)
                     .Select(inv.FollowupNote)
                     .Select(inv.Details)
                     .Select(inv.InvoiceAssignedId)
                     .Where(new Criteria("(" + temp_str + ")"))
                     );

                    temp_str = inv.Status + " = 1 AND " + "CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    //tstr = temp_str.Replace("AssignedId", "InvoiceAssignedId");
                    model.ODInvoiceFollowups = connection.List<InvoiceFollowupsRow>(f => f
                     .SelectTableFields()
                     .Select(inv.InvoiceId)
                     .Select(inv.InvoiceContactsId)
                     .Select(inv.FollowupNote)
                     .Select(inv.Details)
                     .Select(inv.InvoiceAssignedId)
                     .Where(new Criteria("(" + temp_str + ")"))
                     );

                    temp_str = a.Status + " = 1 AND " + "CAST(VisitDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    model.AMC = connection.List<AMCVisitPlannerRow>(ts => ts
                     .SelectTableFields()
                     .Select(a.Id)
                     .Select(a.AMCContactsId)
                     .Select(a.VisitDetails)
                     .Select(a.AssignedTo)
                     .Where(new Criteria("(" + tstr + ")"))
                     );

                    temp_str = a.Status + " = 2 AND " + "CAST(VisitDate as DATE)=" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    model.AMCCompleted = connection.List<AMCVisitPlannerRow>(ts => ts
                     .SelectTableFields()
                     .Select(a.Id)
                     .Select(a.AMCContactsId)
                     .Select(a.VisitDetails)
                     .Select(a.AssignedTo)
                     .Where(new Criteria("(" + tstr + ")"))
                     );

                    temp_str = a.Status + " = 1 AND " + "CAST(VisitDate as DATE)<" + DateTime.Now.ToSqlDate() + " AND (AssignedId = " + userId.ToString() + fstr + ")";
                    tstr = temp_str.Replace("AssignedId", "AssignedTo");
                    model.ODAMC = connection.List<AMCVisitPlannerRow>(ts => ts
                     .SelectTableFields()
                     .Select(a.Id)
                     .Select(a.AMCContactsId)
                     .Select(a.VisitDetails)
                     .Select(a.AssignedTo)
                     .Where(new Criteria("(" + tstr + ")"))
                     );

                    model.Users = connection.List<UserRow>(us => us
                     .SelectTableFields()
                     .Select(u.UserId)
                     .Select(u.Username)
                     );

                    temp_str = e.Date + "<='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "' AND " + e.Date + ">'" + DateTime.Now.Date.AddDays(-9).ToString("yyyy-MM-dd") + "' AND (AssignedId = " + userId.ToString() + fstr + ")";
                    model.EnqListChart = connection.List<EnquiryRow>(el => el
                     .SelectTableFields()
                     .Select(e.Id)
                     .Where(new Criteria("(" + temp_str + ")"))
                     );

                    temp_str = q.Date + "<='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "' AND " + q.Date + ">'" + DateTime.Now.Date.AddDays(-9).ToString("yyyy-MM-dd") + "' AND (AssignedId = " + userId.ToString() + fstr + ")";
                    model.QuotListChart = connection.List<QuotationRow>(ql => ql
                     .SelectTableFields()
                     .Select(q.Id)
                     .Where(new Criteria("(" + temp_str + ")"))
                     );
                }
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        });
            return Task.FromResult<ActionResult<TeamDashboardPageModel>>(Ok(cachedModel));
        }

        private UserRow CurrentUser { get; set; }
        private List<UserRow> Users1 { get; set; }
        private List<UserRow> Users2 { get; set; }
        private List<UserRow> Users3 { get; set; }
        private List<UserRow> Users4 { get; set; }
        private List<UserRow> Users5 { get; set; }
    }
}
