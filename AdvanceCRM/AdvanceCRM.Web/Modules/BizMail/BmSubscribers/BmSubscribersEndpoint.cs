
namespace AdvanceCRM.BizMail.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Settings;
    using AdvanceCRM.Enquiry.Endpoints;
    using AdvanceCRM.ThirdParty;
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
    using MyRepository = Repositories.BmSubscribersRepository;
    using MyRow =BmSubscribersRow;
    using Serenity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Serenity.Abstractions;

    [Route("Services/BizMail/BmSubscribers/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class BmSubscribersController : ServiceEndpoint
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

        public BmSubscribersController(
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
            DynamicDataReport report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.BmSubscribersColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "BizMailSubscribers_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        [HttpPost]
        public StandardResponse MoveToEnquiry(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();
            EnquiryRow LastEnquiry;
            var Contacttyp = 2;
            var br = UserRow.Fields;
            var UData = new UserRow();
            var model = new MyRow();

            var data = new BmSubscribersRow();

            using (var connection = _connections.NewFor<BmSubscribersRow>())
            {
                var ind = BmSubscribersRow.Fields;
                data = connection.TryById<BmSubscribersRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.FirstName)
                   .Select(ind.LastName)
                  // .Select(ind.CompanyPhone)
                   .Select(ind.Email)
                   .Select(ind.DateAdded)
                   );

                UData = connection.First<UserRow>(q => q
             .SelectTableFields()
             .Select(br.CompanyId)
             .Where(br.UserId == Context.User.GetIdentifier())
            );
            }
            try
            {
                using (var connection = _connections.NewFor<ContactsRow>())
                {

                    string date1 = Convert.ToDateTime(data.DateAdded).ToString("yyyy-MM-dd HH:mm:ss");
                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,OwnerId,AssignedId,DateCreated,CompanyId) VALUES('1','81','1','" + data.FirstName + "','" + data.Phone + "','" + data.Email + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + date1 + "','" + UData.CompanyId + "')";

                    connection.Execute(str);

                    // if (LastContact == null)
                    // {
                    //string str = "INSERT INTO Contacts(ContactType,CustomerType,Name,Phone,Email,Address,OwnerId,AssignedId) VALUES('1','1'," + "'" + model.Name + "','" + model.Phone + "','" + model.Email + "','','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "')";

                    //  connection.Execute(str);
                    var c = ContactsRow.Fields;
                    var LastContact = connection.First<ContactsRow>(l => l
                        .Select(c.Id)
                        .Select(c.Name)
                        .OrderBy(c.Id, desc: true)
                        );

                    if (data.FirstName != LastContact.Name)
                    {
                        response.Status = "Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same";

                        throw new Exception("This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");
                    }
                    // }

                    var s = SourceRow.Fields;
                    var Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "Subscribe")
                       //  .Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );

                    if (Source == null)
                    {
                        string str2 = "INSERT INTO Source(Source,CompanyId) VALUES('Subscribe','" + UData.CompanyId + "')";
                        connection.Execute(str2);

                        Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "Subscribe")
                     //   .Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );
                    }

                    var st = StageRow.Fields;
                    var stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(st.Id)
                        .Select(st.Stage)
                        .Where((st.Type == (Int32)Masters.StageTypeMaster.Enquiry))
                         //.Where(st.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );

                    if (stageMaster == null)
                    {
                        string str2 = "INSERT INTO Stage(Stage, Type,CompanyId) VALUES('Initial', 1,'" + UData.CompanyId + "')";
                        connection.Execute(str2);

                        stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(st.Id)
                        .Select(st.Stage)
                        .Where(st.Stage == "Initial")
                         //.Where(st.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );
                    }

                    GetNextNumberResponse nextNumber = new EnquiryController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(data.DateAdded).ToString("yyyy-MM-dd HH:mm:ss");

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + Source.Id + "','" + stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','" + UData.CompanyId + "')";

                    connection.Execute(str1);

                    // var t = EnquiryRow.Fields;
                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                        );

                    connection.Execute("Update MWSubscribers SET IsMoved = 1 WHERE Id = " + request.Id);

                }
                response.Id = LastEnquiry.Id.Value;
                response.Status = "Success";
            }
            catch (Exception ex)
            {
                response.Id = -1;
                response.Status = "Error\n" + ex.ToString();
            }

            return response;

        }


        [HttpPost]
        public StandardResponse Sync(IUnitOfWork uow)
        {
            var response = new StandardResponse();

            var br = UserRow.Fields;
            var UData = new UserRow();

           BizMailConfigRow Config;

            List<BmListRow> mwlist;

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
                var l = BmListRow.Fields;
                mwlist = connection.List<BmListRow>(q => q
                   .SelectTableFields()
                       .Select(l.Id)
                      .Select(l.ListId)
                      .Select(l.Name)
                      .Select(l.DisplayName)
                    );

            }

            try
            {
                foreach (var lst in mwlist.Select(x => x.ListId))
                {
                    var am = lst;
                    var l1 = BmListRow.Fields;
                    //List<MwListRow> mwlist1;
                    var mwlist1 = new BmListRow();
                    using (var connection = _connections.NewFor<BmListRow>())
                    {
                        mwlist1 = connection.TryFirst<BmListRow>(q => q
                       .SelectTableFields()
                           .Select(l1.Id)
                          .Select(l1.ListId)
                          .Select(l1.Name)
                          .Select(l1.DisplayName)
                          .Where(l1.ListId == Convert.ToString(lst))
                        );

                        // }
                        //https://prosender.in/api/lists/xv917g56y4c13/subscribers?page=2&per_page=200
                        string uri = Config.Apiurl + "/lists/" + am + "/subscribers?page=1&per_page=50"; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";

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
                        List<string> Records = new List<string>();
                        int count = bizMailObjects["data"]["count"];
                        int page = 1;
                        if (count > 50)
                        {
                            page = (count / 50) + 1;
                            for (int i = 1; i <= page; i++)
                            {
                                string uri1 = Config.Apiurl + "/lists/" + am + "/subscribers?page=" + i + "&per_page=50"; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";

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

                                foreach (var bizMailObject1 in bizMailObjects1["data"]["records"])
                                {
                                    if (bizMailObject1 != null)
                                    {
                                        BizMailDetail BizMailDetails = new BizMailDetail();
                                        BizMailDetails.Sub_id = bizMailObject1.subscriber_uid;
                                        BizMailDetails.Email = bizMailObject1.EMAIL;
                                        BizMailDetails.FName = bizMailObject1.FNAME;
                                        BizMailDetails.LName = bizMailObject1.LNAME;

                                        BizMailDetails.Status = bizMailObject1.status;
                                        BizMailDetails.Source = bizMailObject1.Source;

                                        BizMailDetails.IpAddres = bizMailObject1.ip_address;
                                        BizMailDetails.DateAdded = bizMailObject1.date_added;

                                        Records.Add("IF NOT EXISTS (SELECT * FROM BMSubscribers WHERE SubscriberId ='" + BizMailDetails.Sub_id + "')" +
                                                      "INSERT INTO BMSubscribers ([ListID],[ListName],[SubscriberId],[Email],[FirstName],[LastName],[Status],[Source],[IPAddress],[DateAdded]) VALUES " +
                                                      "('" + lst + "','" +
                                                        mwlist1.Name + "','" +
                                                           BizMailDetails.Sub_id + "','" +
                                                           BizMailDetails.Email + "','" +
                                                           BizMailDetails.FName + "','" +
                                                           BizMailDetails.LName + "','" +
                                                           BizMailDetails.Status + "','" +
                                                           BizMailDetails.Source + "','" +
                                                           BizMailDetails.IpAddres + "','" +
                                                           BizMailDetails.DateAdded + "')");
                                    }
                                }

                            }
                        }
                        else
                        {
                            foreach (var bizMailObject in bizMailObjects["data"]["records"])
                            {
                                if (bizMailObject != null)
                                {
                                    BizMailDetail BizMailDetails = new BizMailDetail();
                                    BizMailDetails.Sub_id = bizMailObject.subscriber_uid;
                                    BizMailDetails.Email = bizMailObject.EMAIL;
                                    BizMailDetails.FName = bizMailObject.FNAME;
                                    BizMailDetails.LName = bizMailObject.LNAME;

                                    BizMailDetails.Status = bizMailObject.status;
                                    BizMailDetails.Source = bizMailObject.Source;

                                    BizMailDetails.IpAddres = bizMailObject.ip_address;
                                    BizMailDetails.DateAdded = bizMailObject.date_added;

                                    Records.Add("IF NOT EXISTS (SELECT * FROM BMSubscribers WHERE SubscriberId ='" + BizMailDetails.Sub_id + "')" +
                                                  "INSERT INTO BMSubscribers ([ListID],[ListName],[SubscriberId],[Email],[FirstName],[LastName],[Status],[Source],[IPAddress],[DateAdded]) VALUES " +
                                                  "('" + 
                                                    lst + "','" +
                                                        mwlist1.Name + "','" +
                                                       BizMailDetails.Sub_id + "','" +
                                                       BizMailDetails.Email + "','" +
                                                      BizMailDetails.FName + "','" +
                                                       BizMailDetails.LName + "','" +
                                                       BizMailDetails.Status + "','" +
                                                       BizMailDetails.Source + "','" +
                                                       BizMailDetails.IpAddres + "','" +
                                                       BizMailDetails.DateAdded + "')");
                                }
                            }
                        }

                        Records.Reverse();
                        if (Records.Count > 0)
                        {
                            using (var innerConnection = _connections.NewFor<BmSubscribersRow>())
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
            public string Sub_id { get; set; }
            public string Email { get; set; }
            public string FName { get; set; }
            public string LName { get; set; }
            public string Status { get; set; }
            public string Source { get; set; }
            public string IpAddres { get; set; }
            public string DateAdded { get; set; }

            //  public List<MwListRow> mwlist { get; set; }

        }
    }
}
