
namespace AdvanceCRM.Common.Pages
{
    using Serenity;
using AdvanceCRM.Web.Helpers;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    
    using Administration;
    using System.IO;
    using System.Net;
    using AdvanceCRM.Reports;
    using System.Data;
    using System.Linq;
    using Serenity.Services;
    using Microsoft.AspNetCore.Authorization;
    using System.Web;
    using System.Net.Mail;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Products;
    using AdvanceCRM.Tasks;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Attendance;
    using AdvanceCRM.Common;
    using AdvanceCRM.Services;
    using AdvanceCRM.Purchase;
    using AdvanceCRM.Sales;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Configuration;
    using Microsoft.AspNetCore.Mvc;
    using AdvanceCRM.Common.Calendar;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;

    [Route("AttendanceDashboard")]
    public class AttendancePageController : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IMemoryCache _cache;
        private readonly ILogger<CalendarController> _logger;
        private readonly IRequestContext Context;

        public AttendancePageController(ISqlConnections connections, IRequestContext context, IMemoryCache cache, ILogger<CalendarController> logger)
        {
            _connections = connections;
            _cache = cache;
            _logger = logger;
            Context = context ?? throw new ArgumentNullException(nameof(context));

        }
        [Authorize, HttpGet, Route("~/AttendanceDashboard")]
        public ActionResult Index()
        {
            var cachedModel = LocalCache.GetLocalStoreOnly("AttendancePageModel", TimeSpan.FromSeconds(1),
                UserRow.Fields.GenerationKey, () =>
                {
                var model = new AttendancePageModel();
                var att = AttendanceRow.Fields;
                var usr = UserRow.Fields;
                  //  var PunchIn = new AttendanceRow();



                var user = (UserDefinition)Context.User.ToUserDefinition();

                using (var connection = _connections.NewFor<AttendanceRow>())
                {
                    model.TotalEmployee = connection.Count<UserRow>();
                    model.punchoutEmployee = connection.Count<AttendanceRow>(att.PunchIn!=att.PunchOut && (new Criteria("CAST(PunchOut as DATE)="+DateTime.Now.ToSqlDate())));
                        model.TotalInactive = connection.Count<UserRow>(usr.IsActive == 0);
                   // model.TotalPunchedIn = connection.Count<AttendanceRow>(att.DateNTime == DateTime.Now.ToSqlDate());

                      model.Coordinates = connection.List<UserRow>(f => f
                        .SelectTableFields()
                        .Select(usr.Location)
                        .Select(usr.Coordinates)
                        .Select(usr.DisplayName));

                       // object p = Console.log(coordinates);

                        var PunchIn = connection.List<AttendanceRow>(f => f
                        .SelectTableFields()
                        .Select(att.DateNTime)
                        .Select(att.PunchIn)
                        .Select(att.PunchOut)
                        .Select(att.Name)
                        .Where(new Criteria("CAST(PunchIn as DATE)=" + DateTime.Now.ToSqlDate())));

                        var PunchOut = connection.List<AttendanceRow>(f => f
                        .SelectTableFields()
                        .Select(att.DateNTime)
                        .Select(att.PunchIn)
                        .Select(att.PunchOut)
                        .Select(att.Name)
                        .Where(new Criteria("CAST(PunchOut as DATE)=" + DateTime.Now.ToSqlDate())));
                     //  .Where(new Criteria("CAST(PunchOut as DATE) >"+)));

                        model.TotalPunchedIn =  0;
                        model.TotalPunchedOut = 0;
                        foreach(var pin in PunchIn)
                        {
                           ++ model.TotalPunchedIn;
                        }
                        foreach (var pout in PunchOut)
                        {
                            ++model.TotalPunchedOut;
                        }

                        //foreach (var item in coordinates)
                        //{
                        //    //if (Convert.ToString(item.PunchIn) == DateTime.Now.ToSqlDate())
                        //    //{
                        //    //    ++model.TotalPunchedIn;
                        //    //    model.wonquoamt += item.total;
                        //    //    model.quoclosure += (item.closingdate.value - item.date.value).totaldays;
                        //    //}
                        //    //else if (item.closingtype == masters.closingtypemaster.lost)
                        //    //{
                        //    //    ++model.lostquo;
                        //    //    model.lostquoamt += item.total;
                        //    //    model.quoclosure += (item.closingdate.value - item.date.value).totaldays;
                        //    //}
                        //}

                        //string markers = "[";
                        //string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
                        //SqlCommand cmd = new SqlCommand("SELECT * FROM Users");
                        //using (SqlConnection con = new SqlConnection(conString))
                        //{
                        //    cmd.Connection = con;
                        //    con.Open();
                        //    using (SqlDataReader sdr = cmd.ExecuteReader())
                        //    {
                        //        while (sdr.Read())
                        //        {
                        //            markers += "{";
                        //            markers += string.Format("'Coordinates': '{0}',", sdr["Coordinates"]);
                        //            markers += string.Format("'location': '{0}',", sdr["Location"]);
                                   
                        //            markers += "},";
                        //        }
                        //    }
                        //    con.Close();
                        //}

                        //markers += "];";
                        //ViewBag.Markers = markers;


                        // model.TotalPunchedIn = connection.Count<AttendanceRow>(new Criteria("CAST(ExpectedCompletion as DATE)=" DateTime.Now.ToSqlDate() && att.PunchOut != att.PunchIn);
                    }

                    return model;
                });

            return View(MVC.Views.Common.Dashboard.AttendanceIndex, cachedModel);
        }

       
     
    }
}
