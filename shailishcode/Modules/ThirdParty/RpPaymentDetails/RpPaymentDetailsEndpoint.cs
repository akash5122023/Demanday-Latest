

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

    using Razorpay.Api;

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
            var UData = new UserRow();

            RazorPayRow Config;

            using (var connection = _connections.NewFor<RazorPayRow>())
            {

                UData = connection.First<UserRow>(q => q
             .SelectTableFields()
             .Select(br.CompanyId)
             .Where(br.UserId == Context.User.GetIdentifier())
            );

                var s = RazorPayRow.Fields;
                Config = connection.TryFirst<RazorPayRow>(q => q
                    .SelectTableFields()
                    .Select(s.AppId)
                    .Select(s.SecretKey)
                    .Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
                    );
            }
            List<string> Records = new List<string>();
            RazorpayClient rpc = new RazorpayClient(Config.AppId, Config.SecretKey);
            //= rpc.Payment.Capture();
            //Dictionary<string, object> options = new Dictionary<string, object>();
            //Payment payment = rpc.Payment.Capture(options);
            //string amt = payment.Attributes["amount"];
            List<Payment> result = new List<Payment>();
            Payment payment = rpc.Payment;
            // This code is for capture the payment 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("count", 50);
            result = rpc.Payment.All(options);
            //Payment paymentCaptured = payment.Capture(options);
            //string amt = paymentCaptured.Attributes["amount"];
            PaymentDetail PayDetails = new PaymentDetail();
            try
            {
                foreach (var PayObject in result)
                {
                    PayDetails.id = Convert.ToString(PayObject.Attributes["id"]);
                    PayDetails.entity = Convert.ToString(PayObject.Attributes["entity"]);
                    PayDetails.amount = Convert.ToString(PayObject.Attributes["amount"]);
                    PayDetails.currency = Convert.ToString(PayObject.Attributes["currency"]);
                    PayDetails.status = Convert.ToString(PayObject.Attributes["status"]);
                    PayDetails.order_id = Convert.ToString(PayObject.Attributes["order_id"]);
                    PayDetails.invoice_id = Convert.ToString(PayObject.Attributes["invoice_id"]);
                    PayDetails.international = Convert.ToString(PayObject.Attributes["international"]);
                    PayDetails.method = Convert.ToString(PayObject.Attributes["method"]);
                    PayDetails.refund_status = Convert.ToString(PayObject.Attributes["refund_status"]);
                    PayDetails.amount_refunded = Convert.ToString(PayObject.Attributes["amount_refunded"]);
                    PayDetails.Captured = Convert.ToString(PayObject.Attributes["Captured"]);
                    PayDetails.description = Convert.ToString(PayObject.Attributes["description"]);
                    PayDetails.card_id = Convert.ToString(PayObject.Attributes["card_id"]);
                    PayDetails.Bank = Convert.ToString(PayObject.Attributes["bank"]);
                    PayDetails.wallet = Convert.ToString(PayObject.Attributes["wallet"]);
                    PayDetails.contact = Convert.ToString(PayObject.Attributes["contact"]);
                    PayDetails.email = Convert.ToString(PayObject.Attributes["email"]);
                    PayDetails.created_at = Convert.ToString(PayObject.Attributes["created_at"]);
                    string formattedDate = string.Empty;
                    string amount = string.Empty;
                    if (PayDetails.created_at != null)
                    {
                        int ts = Convert.ToInt32(PayDetails.created_at);
                        DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(ts).ToLocalTime();
                        formattedDate = dt.ToString("yyyy-MM-dd");
                    }
                    if (PayDetails.amount != null)
                    {
                        decimal dd = Convert.ToDecimal(PayDetails.amount) / 100;
                        amount = Convert.ToString(dd);
                    }



                    Records.Add("IF NOT EXISTS (SELECT * FROM RPPaymentDetails WHERE PaymentId ='" + PayDetails.id + "')" +
                                                  "INSERT INTO RPPaymentDetails ([CompanyId],[PaymentId] ,[Phone],[Email],[Entity],[Amount],[Currency],[Status],[OrderId],[InvoiceId],[international],[Method],[RefundedAmt],[RefundStatus],[captured],[Discription],[CardId],[Bank],[Wallet],[VPA],[IsMoved],[CreatedDate]) VALUES " +
                                                  "('" + UData.CompanyId + "','" + PayDetails.id + "','" + PayDetails.contact + "','" +
                                                      PayDetails.email + "','" + PayDetails.entity + "','" +
                                                       amount + "','" + PayDetails.currency + "','" +
                                                       PayDetails.status + "','" + PayDetails.order_id + "','" + PayDetails.invoice_id + "','" +
                                                       PayDetails.international + "','" + PayDetails.method + "','" + PayDetails.amount_refunded + "','" +
                                                       PayDetails.refund_status + "','" + PayDetails.Captured + "','" + PayDetails.description + "','" + PayDetails.card_id + "','" + PayDetails.Bank + "','" + PayDetails.wallet + "','" + PayDetails.vpa + "','false','" +
                                                       formattedDate + "')");


                    //  Convert.ToDateTime(PayDetails.created_at).ToString("yyyy-MM-dd") + "')");
                    // FacebookDetails.campaign_id = !Fbdata.ContainsKey("campaign_id") ? "" : Convert.ToString(Fbdata["campaign_id"]).Replace("'", "");


                }

                Records.Reverse();
                if (Records.Count > 0)
                {
                    using (var innerConnection = _connections.NewFor<RpPaymentDetailsRow>())
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
