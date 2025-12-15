
namespace AdvanceCRM.BizMail.Endpoints
{
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
    
    using AdvanceCRM.BizMail;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Settings;
    using MyRepository = Repositories.BmCampaignRepository;
    using MyRow =BmCampaignRow;
    using Serenity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Serenity.Abstractions;

    [Route("Services/BizMail/BmCampaign/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class BmCampaignController : ServiceEndpoint
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

        public BmCampaignController(
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
        [ServiceAuthorize("BizMail:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            DynamicDataReport report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.BmCampaignColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "BizMailCompaign" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
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
                    //.Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
                    );
            }

            //https://prosender.in/api/lists/xv917g56y4c13/subscribers?page=2&per_page=200
            string uri = Config.Apiurl + "/campaigns?page=1&per_page=50"; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);

                myHttpWebRequest.Headers.Add("X-MW-PUBLIC-KEY: " + Config.Apikey.ToString().Trim());
                myHttpWebRequest.ContentType = "application/json";
                myHttpWebRequest.Headers.Add("cache-control", "no-cache");

                myHttpWebRequest.Timeout = 15000;
                HttpWebResponse myHttpWebResponse;
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream());
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                var bizMailObjects = JsonConvert.DeserializeObject<dynamic>(reader.ReadToEnd());

                int count = bizMailObjects["data"]["count"];
                int page = 1;
                if (count > 50) { page = count / 50; }


                List<string> Records = new List<string>();
                foreach (var bizMailObject in bizMailObjects["data"]["records"])
                {
                    if (bizMailObject != null)
                    {
                        BizMailDetail BizMailDetails = new BizMailDetail();
                        BizMailDetails.com_id = bizMailObject.campaign_uid;
                        BizMailDetails.Name = bizMailObject.name;
                        BizMailDetails.Status = bizMailObject.status;

                        Records.Add("IF NOT EXISTS (SELECT * FROM BMCampaign WHERE CampaignId ='" + BizMailDetails.com_id + "')" +
                                      "INSERT INTO BMCampaign ([CampaignId],[Name],[Status]) VALUES " +
                                      "('" + 
                                           BizMailDetails.com_id + "','" +
                                           BizMailDetails.Name + "','" +
                                           BizMailDetails.Status + "')");
                    }
                }
                Records.Reverse();
                if (Records.Count > 0)
                {
                    using (var innerConnection = _connections.NewFor<BmCampaignRow>())
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
            public string com_id { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
        }
    }
}
