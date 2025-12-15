
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
    //using System.Web.Script.Serialization;
    using Newtonsoft.Json;
    using Nito.AsyncEx;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System.Data;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Mail;
    using Microsoft.AspNetCore.Hosting;
    using Serenity.Extensions.DependencyInjection;
    
    using MyRepository = Repositories.InstamojoDetailsRepository;
    using MyRow = InstamojoDetailsRow;

    [Route("Services/ThirdParty/InstamojoDetails/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class InstamojoDetailsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IWebHostEnvironment _env;

        public InstamojoDetailsController(ISqlConnections connections, IWebHostEnvironment env)
        {
            _connections = connections;
            _env = env;
        }

        public InstamojoDetailsController()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IWebHostEnvironment>())
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

        [HttpPost]
        public StandardResponse Sync(IUnitOfWork uow)
        {
            var response = new StandardResponse();
            InstamojoDetailsRow LastEnquiry;
            InstamojoRow Config;
            var br = UserRow.Fields;
            var UData = new UserRow();
            DateTime StartDate, EndDate;
            using (var connection = _connections.NewFor<InstamojoRow>())
            {
                UData = connection.First<UserRow>(q => q
          .SelectTableFields()
          .Select(br.CompanyId)
          .Where(br.UserId == Context.User.GetIdentifier())
         );

                var s = InstamojoRow.Fields;
                Config = connection.TryFirst<InstamojoRow>(q => q
                    .SelectTableFields()
                    .Select(s.AppId)
                    .Select(s.AccessTokenKey)
                    .Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
                    );

                var i = InstamojoDetailsRow.Fields;
                LastEnquiry = connection.TryFirst<InstamojoDetailsRow>(q => q
                .SelectTableFields()
                .Select(i.PayoutDate)
                .OrderBy(i.PayoutDate, true)
                );
            }

            //var m = new List<dynamic>();
            try
            {
                if (LastEnquiry == null)
                {
                    StartDate = DateTime.Now.AddDays(-360);
                }
                else
                {
                    StartDate = LastEnquiry.PayoutDate.Value;
                }

                EndDate = DateTime.Now;

                string uri = "https://www.instamojo.com/api/1.1/payments/";

                // string uri = "http://mapi.indiamart.com/wservce/enquiry/listing/GLUSR_MOBILE/" + Config.MobileNumber + "/GLUSR_MOBILE_KEY/" + Config.ApiKey + "/Start_Time/" + StartDate.ToString("dd-MMM-yyyy") + "/End_Time/" + EndDate.ToString("dd-MMM-yyyy") + "/";
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                myHttpWebRequest.Headers.Add("x-api-key: " + Config.AppId.ToString().Trim());
                myHttpWebRequest.Headers.Add("X-Auth-Token: " + Config.AccessTokenKey.ToString().Trim());
                //string status = string.Empty;
                // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds. 
                myHttpWebRequest.Timeout = 15000;
                HttpWebResponse myHttpWebResponse;

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream());
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                var InstamojoObjects = JsonConvert.DeserializeObject<dynamic>(reader.ReadToEnd());

                // Dictionary<string, object> result = FacebookObjects["data"][0];payment_requests

                List<string> Records = new List<string>();
                foreach (var InstamojoObject in InstamojoObjects["payments"])
                {
                    InstamojoDetail InstamojoDetails = new InstamojoDetail();
                    InstamojoDetails.id = InstamojoObject.payment_id;
                    InstamojoDetails.Name = InstamojoObject.buyer_name;
                    InstamojoDetails.Email = InstamojoObject.buyer_email;
                    InstamojoDetails.Phone = InstamojoObject.buyer_phone;
                    InstamojoDetails.Amount = InstamojoObject.amount;
                    InstamojoDetails.Currency = InstamojoObject.currency;
                    InstamojoDetails.quantity = InstamojoObject.quantity;
                    InstamojoDetails.unitprice = InstamojoObject.unit_price;
                    InstamojoDetails.type = InstamojoObject.instrument_type;
                    InstamojoDetails.requesturl = InstamojoObject.payment_request;
                    InstamojoDetails.Status = InstamojoObject.status;
                    InstamojoDetails.PaymentMode = InstamojoObject.phone;
                    InstamojoDetails.PayoutDate = InstamojoObject.created_at;

                    if (InstamojoDetails.requesturl != null)
                    {
                        string uri1 = InstamojoDetails.requesturl;

                        // string uri = "http://mapi.indiamart.com/wservce/enquiry/listing/GLUSR_MOBILE/" + Config.MobileNumber + "/GLUSR_MOBILE_KEY/" + Config.ApiKey + "/Start_Time/" + StartDate.ToString("dd-MMM-yyyy") + "/End_Time/" + EndDate.ToString("dd-MMM-yyyy") + "/";
                        HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(uri1);
                        myHttpWebRequest1.ContentType = "application/x-www-form-urlencoded";
                        myHttpWebRequest1.Headers.Add("x-api-key: " + Config.AppId.ToString().Trim());
                        myHttpWebRequest1.Headers.Add("X-Auth-Token: " + Config.AccessTokenKey.ToString().Trim());
                        //string status = string.Empty;
                        // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds. 
                        myHttpWebRequest1.Timeout = 15000;
                        HttpWebResponse myHttpWebResponse1;

                        myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
                        StreamReader reader1 = new StreamReader(myHttpWebResponse1.GetResponseStream());
                        myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
                        var InstamojoObjects1 = JsonConvert.DeserializeObject<dynamic>(reader1.ReadToEnd());

                        InstamojoDetails.Purpose = InstamojoObjects1["payment_request"].purpose;
                    }


                    Records.Add("IF NOT EXISTS (SELECT * FROM InstamojoDetails WHERE InstaID ='" + InstamojoDetails.id + "')" +
                                  "INSERT INTO InstamojoDetails ([CompanyId],[InstaID],[Name],[Phone],[Email],[Purpose],[Status],[PaymentMode],[PayoutDate]) VALUES " +
                                                     "('" + UData.CompanyId + "','" +
                                                         InstamojoDetails.id + "','" +
                                                          InstamojoDetails.Name + "','" +
                                                          InstamojoDetails.Phone + "','" +
                                                         InstamojoDetails.Email + "','" +
                                                          InstamojoDetails.Purpose + "','" +
                                                           InstamojoDetails.Status + "','" +
                                                            InstamojoDetails.PaymentMode + "','" +
                                                          InstamojoDetails.PayoutDate + "')");


                }
                Records.Reverse();
                if (Records.Count > 0)
                {
                    using (var innerConnection = _connections.NewFor<InstamojoDetailsRow>())
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


        [HttpPost]
        public StandardResponse MoveToEnquiry(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();
            EnquiryRow LastEnquiry;
            var Contacttyp = 2;
            var br = UserRow.Fields;
            var UData = new UserRow();
            var model = new MyRow();

            var data = new InstamojoDetailsRow();

            using (var connection = _connections.NewFor<InstamojoDetailsRow>())
            {
                var ind = InstamojoDetailsRow.Fields;
                data = connection.TryById<InstamojoDetailsRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.Name)
                   .Select(ind.Phone)
                   .Select(ind.Email)
                   .Select(ind.PayoutDate)
                   .Select(ind.Status)
                   .Select(ind.Address)


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

                    Contacttyp = 1;

                    string date1 = Convert.ToDateTime(data.PayoutDate).ToString("yyyy-MM-dd HH:mm:ss");
                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,Address,OwnerId,AssignedId,DateCreated,CompanyId) VALUES('" + Contacttyp + "','81','1','" + data.Name + "','" + data.Phone + "','" + data.Email + "','" + data.Address + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + date1 + "','" + UData.CompanyId + "')";

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

                    if (data.Name != LastContact.Name)
                    {
                        response.Status = "Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same";

                        throw new Exception("This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");
                    }
                    // }

                    var s = SourceRow.Fields;
                    var Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where((s.Source == "InstaMojo") || (s.Source == "INSTAMOJO") || (s.Source == "Instamojo") || (s.Source == "Insta mojo"))
                         .Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
                        );

                    if (Source == null)
                    {
                        string str2 = "INSERT INTO Source(Source,CompanyId) VALUES('InstaMojo','" + UData.CompanyId + "')";
                        connection.Execute(str2);

                        Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "InstaMojo")
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
                    string date = Convert.ToDateTime(data.PayoutDate).ToString("yyyy-MM-dd HH:mm:ss");

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + Source.Id + "','" + stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','" + UData.CompanyId + "')";

                    connection.Execute(str1);

                    // var t = EnquiryRow.Fields;
                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                        );

                    connection.Execute("Update InstamojoDetails SET IsMoved = 1 WHERE Id = " + request.Id);


                    var instamojoSettings = new InstamojoRow();

                    var i = InstamojoRow.Fields;
                    instamojoSettings = connection.First<InstamojoRow>(l => l
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

                    if (instamojoSettings.AutoEmail.Value == true && !model.Email.IsNullOrEmpty())
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
                            if (instamojoSettings.Host != null)
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(instamojoSettings.EmailId, instamojoSettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(model.Email);
                                mm.Subject = instamojoSettings.Subject;
                                var msg = instamojoSettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
                                mm.Body = msg;

                                if (instamojoSettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(instamojoSettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            var filePath = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                            mm.Attachments.Add(new Attachment(filePath));
                                        }
                                    }
                                }

                                mm.IsBodyHtml = true;

                                EmailHelper.Send(mm, instamojoSettings.EmailId, instamojoSettings.EmailPassword, (Boolean)instamojoSettings.Ssl, instamojoSettings.Host, instamojoSettings.Port.Value);
                            }
                            else
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(User.EmailId, instamojoSettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(model.Email);
                                mm.Subject = instamojoSettings.Subject;
                                var msg = instamojoSettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
                                mm.Body = msg;

                                if (instamojoSettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(instamojoSettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            var filePath = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                            mm.Attachments.Add(new Attachment(filePath));
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

                    if (instamojoSettings.AutoSms.Value == true && !model.Phone.IsNullOrEmpty())
                    {
                        String msg = instamojoSettings.SmsTemplate;
                        String tempId = instamojoSettings.SmsTemplateId;

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

        [ServiceAuthorize("IndiaMART:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.IndiaMartDetailsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "IndiaMart_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        internal class InstamojoDetail
        {
            public string id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
            public string Amount { get; set; }
            public string Currency { get; set; }
            public string quantity { get; set; }
            public string unitprice { get; set; }
            public string type { get; set; }
            public string requesturl { get; set; }
            public string Purpose { get; set; }
            public string PaymentMode { get; set; }
            public string PayoutDate { get; set; }
            public string Status { get; set; }
        }
    }
}
