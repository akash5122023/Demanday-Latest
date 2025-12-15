
using AdvanceCRM.Masters;
using AdvanceCRM.Enquiry;
using AdvanceCRM.ThirdParty;
using AdvanceCRM.Settings;
using AdvanceCRM.Administration;
using Serenity;
using Serenity.Data;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Serenity.Services;
using Serenity.Extensions.DependencyInjection;

namespace AdvanceCRM.Modules.Reports.IVR
{
    [Route("IVRReport")]
    [ReadPermission("Reports:IVRReport")]
    public class IVRReportController : Controller
    {
        private readonly ISqlConnections _connections;
        private IRequestContext Context { get; }

        public IVRReportController(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context;
        }

        public IVRReportController()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }

        // GET: GSTR1 B2B
        [HttpGet, Route("~/Reports/IVRReport")]
        public ActionResult Index()
        {

            return View(MVC.Views.Reports.IVR.IVRReportView);
        }


        public ActionResult LoadData(string StartDate, string EndDate)
        {
            try
            {

                var model = new IVRPageModel();

                if (StartDate == null && EndDate == null)
                {
                    model.StartDate = DateTime.Now.AddMonths(-1);
                    model.EndDate = DateTime.Now;
                }
                else
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    model.StartDate = Convert.ToDateTime(StartDate, culture);
                    model.EndDate = Convert.ToDateTime(EndDate, culture);
                }
                var user = (UserDefinition)Context.User.ToUserDefinition();
                var ucom = ((UserDefinition)Context.User.ToUserDefinition()).CompanyId;
                var IVR = IVRConfigurationRow.Fields;
                var know = KnowlarityDetailsRow.Fields;
                var agents = KnowlarityAgentsRow.Fields;
                var com = CompanyDetailsRow.Fields;

                using (var connection = _connections.NewFor<KnowlarityAgentsRow>())
                {

                    //Sales  List
                    model.IVRList = connection.List<KnowlarityDetailsRow>(us => us
                            .SelectTableFields()
                            .Select(know.Id)
                            .Select(know.EmployeeName)
                            .Select(know.EmployeeNumber)
                            .Select(know.Duration)
                            .Select(know.CustomerNumber)                         
                            .Where(know.DateTime >= model.StartDate)
                            .Where(know.DateTime <= model.EndDate)
                            );

                    //Sales Product  List
                    model.Agentslist = connection.List<KnowlarityAgentsRow>(us => us
                            .SelectTableFields()
                            .Select(agents.Id)
                            .Select(agents.Name)
                            .Select(agents.Number)  
                            );

                }
                return PartialView(MVC.Views.Reports.IVR.IVRReportView, model);
            }
            catch (Exception ex)
            {
                //return PartialView(MVC.Views.Reports.GST.GSTR1B2BPartialView,ex.Message);
                return Json(ex.Message);
            }

        }


        [HttpGet]
        public FileContentResult ExportCSV(string StartDate, string EndDate)
        {
            try
            {
                var model = new IVRPageModel();

                if (StartDate == null && EndDate == null)
                {
                    model.StartDate = DateTime.Now.AddMonths(-1);
                    model.EndDate = DateTime.Now;
                }
                else
                {
                    CultureInfo culture = new CultureInfo("en-US");
                    model.StartDate = Convert.ToDateTime(StartDate, culture);
                    model.EndDate = Convert.ToDateTime(EndDate, culture);
                }
                var user = (UserDefinition)Context.User.ToUserDefinition();
                var ucom = ((UserDefinition)Context.User.ToUserDefinition()).CompanyId;
                var IVR = IVRConfigurationRow.Fields;
                var know = KnowlarityDetailsRow.Fields;
                var agents = KnowlarityAgentsRow.Fields;
                var com = CompanyDetailsRow.Fields;

                using (var connection = _connections.NewFor<KnowlarityAgentsRow>())
                {

                    //Sales  List
                    model.IVRList = connection.List<KnowlarityDetailsRow>(us => us
                            .SelectTableFields()
                            .Select(know.Id)
                            .Select(know.EmployeeName)
                            .Select(know.EmployeeNumber)
                            .Select(know.Duration)
                            .Select(know.CustomerNumber)
                            .Where(know.DateTime >= model.StartDate)
                            .Where(know.DateTime <= model.EndDate)
                            );

                    //Sales Product  List
                    model.Agentslist = connection.List<KnowlarityAgentsRow>(us => us
                            .SelectTableFields()
                            .Select(agents.Id)
                            .Select(agents.Name)
                            .Select(agents.Number)
                            );


                    var sb = new StringBuilder();
                    sb.AppendFormat("{0},{1},{2},{3},{4}", "Agent", "AgentNumber", "TotalCalls", "AvgCallDuration", "Enquiry Added", Environment.NewLine);
                    foreach (var line in model.Agentslist)
                    {
                        //string temp_str = know.EmployeeNumber == line.Number " AND (AssignedId = " + Context.User.GetIdentifier().ToString() + fstr + ")";
                        //string temp_str = know.EmployeeNumber == line.Number +" AND " + "CAST(FollowupDate as DATE)=" + DateTime.Now.ToSqlDate() + ")";

                        model.totalcalls= connection.Count<KnowlarityDetailsRow>(know.EmployeeNumber == line.Number && know.DateTime >= model.StartDate && know.DateTime <= model.EndDate);
                        model.calldurationlist= connection.List<KnowlarityDetailsRow>(know.EmployeeNumber == line.Number && know.DateTime >= model.StartDate && know.DateTime <= model.EndDate);
                        model.totalenquiry = connection.Count<KnowlarityDetailsRow>(know.EmployeeNumber == line.Number && know.IsMoved=="true" && know.DateTime >= model.StartDate && know.DateTime <= model.EndDate);

                        dynamic dur = 0,avgdur=0;
                        foreach (var duration in model.calldurationlist)
                        {
                            dur =dur + duration.Duration;
                        }
                        avgdur = dur / model.totalcalls;

                        sb.AppendFormat("{0},{1},{2},{3},{4}", line.Number, line.Name, model.totalcalls, avgdur, model.totalenquiry, Environment.NewLine);
                      }

		

                    var byteData = ASCIIEncoding.ASCII.GetBytes(sb.ToString());
                    return new FileContentResult(byteData, "text/csv");
                }

            }
            catch (Exception ex)
            {
                //TempData["Errormessage"] = ex.Message;	
                return null;
            }

        }

 
    }
}