namespace AdvanceCRM.Common.Calendar
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Web.Helpers;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Sales;
    using AdvanceCRM.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Serenity;
    using Serenity.Data;
    using Serenity.Web;
    using System;
    using System.Collections.Generic;
    using Serenity.Services;

    public class CalendarController : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IMemoryCache _cache;
        private readonly ILogger<CalendarController> _logger;
        private readonly IRequestContext Context;

        public CalendarController(ISqlConnections connections,IRequestContext context, IMemoryCache cache, ILogger<CalendarController> logger)
        {
            _connections = connections;
            _cache = cache;
            _logger = logger;
            Context = context ?? throw new ArgumentNullException(nameof(context));

        }

        [Route("Calendar")]
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var cacheKey = "AppointmentCalendarModel:" + UserRow.Fields.GenerationKey;
            var cachedModel = _cache.GetOrCreate(cacheKey, entry =>
                {
                    var model = new CalendarModel();
                    using (var connection = _connections.NewFor<UserRow>())
                    {
                        var od = UserRow.Fields;

                        // Getting hierarchy list
                        List<UserRow> Users1 = connection.List<UserRow>(uq => uq
                            .SelectTableFields()
                            .Select(od.UserId)
                            .Select(od.Username)
                            .Where(od.UpperLevel == Context.User.GetIdentifier()));

                        List<UserRow> Users2 = connection.List<UserRow>(uq => uq
                            .SelectTableFields()
                            .Select(od.UserId)
                            .Select(od.Username)
                            .Where(od.UpperLevel2 == Context.User.GetIdentifier()));

                        List<UserRow> Users3 = connection.List<UserRow>(uq => uq
                            .SelectTableFields()
                            .Select(od.UserId)
                            .Select(od.Username)
                            .Where(od.UpperLevel3 == Context.User.GetIdentifier()));

                        List<UserRow> Users4 = connection.List<UserRow>(uq => uq
                            .SelectTableFields()
                            .Select(od.UserId)
                            .Select(od.Username)
                            .Where(od.UpperLevel4 == Context.User.GetIdentifier()));

                        List<UserRow> Users5 = connection.List<UserRow>(uq => uq
                            .SelectTableFields()
                            .Select(od.UserId)
                            .Select(od.Username)
                            .Where(od.UpperLevel5 == Context.User.GetIdentifier()));

                        var str1 = ""; var str2 = ""; var str3 = ""; var str4 = ""; var str5 = "";

                        model.Users = new List<string>();
                        int i = 0;
                        foreach (var item in Users1)
                        {
                            if (i == 0)
                                str1 = "AssignedId = " + item.UserId.Value;
                            else
                                str1 = str1 + " OR AssignedId = " + item.UserId.Value;

                            i++;
                            if (!model.Users.Contains(item.Username))
                                model.Users.Add(item.Username);
                        }

                        foreach (var item in Users2)
                        {
                            if (i == 0)
                                str2 = "AssignedId = " + item.UserId.Value;
                            else
                                str2 = str2 + " OR AssignedId = " + item.UserId.Value;

                            i++;
                            if (!model.Users.Contains(item.Username))
                                model.Users.Add(item.Username);
                        }

                        foreach (var item in Users3)
                        {
                            if (i == 0)
                                str3 = "AssignedId = " + item.UserId.Value;
                            else
                                str3 = str3 + " OR AssignedId = " + item.UserId.Value;

                            i++;
                            if (!model.Users.Contains(item.Username))
                                model.Users.Add(item.Username);
                        }

                        foreach (var item in Users4)
                        {
                            if (i == 0)
                                str4 = "AssignedId = " + item.UserId.Value;
                            else
                                str4 = str4 + " OR AssignedId = " + item.UserId.Value;

                            i++;
                            if (!model.Users.Contains(item.Username))
                                model.Users.Add(item.Username);
                        }

                        foreach (var item in Users5)
                        {
                            if (i == 0)
                                str5 = "AssignedId = " + item.UserId.Value;
                            else
                                str5 = str5 + " OR AssignedId = " + item.UserId.Value;

                            i++;
                            if (!model.Users.Contains(item.Username))
                                model.Users.Add(item.Username);
                        }

                        string fstr = str1 + str2 + str3 + str4 + str5;

                        if (!string.IsNullOrWhiteSpace(fstr))
                        {
                            fstr = " OR " + fstr;
                        }

                        var c = ContactsRow.Fields;
                        model.Contacts = connection.List<ContactsRow>(q => q
                            .SelectTableFields()
                            .Select(c.Id)
                            .Select(c.Name));

                        string temp_str = "AssignedId = " + Context.User.GetIdentifier().ToString() + fstr;
                        temp_str = temp_str.Replace("AssignedId", "RepresentativeId");

                        var e = EnquiryAppointmentsRow.Fields;
                        model.Enquiry = connection.List<EnquiryAppointmentsRow>(q => q
                            .SelectTableFields()
                            .Select(e.EnquiryContactsId)
                            .Select(e.AppointmentDate)
                            .Select(e.RepresentativeId)
                            .Select(e.Details)
                            .Select(e.RepresentativeDisplayName)
                            .Where(e.Status == 1 || e.Status == 2) // Include closed
                            .Where(e.AppointmentDate.IsNotNull())
                            .Where(new Criteria("(" + temp_str + ")")));

                        var qt = QuotationAppointmentsRow.Fields;
                        model.Quotation = connection.List<QuotationAppointmentsRow>(q => q
                            .SelectTableFields()
                            .Select(qt.QuotationContactsId)
                            .Select(qt.AppointmentDate)
                            .Select(qt.RepresentativeId)
                            .Select(qt.Details)
                            .Select(qt.RepresentativeDisplayName)
                            .Where(qt.Status == 1 || qt.Status == 2) // Include closed
                            .Where(qt.AppointmentDate.IsNotNull())
                            .Where(new Criteria("(" + temp_str + ")")));

                        var p = InvoiceAppointmentsRow.Fields;
                        model.Proforma = connection.List<InvoiceAppointmentsRow>(q => q
                            .SelectTableFields()
                            .Select(p.InvoiceContactsId)
                            .Select(p.AppointmentDate)
                            .Select(p.RepresentativeId)
                            .Select(p.Details)
                            .Select(p.RepresentativeDisplayName)
                            .Where(p.Status == 1 || p.Status == 2) // Include closed
                            .Where(p.AppointmentDate.IsNotNull())
                            .Where(new Criteria("(" + temp_str + ")")));

                        var t = TeleCallingAppointmentsRow.Fields;
                        model.TeleCalling = connection.List<TeleCallingAppointmentsRow>(q => q
                            .SelectTableFields()
                            .Select(t.TeleCallingContactsId)
                            .Select(t.AppointmentDate)
                            .Select(t.RepresentativeId)
                            .Select(t.Details)
                            .Select(t.RepresentativeDisplayName)
                            .Where(t.Status == 1 || t.Status == 2) // Include closed
                            .Where(t.AppointmentDate.IsNotNull())
                            .Where(new Criteria("(" + temp_str + ")")));

                        temp_str = temp_str.Replace("RepresentativeId", "AssignedTo");
                        var a = AMCVisitPlannerRow.Fields;
                        model.AMC = connection.List<AMCVisitPlannerRow>(q => q
                            .SelectTableFields()
                            .Select(a.AMCContactsId)
                            .Select(a.VisitDate)
                            .Select(a.AssignedTo)
                            .Select(a.VisitDetails)
                            .Select(a.AssignedToDisplayName)
                            .Where(a.Status == 1 || a.Status == 2) // Include closed
                            .Where(a.VisitDate.IsNotNull())
                            .Where(new Criteria("(" + temp_str + ")")));
                    }
                    return model;
                });

            return View(MVC.Views.Common.Calendar.CalendarIndex, cachedModel);
        }
    }
}
