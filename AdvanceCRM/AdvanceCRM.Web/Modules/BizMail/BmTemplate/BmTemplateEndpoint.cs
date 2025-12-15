
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
    using Serenity.Abstractions;
    using AdvanceCRM.BizMail;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Settings;
    using MyRepository = Repositories.BmTemplateRepository;
    using MyRow =BmTemplateRow;

    [Route("Services/BizMail/BmTemplate/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class BmTemplateController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public BmTemplateController(ISqlConnections connections)
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

                string st = "Delete from BMTemplate where TemplateUID IS NULL";
                connection.Execute(st);
            }

            //https://prosender.in/api/lists/xv917g56y4c13/subscribers?page=2&per_page=200
            string uri = Config.Apiurl + "/templates?page=1&per_page=50"; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";
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
                        BizMailDetails.template_uid = bizMailObject.template_uid;
                        BizMailDetails.Name = bizMailObject.name;

                        string uri1 = Config.Apiurl + "/templates/"+ BizMailDetails.template_uid; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";

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
                        var bizMailObjects1 = JsonConvert.DeserializeObject<dynamic>(reader1.ReadToEnd());

                        BizMailDetails.Content = bizMailObjects1["data"]["record"].content;

                        //foreach (var bizMailObject1 in bizMailObjects1["data"]["record"])
                        //{
                        //    // if (bizMailObject["general"] != null)
                        //    {

                        //        BizMailDetails.Content = bizMailObject1.name;
                             


                                Records.Add("IF NOT EXISTS (SELECT * FROM BMTemplate WHERE TemplateUID ='" + BizMailDetails.template_uid + "')" +
                                      "INSERT INTO BMTemplate ([TemplateUID],[Content],[Name]) VALUES " +
                                      "('" + BizMailDetails.template_uid + "','" +                                          
                                           BizMailDetails.Content + "','" +                                         
                                           BizMailDetails.Name + "')");
                        //    }
                        //}
                    }
                }
                Records.Reverse();
                if (Records.Count > 0)
                {
                    using (var innerConnection = _connections.NewFor<BmTemplateRow>())
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
            public string template_uid { get; set; }
            public string Name { get; set; }
            public string Content { get; set; }
           
        }
    }
}
