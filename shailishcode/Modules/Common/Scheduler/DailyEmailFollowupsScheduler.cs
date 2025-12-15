using AdvanceCRM.Administration;
using AdvanceCRM.Common;
using AdvanceCRM.Contacts;

using AdvanceCRM.Enquiry;
using AdvanceCRM.Quotation;
using AdvanceCRM.Sales;
using AdvanceCRM.Services;
using AdvanceCRM.Tasks;
using Quartz;
using Serenity.Data;
using Serenity;
using Serenity.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Serenity.Extensions.DependencyInjection;

namespace AdvanceCRM.Common.Scheduler
{
    public class DailyEmailFollowupsScheduler : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var data = new DailyEmailModel();
            var connection = Dependency.Resolve<ISqlConnections>().NewByKey("Default");

            var u = UserRow.Fields;
            data.User = connection.List<UserRow>(qq => qq
                .SelectTableFields()
                .Select(u.UserId)
                .Select(u.DisplayName)
                .Select(u.Email)
                );

            data.admin = connection.TryById<UserRow>(1, q => q
                     .SelectTableFields()
                     .Select(u.EmailId)
                     .Select(u.EmailPassword)
                     .Select(u.Host)
                     .Select(u.Port)
                     .Select(u.SSL)
                     );

            foreach (var itm in data.User)
            {
                try
                {
                    var strEnqTodayFoll = "<strong><tr><td colspan=3 align=center>Todays Enquiry Followups</strong><br/></td></tr>" +
                        "<tr bgcolor=#D3D3D3><th width=35%>Name</th><th width=20%>Note</th><th width=45%>Details</th></tr>";
                    var strODEnqFoll = "<tr><td colspan=3 align=center><br/><strong>Overdue Enquiry Followups</strong><br/></td></tr>" +
                        "<tr bgcolor=#D3D3D3><th width=35%>Name</th><th width=20%>Note</th><th width=45%>Details</th></tr>";
                    var strQuotTodayFoll = "<tr><td colspan=3 align=center><br/><strong>Todays Quotation Followups</strong><br/></td></tr>" +
                        "<tr bgcolor=#D3D3D3><th width=35%>Name</th><th width=20%>Note</th><th width=45%>Details</th></tr>";
                    var strQuotODFoll = "<tr><td colspan=3 align=center><br/><strong>Overdue Quotation Followups</strong><br/></td></tr>" +
                        "<tr bgcolor=#D3D3D3><th width=35%>Name</th><th width=20%>Note</th><th width=45%>Details</th></tr>";
                    var strTaskTodayFoll = "<tr><td colspan=3 align=center><br/><strong>Todays Task Followups</strong><br/></td></tr>" +
                       "<tr bgcolor=#D3D3D3><th width=35%>Task</th><th width=20%>Details</th><th width=45%>Creation Date</th></tr>";
                    var strTaskODFoll = "<tr><td colspan=3 align=center><br/><strong>Overdue Task Followups</strong><br/></td></tr>" +
                       "<tr bgcolor=#D3D3D3><th width=35%>Task</th><th width=20%>Details</th><th width=45%>Completion Date</th></tr>";
                    var strCMSTodayFoll = "<tr><td colspan=3 align=center><br/><strong>Todays CMS</strong><br/></td></tr>" +
                       "<tr bgcolor=#D3D3D3><th width=30%>Name</th><th width=40%>Product-Complaint</th><th width=30%>Instructions</th></tr>";
                    var strCMSODFoll = "<tr><td colspan=3 align=center><br/><strong>Overdue CMS</strong><br/></td></tr>" +
                       "<tr bgcolor=#D3D3D3><th width=30%>Name</th><th width=40%>Product-Complaint</th><th width=30%>Instructions</th></tr>";
                    var strCMSFTodayFoll = "<tr><td colspan=3 align=center><br/><strong>Todays CMS Followups</strong><br/></td></tr>" +
                       "<tr bgcolor=#D3D3D3><th width=35%>Name</th><th width=20%>Note</th><th width=45%>Details</th></tr>";
                    var strCMSFODFoll = "<tr><td colspan=3 align=center><br/><strong>Overdue CMS Followups</strong><br/></td></tr>" +
                       "<tr bgcolor=#D3D3D3><th width=35%>Name</th><th width=20%>Note</th><th width=45%>Details</th></tr>";
                    var strAMCTodayFoll = "<tr><td colspan=3 align=center><br/><strong>Todays AMC Followups</strong><br/></td></tr>" +
                       "<tr bgcolor=#D3D3D3><th width=35%>Name</th><th width=20%>Phone</th><th width=45%>Details</th></tr>";
                    var strAMCODFoll = "<tr><td colspan=3 align=center><br/><strong>Overdue AMC Followups</strong><br/></td></tr>" +
                       "<tr bgcolor=#D3D3D3><th width=35%>Name</th><th width=20%>Phone</th><th width=45%>Details</th></tr>";

                    var strInvoiceTodayFoll = "<tr><td colspan=3 align=center><br/><strong>Todays Invoice Followups</strong><br/></td></tr>" +
                       "<tr bgcolor=#D3D3D3><th width=35%>Name</th><th width=20%>Note</th><th width=45%>Details</th></tr>";
                    var strInvoiceODFoll = "<tr><td colspan=3 align=center><br/><strong>Overdue Invoice Followups</strong><br/></td></tr>" +
                        "<tr bgcolor=#D3D3D3><th width=35%>Name</th><th width=20%>Note</th><th width=45%>Details</th></tr>";
                    var strTellecallingTodayFoll = "<tr><td colspan=3 align=center><br/><strong>Todays Tellecalling Followups</strong><br/></td></tr>" +
                        "<tr><th width=35%>Name</th><th width=20%>Note</th><th width=45%>Details</th></tr>";
                    var strTellecallingODFoll = "<tr><td colspan=3 align=center><br/><strong>Overdue Tellecalling Followups</strong><br/></td></tr>" +
                        "<tr bgcolor=#D3D3D3><th width=35%>Name</th><th width=20%>Note</th><th width=45%>Details</th></tr>";
                    var model = new DashboardPageModel();
                    var ef = EnquiryFollowupsRow.Fields;
                    var qf = QuotationFollowupsRow.Fields;
                    var e = EnquiryRow.Fields;
                    var q = QuotationRow.Fields;
                    var t = TasksRow.Fields;
                    var cms = CMSRow.Fields;
                    var a = AMCVisitPlannerRow.Fields;
                    var c = ContactsRow.Fields;
                    var sc = SubContactsRow.Fields;
                    var inv = InvoiceFollowupsRow.Fields;
                    var tel = TeleCallingFollowupsRow.Fields;
                    var cmsf = CMSFollowupsRow.Fields;

                    model.EnqFollowups = connection.List<EnquiryFollowupsRow>(f => f
                                .SelectTableFields()
                                .Select(ef.EnquiryId)
                                .Select(ef.EnquiryContactsId)
                                .Select(ef.FollowupNote)
                                .Select(ef.ContactName)
                                .Select(ef.ContactPhone)
                                .Select(ef.Details)
                                .Where(ef.Status == 1)
                                .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                                .Where(ef.EnquiryAssignedId == (Int32)itm.UserId)
                                );

                    if (model.EnqFollowups.Count <= 0)
                    {
                        strEnqTodayFoll = "";
                    }

                    model.ODEnqFollowups = connection.List<EnquiryFollowupsRow>(f => f
                                .SelectTableFields()
                                .Select(ef.EnquiryId)
                                .Select(ef.EnquiryContactsId)
                                .Select(ef.FollowupNote)
                                .Select(ef.ContactName)
                                .Select(ef.ContactPhone)
                                .Select(ef.Details)
                                .Where(ef.Status == 1)
                                .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                                .Where(ef.EnquiryAssignedId == (Int32)itm.UserId)
                                );

                    if (model.ODEnqFollowups.Count <= 0)
                    {
                        strODEnqFoll = "";
                    }

                    model.QuotFollowups = connection.List<QuotationFollowupsRow>(g => g
                                 .SelectTableFields()
                                 .Select(qf.QuotationId)
                                 .Select(qf.QuotationContactsId)
                                 .Select(qf.FollowupNote)
                                 .Select(qf.Details)
                                 .Select(qf.ContactName)
                                 .Select(qf.ContactPhone)
                                 .Where(qf.Status == 1)
                                 .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                                 .Where(qf.QuotationAssignedId == (Int32)itm.UserId)
                                 );

                    if (model.QuotFollowups.Count <= 0)
                    {
                        strQuotTodayFoll = "";
                    }

                    model.ODQuotFollowups = connection.List<QuotationFollowupsRow>(g => g
                                 .SelectTableFields()
                                 .Select(qf.QuotationId)
                                 .Select(qf.QuotationContactsId)
                                 .Select(qf.FollowupNote)
                                 .Select(qf.Details)
                                 .Select(qf.ContactName)
                                 .Select(qf.ContactPhone)
                                 .Where(qf.Status == 1)
                                 .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                                 .Where(qf.QuotationAssignedId == (Int32)itm.UserId)
                                 );

                    if (model.ODQuotFollowups.Count <= 0)
                    {
                        strQuotODFoll = "";
                    }

                    model.Tasks = connection.List<TasksRow>(ts => ts
                                 .SelectTableFields()
                                 .Select(t.Id)
                                 .Select(t.Task)
                                 .Select(t.Details)
                                 .Select(t.CreationDate)
                                 .Select(t.ExpectedCompletion)
                                 .Select(t.AssignedBy)
                                 .Where(t.Status != 2)
                                 .Where(new Criteria("CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate()))
                                 .Where(t.AssignedTo == (Int32)itm.UserId)
                                 );

                    if (model.Tasks.Count <= 0)
                    {
                        strTaskTodayFoll = "";
                    }

                    model.ODTasks = connection.List<TasksRow>(td => td
                     .SelectTableFields()
                     .Select(t.Id)
                     .Select(t.Task)
                     .Select(t.Details)
                     .Select(t.CreationDate)
                     .Select(t.ExpectedCompletion)
                     .Select(t.AssignedBy)
                     .Select(t.AssignedTo)
                     .Where(t.Status != 2)
                     .Where(new Criteria("CAST(ExpectedCompletion as DATE)<" + DateTime.Now.ToSqlDate()))
                     .Where(t.AssignedTo == (Int32)itm.UserId)
                     );

                    if (model.ODTasks.Count <= 0)
                    {
                        strTaskODFoll = "";
                    }

                    model.CMS = connection.List<CMSRow>(ts => ts
                     .SelectTableFields()
                     .Select(cms.Id)
                     .Select(cms.ContactsName)
                     .Select(cms.ContactsPhone)
                     .Select(cms.Date)
                     .Select(cms.ProductsName)
                     .Select(cms.CompletionDate)
                     .Select(cms.ComplaintComplaintType)
                     .Select(cms.Instructions)
                     .Where(cms.Status == 1)
                     .Where(new Criteria("CAST(ExpectedCompletion as DATE)=" + DateTime.Now.ToSqlDate()))
                     .Where(cms.AssignedTo == (Int32)itm.UserId)
                     );

                    if (model.CMS.Count <= 0)
                    {
                        strCMSTodayFoll = "";
                    }

                    model.ODCMS = connection.List<CMSRow>(td => td
                     .Select(cms.Id)
                     .Select(cms.ContactsName)
                     .Select(cms.Date)
                     .Select(cms.ProductsName)
                     .Select(cms.ContactsPhone)
                     .Select(cms.CompletionDate)
                     .Select(cms.ComplaintComplaintType)
                     .Select(cms.Instructions)
                     .Where(cms.Status == 1)
                     .Where(new Criteria("CAST(ExpectedCompletion as DATE)<" + DateTime.Now.ToSqlDate()))
                     .Where(cms.AssignedTo == (Int32)itm.UserId)
                     );

                    if (model.ODCMS.Count <= 0)
                    {
                        strCMSODFoll = "";
                    }

                    model.InvoiceFollowups = connection.List<InvoiceFollowupsRow>(g => g
                     .SelectTableFields()
                     .Select(inv.InvoiceId)
                     .Select(inv.InvoiceContactsId)
                     .Select(inv.ContactName)
                     .Select(inv.ContactPhone)
                     .Select(inv.FollowupNote)
                     .Select(inv.Details)
                     .Where(inv.Status == 1)
                     .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                     .Where(inv.InvoiceAssignedId == (Int32)itm.UserId)
                     );

                    if (model.InvoiceFollowups.Count <= 0)
                    {
                        strInvoiceTodayFoll = "";
                    }

                    model.ODInvoiceFollowups = connection.List<InvoiceFollowupsRow>(f => f
                     .SelectTableFields()
                     .Select(inv.InvoiceId)
                     .Select(inv.InvoiceContactsId)
                     .Select(inv.ContactName)
                     .Select(inv.ContactPhone)
                     .Select(inv.FollowupNote)
                     .Select(inv.Details)
                     .Where(inv.Status == 1)
                     .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                     .Where(inv.InvoiceAssignedId == (Int32)itm.UserId)
                     );

                    if (model.ODInvoiceFollowups.Count <= 0)
                    {
                        strInvoiceODFoll = "";
                    }

                    model.AMC = connection.List<AMCVisitPlannerRow>(ts => ts
                     .SelectTableFields()
                     .Select(a.Id)
                     .Select(a.AMCContactsId)
                     .Select(a.ContactName)
                     .Select(a.ContactPhone)
                     .Select(a.VisitDetails)
                     .Where(a.Status == 1)
                     .Where(new Criteria("CAST(VisitDate as DATE)=" + DateTime.Now.ToSqlDate()))
                     .Where(a.AssignedTo == (Int32)itm.UserId)
                     );

                    if (model.AMC.Count <= 0)
                    {
                        strAMCTodayFoll = "";
                    }

                    model.ODAMC = connection.List<AMCVisitPlannerRow>(ts => ts
                     .SelectTableFields()
                     .Select(a.Id)
                     .Select(a.AMCContactsId)
                     .Select(a.ContactName)
                     .Select(a.ContactPhone)
                     .Select(a.VisitDetails)
                     .Where(a.Status == 1)
                     .Where(new Criteria("CAST(VisitDate as DATE)<" + DateTime.Now.ToSqlDate()))
                     .Where(a.AssignedTo == (Int32)itm.UserId)
                     );

                    if (model.ODAMC.Count <= 0)
                    {
                        strAMCODFoll = "";
                    }

                    //TeleCalling Followups
                    model.TCFollowups = connection.List<TeleCallingFollowupsRow>(g => g
                     .SelectTableFields()
                     .Select(tel.TeleCallingId)
                     .Select(tel.ContactName)
                     .Select(tel.ContactPhone)
                     .Select(tel.TeleCallingContactsId)
                     .Select(tel.FollowupNote)
                     .Select(tel.Details)
                     .Where(tel.Status == 1)
                     .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                     .Where(tel.RepresentativeId == (Int32)itm.UserId)
                     );

                    if (model.TCFollowups.Count <= 0)
                    {
                        strTellecallingTodayFoll = "";
                    }

                    model.ODTCFollowups = connection.List<TeleCallingFollowupsRow>(f => f
                     .SelectTableFields()
                     .Select(tel.TeleCallingId)
                     .Select(tel.ContactName)
                     .Select(tel.ContactPhone)
                     .Select(tel.TeleCallingContactsId)
                     .Select(tel.FollowupNote)
                     .Select(tel.Details)
                     .Where(tel.Status == 1)
                     .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                     .Where(tel.RepresentativeId == (Int32)itm.UserId)
                     );

                    if (model.ODTCFollowups.Count <= 0)
                    {
                        strTellecallingODFoll = "";
                    }

                    model.CMSFollowups = connection.List<CMSFollowupsRow>(f => f
                                      .SelectTableFields()
                                      .Select(cmsf.CMSId)
                                      .Select(cmsf.CMSContactsId)
                                      .Select(cmsf.FollowupNote)
                                      .Select(cmsf.Details)
                                      .Select(cmsf.ContactName)
                                      .Select(cmsf.ContactPhone)
                                      .Select(cmsf.ProductsName)
                                      .Select(cmsf.CMSInstructions)
                                      .Select(cmsf.ComplaintType)
                                      .Where(cmsf.Status == 1)
                                      .Where(new Criteria("CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate()))
                                      .Where(cmsf.CMSAssignedTo == (Int32)itm.UserId)
                                    );

                    if (model.CMSFollowups.Count <= 0)
                    {
                        strCMSFTodayFoll = "";
                    }

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
                           .Select(cmsf.CMSInstructions)
                           .Where(cmsf.Status == 1)
                           .Where(new Criteria("CAST(FollowupDate as DATE)<" + DateTime.Now.ToSqlDate()))
                           .Where(cmsf.CMSAssignedTo == (Int32)itm.UserId)
                         );

                    if (model.ODCMSFollowups.Count <= 0)
                    {
                        strCMSFODFoll = "";
                    }

                    int cnt = 1;
                    foreach (var item in model.EnqFollowups)
                    {
                        strEnqTodayFoll = strEnqTodayFoll +
                            "<tr><td>" + cnt + ") " + item.ContactName + " - " + item.ContactPhone +
                            "</td><td>" + item.FollowupNote +
                            "</td><td>" + item.Details + "</td></tr>";
                        cnt = cnt + 1;
                    }

                    cnt = 1;
                    foreach (var item in model.ODEnqFollowups)
                    {
                        strODEnqFoll = strODEnqFoll +
                            "<tr><td>" + cnt + ") " + item.ContactName + " - " + item.ContactPhone +
                            "</td><td>" + item.FollowupNote +
                            "</td><td>" + item.Details + "</td></tr>";
                        cnt = cnt + 1;
                    }

                    cnt = 1;
                    foreach (var item in model.QuotFollowups)
                    {
                        strQuotTodayFoll = strQuotTodayFoll +
                            "<tr><td>" + cnt + ") " + item.ContactName + " - " + item.ContactPhone +
                            "</td><td>" + item.FollowupNote +
                            "</td><td>" + item.Details + "</td></tr>";
                        cnt = cnt + 1;
                    }

                    cnt = 1;
                    foreach (var item in model.ODQuotFollowups)
                    {
                        strQuotODFoll = strQuotODFoll +
                            "<tr><td>" + cnt + ") " + item.ContactName + " - " + item.ContactPhone +
                            "</td><td>" + item.FollowupNote +
                            "</td><td>" + item.Details + "</td></tr>";
                        cnt = cnt + 1;
                    }

                    cnt = 1;
                    foreach (var item in model.Tasks)
                    {
                        strTaskTodayFoll = strTaskTodayFoll +
                            "<tr><td>" + cnt + ") " + item.Task +
                            "</td><td>" + item.Details +
                            "</td><td>" + item.CreationDate.Value.ToString("dd MMM yy") + "</td></tr>";
                        cnt = cnt + 1;
                    }

                    cnt = 1;
                    foreach (var item in model.ODTasks)
                    {
                        strTaskODFoll = strTaskODFoll +
                            "<tr><td>" + cnt + ") " + item.Task +
                            "</td><td>" + item.Details +
                            "</td><td>" + item.ExpectedCompletion.Value.ToString("dd MMM yy") + "</td></tr>";
                        cnt = cnt + 1;
                    }

                    cnt = 1;
                    foreach (var item in model.CMS)
                    {
                        strCMSTodayFoll = strCMSTodayFoll +
                            "<tr><td>" + cnt + ") " + item.ContactsName +
                            "</td><td>" + item.ProductsName +
                            "-" + item.ComplaintComplaintType +
                            "</td><td>" + item.Instructions + "</td></tr>";
                        cnt = cnt + 1;
                    }

                    cnt = 1;
                    foreach (var item in model.ODCMS)
                    {
                        strCMSODFoll = strCMSODFoll +
                            "<tr><td>" + cnt + ") " + item.ContactsName +
                            "</td><td>" + item.ProductsName +
                            "-" + item.ComplaintComplaintType +
                            "</td><td>" + item.Instructions + "</td></tr>";
                        cnt = cnt + 1;
                    }

                    cnt = 1;
                    foreach (var item in model.CMSFollowups)
                    {
                        strCMSFTodayFoll = strCMSFTodayFoll +
                            "<tr><td>" + cnt + ") " + item.ContactName +
                            "</td><td>" + item.ProductsName +
                            "-" + item.ComplaintType +
                            "</td><td>" + item.CMSInstructions + "</td></tr>";
                        cnt = cnt + 1;
                    }

                    cnt = 1;
                    foreach (var item in model.ODCMSFollowups)
                    {
                        strCMSFODFoll = strCMSFODFoll +
                            "<tr><td>" + cnt + ") " + item.ContactName +
                            "</td><td>" + item.ProductsName +
                            "-" + item.ComplaintType +
                            "</td><td>" + item.CMSInstructions + "</td></tr>";
                        cnt = cnt + 1;
                    }

                    cnt = 1;
                    foreach (var item in model.AMC)
                    {
                        strAMCTodayFoll = strAMCTodayFoll +
                            "<tr><td>" + cnt + ") " + item.ContactName +
                            "</td><td>" + item.ContactPhone +
                            "</td><td>" + item.VisitDetails + "</td></tr>";
                        cnt = cnt + 1;
                    }

                    cnt = 1;
                    foreach (var item in model.ODAMC)
                    {
                        strAMCODFoll = strAMCODFoll +
                            "<tr><td>" + cnt + ") " + item.ContactName +
                            "</td><td>" + item.ContactPhone +
                            "</td><td>" + item.VisitDetails + "</td></tr>";
                        cnt = cnt + 1;
                    }

                    cnt = 1;
                    foreach (var item in model.TCFollowups)
                    {
                        strTellecallingTodayFoll = strTellecallingTodayFoll +
                            "<tr><td>" + cnt + ") " + item.ContactName + " - " + item.ContactPhone +
                            "</td><td>" + item.FollowupNote +
                            "</td><td>" + item.Details + "</td></tr>";
                        cnt = cnt + 1;
                    }

                    cnt = 1;
                    foreach (var item in model.ODTCFollowups)
                    {
                        strTellecallingODFoll = strTellecallingODFoll +
                            "<tr><td>" + cnt + ") " + item.ContactName + " - " + item.ContactPhone +
                            "</td><td>" + item.FollowupNote +
                            "</td><td>" + item.Details + "</td></tr>";
                        cnt = cnt + 1;
                    }

                    cnt = 1;
                    foreach (var item in model.InvoiceFollowups)
                    {
                        strInvoiceTodayFoll = strInvoiceTodayFoll +
                            "<tr><td>" + cnt + ") " + item.ContactName + " - " + item.ContactPhone +
                            "</td><td>" + item.FollowupNote +
                            "</td><td>" + item.Details + "</td></tr>";
                        cnt = cnt + 1;
                    }

                    cnt = 1;
                    foreach (var item in model.ODInvoiceFollowups)
                    {
                        strInvoiceODFoll = strInvoiceODFoll +
                            "<tr><td>" + cnt + ") " + item.ContactName + " - " + item.ContactPhone +
                            "</td><td>" + item.FollowupNote +
                            "</td><td>" + item.Details + "</td></tr>";
                        cnt = cnt + 1;
                    }


                    //strCMSFODFoll
                    var t_str = strEnqTodayFoll + strODEnqFoll + strQuotTodayFoll + strQuotODFoll + strTaskTodayFoll + strTaskODFoll
                       + strCMSTodayFoll + strCMSODFoll + strCMSFTodayFoll + strCMSFODFoll + strAMCTodayFoll
                       + strAMCODFoll + strInvoiceTodayFoll + strInvoiceODFoll +
                        strTellecallingTodayFoll + strTellecallingODFoll;

                    if (t_str.Trim() != "")
                    {
                        var MailBody = "<html><body>" +
                        "<h3>Hi " + itm.DisplayName + ", below is your work schedule</h3>" +
                        "<table border=1>" +
                        t_str
                         + "</table></body></html>"
                        ;

                        using (var message = new MailMessage(data.admin.EmailId, itm.Email))
                        {

                            message.Subject = "Daily Followups";
                            //message.Body = "Scheduled at " + DateTime.Now; //Message body
                            message.IsBodyHtml = true;
                            message.Body = MailBody; //Message body
                            EmailHelper.Send(message, data.admin.EmailId, data.admin.EmailPassword, (Boolean)data.admin.SSL, data.admin.Host, data.admin.Port.Value);

                        }
                    }
                }
                catch (Exception ex) { }
            }

            return Task.CompletedTask;
        }
    }


    public class DailyEmailModel
    {
        public List<UserRow> User { get; set; }
        public UserRow admin { get; set; }
    }
}