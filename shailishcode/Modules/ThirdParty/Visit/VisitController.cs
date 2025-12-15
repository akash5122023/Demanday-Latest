

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Modules.ThirdParty.Visit
{
    public class VisitController : Controller
    {
        // GET: Visit
        public ActionResult Index()
        {
            return View();
        }

        // GET: Visit/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Visit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Visit/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Visit/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Visit/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //private IPagedList<VisitModelList> GetTeleCMIData()
        //{

        //    string apiUrl = "http://localhost:50289/api/Visit";

           

        //    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
        //    myHttpWebRequest.ContentType = "application/json";
        //    myHttpWebRequest.Method = "POST";

        //    //using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
        //    //{
        //    //    string json = "{\"appid\":" + IVRConfig.AppId + "," +
        //    //                  "\"token\":\"" + IVRConfig.AppSecret + "\"," +
        //    //                  "\"start_date\":" + start_time + "," +
        //    //                  "\"end_date\":" + end_time + "," +
        //    //                  "\"page\":" + page + "}";

        //    //    streamWriter.Write(json);
        //    //}
        //    myHttpWebRequest.Timeout = 15000;

        //    HttpWebResponse myHttpWebResponse;

        //    myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
        //    var reader = new StreamReader(myHttpWebResponse.GetResponseStream());

        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    var obj = js.Deserialize<dynamic>(reader.ReadToEnd());

        //    List<VisitModelList> listRecords = new List<VisitModelList>();

        //    var totalCount = obj["total"];
        //    foreach (var o in obj["cdr"])
        //    {
        //        VisitModelList TeleCMIRecord = new VisitModelList();
        //        TeleCMIRecord.Name = Convert.ToString(o["to"]);
        //        TeleCMIRecord.CompanyName = Convert.ToString(o["from"]);
               
        //        //System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        //        //long timestamp = o["time"];
        //        //dtDateTime = dtDateTime.AddMilliseconds(timestamp).ToLocalTime();
        //        //TeleCMIRecord.DateTime = dtDateTime.ToString();
        //        //if (type == 1 || type == 3)
        //        //    TeleCMIRecord.Recording = "https://piopiy.telecmi.com/v1/play?appid=" + IVRConfig.AppId + "&token=" + IVRConfig.AppSecret + "&file=" + o["filename"];

        //        listRecords.Add(TeleCMIRecord);

        //    }

        //    IPagedList<VisitModelList> Records = new StaticPagedList<VisitModelList>(listRecords, 1, 100, totalCount);
        //    return Records;
        //}
    }
}
