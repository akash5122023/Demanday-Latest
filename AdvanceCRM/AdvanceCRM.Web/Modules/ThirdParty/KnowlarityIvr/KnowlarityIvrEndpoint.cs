
namespace AdvanceCRM.ThirdParty.Endpoints
{
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System.Data;
    
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Enquiry.Endpoints;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Settings;
    using AdvanceCRM.Settings.Endpoints;
    using AdvanceCRM.ThirdParty;
    using Newtonsoft.Json.Linq;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
using System.Text.Json;
    
    //using System.Web.Script.Serialization;
    using MyRepository = Repositories.KnowlarityIvrRepository;
    using MyRow = KnowlarityIvrRow;
    using Serenity.Extensions.DependencyInjection;

    [Route("Services/ThirdParty/KnowlarityIvr/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class KnowlarityIvrController : ServiceEndpoint
    {
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context).Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context).Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
           return new MyRepository(Context).Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
             return new MyRepository(Context).Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository(Context).List(connection, request);
        }

        [HttpPost]
        public StandardResponse Sync(IUnitOfWork uow)
        {
            var response = new StandardResponse();
            KnowlarityIvrRow LastEnquiry;
            IVRConfigurationRow Config;
            DateTime StartDate, EndDate;
            using (var connection = Dependency.Resolve<ISqlConnections>().NewFor<IVRConfigurationRow>())
            {
                var s = IVRConfigurationRow.Fields;
                Config = connection.TryFirst<IVRConfigurationRow>(q => q
                    .SelectTableFields()
                    .Select(s.IVRNumber)
                    .Select(s.ApiKey)
                    .Select(s.Plan)
                    );

                var i = KnowlarityIvrRow.Fields;
                LastEnquiry = connection.TryFirst<KnowlarityIvrRow>(q => q
                .SelectTableFields()
                .Select(i.Date)
                .OrderBy(i.Date, true)
                );
            }

            {            
            
            string knowlarity_no = Config.IVRNumber.Trim();
            knowlarity_no = knowlarity_no.Replace("+", "%2B");
                string start_time = "2021 - 10 - 28 % 2011 % 3A54 % 3A00 % 2B05 % 3A30";
                string end_time = "2021 - 10 - 29 % 2011 % 3A54 % 3A00 % 2B05 % 3A30";

            string apiURL = ("https://kpi.knowlarity.com/" + Config.Plan + "/v1/account/calllog?start_time=" + start_time.Trim() + "&end_time=" + end_time.Trim() + "&knowlarity_number=" + knowlarity_no.Trim()) + "&limit=500";//


           // string apiURL = ("https://kpi.knowlarity.com/" + Config.Plan + "/v1/account/calllog?start_time=" + start_time.Trim() + "&end_time=" + end_time.Trim() + "&knowlarity_number=%2B" + Config.IVRNumber.Trim()) + "&limit=500";//&offset=" + offset;

                // string apiURL = ("https://kpi.knowlarity.com/" + IVRConfig.Plan + "/v1/account/calllog?start_time=" + start_time.Trim() + "&end_time=" + end_time.Trim() + "&knowlarity_number=" + knowlarity_no.Trim()) + "&limit=500&offset=" + offset;//


                //string apiURL = "https://kpi.knowlarity.com/Basic/v1/account/calllog?start_time=2016-01-01%2012%3A00%3A00%2B05%3A30&end_time=2017-07-17%2011%3A54%3A00%2B05%3A30&knowlarity_number=%2B919021023456&limit=200";

                //String uri = apiURL + "?limit=10";

                // Create a new 'HttpWebRequest' Object to the mentioned URL.
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(apiURL);
                //myHttpWebRequest.Headers.Add("channel", data.IVRConfig.Plan.ToString().Trim());
                myHttpWebRequest.Headers.Add("x-api-key: L3WEMukeez1uFPZhiEv6a6vDf2pu9pJPjtziFqN7");
                myHttpWebRequest.Headers.Add("authorization: " + Config.ApiKey.ToString().Trim());
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

                string jsonResponse = reader.ReadToEnd();
                var Knowlarityobj = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonResponse);

                if (Knowlarityobj != null)
                {
                    Dictionary<string, object> result = Knowlarityobj[0];
                    if (result.ContainsKey("Error_Message"))
                    {
                        response.Status = Convert.ToString(result["Error_Message"]).Replace("'", "");
                        return response;
                    }

                    List<string> listRecords = new List<string>();

                    //var meta = obj["meta"];
                    //var totalCount = meta["total_count"];

                    foreach (Dictionary<string, object> knowlarityresponseObject in Knowlarityobj)
                    {
                        KnowlarityDetail knowlarityRecord = new KnowlarityDetail();
                        // knowlarityRecord.CustomerNumber = o["customer_number"];
                        knowlarityRecord.custno = !knowlarityresponseObject.ContainsKey("customer_number") ? "" : Convert.ToString(knowlarityresponseObject["customer_number"]).Replace("'", "");
                        knowlarityRecord.EmpNo = !knowlarityresponseObject.ContainsKey("agent_number") ? "" : Convert.ToString(knowlarityresponseObject["agent_number"]).Replace("'", "");
                        knowlarityRecord.KnowNo = !knowlarityresponseObject.ContainsKey("knowlarity_number") ? "" : Convert.ToString(knowlarityresponseObject["knowlarity_number"]).Replace("'", "");
                        knowlarityRecord.Record = !knowlarityresponseObject.ContainsKey("call_recording") ? "" : Convert.ToString(knowlarityresponseObject["call_recording"]).Replace("'", "");
                        knowlarityRecord.Date = !knowlarityresponseObject.ContainsKey("start_time") ? "" : Convert.ToString(knowlarityresponseObject["start_time"]).Replace("'", "");
                        knowlarityRecord.Duration = !knowlarityresponseObject.ContainsKey("call_duration") ? "" : Convert.ToString(knowlarityresponseObject["call_duration"]).Replace("'", "");

                        // listRecords.Add(knowlarityRecord);
                        listRecords.Add("INSERT INTO KnowlarityIVR ([Mobile],[EmpMobile],[IVRNo],[Recording],[Date],[Duration]) VALUES " +
                               "('" + knowlarityRecord.custno + "','" +
                                     knowlarityRecord.EmpNo + "','" +
                                     knowlarityRecord.KnowNo + "','" +
                                     knowlarityRecord.Record + "','" +
                                     knowlarityRecord.Date + "','" +
                                     knowlarityRecord.Duration + "')");

                    }
                    // IPagedList<IVRKnowlarityList> Records = new StaticPagedList<IVRKnowlarityList>(listRecords, page, 10, totalCount);
                    listRecords.Reverse();
                    if (listRecords.Count > 0)
                    {
                        using (var innerConnection = Dependency.Resolve<ISqlConnections>().NewFor<KnowlarityIvrRow>())
                        {
                            for (int ij = 0; ij < listRecords.Count; ij++)
                            {
                                try
                                {
                                    innerConnection.Execute(listRecords[ij]);
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }
                    }
                }
                response.Status = "Sync completed";
                
            }
            return response;
            // return null;
        }
        internal class KnowlarityDetail
        {
            public int Id { get; set; }
            public string custno { get; set; }
            public string EmpNo { get; set; }
            public string KnowNo { get; set; }
            public string Record { get; set; }
            public string Date { get; set; }
            public string Duration { get; set; }
           
        }
    }
}
