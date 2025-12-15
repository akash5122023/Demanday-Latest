namespace AdvanceCRM.ThirdParty.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Services;
    using AdvanceCRM.Enquiry.Endpoints;
    using AdvanceCRM.Services.Endpoints;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Settings;
    using AdvanceCRM.ThirdParty;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Serenity;
    using Serenity.Data;
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using MyRepository = Repositories.KnowlarityDetailsRepository;
    using MyRow = KnowlarityDetailsRow;
    using PagedList;
    using System.Linq;
    using Serenity.Abstractions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Caching.Memory;
    using Serenity.Extensions.DependencyInjection;

    [Route("Services/ThirdParty/KnowlarityDetails/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class KnowlarityDetailsController : ServiceEndpoint
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

        public KnowlarityDetailsController(
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
            KnowlarityDetailsRow LastEnquiry;
            IVRConfigurationRow Config;
            DateTime StartDate, EndDate;
            List<UserRow> users;
            int lastAssignedUserId;
            int lastIndex;
            int nextIndex = 0;

            using (var connection = _connections.NewFor<IVRConfigurationRow>())
            {
                var s = IVRConfigurationRow.Fields;
                Config = connection.TryFirst<IVRConfigurationRow>(q => q
                    .SelectTableFields()
                    .Select(s.IVRType)
                    .Select(s.IVRNumber)
                    .Select(s.ApiKey)
                    .Select(s.Plan)
                    .Select(s.AutoRefresh)
                    .Select(s.RoundRobin)
                );

                var i = KnowlarityDetailsRow.Fields;
                LastEnquiry = connection.TryFirst<KnowlarityDetailsRow>(q => q
                    .SelectTableFields()
                    .Select(i.DateTime)
                    .OrderBy(i.DateTime, true)
                );
            }

            // Fetch all users
            using (var userConnection = _connections.NewFor<UserRow>())
            {
                users = userConnection.List<UserRow>(q => q
                    .SelectTableFields()
                    .Where(UserRow.Fields.TeamsId == 5)).ToList();
            }

            if (Config.RoundRobin == true)
            {
                // Fetch Last Assigned User ID from LeadAssignment
                using (var leadAssignmentConnection = _connections.NewFor<UserRow>())
                {
                    object result = Serenity.Data.SqlHelper.ExecuteScalar(
                        leadAssignmentConnection,
                        "SELECT LastAssignedUserId FROM LeadAssignment WHERE Id = (SELECT MAX(Id) FROM LeadAssignment)"
                    );

                    lastAssignedUserId = result != null ? Convert.ToInt32(result) : 0;
                }

                if (users.Count == 0)
                {
                    response.Status = "No users available for assignment.";
                    return response;
                }

                lastIndex = users.FindIndex(u => u.UserId == lastAssignedUserId);
                nextIndex = (lastIndex + 1) % users.Count;
            }

            {
                HttpWebResponse myHttpWebResponse;
                HttpWebResponse myHttpWebResponse1;
                if (Config.IVRType.Value == Masters.IVRTypeMaster.Knowlarity)
                {
                    string no = Config.IVRNumber.ToString();
                    if (LastEnquiry == null)
                    {
                        StartDate = DateTime.Now.AddDays(-7);
                    }
                    else
                    {
                        StartDate = LastEnquiry.DateTime.Value;
                    }

                    EndDate = StartDate.AddDays(7);

                    var dt1 = StartDate.ToString("yyyy-MM-dd 00:00:00+05:30");
                    var dt2 = EndDate.ToString("yyyy-MM-dd 23:59:59+05:30");

                    string start_time = dt1.Replace(":", "%3A")
                                           .Replace("+", "%2B")
                                           .Replace(" ", "+");
                    string end_time = dt2.Replace(":", "%3A")
                                         .Replace("+", "%2B")
                                         .Replace(" ", "+");

                    List<string> IvrNumberList = new List<string>(no.Split(", ".ToCharArray()));
                    foreach (string IvrNo in IvrNumberList)
                    {
                        string knowlarity_no = IvrNo.Replace("+", "%2B");

                        string apiURL = ("https://kpi.knowlarity.com/" + Config.Plan + "/v1/account/calllog?start_time=" + start_time.Trim() + "&end_time=" + end_time.Trim() + "&knowlarity_number=" + knowlarity_no.Trim()) + "&limit=500";

                        HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(apiURL);
                        myHttpWebRequest.Headers.Add("x-api-key: TbnEKemdYB920V2xohzv41Hi7YCjFkNV9GRuGK1k");
                        myHttpWebRequest.Headers.Add("authorization: " + Config.ApiKey.ToString().Trim());
                        myHttpWebRequest.ContentType = "application/json; charset=utf-8";
                        myHttpWebRequest.Headers.Add("cache-control", "no-cache");
                        myHttpWebRequest.Method = "GET";
                        myHttpWebRequest.Timeout = 15000;

                        myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                        var reader = new StreamReader(myHttpWebResponse.GetResponseStream());

                        var Knowlarityobjs = JsonConvert.DeserializeObject<dynamic>(reader.ReadToEnd());

                        if (Config.RoundRobin.Value == true)
                        {
                            foreach (var Knowlarityobj in Knowlarityobjs["objects"])
                            {
                                if (Knowlarityobj != null)
                                {
                                    var nextUser = users[nextIndex];
                                    nextIndex = (nextIndex + 1) % users.Count;

                                    List<string> listRecords = new List<string>();
                                    {
                                        KnowlarityDetail knowlarityRecord = new KnowlarityDetail
                                        {
                                            to = "Unknown",
                                            Cuimid = Knowlarityobj.uuid,
                                            custno = Knowlarityobj.customer_number,
                                            EmpNo = Knowlarityobj.agent_number,
                                            KnowNo = Knowlarityobj.knowlarity_number,
                                            Record = Knowlarityobj.call_recording,
                                            Date = Knowlarityobj.start_time,
                                            Duration = Knowlarityobj.call_duration
                                        };

                                        DateTime dtt = Convert.ToDateTime(knowlarityRecord.Date);
                                        var dt = dtt.ToString("yyyy/MM/dd HH:mm:ss");
                                        var currentDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                                        var connection = uow.Connection;

                                        var source = SourceRow.Fields;

                                        var sourceMaster = connection.TryFirst<SourceRow>(new Criteria(source.Source) == "IVR");
                                        if (sourceMaster == null)
                                            throw new ValidationError("IVR source not found in SourceMaster.");

                                        var stage = StageRow.Fields;

                                        var stageMaster = connection.TryFirst<StageRow>(new Criteria(stage.Stage) == "New");
                                        if (stageMaster == null)
                                            throw new ValidationError("Stage 'New' not found in StageMaster.");

                                        var br = UserRow.Fields;

                                        var UData = connection.First<UserRow>(q => q
                                            .Select(br.CompanyId)
                                            .Where(br.UserId == Convert.ToInt32(Context.User.GetIdentifier()))
                                        );

                                        listRecords.Add($@"
                                DECLARE @ContactId INT;
                                SELECT @ContactId = Id FROM Contacts WHERE Phone = '{knowlarityRecord.custno}';
                                IF @ContactId IS NOT NULL
                                BEGIN
                                    IF NOT EXISTS (SELECT Id FROM Enquiry WHERE ContactsId = @ContactId)
                                    BEGIN
                                        INSERT INTO Enquiry
                                        ([ContactsId], [Date], [Status], [SourceId], [StageId], [OwnerId], [AssignedId], [EnquiryNo], [EnquiryN], [CompanyId])
                                        VALUES
                                        (@ContactId, '{currentDateTime}', '1', '{sourceMaster.Id}', '{stageMaster.Id}', '{nextUser.UserId}', '{nextUser.UserId}', '{GetNextNumber(uow.Connection).Serial}', '{GetNextNumber(uow.Connection).SerialN}', '{UData.CompanyId}')
                                    END
                                END");

                                        listRecords.Add("IF NOT EXISTS(SELECT * FROM KnowlarityDetails WHERE CMIUID = '" + knowlarityRecord.Cuimid + "')" +
                                            "INSERT INTO KnowlarityDetails ([Name],[CMIUID],[CustomerNumber],[EmployeeNumber],[From],[Recording],[DateTime],[Duration],[IsMoved]) VALUES " +
                                               "('" + knowlarityRecord.to + "', '" +
                                                knowlarityRecord.Cuimid + "','" +
                                                     knowlarityRecord.custno + "','" +
                                                     knowlarityRecord.EmpNo + "','" +
                                                     knowlarityRecord.KnowNo + "','" +
                                                     knowlarityRecord.Record + "','" +
                                                     dt + "','" +
                                                     knowlarityRecord.Duration + "','" + 1 + "')");

                                        listRecords.Add("IF NOT EXISTS(SELECT * FROM Contacts WHERE Phone = '" + knowlarityRecord.custno + "')" +
                                              "INSERT INTO Contacts ([ContactType],[Country],[CustomerType],[Name],[Phone],[OwnerId],[AssignedId],[DateCreated]) VALUES " +
                                               "('" + 2 + "','81','1','" +
                                                     knowlarityRecord.to + "','" +
                                                     knowlarityRecord.custno + "','" +
                                                     nextUser.UserId + "','" +
                                                     nextUser.UserId + "','" +
                                                     currentDateTime + "')");
                                    }

                                    using (var leadAssignmentConnection = _connections.NewFor<UserRow>())
                                    {
                                        leadAssignmentConnection.Execute($"INSERT INTO LeadAssignment (LastAssignedUserId) VALUES ({users[nextIndex].UserId})");
                                    }

                                    listRecords.Reverse();
                                    if (listRecords.Count > 0)
                                    {
                                        using (var innerConnection = _connections.NewFor<KnowlarityDetailsRow>())
                                        {
                                            for (int ij = 0; ij < listRecords.Count; ij++)
                                            {
                                                try
                                                {
                                                    innerConnection.Execute(listRecords[ij]);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("SQL Error: " + ex.Message);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var Knowlarityobj in Knowlarityobjs["objects"])
                            {
                                if (Knowlarityobj != null)
                                {
                                    List<string> listRecords = new List<string>();
                                    {
                                        KnowlarityDetail knowlarityRecord = new KnowlarityDetail
                                        {
                                            to = "Unknown",
                                            Cuimid = Knowlarityobj.uuid,
                                            custno = Knowlarityobj.customer_number,
                                            EmpNo = Knowlarityobj.agent_number,
                                            KnowNo = Knowlarityobj.knowlarity_number,
                                            Record = Knowlarityobj.call_recording,
                                            Date = Knowlarityobj.start_time,
                                            Duration = Knowlarityobj.call_duration
                                        };

                                        DateTime dtt = Convert.ToDateTime(knowlarityRecord.Date);
                                        var dt = dtt.ToString("yyyy/MM/dd HH:mm:ss");

                                        listRecords.Add("IF NOT EXISTS(SELECT * FROM KnowlarityDetails WHERE CMIUID = '" + knowlarityRecord.Cuimid + "')" +
                                            "INSERT INTO KnowlarityDetails ([Name],[CMIUID],[CustomerNumber],[EmployeeNumber],[From],[Recording],[DateTime],[Duration]) VALUES " +
                                               "('" + knowlarityRecord.to + "', '" +
                                                knowlarityRecord.Cuimid + "','" +
                                                     knowlarityRecord.custno + "','" +
                                                     knowlarityRecord.EmpNo + "','" +
                                                     knowlarityRecord.KnowNo + "','" +
                                                     knowlarityRecord.Record + "','" +
                                                     dt + "','" +
                                                     knowlarityRecord.Duration + "')");
                                    }

                                    listRecords.Reverse();
                                    if (listRecords.Count > 0)
                                    {
                                        using (var innerConnection = _connections.NewFor<KnowlarityDetailsRow>())
                                        {
                                            for (int ij = 0; ij < listRecords.Count; ij++)
                                            {
                                                try
                                                {
                                                    innerConnection.Execute(listRecords[ij]);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("SQL Error: " + ex.Message);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (Config.IVRType.Value == Masters.IVRTypeMaster.Cloud_Connect)
                {
                    if (LastEnquiry == null)
                    {
                        StartDate = DateTime.Now.AddDays(-7);
                    }
                    else
                    {
                        StartDate = LastEnquiry.DateTime.Value;
                    }

                    EndDate = StartDate.AddDays(7);

                    string dt1 = StartDate.ToString("yyyy-MM-dd");
                    string dt2 = EndDate.ToString("yyyy-MM-dd");
                    string token = Config.Token_Id;

                    string apiURL = $"https://crm2.cloud-connect.in/ccpl_api/v1.4/api/info/{dt1}/{dt2}/{Config.userType}/callLog";

                    HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(apiURL);
                    myHttpWebRequest.ContentType = "application/json; charset=utf-8";
                    myHttpWebRequest.Headers.Add("cache-control", "no-cache");
                    myHttpWebRequest.Method = "POST";
                    myHttpWebRequest.Timeout = 15000;

                    string jsonBody = $@"
{{
    ""token_id"": ""{token}""
}}";

                    using (StreamWriter streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(jsonBody);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }

                    try
                    {
                        using (HttpWebResponse httpResponse = (HttpWebResponse)myHttpWebRequest.GetResponse())
                        {
                            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                string responseContent = streamReader.ReadToEnd();
                                Console.WriteLine("Response: " + responseContent);

                                var Knowlarityobjs = JsonConvert.DeserializeObject<dynamic>(responseContent);
                                foreach (var Knowlarityobj in Knowlarityobjs["data"])
                                {
                                    if (Knowlarityobj != null)
                                    {
                                        List<string> listRecords = new List<string>();
                                        {
                                            KnowlarityDetail knowlarityRecord = new KnowlarityDetail
                                            {
                                                to = "Unknown",
                                                Cuimid = Knowlarityobj.unique_token,
                                                custno = Knowlarityobj.callee,
                                                EmpNo = Knowlarityobj.caller,
                                                KnowNo = Knowlarityobj.call_direction == "OUTBOUND"
                                                    ? Knowlarityobj.outbound_caller_id
                                                    : Knowlarityobj.did,
                                                Record = Knowlarityobj.call_rec_path,
                                                Date = Knowlarityobj.start_date,
                                                Duration = Knowlarityobj.call_sec,
                                                RecordStat = Knowlarityobj.call_direction
                                            };

                                            DateTime dtt = Convert.ToDateTime(knowlarityRecord.Date);
                                            var dt = dtt.ToString("yyyy/MM/dd HH:mm:ss");

                                            listRecords.Add("IF NOT EXISTS(SELECT * FROM KnowlarityDetails WHERE CMIUID = '" + knowlarityRecord.Cuimid + "')" +
                                                "INSERT INTO KnowlarityDetails ([Name],[CMIUID],[CustomerNumber],[EmployeeNumber],[From],[Recording],[DateTime],[Duration],[Record]) VALUES " +
                                                   "('" + knowlarityRecord.to + "', '" +
                                                    knowlarityRecord.Cuimid + "','" +
                                                         knowlarityRecord.custno + "','" +
                                                         knowlarityRecord.EmpNo + "','" +
                                                         knowlarityRecord.KnowNo + "','" +
                                                         knowlarityRecord.Record + "','" +
                                                         dt + "','" +
                                                         knowlarityRecord.Duration + "','" +
                                                         knowlarityRecord.RecordStat +
                                                         "')");
                                        }

                                        listRecords.Reverse();
                                        if (listRecords.Count > 0)
                                        {
                                            using (var innerConnection = _connections.NewFor<KnowlarityDetailsRow>())
                                            {
                                                for (int ij = 0; ij < listRecords.Count; ij++)
                                                {
                                                    try
                                                    {
                                                        innerConnection.Execute(listRecords[ij]);
                                                    }
                                                    catch (Exception)
                                                    {
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        if (ex.Response is HttpWebResponse errorResponse)
                        {
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                string errorDetails = reader.ReadToEnd();
                                Console.WriteLine($"Error Details: {errorDetails}");
                            }
                        }
                        Console.WriteLine($"WebException: {ex.Message}");
                    }
                }
                else if (Config.IVRType.Value == Masters.IVRTypeMaster.TeleCMI)
                {
                    dynamic start_time, end_time;
                    DateTime dttstart;
                    string knowlarity_no = Config.IVRNumber.Trim().Replace("+", "%2B");
                    if (LastEnquiry == null)
                    {
                        dttstart = DateTime.Now.AddDays(-7);
                        long yourDateTimeMilliseconds = new DateTimeOffset(DateTime.Now.AddDays(-7)).ToUnixTimeMilliseconds();
                        start_time = Convert.ToString(yourDateTimeMilliseconds);
                    }
                    else
                    {
                        dttstart = Convert.ToDateTime(LastEnquiry.DateTime);
                        long yourDateTimeMilliseconds = new DateTimeOffset(dttstart).ToUnixTimeMilliseconds();
                        start_time = Convert.ToString(yourDateTimeMilliseconds);
                    }

                    long CurrentDateTimeMilliseconds = new DateTimeOffset(dttstart.AddDays(7)).ToUnixTimeMilliseconds();
                    end_time = Convert.ToString(CurrentDateTimeMilliseconds);

                    string apiUrl = "https://rest.telecmi.com/v2/";
                    string type = string.Empty;
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 0)
                        {
                            apiUrl = "https://rest.telecmi.com/v2/answered";
                            type = "answered";
                        }
                        else if (i == 1)
                        {
                            apiUrl = "https://rest.telecmi.com/v2/missed";
                            type = "missed";
                        }
                        else if (i == 2)
                        {
                            apiUrl = "https://rest.telecmi.com/v2/out_answered";
                            type = "outanswered";
                        }
                        else if (i == 3)
                        {
                            apiUrl = "https://rest.telecmi.com/v2/out_missed";
                            type = "outmissed";
                        }

                        HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
                        myHttpWebRequest.ContentType = "application/json";
                        myHttpWebRequest.Method = "POST";

                        using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                        {
                            string json = "{\"appid\":" + Config.AppId + "," +
                                          "\"secret\":\"" + Config.AppSecret + "\"," +
                                          "\"start_date\":" + start_time + "," +
                                          "\"end_date\":" + end_time + "," +
                                          "\"page\":" + 1 + "}";
                            streamWriter.Write(json);
                        }

                        myHttpWebRequest.Timeout = 15000;

                        myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                        var reader = new StreamReader(myHttpWebResponse.GetResponseStream());

                        var Knowlarityobjs = JsonConvert.DeserializeObject<dynamic>(reader.ReadToEnd());

                        List<string> listRecords = new List<string>();

                        KnowlarityDetail knowlarityRecord = new KnowlarityDetail();
                        knowlarityRecord.custno = Knowlarityobjs["count"];
                        int count = Convert.ToInt32(knowlarityRecord.custno);
                        int page = 1;
                        if (count > 10)
                        {
                            page = (count / 10) + 1;
                            for (int ii = 1; ii <= page; ii++)
                            {
                                HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(apiUrl);
                                myHttpWebRequest1.ContentType = "application/json";
                                myHttpWebRequest1.Method = "POST";

                                using (var streamWriter1 = new StreamWriter(myHttpWebRequest1.GetRequestStream()))
                                {
                                    string json1 = "{\"appid\":" + Config.AppId + "," +
                                                  "\"secret\":\"" + Config.AppSecret + "\"," +
                                                  "\"start_date\":" + start_time + "," +
                                                  "\"end_date\":" + end_time + "," +
                                                  "\"page\":" + ii + "}";
                                    streamWriter1.Write(json1);
                                }

                                myHttpWebRequest1.Timeout = 15000;

                                myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
                                var reader1 = new StreamReader(myHttpWebResponse1.GetResponseStream());

                                var Knowlarityobjs1 = JsonConvert.DeserializeObject<dynamic>(reader1.ReadToEnd());

                                foreach (var Knowlarityobj1 in Knowlarityobjs1["cdr"])
                                {
                                    if (Knowlarityobj1 != null)
                                    {
                                        KnowlarityDetail detail = new KnowlarityDetail
                                        {
                                            custno = Knowlarityobj1.name,
                                            EmpNo = Knowlarityobj1.agent,
                                            KnowNo = Knowlarityobj1.to,
                                            Record = Knowlarityobj1.filename,
                                            Date = Knowlarityobj1.time,
                                            Duration = Knowlarityobj1.duration,
                                            Cuimid = Knowlarityobj1.cmiuid,
                                            BilledSec = Knowlarityobj1.billedsec,
                                            Rate = Knowlarityobj1.rate,
                                            to = Knowlarityobj1.from,
                                            Type = type,
                                            RecordStat = Knowlarityobj1.record
                                        };
                                        double dat = Convert.ToDouble(detail.Date);
                                        DateTime dtt = new DateTime(1970, 1, 1, 0, 0, 0, 0)
                                            .AddSeconds(Math.Round(dat / 1000d))
                                            .ToLocalTime();
                                        var dt = dtt.ToString("yyyy/MM/dd HH:mm:ss");

                                        listRecords.Add("IF NOT EXISTS(SELECT * FROM KnowlarityDetails WHERE CMIUID = '" + detail.Cuimid + "')" +
                                            "INSERT INTO KnowlarityDetails ([Name],[CMIUID],[BilledSec],[Rate],[Type],[Record],[CustomerNumber],[EmployeeNumber],[From],[Recording],[DateTime],[Duration]) VALUES " +
                                               "('" + detail.custno + "','" +
                                                     detail.Cuimid + "','" +
                                                     detail.BilledSec + "','" +
                                                     detail.Rate + "','" +
                                                     detail.Type + "','" +
                                                     detail.RecordStat + "','" +
                                                     detail.to + "','" +
                                                     detail.EmpNo + "','" +
                                                     detail.KnowNo + "','" +
                                                     detail.Record + "','" +
                                                     dt + "','" +
                                                     detail.Duration + "')");
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var Knowlarityobj in Knowlarityobjs["cdr"])
                            {
                                if (Knowlarityobj != null)
                                {
                                    KnowlarityDetail detail = new KnowlarityDetail
                                    {
                                        custno = Knowlarityobj.name,
                                        EmpNo = Knowlarityobj.agent,
                                        KnowNo = Knowlarityobj.to,
                                        Record = Knowlarityobj.filename,
                                        Date = Knowlarityobj.time,
                                        Duration = Knowlarityobj.duration,
                                        Cuimid = Knowlarityobj.cmiuid,
                                        BilledSec = Knowlarityobj.billedsec,
                                        Rate = Knowlarityobj.rate,
                                        to = Knowlarityobj.from,
                                        Type = type,
                                        RecordStat = Knowlarityobj.record
                                    };
                                    double dat = Convert.ToDouble(detail.Date);
                                    DateTime dtt = new DateTime(1970, 1, 1, 0, 0, 0, 0)
                                        .AddSeconds(Math.Round(dat / 1000d))
                                        .ToLocalTime();
                                    var dt = dtt.ToString("yyyy/MM/dd HH:mm:ss");

                                    listRecords.Add("IF NOT EXISTS(SELECT * FROM KnowlarityDetails WHERE CMIUID = '" + detail.Cuimid + "')" +
                                        "INSERT INTO KnowlarityDetails ([Name],[CMIUID],[BilledSec],[Rate],[Type],[Record],[CustomerNumber],[EmployeeNumber],[From],[Recording],[DateTime],[Duration]) VALUES " +
                                           "('" + detail.custno + "','" +
                                                 detail.Cuimid + "','" +
                                                 detail.BilledSec + "','" +
                                                 detail.Rate + "','" +
                                                 detail.Type + "','" +
                                                 detail.RecordStat + "','" +
                                                 detail.to + "','" +
                                                 detail.EmpNo + "','" +
                                                 detail.KnowNo + "','" +
                                                 detail.Record + "','" +
                                                 dt + "','" +
                                                 detail.Duration + "')");
                                }
                            }
                        }

                        listRecords.Reverse();
                        if (listRecords.Count > 0)
                        {
                            using (var innerConnection = _connections.NewFor<KnowlarityDetailsRow>())
                            {
                                for (int ij = 0; ij < listRecords.Count; ij++)
                                {
                                    try
                                    {
                                        innerConnection.Execute(listRecords[ij]);
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                            }
                        }
                    }
                }

                response.Status = "Sync completed";
            }
            return response;
        }

        [HttpPost]
        public StandardResponse play(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();
            IVRConfigurationRow Config;
            var data = new MyRow();
            var sl = MyRow.Fields;
            using (var connection = _connections.NewFor<IVRConfigurationRow>())
            {
                var s = IVRConfigurationRow.Fields;
                Config = connection.TryFirst<IVRConfigurationRow>(q => q
                    .SelectTableFields()
                    .Select(s.IVRType)
                    .Select(s.IVRNumber)
                    .Select(s.AppId)
                    .Select(s.AppSecret)
                );
                data = connection.TryById<MyRow>(request.Id, q => q
                    .SelectTableFields()
                    .Select(sl.Id)
                    .Select(sl.Recording)
                    .OrderBy(sl.Id, desc: true)
                );
            }
            if (data != null)
            {
                if (Config.IVRType.Value == Masters.IVRTypeMaster.Knowlarity)
                {
                    string url = data.Recording;
                    response.Status = " <audio controls autoplay><source src = '" + url + "' /></audio>";
                }
                else if (Config.IVRType.Value == Masters.IVRTypeMaster.Cloud_Connect)
                {
                    string url = data.Recording;
                    response.Status = " <audio controls autoplay><source src = '" + url + "' /></audio>";
                }
                else
                {
                    if (Config.IVRType.Value == Masters.IVRTypeMaster.TeleCMI)
                    {
                        string url = "https://rest.telecmi.com/v2/play?appid=" + Config.AppId + "&secret=" + Config.AppSecret + "&file=" + data.Recording;
                        response.Status = " <audio controls autoplay><source src = '" + url + "'/></audio>";
                    }
                }
            }
            return response;
        }

        [ServiceAuthorize("BizplusIVR:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.KnowlarityDetailsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "BizplusIVR" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        [HttpPost]
        public StandardResponse MoveToCMS(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();
            var Contacttyp = 2;
            CMSRow LastEnquiry;
            var conid = 0;
            var br = UserRow.Fields;
            var UData = new UserRow();
            var data = new KnowlarityDetailsRow();

            using (var connection = _connections.NewFor<KnowlarityDetailsRow>())
            {
                var ind = KnowlarityDetailsRow.Fields;
                data = connection.TryById<KnowlarityDetailsRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.Name)
                   .Select(ind.CustomerNumber)
                   .Select(ind.DateTime)
                   .Select(ind.Email)
                   .Select(ind.Type)
                   .Select(ind.EmployeeNumber)
                   .Select(ind.CompanyType)
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
                    var c = ContactsRow.Fields;
                    var LastContactc = connection.Count<ContactsRow>(c.Phone == data.CustomerNumber);

                    {
                        Contacttyp = 1;
                    }
                    if (LastContactc == 0)
                    {
                        if (data.Name == "")
                        {
                            data.Name = "unknown";
                        }
                        string date1 = Convert.ToDateTime(data.DateTime).ToString("yyyy-MM-dd");
                        string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,OwnerId,AssignedId,DateCreated) VALUES('" + Contacttyp + "','81','1','" + data.Name + "','" + data.CustomerNumber + "','" + data.Email + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + date1 + "')";
                        connection.Execute(str);
                    }

                    var LastContact = connection.First<ContactsRow>(l => l
                        .Select(c.Id)
                        .Select(c.Name)
                        .Where(c.Phone == data.CustomerNumber)
                        .OrderBy(c.Id, desc: true)
                    );

                    var s = SourceRow.Fields;
                    var sourceMaster = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where((s.Source == "IVR") || (s.Source == "ivr") || (s.Source == "I V R") || (s.Source == "i v r"))
                    );

                    if (sourceMaster == null)
                    {
                        response.Status = "Error: IVR Source not found in Source master\nKindly add in masters and try again";
                        throw new Exception("IVR Source not found in Source master\nKindly add in masters and try again");
                    }

                    var st = StageRow.Fields;
                    var stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(st.Id)
                        .Select(st.Stage)
                        .Where((st.Type == (Int32)Masters.StageTypeMaster.Enquiry))
                    );

                    if (stageMaster == null)
                    {
                        response.Status = "Error: Enquiry Stage not found in Stage master\nKindly add in masters and try again";
                        throw new Exception("Enquiry Stage not found in Stage master\nKindly add in masters and try again");
                    }

                    var nextNumber = new CMSController(
                        Dependency.Resolve<ISqlConnections>(),
                        Context).GetNextNumber(uow.Connection,new GetNextNumberRequest());
                    string date = Convert.ToDateTime(data.DateTime).ToString("yyyy-MM-dd");

                    var str1 = "INSERT INTO CMS(ContactsId,ProductsId,Date,Status,AssignedBy,AssignedTo,CMSNo,CMSN,ComplaintId,Category) VALUES('" + LastContact.Id + "','1','" + date + "','1','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','1','1')";
                    connection.Execute(str1);

                    var e = CMSRow.Fields;
                    LastEnquiry = connection.First<CMSRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                    );

                    connection.Execute("Update KnowlarityDetails SET IsMoved = 1 WHERE Id = " + request.Id);

                    var IndiaMartSettings = new IVRConfigurationRow();
                    var i = IVRConfigurationRow.Fields;
                    IndiaMartSettings = connection.First<IVRConfigurationRow>(l => l
                        .SelectTableFields()
                        .Select(i.AutoEmail)
                        .Select(i.AutoSms)
                        .Select(i.Sender)
                        .Select(i.Subject)
                        .Select(i.SmsTemplate)
                        .Select(i.TemplateId)
                        .Select(i.EmailTemplate)
                        .Select(i.Host)
                        .Select(i.Port)
                        .Select(i.Ssl)
                        .Select(i.EmailId)
                        .Select(i.EmailPassword)
                    );

                    if (IndiaMartSettings.AutoEmail.Value == true && !data.Email.IsNullOrEmpty())
                    {
                        var u = UserRow.Fields;
                        var User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                            .SelectTableFields()
                            .Select(u.Host)
                            .Select(u.Port)
                            .Select(u.SSL)
                            .Select(u.EmailId)
                            .Select(u.EmailPassword)
                        );

                        try
                        {
                            MailMessage mm = new MailMessage();
                            MailAddress addr = null;

                            if (IndiaMartSettings.Host != null)
                            {
                                addr = new MailAddress(IndiaMartSettings.EmailId, IndiaMartSettings.Sender);
                            }
                            else
                            {
                                addr = new MailAddress(User.EmailId, IndiaMartSettings.Sender);
                            }

                            mm.From = addr;
                            mm.Sender = addr;
                            mm.To.Add(data.Email);
                            mm.Subject = IndiaMartSettings.Subject;
                            string msgBody = IndiaMartSettings.EmailTemplate;
                            msgBody = msgBody.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                            msgBody = msgBody.Replace("#customername", data.Name.IsNullOrEmpty() ? "Customer" : data.Name);
                            mm.Body = msgBody;

                            if (IndiaMartSettings.Attachment != null)
                            {
                                JArray att = JArray.Parse(IndiaMartSettings.Attachment);
                                foreach (var f in att)
                                {
                                    if (f["Filename"].HasValue())
                                    {
                                        string filename = f["Filename"].ToString();
                                        string filePath = Path.Combine(_env.ContentRootPath, "App_Data", "upload", filename);
                                        mm.Attachments.Add(new Attachment(filePath));
                                    }
                                }
                            }

                            mm.IsBodyHtml = true;

                            if (IndiaMartSettings.Host != null)
                            {
                                EmailHelper.Send(mm, IndiaMartSettings.EmailId, IndiaMartSettings.EmailPassword, (Boolean)IndiaMartSettings.Ssl, IndiaMartSettings.Host, IndiaMartSettings.Port.Value);
                            }
                            else
                            {
                                EmailHelper.Send(mm, User.EmailId, User.EmailPassword, (Boolean)User.SSL, User.Host, User.Port.Value);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message.ToString());
                        }
                    }

                    if (IndiaMartSettings.AutoSms.Value == true && !data.CustomerNumber.IsNullOrEmpty())
                    {
                        string msg = IndiaMartSettings.SmsTemplate;
                        string tempId = IndiaMartSettings.TemplateId;
                        msg = msg.Replace("#customername", data.Name.IsNullOrEmpty() ? "Customer" : data.Name);
                        data.CustomerNumber = data.CustomerNumber.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                        SMSHelper.SendSMS(data.CustomerNumber, msg, tempId);
                    }

                    response.Id = LastEnquiry.Id.Value;
                    response.Status = "Success";
                }
            }
            catch (Exception ex)
            {
                response.Id = -1;
                response.Status = "Error\n" + ex.ToString();
            }

            return response;
        }

        [HttpPost]
        public StandardResponse MoveToEnquiry(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();
            var Contacttyp = 2;
            EnquiryRow LastEnquiry;
            var br = UserRow.Fields;
            var UData = new UserRow();
            var data = new KnowlarityDetailsRow();

            using (var connection = _connections.NewFor<KnowlarityDetailsRow>())
            {
                var ind = KnowlarityDetailsRow.Fields;
                data = connection.TryById<KnowlarityDetailsRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.Name)
                   .Select(ind.CustomerNumber)
                   .Select(ind.DateTime)
                   .Select(ind.Email)
                   .Select(ind.Type)
                   .Select(ind.EmployeeNumber)
                   .Select(ind.CompanyType)
                );

                UData = connection.First<UserRow>(q => q
                    .SelectTableFields()
                    .Select(br.CompanyId)
                    .Where(br.UserId == Context.User.GetIdentifier())
                );
            }

            using (var connection = _connections.NewFor<ContactsRow>())
            {
                var c = ContactsRow.Fields;
                var LastContactc = connection.Count<ContactsRow>(c.Phone == data.CustomerNumber);

                Contacttyp = 1;
                if (LastContactc == 0)
                {
                    if (data.Name == "")
                    {
                        data.Name = "unknown";
                    }
                    var CustomerNo = data.CustomerNumber.Replace("+", "");
                    string date1 = Convert.ToDateTime(data.DateTime).ToString("yyyy-MM-dd");
                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,OwnerId,AssignedId,DateCreated) VALUES('" + Contacttyp + "','81','1','" + data.Name + "','" + CustomerNo + "','" + data.Email + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + date1 + "')";
                    connection.Execute(str);
                }

                var CustomerNumber = data.CustomerNumber.Replace("+", "");
                var LastContact = connection.First<ContactsRow>(l => l
                           .Select(c.Id)
                           .Select(c.Name)
                           .Where(c.Phone == CustomerNumber)
                           .OrderBy(c.Id, desc: true)
                );
                if (data.Name != LastContact.Name)
                {
                    response.Status = "Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same";
                    throw new Exception("This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");
                }

                var s = SourceRow.Fields;
                var sourceMaster = connection.TryFirst<SourceRow>(l => l
                    .Select(s.Id)
                    .Select(s.Source)
                    .Where((s.Source == "IVR") || (s.Source == "ivr") || (s.Source == "I V R") || (s.Source == "i v r"))
                );

                if (sourceMaster == null)
                {
                    response.Status = "Error: IVR Source not found in Source master\nKindly add in masters and try again";
                    throw new Exception("IVR Source not found in Source master\nKindly add in masters and try again");
                }

                var st = StageRow.Fields;
                var stageMaster = connection.TryFirst<StageRow>(l => l
                    .Select(st.Id)
                    .Select(st.Stage)
                    .Where((st.Type == (Int32)Masters.StageTypeMaster.Enquiry))
                );

                if (stageMaster == null)
                {
                    response.Status = "Error: Enquiry Stage not found in Stage master\nKindly add in masters and try again";
                    throw new Exception("Enquiry Stage not found in Stage master\nKindly add in masters and try again");
                }

                var enquiryCtrl = new EnquiryController(
                    _userAccessor,
                    _connections,
                    _configuration,
                    _env,
                    _permissionService,
                    _requestContext,
                    _memoryCache,
                    _typeSource,
                    _userRetriever);
                GetNextNumberResponse nextNumber = enquiryCtrl.GetNextNumber(uow.Connection, new GetNextNumberRequest());
                string date = Convert.ToDateTime(data.DateTime).ToString("yyyy-MM-dd");

                var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + sourceMaster.Id + "','" + stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','" + UData.CompanyId + "')";
                connection.Execute(str1);

                var e = EnquiryRow.Fields;
                LastEnquiry = connection.First<EnquiryRow>(l => l
                    .Select(e.Id)
                    .OrderBy(e.Id, desc: true)
                );

                connection.Execute("Update KnowlarityDetails SET IsMoved = 1 WHERE Id = " + request.Id);

                var IndiaMartSettings = new IVRConfigurationRow();
                var i = IVRConfigurationRow.Fields;
                IndiaMartSettings = connection.First<IVRConfigurationRow>(l => l
                .SelectTableFields()
                    .Select(i.AutoEmail)
                    .Select(i.AutoSms)
                    .Select(i.Sender)
                    .Select(i.Subject)
                    .Select(i.SmsTemplate)
                    .Select(i.TemplateId)
                    .Select(i.EmailTemplate)
                    .Select(i.Host)
                    .Select(i.Port)
                    .Select(i.Ssl)
                    .Select(i.EmailId)
                    .Select(i.EmailPassword)
                );

                if (IndiaMartSettings.AutoEmail.Value == true && !data.Email.IsNullOrEmpty())
                {
                    var u = UserRow.Fields;
                    var User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                        .SelectTableFields()
                        .Select(u.Host)
                        .Select(u.Port)
                        .Select(u.SSL)
                        .Select(u.EmailId)
                        .Select(u.EmailPassword)
                    );

                    try
                    {
                        MailMessage mm = new MailMessage();
                        MailAddress addr = null;

                        if (IndiaMartSettings.Host != null)
                        {
                            addr = new MailAddress(IndiaMartSettings.EmailId, IndiaMartSettings.Sender);
                        }
                        else
                        {
                            addr = new MailAddress(User.EmailId, IndiaMartSettings.Sender);
                        }

                        mm.From = addr;
                        mm.Sender = addr;
                        mm.To.Add(data.Email);
                        mm.Subject = IndiaMartSettings.Subject;
                        string msgBody = IndiaMartSettings.EmailTemplate;
                        msgBody = msgBody.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                        msgBody = msgBody.Replace("#customername", data.Name.IsNullOrEmpty() ? "Customer" : data.Name);
                        mm.Body = msgBody;

                        if (IndiaMartSettings.Attachment != null)
                        {
                            JArray att = JArray.Parse(IndiaMartSettings.Attachment);
                            foreach (var f in att)
                            {
                                if (f["Filename"].HasValue())
                                {
                                    string filename = f["Filename"].ToString();
                                    string filePath = Path.Combine(_env.ContentRootPath, "App_Data", "upload", filename);
                                    mm.Attachments.Add(new Attachment(filePath));
                                }
                            }
                        }

                        mm.IsBodyHtml = true;

                        if (IndiaMartSettings.Host != null)
                        {
                            EmailHelper.Send(mm, IndiaMartSettings.EmailId, IndiaMartSettings.EmailPassword, (Boolean)IndiaMartSettings.Ssl, IndiaMartSettings.Host, IndiaMartSettings.Port.Value);
                        }
                        else
                        {
                            EmailHelper.Send(mm, User.EmailId, User.EmailPassword, (Boolean)User.SSL, User.Host, User.Port.Value);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message.ToString());
                    }
                }

                if (IndiaMartSettings.AutoSms.Value == true && !data.CustomerNumber.IsNullOrEmpty())
                {
                    string msg = IndiaMartSettings.SmsTemplate;
                    string tempid = IndiaMartSettings.TemplateId;
                    msg = msg.Replace("#customername", data.Name.IsNullOrEmpty() ? "Customer" : data.Name);
                    data.CustomerNumber = data.CustomerNumber.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                    SMSHelper.SendSMS(data.CustomerNumber, msg, tempid);
                }

                response.Id = LastEnquiry.Id.Value;
                response.Status = "Success";
            }

            return response;
        }

        [HttpPost]
        public BulkImportResponse BulkMoveToEnquiry(IUnitOfWork uow, BulkRequest request)
        {
            var response = new BulkImportResponse();
            var Contacttyp = 2;
            EnquiryRow LastEnquiry;
            var br = UserRow.Fields;
            var UData = new UserRow();
            List<KnowlarityDetailsRow> data1;

            var ind1 = KnowlarityDetailsRow.Fields;

            using (var connection = _connections.NewFor<KnowlarityDetailsRow>())
            {
                data1 = connection.List<KnowlarityDetailsRow>(q => q
                  .SelectTableFields()
                  .Select(ind1.Name)
                  .Select(ind1.CustomerNumber)
                  .Select(ind1.DateTime)
                  .Select(ind1.Email)
                  .Select(ind1.Type)
                  .Select(ind1.EmployeeNumber)
                  .Select(ind1.CompanyType)
                );

                int NoOfEnquiries = data1.Count;
                int NoOfUsrs = request.Ids.Count();

                if (NoOfUsrs < 1)
                {
                    response.ErrorList.Add("No users selected");
                    return response;
                }

                int recordPerUser = NoOfEnquiries / NoOfUsrs;

                int counter = 0;
                int currRecCount = 1;
                string TuserId = request.Ids.ElementAt(counter);

                foreach (var item in data1)
                {
                    string UserId = TuserId;
                    Contacttyp = 1;

                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,OwnerId,AssignedId) VALUES('" + Contacttyp + "','81','1','" + item.Name + "','" + item.CustomerNumber + "','" + item.Email + "','" + UserId + "','" + UserId + "')";
                    connection.Execute(str);

                    var c2 = ContactsRow.Fields;
                    var LastContact = connection.First<ContactsRow>(l => l
                        .Select(c2.Id)
                        .Select(c2.Name)
                        .OrderBy(c2.Id, desc: true)
                    );
                    if (item.Name != LastContact.Name)
                    {
                        response.Status = "Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same";
                        throw new Exception("This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");
                    }

                    var s = SourceRow.Fields;
                    var sourceMaster = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where((s.Source == "IVR") || (s.Source == "ivr") || (s.Source == "I V R") || (s.Source == "i v r"))
                    );

                    if (sourceMaster == null)
                    {
                        response.Status = "Error: IVR Source not found in Source master\nKindly add in masters and try again";
                        throw new Exception("IVR Source not found in Source master\nKindly add in masters and try again");
                    }

                    var st = StageRow.Fields;
                    var stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(st.Id)
                        .Select(st.Stage)
                        .Where((st.Type == (Int32)Masters.StageTypeMaster.Enquiry))
                    );

                    if (stageMaster == null)
                    {
                        response.Status = "Error: Enquiry Stage not found in Stage master\nKindly add in masters and try again";
                        throw new Exception("Enquiry Stage not found in Stage master\nKindly add in masters and try again");
                    }

                    var enquiryCtrl = new EnquiryController(
                        _userAccessor,
                        _connections,
                        _configuration,
                        _env,
                        _permissionService,
                        _requestContext,
                        _memoryCache,
                        _typeSource,
                        _userRetriever);
                    GetNextNumberResponse nextNumber = enquiryCtrl.GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(item.DateTime).ToString("yyyy-MM-dd");

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN) VALUES('" + LastContact.Id + "','" + date + "','1','" + sourceMaster.Id + "','" + stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "')";
                    connection.Execute(str1);

                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                    );

                    connection.Execute("Update KnowlarityDetails SET IsMoved = 1 WHERE Id = " + item.Id);

                    var IndiaMartSettings = new IVRConfigurationRow();
                    var i = IVRConfigurationRow.Fields;
                    IndiaMartSettings = connection.First<IVRConfigurationRow>(l => l
                    .SelectTableFields()
                        .Select(i.AutoEmail)
                        .Select(i.AutoSms)
                        .Select(i.Sender)
                        .Select(i.Subject)
                        .Select(i.SmsTemplate)
                        .Select(i.EmailTemplate)
                        .Select(i.Host)
                        .Select(i.Port)
                        .Select(i.Ssl)
                        .Select(i.EmailId)
                        .Select(i.EmailPassword)
                    );

                    if (IndiaMartSettings.AutoEmail.Value == true && !item.Email.IsNullOrEmpty())
                    {
                        var u = UserRow.Fields;
                        var User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                            .SelectTableFields()
                            .Select(u.Host)
                            .Select(u.Port)
                            .Select(u.SSL)
                            .Select(u.EmailId)
                            .Select(u.EmailPassword)
                        );

                        try
                        {
                            MailMessage mm = new MailMessage();
                            MailAddress addr = null;

                            if (IndiaMartSettings.Host != null)
                            {
                                addr = new MailAddress(IndiaMartSettings.EmailId, IndiaMartSettings.Sender);
                            }
                            else
                            {
                                addr = new MailAddress(User.EmailId, IndiaMartSettings.Sender);
                            }

                            mm.From = addr;
                            mm.Sender = addr;
                            mm.To.Add(item.Email);
                            mm.Subject = IndiaMartSettings.Subject;
                            string msgBody = IndiaMartSettings.EmailTemplate;
                            msgBody = msgBody.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                            msgBody = msgBody.Replace("#customername", item.Name.IsNullOrEmpty() ? "Customer" : item.Name);
                            mm.Body = msgBody;

                            if (IndiaMartSettings.Attachment != null)
                            {
                                JArray att = JArray.Parse(IndiaMartSettings.Attachment);
                                foreach (var f in att)
                                {
                                    if (f["Filename"].HasValue())
                                    {
                                        string filename = f["Filename"].ToString();
                                        string filePath = Path.Combine(_env.ContentRootPath, "App_Data", "upload", filename);
                                        mm.Attachments.Add(new Attachment(filePath));
                                    }
                                }
                            }

                            mm.IsBodyHtml = true;

                            if (IndiaMartSettings.Host != null)
                            {
                                EmailHelper.Send(mm, IndiaMartSettings.EmailId, IndiaMartSettings.EmailPassword, (Boolean)IndiaMartSettings.Ssl, IndiaMartSettings.Host, IndiaMartSettings.Port.Value);
                            }
                            else
                            {
                                EmailHelper.Send(mm, User.EmailId, User.EmailPassword, (Boolean)User.SSL, User.Host, User.Port.Value);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message.ToString());
                        }
                    }

                    if (IndiaMartSettings.AutoSms.Value == true && !item.CustomerNumber.IsNullOrEmpty())
                    {
                        string msg = IndiaMartSettings.SmsTemplate;
                        string tempId = IndiaMartSettings.TemplateId;
                        msg = msg.Replace("#customername", item.Name.IsNullOrEmpty() ? "Customer" : item.Name);
                        item.CustomerNumber = item.CustomerNumber.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                        SMSHelper.SendSMS(item.CustomerNumber, msg, tempId);
                    }

                    if (currRecCount == recordPerUser)
                    {
                        if ((counter < NoOfUsrs) && (counter != (NoOfUsrs - 1)))
                        {
                            TuserId = request.Ids.ElementAt(counter + 1);
                            currRecCount = 0;
                        }
                        counter++;
                    }
                    currRecCount++;

                    response.Inserted = response.Inserted + 1;
                }
            }
            return response;
        }

        // Helper method to get the next number
        private GetNextNumberResponse GetNextNumber(IDbConnection connection)
        {
            var enquiryCtrl = new EnquiryController(
                _userAccessor,
                _connections,
                _configuration,
                _env,
                _permissionService,
                _requestContext,
                _memoryCache,
                _typeSource,
                _userRetriever);
            return enquiryCtrl.GetNextNumber(connection, new GetNextNumberRequest());
        }

        internal class KnowlarityDetail
        {
            public int count { get; set; }
            public int Id { get; set; }
            public string Cuimid { get; set; }
            public string BilledSec { get; set; }
            public string Rate { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string RecordStat { get; set; }
            public string Type { get; set; }
            public string custno { get; set; }
            public string EmpNo { get; set; }
            public string KnowNo { get; set; }
            public string Record { get; set; }
            public string Date { get; set; }
            public string Duration { get; set; }
        }
    }
}
