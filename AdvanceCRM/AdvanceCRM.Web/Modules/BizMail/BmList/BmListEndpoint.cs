
namespace AdvanceCRM.BizMail.Endpoints
{
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
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
    using System.Threading.Tasks;
    using Serenity.Abstractions;
    using AdvanceCRM.BizMail;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Settings;
    using MyRepository = Repositories.BmListRepository;
    using MyRow =BmListRow;

    [Route("Services/BizMail/BmList/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class BmListController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public BmListController(ISqlConnections connections)
        {
            _connections = connections;
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

        [ServiceAuthorize("BizMail:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            DynamicDataReport report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.BmListColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "BizMailList" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        [HttpPost]
        public async Task PostData()
        {
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
                    //.Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
                    );
            }
            IEnumerable<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>()
                    {
                    new KeyValuePair<string, string>( "general[name]", "asmit" ),
                     new KeyValuePair<string, string>( "general[description]",  "asmit" ),
                     new KeyValuePair<string, string>( "defaults[from_email]", "asmit" ),
                      new KeyValuePair<string, string>( "defaults[from_name]", "asmit" ),
                      new KeyValuePair<string, string>( "defaults[reply_to]", "asmit" ),

                     };

            HttpContent q = new FormUrlEncodedContent(data);
            string api = Config.Apiurl + "/lists";
            using (HttpClient hclient = new HttpClient())
            {
                using (HttpResponseMessage hresponse = await hclient.PostAsync(api, q))
                {
                    using (HttpContent hcontent = hresponse.Content)
                    {
                        string mycontent = await hcontent.ReadAsStringAsync();
                        HttpContentHeaders header = hcontent.Headers;

                        Console.WriteLine(mycontent);
                    }
                }
            }
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

                string st = "Delete from BmList where ListId IS NULL";
                connection.Execute(st);
            }

            //https://prosender.in/api/lists/xv917g56y4c13/subscribers?page=2&per_page=200
            string uri = Config.Apiurl + "/lists?page=1&per_page=50"; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";
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
                        BizMailDetails.list_id = bizMailObject["general"].list_uid;
                        BizMailDetails.Name = bizMailObject["general"].name;
                        BizMailDetails.DisplayName = bizMailObject["general"].display_name;
                        BizMailDetails.Description = bizMailObject["general"].description;

                        BizMailDetails.Froms = bizMailObject["defaults"].from_email;
                        BizMailDetails.ReplyTo = bizMailObject["defaults"].reply_to;

                        BizMailDetails.CompanyName = bizMailObject["company"].name;
                        BizMailDetails.City = bizMailObject["company"].city;



                        Records.Add("IF NOT EXISTS (SELECT * FROM BMList WHERE ListId ='" + BizMailDetails.list_id + "')" +
                                      "INSERT INTO BMList ([ListId],[CompanyName],[Name],[DisplayName],[City],[Description],[From],[ReplyTo]) VALUES " +
                                      "('" + BizMailDetails.list_id + "','" +
                                           BizMailDetails.CompanyName + "','" +
                                           BizMailDetails.Name + "','" +
                                           BizMailDetails.DisplayName + "','" +
                                           BizMailDetails.City + "','" +
                                           BizMailDetails.Description + "','" +
                                           BizMailDetails.Froms + "','" +
                                           BizMailDetails.ReplyTo + "')");
                    }
                }
                Records.Reverse();
                if (Records.Count > 0)
                {
                    using (var innerConnection = _connections.NewFor<BmListRow>())
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
            public string list_id { get; set; }
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public string Description { get; set; }
            public string Froms { get; set; }
            public string ReplyTo { get; set; }
            public string CompanyName { get; set; }
            public string City { get; set; }
        }
    }
}
