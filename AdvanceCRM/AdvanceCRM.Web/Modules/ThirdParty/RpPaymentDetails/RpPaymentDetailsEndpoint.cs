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
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System.Data;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    //using System.Web.Script.Serialization;
    using Newtonsoft.Json;
    using Nito.AsyncEx;
    using Serenity.Reporting;
    using Serenity.Web;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Mail;
    using MyRepository = Repositories.RpPaymentDetailsRepository;
    using MyRow = RpPaymentDetailsRow;
    using Microsoft.AspNetCore.Hosting;
    using Serenity.Abstractions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Caching.Memory;

    [Route("Services/ThirdParty/RpPaymentDetails/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class RpPaymentDetailsController : ServiceEndpoint
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

        public RpPaymentDetailsController(
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

        [ServiceAuthorize("Facebook:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            DynamicDataReport report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.RpPaymentDetailsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "RazorPayPayments_" +
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

            var data = new RpPaymentDetailsRow();

            using (var connection = _connections.NewFor<RpPaymentDetailsRow>())
            {
                var ind = RpPaymentDetailsRow.Fields;
                data = connection.TryById<RpPaymentDetailsRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.Name)
                   .Select(ind.Phone)
                   .Select(ind.Email)
                   .Select(ind.CreatedDate)
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

                    string date1 = Convert.ToDateTime(data.CreatedDate).ToString("yyyy-MM-dd HH:mm:ss");
                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,OwnerId,AssignedId,DateCreated,CompanyId) VALUES('1','81','1','" + data.Name + "','" + data.Phone + "','" + data.Email + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + date1 + "','" + UData.CompanyId + "')";

                    connection.Execute(str);


                    var c = ContactsRow.Fields;
                    var LastContact = connection.First<ContactsRow>(l => l
                        .Select(c.Id)
                        .Select(c.Name)
                        .OrderBy(c.Id, desc: true)
                        );

                    if (data.Name != LastContact.Name)
                    {
                        response.Status = "Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same";

                        throw new Exception("This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");
                    }

                    var s = SourceRow.Fields;
                    var Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where((s.Source == "RazorPay") || (s.Source == "RAZORPAY") || (s.Source == "Razor Pay"))
                         .Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );

                    if (Source == null)
                    {
                        string str2 = "INSERT INTO Source(Source,CompanyId) VALUES('RazorPay','" + UData.CompanyId + "')";
                        connection.Execute(str2);

                        Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "RazorPay")
                        .Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );
                    }

                    var st = StageRow.Fields;
                    var stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(st.Id)
                        .Select(st.Stage)
                        .Where((st.Type == (Int32)Masters.StageTypeMaster.Enquiry))
                         .Where(st.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );

                    if (stageMaster == null)
                    {
                        string str2 = "INSERT INTO Stage(Stage, Type,CompanyId) VALUES('Initial', 1,'" + UData.CompanyId + "')";
                        connection.Execute(str2);

                        stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(st.Id)
                        .Select(st.Stage)
                        .Where(st.Stage == "Initial")
                         .Where(st.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );
                    }

                    GetNextNumberResponse nextNumber = new EnquiryController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(data.CreatedDate).ToString("yyyy-MM-dd HH:mm:ss");

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + Source.Id + "','" + stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','" + UData.CompanyId + "')";

                    connection.Execute(str1);

                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                        );

                    connection.Execute("Update RpPaymentDetails SET IsMoved = 1 WHERE Id = " + request.Id);


                    var FacebookEnquirySettings = new RazorPayRow();

                    var i = RazorPayRow.Fields;
                    FacebookEnquirySettings = connection.First<RazorPayRow>(l => l
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
                             .Select(i.SmsTemplateId)

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
                                msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
                                mm.Body = msg;

                                if (FacebookEnquirySettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(FacebookEnquirySettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                                mm.Attachments.Add(new Attachment(Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString())));
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
                                msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
                                mm.Body = msg;

                                if (FacebookEnquirySettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(FacebookEnquirySettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                                mm.Attachments.Add(new Attachment(Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString())));
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
                        String tempId = FacebookEnquirySettings.SmsTemplateId;

                        msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
                        model.Phone = model.Phone.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                        SMSHelper.SendSMS(model.Phone, msg, tempId);
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
        public StandardResponse Sync(IUnitOfWork uow)
        {
            var response = new StandardResponse();
            var br = UserRow.Fields;
            var uData = new UserRow();
            RazorPayRow config;

            try
            {
                using (var connection = _connections.NewFor<RazorPayRow>())
                {
                    uData = connection.First<UserRow>(q => q
                        .SelectTableFields()
                        .Select(br.CompanyId)
                        .Where(br.UserId == Context.User.GetIdentifier()));

                    var s = RazorPayRow.Fields;
                    config = connection.TryFirst<RazorPayRow>(q => q
                        .SelectTableFields()
                        .Select(s.AppId)
                        .Select(s.SecretKey)
                        .Where(s.CompanyId == Convert.ToInt32(uData.CompanyId)));
                }

                if (config == null || string.IsNullOrWhiteSpace(config.AppId) || string.IsNullOrWhiteSpace(config.SecretKey))
                {
                    response.Status = "Razorpay configuration missing for company.";
                    return response;
                }

                // Call Razorpay Payments API directly (SDK removed)
                string authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{config.AppId}:{config.SecretKey}"));
                using var http = new HttpClient();
                http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authToken);
                var requestUrl = "https://api.razorpay.com/v1/payments?count=50";
                var apiResponse = http.GetAsync(requestUrl).GetAwaiter().GetResult();
                var body = apiResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (!apiResponse.IsSuccessStatusCode)
                {
                    response.Status = $"Razorpay API error {(int)apiResponse.StatusCode}: {apiResponse.ReasonPhrase}";
                    return response;
                }

                var root = JObject.Parse(body);
                var items = root["items"] as JArray ?? new JArray();

                var records = new List<string>();
                foreach (var item in items)
                {
                    var pay = new PaymentDetail
                    {
                        id = Convert.ToString(item["id"]),
                        entity = Convert.ToString(item["entity"]),
                        amount = Convert.ToString(item["amount"]),
                        currency = Convert.ToString(item["currency"]),
                        status = Convert.ToString(item["status"]),
                        order_id = Convert.ToString(item["order_id"]),
                        invoice_id = Convert.ToString(item["invoice_id"]),
                        international = Convert.ToString(item["international"]),
                        method = Convert.ToString(item["method"]),
                        refund_status = Convert.ToString(item["refund_status"]),
                        amount_refunded = Convert.ToString(item["amount_refunded"]),
                        Captured = Convert.ToString(item["captured"]),
                        description = Convert.ToString(item["description"]),
                        card_id = Convert.ToString(item["card_id"]),
                        Bank = Convert.ToString(item["bank"]),
                        wallet = Convert.ToString(item["wallet"]),
                        contact = Convert.ToString(item["contact"]),
                        email = Convert.ToString(item["email"]),
                        created_at = Convert.ToString(item["created_at"]),
                        vpa = Convert.ToString(item["vpa"])
                    };

                    string formattedDate = string.Empty;
                    string amountDisplay = string.Empty;
                    if (!string.IsNullOrWhiteSpace(pay.created_at) && int.TryParse(pay.created_at, out var ts))
                    {
                        var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(ts).ToLocalTime();
                        formattedDate = dt.ToString("yyyy-MM-dd");
                    }
                    if (!string.IsNullOrWhiteSpace(pay.amount) && decimal.TryParse(pay.amount, out var amountMinor))
                    {
                        amountDisplay = (amountMinor / 100m).ToString();
                    }

                    records.Add("IF NOT EXISTS (SELECT * FROM RPPaymentDetails WHERE PaymentId ='" + pay.id + "')" +
                        "INSERT INTO RPPaymentDetails ([CompanyId],[PaymentId] ,[Phone],[Email],[Entity],[Amount],[Currency],[Status],[OrderId],[InvoiceId],[international],[Method],[RefundedAmt],[RefundStatus],[captured],[Discription],[CardId],[Bank],[Wallet],[VPA],[IsMoved],[CreatedDate]) VALUES " +
                        "('" + uData.CompanyId + "','" + pay.id + "','" + pay.contact + "','" +
                        pay.email + "','" + pay.entity + "','" + amountDisplay + "','" + pay.currency + "','" +
                        pay.status + "','" + pay.order_id + "','" + pay.invoice_id + "','" + pay.international + "','" + pay.method + "','" +
                        pay.amount_refunded + "','" + pay.refund_status + "','" + pay.Captured + "','" + pay.description + "','" + pay.card_id + "','" +
                        pay.Bank + "','" + pay.wallet + "','" + pay.vpa + "','false','" + formattedDate + "')");
                }

                records.Reverse();
                if (records.Count > 0)
                {
                    using var innerConnection = _connections.NewFor<RpPaymentDetailsRow>();
                    foreach (var sql in records)
                    {
                        try { innerConnection.Execute(sql); } catch { }
                    }
                }

                response.Status = "Sync Done";
            }
            catch (Exception ex)
            {
                response.Status = "Error\n" + ex.Message;
            }

            return response;
        }

        internal class PaymentDetail
        {

            public string id { get; set; }
            public string entity { get; set; }
            public string currency { get; set; }
            public string amount { get; set; }
            public string order_id { get; set; }
            public string status { get; set; }
            public string invoice_id { get; set; }
            public string international { get; set; }
            public string method { get; set; }
            public string card_id { get; set; }
            public string amount_refunded { get; set; }
            public string refund_status { get; set; }
            public string Captured { get; set; }
            public string description { get; set; }
            public string Bank { get; set; }
            public string wallet { get; set; }

            public string vpa { get; set; }
            public string email { get; set; }
            public string contact { get; set; }
            public string created_at { get; set; }




        }

    }
}
