
namespace AdvanceCRM.ThirdParty.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Settings;
    using AdvanceCRM.Enquiry.Endpoints;
    using AdvanceCRM.ThirdParty;
    using Newtonsoft.Json.Linq;
    using Serenity;
    using System;
    using System.Linq;
    using System.Net;
    using Newtonsoft.Json;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System.Data;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Mail;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Caching.Memory;
    using Serenity.Abstractions;

    //using System.Web.Script.Serialization;
    using MyRepository = Repositories.InteraktUserRepository;
    using MyRow = InteraktUserRow;
    using System.Net.Http;
    using Serenity.Extensions.DependencyInjection;

    [Route("Services/ThirdParty/InteraktUser/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class InteraktUserController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly IUserAccessor _userAccessor;
        private readonly IPermissionService _permissionService;
        private readonly IRequestContext _requestContext;
        private readonly IMemoryCache _memoryCache;
        private readonly ITypeSource _typeSource;
        private readonly IUserRetrieveService _userRetriever;

        public InteraktUserController(
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

        public InteraktUserController()
            : this(
                  Dependency.Resolve<IUserAccessor>(),
                  Dependency.Resolve<ISqlConnections>(),
                  Dependency.Resolve<IConfiguration>(),
                  Dependency.Resolve<IWebHostEnvironment>(),
                  Dependency.Resolve<IPermissionService>(),
                  Dependency.Resolve<IRequestContext>(),
                  Dependency.Resolve<IMemoryCache>(),
                  Dependency.Resolve<ITypeSource>(),
                  Dependency.Resolve<IUserRetrieveService>())
        {
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

        [ServiceAuthorize("Interakt:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            DynamicDataReport report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.InteraktUserColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "InteraktEnquiries_" +
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

            var data = new InteraktUserRow();

            using (var connection = _connections.NewFor<InteraktUserRow>())
            {
                var ind = InteraktUserRow.Fields;
                data = connection.TryById<InteraktUserRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.FullName)
                   .Select(ind.Phone)
                   .Select(ind.Email)
                   .Select(ind.Created)
                  
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
                    //if (!(string.IsNullOrEmpty)(data.Company))
                    //{
                    //    Contacttyp = 2;
                    //}
                   // else
                    {
                        Contacttyp = 1;
                    }
                    string date1 = Convert.ToDateTime(data.Created).ToString("yyyy-MM-dd HH:mm:ss");
                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,OwnerId,AssignedId,DateCreated) VALUES('" + Contacttyp + "','81','1','" + data.FullName + "','" + data.Phone + "','" + data.Email + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + date1 + "')";

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

                    if (data.FullName != LastContact.Name)
                    {
                        response.Status = "Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same";

                        throw new Exception("This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");
                    }
                    // }

                    var s = SourceRow.Fields;
                    var Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where((s.Source == "Interakt") || (s.Source == "INTERAKT"))
                        );

                    if (Source == null)
                    {
                        string str2 = "INSERT INTO Source(Source) VALUES('Interakt')";
                        connection.Execute(str2);

                        Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "Interakt")
                        );
                    }

                    var st = StageRow.Fields;
                    var stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(st.Id)
                        .Select(st.Stage)
                        .Where((st.Type == (Int32)Masters.StageTypeMaster.Enquiry))
                        );

                    if (stageMaster == null)
                    {
                        string str2 = "INSERT INTO Stage(Stage, Type) VALUES('Initial', 1)";
                        connection.Execute(str2);

                        stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "Facebook")
                        );
                    }

                    GetNextNumberResponse nextNumber = new EnquiryController(
                        _userAccessor,
                        _connections,
                        _configuration,
                        _env,
                        _permissionService,
                        _requestContext,
                        _memoryCache,
                        _typeSource,
                        _userRetriever
                    ).GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(data.Created).ToString("yyyy-MM-dd HH:mm:ss");

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + Source.Id + "','" + stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','" + UData.CompanyId + "')";

                    connection.Execute(str1);

                    // var t = EnquiryRow.Fields;
                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                        );

                    connection.Execute("Update InteraktUser SET IsMoved = 1 WHERE Id = " + request.Id);


                    var FacebookEnquirySettings = new InteraktConfigRow();

                    var i = InteraktConfigRow.Fields;
                    FacebookEnquirySettings = connection.First<InteraktConfigRow>(l => l
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

                    if (FacebookEnquirySettings.AutoEmail.Value == true && !model.Email.IsNullOrEmpty())
                    {
                        var u = UserRow.Fields;
                        var User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                            .SelectTableFields()
                            .Select(u.Host)
                            .Select(u.Port)
                            .Select(u.SSL)
                            .Select(u.EmailId)
                            .Select(u.EmailPassword));

                        try
                        {
                            if (FacebookEnquirySettings.Host != null)
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(FacebookEnquirySettings.EmailId, FacebookEnquirySettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(model.Email);
                                mm.Subject = FacebookEnquirySettings.Subject;
                                var msg = FacebookEnquirySettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", model.FullName.IsNullOrEmpty() ? "Customer" : model.FullName);
                                mm.Body = msg;

                                if (FacebookEnquirySettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(FacebookEnquirySettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            var path = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                            mm.Attachments.Add(new Attachment(path));
                                        }
                                    }
                                }

                                mm.IsBodyHtml = true;

                                EmailHelper.Send(mm, FacebookEnquirySettings.EmailId, FacebookEnquirySettings.EmailPassword, (Boolean)FacebookEnquirySettings.Ssl, FacebookEnquirySettings.Host, FacebookEnquirySettings.Port.Value);
                            }
                            else
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(User.EmailId, FacebookEnquirySettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(model.Email);
                                mm.Subject = FacebookEnquirySettings.Subject;
                                var msg = FacebookEnquirySettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", model.FullName.IsNullOrEmpty() ? "Customer" : model.FullName);
                                mm.Body = msg;

                                if (FacebookEnquirySettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(FacebookEnquirySettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            var path = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                            mm.Attachments.Add(new Attachment(path));
                                        }
                                    }
                                }
                                mm.IsBodyHtml = true;

                                EmailHelper.Send(mm, User.EmailId, User.EmailPassword, (Boolean)User.SSL, User.Host, User.Port.Value);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message.ToString());
                        }
                    }

                    if (FacebookEnquirySettings.AutoSms.Value == true && !model.Phone.IsNullOrEmpty())
                    {
                        String msg = FacebookEnquirySettings.SmsTemplate;
                        String TempId = FacebookEnquirySettings.TemplateId;
                        msg = msg.Replace("#customername", model.FullName.IsNullOrEmpty() ? "Customer" : model.FullName);
                        model.Phone = model.Phone.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                        SMSHelper.SendSMS(model.Phone, msg, TempId);
                    }
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
        public BulkImportResponse BulkMoveToEnquiry(IUnitOfWork uow, BulkRequest request)
        {

            var response = new BulkImportResponse();
            EnquiryRow LastEnquiry;
            var Contacttyp = 2;
            var br = UserRow.Fields;
            var UData = new UserRow();
            //var model = new MyRow();
            //////////////////////////////////////////////
            ///
            List<InteraktUserRow> data1;
            var ind1 = InteraktUserRow.Fields;

            using (var connection = _connections.NewFor<InteraktUserRow>())
            {

                data1 = connection.List<InteraktUserRow>(q => q
                    .SelectTableFields()
                   .Select(ind1.FullName)
                   .Select(ind1.Phone)
                   .Select(ind1.Email)
                   .Select(ind1.Created)

                   .Where(ind1.IsMoved == "false")
                );

                int NoOfEnquiries = data1.Count;
                int NoOfUsrs = request.Ids.Count();
                // int NoOfUsrs1 = request.EnqIds.Count();

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
                    var UserId = TuserId;
                  
                        Contacttyp = 1;
                    
                    var fid = item.Id;
                    var abc = item.FullName;
                    string date1 = Convert.ToDateTime(item.Created).ToString("yyyy-MM-dd HH:mm:ss");
                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,OwnerId,AssignedId,DateCreated) VALUES('" + Contacttyp + "','81','1','" + item.FullName + "','" + item.Phone + "','" + item.Email + "','" + UserId + "','" + UserId + "','" + date1 + "')";

                    connection.Execute(str);

                    var c = ContactsRow.Fields;
                    var LastContact = connection.First<ContactsRow>(l => l
                        .Select(c.Id)
                        .Select(c.Name)
                        .OrderBy(c.Id, desc: true)
                        );

                    var s = SourceRow.Fields;
                    var Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where((s.Source == "Interakt") || (s.Source == "INTERAKT"))
                        );

                    if (Source == null)
                    {
                        string str2 = "INSERT INTO Source(Source) VALUES('Interakt')";
                        connection.Execute(str2);

                        Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "Interakt")
                        );
                    }

                    var st = StageRow.Fields;
                    var stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(st.Id)
                        .Select(st.Stage)
                        .Where((st.Type == (Int32)Masters.StageTypeMaster.Enquiry))
                        );

                    if (stageMaster == null)
                    {
                        string str2 = "INSERT INTO Stage(Stage, Type) VALUES('Initial', 1)";
                        connection.Execute(str2);

                        stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "Facebook")
                        );
                    }

                    GetNextNumberResponse nextNumber = new EnquiryController(
                        _userAccessor,
                        _connections,
                        _configuration,
                        _env,
                        _permissionService,
                        _requestContext,
                        _memoryCache,
                        _typeSource,
                        _userRetriever
                    ).GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(item.Created).ToString("yyyy-MM-dd HH:mm:ss");

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + Source.Id + "','" + stageMaster.Id + "','" + UserId + "','" + UserId + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','1')";

                    connection.Execute(str1);

                    // var t = EnquiryRow.Fields;
                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                        );

                    connection.Execute("Update InteraktUser SET IsMoved = 1 WHERE Id = " + item.Id);

                    var FacebookEnquirySettings = new InteraktConfigRow();

                    var i = InteraktConfigRow.Fields;
                    FacebookEnquirySettings = connection.First<InteraktConfigRow>(l => l
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

                    if (FacebookEnquirySettings.AutoEmail.Value == true && !item.Email.IsNullOrEmpty())
                    {
                        var u = UserRow.Fields;
                        var User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                            .SelectTableFields()
                            .Select(u.Host)
                            .Select(u.Port)
                            .Select(u.SSL)
                            .Select(u.EmailId)
                            .Select(u.EmailPassword));

                        try
                        {
                            if (FacebookEnquirySettings.Host != null)
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(FacebookEnquirySettings.EmailId, FacebookEnquirySettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(item.Email);
                                mm.Subject = FacebookEnquirySettings.Subject;
                                var msg = FacebookEnquirySettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", item.FullName.IsNullOrEmpty() ? "Customer" : item.FullName);
                                mm.Body = msg;

                                if (FacebookEnquirySettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(FacebookEnquirySettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            var path = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                            mm.Attachments.Add(new Attachment(path));
                                        }
                                    }
                                }

                                mm.IsBodyHtml = true;

                                EmailHelper.Send(mm, FacebookEnquirySettings.EmailId, FacebookEnquirySettings.EmailPassword, (Boolean)FacebookEnquirySettings.Ssl, FacebookEnquirySettings.Host, FacebookEnquirySettings.Port.Value);
                            }
                            else
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(User.EmailId, FacebookEnquirySettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(item.Email);
                                mm.Subject = FacebookEnquirySettings.Subject;
                                var msg = FacebookEnquirySettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", item.FullName.IsNullOrEmpty() ? "Customer" : item.FullName);
                                mm.Body = msg;

                                if (FacebookEnquirySettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(FacebookEnquirySettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            var path = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                            mm.Attachments.Add(new Attachment(path));
                                        }
                                    }
                                }
                                mm.IsBodyHtml = true;

                                EmailHelper.Send(mm, User.EmailId, User.EmailPassword, (Boolean)User.SSL, User.Host, User.Port.Value);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message.ToString());
                        }
                    }
                    if (FacebookEnquirySettings.AutoSms.Value == true && !item.Phone.IsNullOrEmpty())
                    {
                        String msg = FacebookEnquirySettings.SmsTemplate;
                        msg = msg.Replace("#customername", item.FullName.IsNullOrEmpty() ? "Customer" : item.FullName);
                        item.Phone = item.Phone.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                        SMSHelper.SendSMS(item.Phone, msg, msg);
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
                    // contacts1.Add(data.Find(d => d.Id.ToString() == item));
                }
                return response;

            }
        }


        [HttpPost]
        public StandardResponse Sync(IUnitOfWork uow)
        {
            var response = new StandardResponse();
            InteraktUserRow LastEnquiry;
            DateTime StartDate, EndDate;
            InteraktConfigRow Config;
            //try
            {

                using (var connection = _connections.NewFor<InteraktConfigRow>())
                {
                    var s = InteraktConfigRow.Fields;
                    Config = connection.TryFirst<InteraktConfigRow>(q => q
                        .SelectTableFields()
                        .Select(s.SecretKey)
                        );

                    var i = InteraktUserRow.Fields;
                    LastEnquiry = connection.TryFirst<InteraktUserRow>(q => q
                    .SelectTableFields()
                    .Select(i.Created)
                    .OrderBy(i.Created, true)
                    );
                }


                if (LastEnquiry == null)
                {
                    StartDate = DateTime.Now.AddMonths(-3);
                }
                else
                {
                    StartDate = LastEnquiry.Created.Value;
                }

                EndDate = DateTime.Now;


                //string uri = "https://api.godaddy.com/v1/domains/bizpluscrm.com/subdomains"; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";

                //HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                ////  var request = new HttpRequestMessage(HttpMethod.Post, "https://api.interakt.ai/v1/public/apis/users/?offset=1&limit=100");
                //myHttpWebRequest.Headers.Add("Authorization", "sso-key VVQfy4B7_3ueHA6CQXHbi3T6anzYtxo:VZBdzjUagMdzwFcT3LHwQs");
                //myHttpWebRequest.Headers.Add("Cookie", "_abck=1FC39600CD22197280196FE53E9F15DE~-1~YAAQPtcLFxqXwaCHAQAA6VD3sQl370zvSWxJRCLW9p6SDxTOEYrnKo4/zhTyKZuf+i+sAH4ucI+DD1/edUK5C0uD713hEBUuK6uJ8z00Rm+P5i5DE7+CYHmi2CopcNnEmtuPxmhM51KVb/ebjsElD7963a5+zobcdsjrxm+VqzFc6d+HXCwGytO33K9JMNB13rILX5MBGpxGP9JDPfRsXLem0DB/Tr4G7jQdVwI58e0JUzBps1EBBSV00kEJ5PIgMZSEVdo6N7RJLrNwHkjepY8Vu6EmkGPUuiXslpQsdVWxQdc+GdIZprWwuJQzjCT6k4kKGJhdZR+0asRhosmnfV3+kG9Lm/9j42QLdqr+GOd1fB8KQEY8jP3q~-1~-1~-1");
                //myHttpWebRequest.ContentType = "application/json";
                //myHttpWebRequest.Method = "POST";
                //using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                //{
                //    string json = "\"{\\\"name\\\":\\\"amitnew\\\",\\\"type\\\":\\\"A\\\",\\\"data\\\":\\\"154.61.75.222\\\"}\",null,\"application/json\"";
                //    streamWriter.Write(json);
                //}
                //myHttpWebRequest.Timeout = 15000;
                //HttpWebResponse myHttpWebResponse;

                //myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                //var reader = new StreamReader(myHttpWebResponse.GetResponseStream());

                //JavaScriptSerializer js = new JavaScriptSerializer();
                //var InteraktObjects = js.Deserialize<dynamic>(reader.ReadToEnd());


                //StartDate = StartDate.ToUniversalTime();
                //end

                string uri = "https://api.interakt.ai/v1/public/apis/users/?offset=0&limit=100"; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";

                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                //  var request = new HttpRequestMessage(HttpMethod.Post, "https://api.interakt.ai/v1/public/apis/users/?offset=1&limit=100");
                myHttpWebRequest.Headers.Add("Authorization", "Basic " + Config.SecretKey);
                myHttpWebRequest.Headers.Add("Cookie", "ApplicationGatewayAffinity=a8f6ae06c0b3046487ae2c0ab287e175; ApplicationGatewayAffinityCORS=a8f6ae06c0b3046487ae2c0ab287e175");
                myHttpWebRequest.ContentType = "application/json";
                myHttpWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(myHttpWebRequest.GetRequestStream()))
                {
                    string json = "{\r\n\"filters\": [{\"trait\":\"created_at_utc\",\"op\":\"gt\",\"val\":\"" + StartDate.ToString("yyyy-MM-dd") + "\"},{\"trait\":\"created_at_utc\",\"op\":\"lt\",\"supr_op\":\"and\",\"val\":\"" + EndDate.ToString("yyyy-MM-dd") + "\"}]\r\n}";
                    streamWriter.Write(json);
                }
                myHttpWebRequest.Timeout = 15000;
                HttpWebResponse myHttpWebResponse;

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                var reader = new StreamReader(myHttpWebResponse.GetResponseStream());

                var InteraktObjects = JsonConvert.DeserializeObject<dynamic>(reader.ReadToEnd());

                List<string> Records = new List<string>();

                if (InteraktObjects != null)
                {
                    var status = InteraktObjects["result"];

                    if (status != true)
                    {
                        response.Status = Convert.ToString(InteraktObjects["message"]).Replace("'", "");
                        return response;
                    }
                    int n = 0;
                    InteraktDetail InteraktDetails = new InteraktDetail();

                    foreach (var InteraktObject in InteraktObjects["data"])
                    {
                        if (n == 0)
                        {
                            InteraktDetails.custno = Convert.ToInt32(InteraktObject.Value);
                            foreach (var InteraktObject1 in InteraktObjects["data"]["customers"])
                            {
                                InteraktDetails.id = InteraktObject1["id"];
                                InteraktDetails.created_at = InteraktObject1["created_at_utc"];
                                InteraktDetails.modified_at = InteraktObject1["modified_at_utc"];
                                InteraktDetails.phone_number = InteraktObject1["phone_number"];
                                InteraktDetails.country_code = InteraktObject1["country_code"];
                                InteraktDetails.user_id = InteraktObject1["user_id"];

                                InteraktDetails.name = InteraktObject1["traits"]["name"];
                                InteraktDetails.email = "";// InteraktObject1["traits"]["email"];
                                InteraktDetails.whatsapp_opted_in = InteraktObject1["traits"]["whatsapp_opted_in"];

                                Records.Add("IF NOT EXISTS (SELECT * FROM InteraktUser WHERE InteraktID ='" + InteraktDetails.id + "')" +
                                                "INSERT INTO InteraktUser ([InteraktID],[Created],[Modified],[Phone],[CountryCode],[UserId],[FullName],[Email],[WpOptedIn]) VALUES " +
                                                "('" + InteraktDetails.id + "','" +
                                                    Convert.ToDateTime(InteraktDetails.created_at).ToString("yyyy-MM-dd hh:mm:ss") + "','" +
                                                    Convert.ToDateTime(InteraktDetails.modified_at).ToString("yyyy-MM-dd hh:mm:ss") + "','" +
                                                     InteraktDetails.phone_number + "','" +
                                                     InteraktDetails.country_code + "','" +
                                                 InteraktDetails.user_id + "','" +
                                                     InteraktDetails.name + "','" +
                                                    InteraktDetails.email + "','" +
                                                    InteraktDetails.whatsapp_opted_in + "')");


                            }
                            Records.Reverse();

                            if (Records.Count > 0)
                            {
                                using (var innerConnection = _connections.NewFor<InteraktUserRow>())
                                {
                                    for (int ij = 0; ij < Records.Count; ij++)
                                    {
                                        try
                                        {
                                            innerConnection.Execute(Records[ij]);

                                        }
                                        catch (Exception ex)
                                        {
                                            response.Status = Records[ij];
                                        }

                                    }
                                }
                            }
                            n = 1;

                        }
                        else if (n == 1)
                        {
                            int cust = InteraktDetails.custno;
                            int pages = (cust / 100) + 1; List<string> Records1 = new List<string>();

                            for (int j = 1; j < pages; j++)
                            {
                                var off = j + "00";
                                string uri1 = "https://api.interakt.ai/v1/public/apis/users/?offset=" + off + "&limit=100"; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";

                                HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(uri1);

                                myHttpWebRequest1.Headers.Add("Authorization", "Basic "+Config.SecretKey);
                                myHttpWebRequest1.Headers.Add("Cookie", "ApplicationGatewayAffinity=a8f6ae06c0b3046487ae2c0ab287e175; ApplicationGatewayAffinityCORS=a8f6ae06c0b3046487ae2c0ab287e175");
                                myHttpWebRequest1.ContentType = "application/json";
                                myHttpWebRequest1.Method = "POST";
                                using (var streamWriter1 = new StreamWriter(myHttpWebRequest1.GetRequestStream()))
                                {
                                    string json = "{\r\n\"filters\": [{\"trait\":\"created_at_utc\",\"op\":\"gt\",\"val\":\"" + StartDate.ToString("yyyy-MM-dd") + "\"},{\"trait\":\"created_at_utc\",\"op\":\"lt\",\"supr_op\":\"and\",\"val\":\"" + EndDate.ToString("yyyy-MM-dd") + "\"}]\r\n}";
                                    streamWriter1.Write(json);
                                }
                                myHttpWebRequest1.Timeout = 15000;
                                HttpWebResponse myHttpWebResponse1;

                                myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
                                var reader1 = new StreamReader(myHttpWebResponse1.GetResponseStream());

                                var InteraktObjects1 = JsonConvert.DeserializeObject<dynamic>(reader1.ReadToEnd());

                               

                                if (InteraktObjects1 != null)
                                {
                                    var status1 = InteraktObjects1["result"];

                                    if (status1 != true)
                                    {
                                        response.Status = Convert.ToString(InteraktObjects1["message"]).Replace("'", "");
                                        return response;
                                    }

                                    InteraktDetail InteraktDetails1 = new InteraktDetail();
                                        foreach (var InteraktObject11 in InteraktObjects1["data"]["customers"])
                                        {
                                            InteraktDetails.id = InteraktObject11["id"];
                                            InteraktDetails.created_at = InteraktObject11["created_at_utc"];
                                            InteraktDetails.modified_at = InteraktObject11["modified_at_utc"];
                                            InteraktDetails.phone_number = InteraktObject11["phone_number"];
                                            InteraktDetails.country_code = InteraktObject11["country_code"];
                                            InteraktDetails.user_id = InteraktObject11["user_id"];

                                            InteraktDetails.name = InteraktObject11["traits"]["name"];
                                            InteraktDetails.email = "";// InteraktObject1["traits"]["email"];
                                            InteraktDetails.whatsapp_opted_in = InteraktObject11["traits"]["whatsapp_opted_in"];

                                            Records1.Add("IF NOT EXISTS (SELECT * FROM InteraktUser WHERE InteraktID ='" + InteraktDetails.id + "')" +
                                                            "INSERT INTO InteraktUser ([InteraktID],[Created],[Modified],[Phone],[CountryCode],[UserId],[FullName],[Email],[WpOptedIn]) VALUES " +
                                                            "('" + InteraktDetails.id + "','" +
                                                                Convert.ToDateTime(InteraktDetails.created_at).ToString("yyyy-MM-dd hh:mm:ss") + "','" +
                                                                Convert.ToDateTime(InteraktDetails.modified_at).ToString("yyyy-MM-dd hh:mm:ss") + "','" +
                                                                    InteraktDetails.phone_number + "','" +
                                                                    InteraktDetails.country_code + "','" +
                                                                    InteraktDetails.user_id + "','" +
                                                                    InteraktDetails.name + "','" +
                                                                    InteraktDetails.email + "','" +
                                                                    InteraktDetails.whatsapp_opted_in + "')");

                                        }
                                }
                            }
                            Records1.Reverse();

                            if (Records1.Count > 0)
                            {
                                using (var innerConnection1 = _connections.NewFor<InteraktUserRow>())
                                {
                                    for (int ij = 0; ij < Records1.Count; ij++)
                                    {
                                        try
                                        {
                                            innerConnection1.Execute(Records1[ij]);

                                        }
                                        catch (Exception ex)
                                        {
                                            response.Status = Records1[ij];
                                        }

                                    }
                                }
                            }
                            n = 2;
                    }

                }
            }
                   
                

                response.Status = "Sync Done";
            }
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return response;

        }

        internal class InteraktDetail
        {
            public int custno { get; set; }
            public int offset { get; set; }            
            public string id { get; set; }
            public string created_at { get; set; }
            public string modified_at { get; set; }
            public string phone_number { get; set; }
            public string country_code { get; set; }
            public string user_id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public bool whatsapp_opted_in { get; set; }
           

        }

    }
}
