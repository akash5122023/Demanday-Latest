
namespace AdvanceCRM.BizMail.Endpoints
{
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System.Data.SqlClient;
    using System.Data;
    
    using System;
    using System.Linq;
    using System.Net;
    //using System.Web.Script.Serialization;
    using Newtonsoft.Json;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System.Data;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using AdvanceCRM.BizMail;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Settings;
    using MyRepository = Repositories.CampaignBmRepository;
    using MyRow =CampaignBmRow;
    using System.Configuration;
    using System.Globalization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Serenity.Abstractions;

    [Route("Services/BizMail/CampaignBm/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class CampaignBmController : ServiceEndpoint
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

        public CampaignBmController(
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

            var br = UserRow.Fields;
            var UData = new UserRow();

            BizMailConfigRow Config;

            using (var connection = _connections.NewFor<BizMailConfigRow>())
            {

                UData = connection.First<UserRow>(q => q
             .SelectTableFields()
             .Select(br.CompanyId)
             .Where(br.UserId == Context.User.GetIdentifier())
            );

                var s = BizMailConfigRow.Fields;
                Config = connection.TryFirst<BizMailConfigRow>(q => q
                    .SelectTableFields()
                    .Select(s.Apiurl)
                    .Select(s.Apikey)
                    // .Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
                    );

                string st = "Delete from CampaignBM where Campaignuid IS NULL";
                connection.Execute(st);
            }

            //https://prosender.in/api/lists/xv917g56y4c13/subscribers?page=2&per_page=200
            string uri = Config.Apiurl + "/campaigns?page=1&per_page=50"; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);

                myHttpWebRequest.Headers.Add("X-MW-PUBLIC-KEY: " + Config.Apikey.ToString().Trim());
                myHttpWebRequest.ContentType = "application/json";
                myHttpWebRequest.Headers.Add("cache-control", "no-cache");

                //string status = string.Empty;
                // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds. 
                myHttpWebRequest.Timeout = 15000;
                HttpWebResponse myHttpWebResponse;
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream());
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                var bizMailObjects = JsonConvert.DeserializeObject<dynamic>(reader.ReadToEnd());

                // Dictionary<string, object> result = FacebookObjects["data"][0];

                List<string> Records = new List<string>();
                foreach (var bizMailObject in bizMailObjects["data"]["records"])
                {
                    // if (bizMailObject["general"] != null)
                    {
                        BizMailDetail BizMailDetails = new BizMailDetail();
                        BizMailDetails.Campaign_id = bizMailObject["campaign_uid"];
                        BizMailDetails.type = bizMailObject["type"];
                        BizMailDetails.Campaign_name = bizMailObject["name"];
                        BizMailDetails.Status = bizMailObject["status"];

                        //BizMailDetails.Froms = bizMailObject["defaults"].from_email;
                        //BizMailDetails.ReplyTo = bizMailObject["defaults"].reply_to;

                        //BizMailDetails.CompanyName = bizMailObject["company"].name;
                        //BizMailDetails.City = bizMailObject["company"].city;
                        var type = 0;
                        if(BizMailDetails.type== "autoresponder")
                        {
                            type = 2;
                        }
                        else
                        {
                            type = 1;
                        }

                        if (BizMailDetails.Campaign_id != "")
                        {
                            string uri1 = Config.Apiurl + "/campaigns/" + BizMailDetails.Campaign_id ; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";

                            HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(uri1);
                            myHttpWebRequest1.Headers.Add("X-MW-PUBLIC-KEY: " + Config.Apikey.ToString().Trim());
                            myHttpWebRequest1.ContentType = "application/json";
                            myHttpWebRequest1.Headers.Add("cache-control", "no-cache");
                            //string status = string.Empty;
                            // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds. 
                            myHttpWebRequest1.Timeout = 15000;
                            HttpWebResponse myHttpWebResponse1;
                            myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
                            StreamReader reader1 = new StreamReader(myHttpWebResponse1.GetResponseStream());
                            myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
                            //JavaScriptSerializer js1 = new JavaScriptSerializer();
                            var bizMailObjects1 = JsonConvert.DeserializeObject<dynamic>(reader1.ReadToEnd());

                           // foreach (var bizMailObject1 in bizMailObjects1["data"]["record"])
                            {
                                BizMailDetails.from_name = bizMailObjects1["data"]["record"].from_name;
                                BizMailDetails.from_email = bizMailObjects1["data"]["record"].from_email;
                                BizMailDetails.to_name = bizMailObjects1["data"]["record"].to_name;
                                BizMailDetails.reply_to = bizMailObjects1["data"]["record"].reply_to;
                                BizMailDetails.subject = bizMailObjects1["data"]["record"].subject;
                                BizMailDetails.date_added = bizMailObjects1["data"]["record"].date_added;
                                BizMailDetails.send_at = bizMailObjects1["data"]["record"].send_at;
                                BizMailDetails.list_uid = bizMailObjects1["data"]["record"]["list"]["list_uid"];
                                BizMailDetails.name = bizMailObjects1["data"]["record"]["list"]["name"];
                                BizMailDetails.subscribers_count = bizMailObjects1["data"]["record"]["list"]["subscribers_count"];

                                //BizMailDetails.send_at = BizMailDetails.send_at.Replace(",", " ");
                                //DateTime d = DateTime.ParseExact(BizMailDetails.send_at, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                                ////  DateTime.ParseExact("7/3/2015 1:52:16 PM", "d/M/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                                //BizMailDetails.send_at = d.ToString();
                              //  string am = "AM";string pm = "PM";
                               //if(BizMailDetails.send_at.Contains(am))
                               // {
                               //    // DateTime d = DateTime.ParseExact(BizMailDetails.send_at, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                               //     BizMailDetails.send_at = BizMailDetails.send_at.Replace("AM", " ");
                               // }
                               //else if(BizMailDetails.send_at.Contains(pm))
                               // {
                               //     DateTime d = DateTime.ParseExact(BizMailDetails.send_at, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                               //   //  DateTime.ParseExact("7/3/2015 1:52:16 PM", "d/M/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                               //     BizMailDetails.send_at = d.ToString();
                               // }
                                SqlConnection con = new SqlConnection(Startup.connectionString);
                                string str4 = "Select Id from BMList where ListId='" + BizMailDetails.list_uid+"'";
                                SqlDataAdapter sda4 = new SqlDataAdapter(str4, con);
                                DataSet ds4 = new DataSet();
                                sda4.Fill(ds4);
                                var listid = Convert.ToString(ds4.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                                var template = "5";

                                Records.Add("IF NOT EXISTS (SELECT * FROM CampaignBM WHERE Campaignuid ='" + BizMailDetails.Campaign_id + "')" +
                                          "INSERT INTO CampaignBM ([Campaignuid],[Name],[FromName],[FromEmail],[Subject],[ReplyTo],[BMListId],[BMTemplateId],[Type]) VALUES " +
                                          "('" + BizMailDetails.Campaign_id + "','" +
                                               BizMailDetails.Campaign_name + "','" +
                                               BizMailDetails.from_name + "','" +
                                               BizMailDetails.from_email + "','" +
                                               BizMailDetails.subject + "','" +
                                               BizMailDetails.reply_to + "','" +
                                               listid + "','" +
                                              // Convert.ToDateTime(BizMailDetails.send_at).ToString("dd/MM/yyyy HH:mm:ss") + "','" +
                                               template + "','" +
                                               type + "')");
                            }
                        }
                    }
                }
                Records.Reverse();
                if (Records.Count > 0)
                {
                    using (var innerConnection = _connections.NewFor<CampaignBmRow>())
                    {
                        for (int ij = 0; ij < Records.Count; ij++)
                        {
                            try
                            {
                                innerConnection.Execute(Records[ij]);
                            }
                            catch (Exception ex)
                            {
                            }

                        }
                    }
                }
                response.Status = "Sync Done";

            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }
        internal class BizMailDetail
        {
            public string id { get; set; }
            public string Campaign_id { get; set; }
            public string Campaign_name { get; set; }
            public string type { get; set; }

            public string Status { get; set; }
            public string from_name { get; set; }
            public string from_email { get; set; }
            public string to_name { get; set; }
            public string reply_to { get; set; }
            public string subject { get; set; }
            public string date_added { get; set; }
            public string send_at { get; set; }
            public string list_uid { get; set; }
            public string name { get; set; }
            public string subscribers_count { get; set; }
          
        }
    }
}
