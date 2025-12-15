namespace AdvanceCRM.Common.Pages
{
    using Serenity;
using AdvanceCRM.Web.Helpers;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    
    using System.Linq;
    using AdvanceCRM.Common;
    using AdvanceCRM.ThirdParty;
    using System.Collections.Generic;
    using static MVC.Views.ThirdParty;
    using AdvanceCRM.ThirdParty.Endpoints;
    using Serenity.Services;
    using Microsoft.AspNetCore.Mvc;
    using AdvanceCRM.Common.Calendar;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Authorization;

    [Route("IVRDashboard")]
    public class IVRDashboardController : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IMemoryCache _cache;
        private readonly ILogger<CalendarController> _logger;
        private readonly IRequestContext Context;

        public IVRDashboardController(ISqlConnections connections, IRequestContext context, IMemoryCache cache, ILogger<CalendarController> logger)
        {
            _connections = connections;
            _cache = cache;
            _logger = logger;
            Context = context ?? throw new ArgumentNullException(nameof(context));

        }
        [Authorize, HttpGet, Route("~/IvrDashboard")]
        public ActionResult Index()
        {
            var cachedModel = LocalCache.GetLocalStoreOnly("IVRDashboardPageModel", TimeSpan.FromSeconds(1),
                KnowlarityDetailsRow.Fields.GenerationKey, () =>
                {
                    var model = new IVRDashboardPageModel();
                    var today = DateTime.Today;
                    var startOfMonth = new DateTime(today.Year, today.Month, 1);
                    var startOfNextMonth = startOfMonth.AddMonths(1);

                    var o = KnowlarityDetailsRow.Fields;



                    using (var connection = _connections.NewFor<KnowlarityDetailsRow>())
                    {

                        // Count records with Duration = 0 (Miss Call)
                        model.MissCall = connection.Count<KnowlarityDetailsRow>(o.Duration == 0);

                        model.AnswerCall = connection.Count<KnowlarityDetailsRow>(o.EmployeeNumber.StartsWith("+91"));


                        model.TotalCall = model.MissCall + model.AnswerCall;

                        // Count records with EmployeeNumber = 'Welcome Sound'
                        //model.WelcomeSound = connection.Count<KnowlarityDetailsRow>(o.EmployeeNumber.Contains("Welcome Sound"));

                        // Count records with EmployeeNumber = 'User Disconnected'
                        //model.UserDisconnected = connection.Count<KnowlarityDetailsRow>(o.EmployeeNumber.Contains("User Disconnected"));

                        // Count records with EmployeeNumber LIKE 'Customer Missed%'
                        model.CustomerMissed = connection.Count<KnowlarityDetailsRow>(o.EmployeeNumber.StartsWith("Customer Missed"));

                        // Count records with EmployeeNumber LIKE 'Agent Missed%'
                        model.AgentMissed = connection.Count<KnowlarityDetailsRow>(o.EmployeeNumber.StartsWith("Agent Missed"));


                        model.AnsweredCalls = connection.List<KnowlarityDetailsRow>(f => f
                                .SelectTableFields()
                                .Select(o.Recording)
                                .Where(o.Recording != string.Empty)
                                );

                        model.TodaysCalls = connection.List<KnowlarityDetailsRow>(f => f
                                .SelectTableFields()
                                .Select(o.DateTime)
                                .Where(o.DateTime >= today && o.DateTime < today.AddDays(1))
                                );

                        model.TotalMonthlyCalls = connection.List<KnowlarityDetailsRow>(f => f
                               .SelectTableFields()
                               .Select(o.DateTime)
                               .Where(o.DateTime >= startOfMonth && o.DateTime < startOfNextMonth)
                               );
                    }

                    return model;
                });

            return View(MVC.Views.Common.Dashboard.IVRDashboardIndex, cachedModel);
        }


    }
}