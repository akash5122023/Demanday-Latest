using AdvanceCRM.Settings;
using PagedList;
using Serenity.Data;
using System;
using AdvanceCRM.Masters;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
//using System.Web.Script.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Serenity.Abstractions;
using Serenity.Services;

namespace AdvanceCRM.Modules.ThirdParty.IVRDetails
{
    [Authorize, Route("ThirdParty/IVR")]
    [ReadPermission("IVR:Inbox")]
    public class IVRController : Controller
    {
        private readonly ISqlConnections _connections;
        private readonly IUserAccessor _userAccessor;
        private readonly IPermissionService _permissionService;
        private readonly IRequestContext _requestContext;
        private readonly IMemoryCache _memoryCache;
        private readonly ITypeSource _typeSource;
        private readonly IUserRetrieveService _userRetriever;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public IVRController(
            IUserAccessor userAccessor,
            ISqlConnections connections,
            IConfiguration configuration,
            IWebHostEnvironment env,
            IPermissionService permissionService,
            IRequestContext requestContext,
            IMemoryCache memoryCache,
            ITypeSource typeSource,
            IUserRetrieveService userRetriever)
        {
            _userAccessor = userAccessor;
            _permissionService = permissionService;
            _requestContext = requestContext;
            _memoryCache = memoryCache;
            _typeSource = typeSource;
            _userRetriever = userRetriever;
            _connections = connections;
            _configuration = configuration;
            _env = env;
        }
        [Authorize, HttpGet, Route("~/IVR")]
        public ActionResult Index(int? Page, int? Type, string StartDate, string EndDate)
        {
            using (var connection = _connections.NewFor<IVRConfigurationRow>())
            {
                var s = IVRConfigurationRow.Fields;
                IVRConfig = connection.TryById<IVRConfigurationRow>(1, q => q
                    .SelectTableFields()
                    .Select(s.IVRType)
                    .Select(s.IVRNumber)
                    .Select(s.ApiKey)
                    .Select(s.Plan)
                    .Select(s.AppId)
                    .Select(s.AppSecret)
                    );
            }
            if (IVRConfig.IVRType == Masters.IVRTypeMaster.TeleCMI)
            {
                try
                {
                    IVRTeleCMIModel model = new IVRTeleCMIModel();

                    if (!String.IsNullOrEmpty(StartDate) && !String.IsNullOrEmpty(EndDate))
                    {
                        model.StartDate = Convert.ToDateTime(StartDate);
                        model.EndDate = Convert.ToDateTime(EndDate);
                    }
                    else
                    {
                        TimeSpan ts = new TimeSpan(0, 0, 0);
                        model.StartDate = DateTime.Now.AddDays(-7).Date + ts;
                        model.EndDate = Convert.ToDateTime(DateTime.Now);
                    }

                    DateTimeOffset st = DateTime.SpecifyKind(model.StartDate, DateTimeKind.Local);
                    DateTimeOffset et = DateTime.SpecifyKind(model.EndDate, DateTimeKind.Local);
                    long start_time = st.ToUnixTimeMilliseconds();
                    long end_time = et.ToUnixTimeMilliseconds();

                    model.Page = Page.HasValue ? Page.Value : 1;
                    model.Type = Type.HasValue ? Type.Value : 1;

                    model.list = GetTeleCMIData(start_time, end_time, model.Page, model.Type);

                    return View(MVC.Views.ThirdParty.IVRDetails.IVRTeleCMI, model);
                }
                catch (Exception ex)
                {
                    TempData["Errormessage"] = ex.Message.ToString();
                }
                return View(MVC.Views.ThirdParty.IVRDetails.IVRTeleCMI, new IVRTeleCMIModel());

            }
            else
            {
                try
                {
                    IVRKnowlarityModel model = new IVRKnowlarityModel();

                    if (!String.IsNullOrEmpty(StartDate) && !String.IsNullOrEmpty(EndDate))
                    {
                        model.StartDate = Convert.ToDateTime(StartDate);
                        model.EndDate = Convert.ToDateTime(EndDate);
                    }
                    else
                    {
                        model.StartDate = DateTime.Now.AddDays(-7);
                        model.EndDate = DateTime.Now;
                    }

                    string start_time = model.StartDate.ToString("yyyy-MM-dd") + " 00:00:00";//+05:30
                    string end_time = model.EndDate.ToString("yyyy-MM-dd") + " " + System.DateTime.Now.ToString("HH:mm:ss");//+ "+05:30"

                    start_time = start_time.Replace(" ", "%20");
                    start_time = start_time.Replace("+", "%2B");
                    start_time = start_time.Replace(":", "%3A");

                    end_time = end_time.Replace(" ", "%20");
                    end_time = end_time.Replace("+", "%2B");
                    end_time = end_time.Replace(":", "%3A");

                    if (IVRConfig.IVRNumber != null)
                    {
                        model.IVRNumbers = IVRConfig.IVRNumber.Split(',').ToList<string>();
                    }
                    model.Page = Page.HasValue ? Page.Value : 1;
                    model.Type = Type.HasValue ? Type.Value : 0;
                    model.list = GetKnowlarityData(start_time, end_time, model.Type, model.Page);

                    return View(MVC.Views.ThirdParty.IVRDetails.IVRKnowlarity, model);
                }
                catch (Exception ex)
                {
                    TempData["Errormessage"] = ex.Message.ToString();
                }
                return View(MVC.Views.ThirdParty.IVRDetails.IVRKnowlarity, new IVRKnowlarityModel());
            }
        }

        [HttpGet]
        public ActionResult LoadKnowlarityData(string StartDate, string EndDate, int? Index)
        {
            IVRKnowlarityModel model = new IVRKnowlarityModel();

            try
            {
                using (var connection = _connections.NewFor<IVRConfigurationRow>())
                {
                    var s = IVRConfigurationRow.Fields;
                    IVRConfig = connection.TryById<IVRConfigurationRow>(1, q => q
                        .SelectTableFields()
                        .Select(s.IVRNumber)
                        .Select(s.ApiKey)
                        .Select(s.Plan)
                        );
                }

                if (!String.IsNullOrEmpty(StartDate) && !String.IsNullOrEmpty(EndDate))
                {
                    model.StartDate = Convert.ToDateTime(StartDate);
                    model.EndDate = Convert.ToDateTime(EndDate);
                }
                else
                {
                    model.StartDate = DateTime.Now.AddDays(-7);
                    model.EndDate = DateTime.Now;
                }

                string start_time = model.StartDate.ToString("yyyy-MM-dd") + " 00:00:00";//+05:30
                string end_time = model.EndDate.ToString("yyyy-MM-dd") + " " + " 23:59:59";//+ "+05:30"System.DateTime.Now.ToString("HH:mm:ss")

                start_time = start_time.Replace(" ", "%20");
                start_time = start_time.Replace("+", "%2B");
                start_time = start_time.Replace(":", "%3A");

                end_time = end_time.Replace(" ", "%20");
                end_time = end_time.Replace("+", "%2B");
                end_time = end_time.Replace(":", "%3A");

                if (IVRConfig.IVRNumber != null)
                {
                    model.IVRNumbers = IVRConfig.IVRNumber.Split(',').ToList<string>();
                }
                model.Page = 1;
                model.Type = Index.HasValue ? Index.Value : 0;
                model.list = GetKnowlarityData(start_time, end_time, model.Type, model.Page);

            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message.ToString();
            }
            return PartialView(MVC.Views.ThirdParty.IVRDetails.IVRKnowlarityData, model);
        }

        [HttpGet]
        public ActionResult LoadTeleCMIData(string StartDate, string EndDate, int? Type)
        {
            IVRTeleCMIModel model = new IVRTeleCMIModel();

            try
            {
                using (var connection = _connections.NewFor<IVRConfigurationRow>())
                {
                    var s = IVRConfigurationRow.Fields;
                    IVRConfig = connection.TryById<IVRConfigurationRow>(1, q => q
                        .SelectTableFields()
                        .Select(s.IVRNumber)
                        .Select(s.ApiKey)
                        .Select(s.Plan)
                        );
                }

                if (!String.IsNullOrEmpty(StartDate) && !String.IsNullOrEmpty(EndDate))
                {
                    model.StartDate = Convert.ToDateTime(StartDate);
                    model.EndDate = Convert.ToDateTime(EndDate);
                }
                else
                {
                    model.StartDate = DateTime.Now.AddDays(-7);
                    model.EndDate = DateTime.Now;
                }

                DateTimeOffset st = DateTime.SpecifyKind(model.StartDate, DateTimeKind.Local);
                DateTimeOffset et = DateTime.SpecifyKind(model.EndDate, DateTimeKind.Local);
                long start_time = st.ToUnixTimeMilliseconds();
                long end_time = et.ToUnixTimeMilliseconds();

                model.Page = 1;
                model.Type = Type.HasValue ? Type.Value : 1;

                model.list = GetTeleCMIData(start_time, end_time, model.Page, model.Type);

            }
            catch (Exception ex)
            {
                TempData["Errormessage"] = ex.Message.ToString();
            }
            return PartialView(MVC.Views.ThirdParty.IVRDetails.IVRTeleCMIData, model);
        }

        private IPagedList<IVRKnowlarityList> GetKnowlarityData(string start_time, string end_time, int num_index, int page)
        {
            List<string> k_num;
            k_num = null;
            if (page < 1)
                page = 1;
            int offset = (page - 1) * 10;

            if (IVRConfig.IVRNumber != null)
            {
                k_num = IVRConfig.IVRNumber.Split(',').ToList<string>();
            }

            if (k_num != null)
            {
                if (num_index >= k_num.Count || num_index < 0)
                    num_index = 0;

                string knowlarity_no = k_num.ElementAt(num_index).Trim();

                knowlarity_no = knowlarity_no.Replace("+", "%2B");

                string apiURL = ("https://kpi.knowlarity.com/" + IVRConfig.Plan + "/v1/account/calllog?start_time=" + start_time.Trim() + "&end_time=" + end_time.Trim() + "&knowlarity_number=" + knowlarity_no.Trim()) + "&limit=500&offset=" + offset;//


                //string apiURL = "https://kpi.knowlarity.com/Basic/v1/account/calllog?start_time=2016-01-01%2012%3A00%3A00%2B05%3A30&end_time=2017-07-17%2011%3A54%3A00%2B05%3A30&knowlarity_number=%2B919021023456&limit=200";

                //String uri = apiURL + "?limit=10";

                // Create a new 'HttpWebRequest' Object to the mentioned URL.
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(apiURL);
                //myHttpWebRequest.Headers.Add("channel", data.IVRConfig.Plan.ToString().Trim());
                myHttpWebRequest.Headers.Add("x-api-key: L3WEMukeez1uFPZhiEv6a6vDf2pu9pJPjtziFqN7");
                myHttpWebRequest.Headers.Add("authorization: " + IVRConfig.ApiKey.ToString().Trim());
                myHttpWebRequest.ContentType = "application/json";
                //myHttpWebRequest.Headers.Add("start_time", start_time);
                //myHttpWebRequest.Headers.Add("end_time", end_time);
                myHttpWebRequest.Headers.Add("cache-control", "no-cache");
                //myHttpWebRequest.Headers.Add("limit", "200");


                // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds.
                myHttpWebRequest.Timeout = 15000;

                HttpWebResponse myHttpWebResponse;

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                var reader = new StreamReader(myHttpWebResponse.GetResponseStream());

                using var doc = JsonDocument.Parse(reader.ReadToEnd());
                var obj = doc.RootElement;

                List<IVRKnowlarityList> listRecords = new List<IVRKnowlarityList>();

                var meta = obj.GetProperty("meta");
                var totalCount = meta.GetProperty("total_count").GetInt32();

                foreach (var o in obj.GetProperty("objects").EnumerateArray())
                {
                    IVRKnowlarityList knowlarityRecord = new IVRKnowlarityList();
                    knowlarityRecord.CustomerNumber = o.GetProperty("customer_number").GetString();
                  // knowlarityRecord.RN = !IndiaMartResponsObject.ContainsKey("RN") ? "" : Convert.ToString(IndiaMartResponsObject["RN"]).Replace("'", "");
                    knowlarityRecord.EmployeeNumber = o.GetProperty("agent_number").GetString();
                    knowlarityRecord.Duration = o.GetProperty("call_duration").GetRawText();
                    knowlarityRecord.Recording = o.GetProperty("call_recording").GetString();
                    knowlarityRecord.IVRNumber = o.GetProperty("knowlarity_number").GetString();
                    knowlarityRecord.DateTime = o.GetProperty("start_time").GetString();

                    listRecords.Add(knowlarityRecord);
                    //listRecords.Add("INSERT INTO KnowlarityIVR ([Mobile],[EmpMobile],[IVRNo],[Recording],[Date],[Duration]) VALUES " +
                    //       "('" + knowlarityRecord.CustomerNumber + "','" +
                    //             knowlarityRecord.EmployeeNumber + "','" +
                    //             knowlarityRecord.IVRNumber + "','" +
                    //             knowlarityRecord.Recording + "','" +
                    //             knowlarityRecord.DateTime + "','" +
                    //             knowlarityRecord.Duration + "')");

                }
                IPagedList<IVRKnowlarityList> Records = new StaticPagedList<IVRKnowlarityList>(listRecords, page, 10, totalCount);
                return Records;
            }

            return null;
        }

        private IPagedList<IVRTeleCMIList> GetTeleCMIData(long start_time, long end_time, int page, int type)
        {

            string apiUrl = "https://piopiy.telecmi.com/v1/";

            switch (type)
            {
                case 1:
                    apiUrl = apiUrl + "answered";
                    break;
                case 2:
                    apiUrl = apiUrl + "missed";
                    break;
                case 3:
                    apiUrl = apiUrl + "outanswered";
                    break;
                case 4:
                    apiUrl = apiUrl + "outmissed";
                    break;
                default:
                    apiUrl = apiUrl + "answered";
                    break;
            }

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            myHttpWebRequest.ContentType = "application/json";
            myHttpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
            {
                string json = "{\"appid\":" + IVRConfig.AppId + "," +
                              "\"token\":\"" + IVRConfig.AppSecret + "\"," +
                              "\"start_date\":" + start_time + "," +
                              "\"end_date\":" + end_time + "," +
                              "\"page\":" + page + "}";

                streamWriter.Write(json);
            }
            myHttpWebRequest.Timeout = 15000;

            HttpWebResponse myHttpWebResponse;

            myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            var reader = new StreamReader(myHttpWebResponse.GetResponseStream());

            using var doc = JsonDocument.Parse(reader.ReadToEnd());
            var obj = doc.RootElement;

            List<IVRTeleCMIList> listRecords = new List<IVRTeleCMIList>();

            var totalCount = obj.GetProperty("total").GetInt32();
            foreach (var o in obj.GetProperty("cdr").EnumerateArray())
            {
                IVRTeleCMIList TeleCMIRecord = new IVRTeleCMIList();
                TeleCMIRecord.CustomerNumber = o.GetProperty("to").GetString();
                TeleCMIRecord.EmployeeNumber = o.GetProperty("from").GetString();
                TeleCMIRecord.Duration = o.GetProperty("billedsec").GetRawText();
                TeleCMIRecord.CustomerName = o.GetProperty("name").GetString();
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                long timestamp = o.GetProperty("time").GetInt64();
                dtDateTime = dtDateTime.AddMilliseconds(timestamp).ToLocalTime();
                TeleCMIRecord.DateTime = dtDateTime.ToString();
                if(type == 1 || type == 3)
                    TeleCMIRecord.Recording = "https://piopiy.telecmi.com/v1/play?appid=" + IVRConfig.AppId + "&token=" + IVRConfig.AppSecret + "&file=" + o.GetProperty("filename").GetString();
                
                listRecords.Add(TeleCMIRecord);

            }

            IPagedList<IVRTeleCMIList> Records = new StaticPagedList<IVRTeleCMIList>(listRecords, page, totalCount, totalCount);
            return Records;
        }

        private IVRConfigurationRow IVRConfig;

       

    }
}