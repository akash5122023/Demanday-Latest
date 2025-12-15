
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
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System.Data;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Mail;
    using Microsoft.AspNetCore.Hosting;
    
    using MyRepository = Repositories.WoocommerceDetailsRepository;
    using MyRow = WoocommerceDetailsRow;

    [Route("Services/ThirdParty/WoocommerceDetails/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class WoocommerceDetailsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext Context;
        private readonly IWebHostEnvironment _env;


        public WoocommerceDetailsController(

            ISqlConnections connections,
            IRequestContext context,
            IWebHostEnvironment env
          )
        {
            this._connections = connections;
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
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
        [ServiceAuthorize("Woocommerce:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            DynamicDataReport report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.WoocommerceDetailsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "WooCommerceLeads_" +
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

            var data = new WoocommerceDetailsRow();

            using (var connection = _connections.NewFor<WoocommerceDetailsRow>())
            {
                var ind = WoocommerceDetailsRow.Fields;
                data = connection.TryById<WoocommerceDetailsRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.FirstName)
                   .Select(ind.LastName)
                   .Select(ind.Phone)
                   .Select(ind.Email)
                   .Select(ind.CreatedDate)
                   .Select(ind.Address)
                   .Select(ind.State)
                   .Select(ind.City)                   
                   .Select(ind.Address)
                   .Select(ind.Company)        
                   .Select(ind.Feedback)
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
                    if (!(string.IsNullOrEmpty)(data.Company))
                    {
                        Contacttyp = 2;
                    }
                    else
                    {
                        Contacttyp = 1;
                    }
                    string date1 = Convert.ToDateTime(data.CreatedDate).ToString("yyyy-MM-dd HH:mm:ss");
                    string name = data.FirstName + " " + data.LastName;
                    string additionnal =data.City+ " State :"+data.State+" Country=" + data.Country;
                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,Address,AdditionalInfo,OwnerId,AssignedId,DateCreated) VALUES('" + Contacttyp + "','81','1','" + name + "','" + data.Phone + "','" + data.Email + "','" + data.Address + "','"+additionnal+"','" + Context.User.GetIdentifier()+ "','" + date1 + "')";

                    connection.Execute(str);

                    //if (LastContact == null)
                    //{
                    //    string str = "INSERT INTO Contacts(ContactType,CustomerType,Name,Phone,Email,Address,OwnerId,AssignedId) VALUES('1','1'," + "'" + model.Name + "','" + model.Phone + "','" + model.Email + "','','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "')";

                    //    connection.Execute(str);
                        var c = ContactsRow.Fields;
                    var LastContact = connection.First<ContactsRow>(l => l
                        .Select(c.Id)
                        .Select(c.Name)
                        .OrderBy(c.Id, desc: true)
                        );

                    if (name != LastContact.Name)
                    {
                        response.Status = "Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same";

                        throw new Exception("This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");
                    }
                    // }

                    var s = SourceRow.Fields;
                    var Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where((s.Source == "WOO") || (s.Source == "WC") || (s.Source == "WooCommerce"))
                        );

                    if (Source == null)
                    {
                        string str2 = "INSERT INTO Source(Source) VALUES('WooCommerce')";
                        connection.Execute(str2);

                        Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "WooCommerce")
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
                        .Where(s.Source == "WooCommerce")
                        );
                    }

                    GetNextNumberResponse nextNumber = new EnquiryController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(data.CreatedDate).ToString("yyyy-MM-dd HH:mm:ss");

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId,AdditionalInfo) VALUES('" + LastContact.Id + "','" + date + "','1','" + Source.Id + "','" + stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','" + UData.CompanyId + "','"+data.Feedback+"')";

                    connection.Execute(str1);

                    // var t = EnquiryRow.Fields;
                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                        );

                    connection.Execute("Update WoocommerceDetails SET IsMoved = 1 WHERE Id = " + request.Id);


                    var FacebookEnquirySettings = new WoocommerceRow();

                    var i = WoocommerceRow.Fields;
                    FacebookEnquirySettings = connection.First<WoocommerceRow>(l => l
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
                                msg = msg.Replace("#customername", name.IsNullOrEmpty() ? "Customer" : name);
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
                                msg = msg.Replace("#customername", name.IsNullOrEmpty() ? "Customer" : name);
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
                        String tempId = FacebookEnquirySettings.TemplateId;
                        msg = msg.Replace("#customername", name.IsNullOrEmpty() ? "Customer" : name);
                        model.Phone = model.Phone.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                        SMSHelper.SendSMS(model.Phone, msg,tempId);
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

            List<WoocommerceDetailsRow> data1;
            var ind1 = WoocommerceDetailsRow.Fields;

            using (var connection = _connections.NewFor<WoocommerceDetailsRow>())
            {
               // var ind = WoocommerceDetailsRow.Fields;
                data1 = connection.List<WoocommerceDetailsRow>( q => q
                   .SelectTableFields()
                   .Select(ind1.FirstName)
                   .Select(ind1.LastName)
                   .Select(ind1.Phone)
                   .Select(ind1.Email)
                   .Select(ind1.CreatedDate)
                   .Select(ind1.Address)
                   .Select(ind1.State)
                   .Select(ind1.City)
                   .Select(ind1.Address)
                   .Select(ind1.Company)
                   .Select(ind1.Feedback)
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
                    if (!(string.IsNullOrEmpty)(item.Company))
                    {
                        Contacttyp = 2;
                    }
                    else
                    {
                        Contacttyp = 1;
                    }
                    string date1 = Convert.ToDateTime(item.CreatedDate).ToString("yyyy-MM-dd HH:mm:ss");
                    string name = item.FirstName + " " + item.LastName;
                    string additionnal = item.City + " State :" + item.State + " Country=" + item.Country;
                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,Address,AdditionalInfo,OwnerId,AssignedId,DateCreated) VALUES('" + Contacttyp + "','81','1','" + name + "','" + item.Phone + "','" + item.Email + "','" + item.Address + "','" + additionnal + "','" + UserId + "','" + UserId + "','" + date1 + "')";

                    connection.Execute(str);

                    //if (LastContact == null)
                    //{
                    //    string str = "INSERT INTO Contacts(ContactType,CustomerType,Name,Phone,Email,Address,OwnerId,AssignedId) VALUES('1','1'," + "'" + model.Name + "','" + model.Phone + "','" + model.Email + "','','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "')";

                    //    connection.Execute(str);
                    var c = ContactsRow.Fields;
                    var LastContact = connection.First<ContactsRow>(l => l
                        .Select(c.Id)
                        .Select(c.Name)
                        .OrderBy(c.Id, desc: true)
                        );

                    if (name != LastContact.Name)
                    {
                        response.Status = "Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same";

                        throw new Exception("This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");
                    }
                    // }

                    var s = SourceRow.Fields;
                    var Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where((s.Source == "WOO") || (s.Source == "WC") || (s.Source == "WooCommerce"))
                        );

                    if (Source == null)
                    {
                        string str2 = "INSERT INTO Source(Source) VALUES('WooCommerce')";
                        connection.Execute(str2);

                        Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "WooCommerce")
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
                        .Where(s.Source == "WooCommerce")
                        );
                    }

                    GetNextNumberResponse nextNumber = new EnquiryController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(item.CreatedDate).ToString("yyyy-MM-dd HH:mm:ss");

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId,AdditionalInfo) VALUES('" + LastContact.Id + "','" + date + "','1','" + Source.Id + "','" + stageMaster.Id + "','" + UserId + "','" +UserId + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','1','" + item.Feedback + "')";

                    connection.Execute(str1);

                    // var t = EnquiryRow.Fields;
                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                        );

                   // connection.Execute("Update WoocommerceDetails SET IsMoved = 1 WHERE Id = " + request.Id);


                    var FacebookEnquirySettings = new WoocommerceRow();

                    var i = WoocommerceRow.Fields;
                    FacebookEnquirySettings = connection.First<WoocommerceRow>(l => l
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
                                msg = msg.Replace("#customername", name.IsNullOrEmpty() ? "Customer" : name);
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
                                msg = msg.Replace("#customername", name.IsNullOrEmpty() ? "Customer" : name);
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
                        String tempId = "XXXXXXX";
                        msg = msg.Replace("#customername", name.IsNullOrEmpty() ? "Customer" : name);
                        item.Phone = item.Phone.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                        SMSHelper.SendSMS(item.Phone, msg,tempId);
                    }
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

        [HttpPost]
        public StandardResponse Sync(IUnitOfWork uow)
        {
            var response = new StandardResponse();

            WoocommerceRow Config;

            using (var connection = _connections.NewFor<WoocommerceRow>())
            {
                var s = WoocommerceRow.Fields;
                Config = connection.TryFirst<WoocommerceRow>(q => q
                    .SelectTableFields()
                    .Select(s.SiteUrl)
                    .Select(s.ConsumerKey)
                     .Select(s.ConsumerSecret)
                    );
            }
            
            string uri = Config.SiteUrl+ "/wp-json/wc/v3/customers?consumer_key=" + Config.ConsumerKey + "&consumer_secret=" + Config.ConsumerSecret; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                myHttpWebRequest.ContentType = "application/json";

                myHttpWebRequest.Timeout = 15000;
                HttpWebResponse myHttpWebResponse;
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream());
                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();


                var WooCommerceObjects = JsonConvert.DeserializeObject<dynamic>(reader.ReadToEnd());

                // Dictionary<string, object> result = FacebookObjects["data"][0];

                List<string> Records = new List<string>();
                foreach (var WooCommerceObject in WooCommerceObjects)
                {
                    WooCommerceDetail WooCommerceDetails = new WooCommerceDetail();
                    WooCommerceDetails.id = WooCommerceObject.id;
                    WooCommerceDetails.first_name = WooCommerceObject.first_name;
                    WooCommerceDetails.last_name = WooCommerceObject.last_name;
                    WooCommerceDetails.email = WooCommerceObject.email;
                    WooCommerceDetails.date_created = WooCommerceObject.date_created;
                    WooCommerceDetails.city = WooCommerceObject["billing"]["city"];
                    WooCommerceDetails.state = WooCommerceObject["billing"]["state"];
                    WooCommerceDetails.postcode = WooCommerceObject["billing"]["postcode"];
                    WooCommerceDetails.address_1 = WooCommerceObject["billing"]["address_1"];
                    WooCommerceDetails.address_2 = WooCommerceObject["billing"]["address_2"];
                    WooCommerceDetails.country = WooCommerceObject["billing"]["country"];
                    WooCommerceDetails.phone = WooCommerceObject["billing"]["phone"];
                    //
                    Records.Add("IF NOT EXISTS (SELECT * FROM WoocommerceDetails WHERE WooId ='" + WooCommerceDetails.id + "')" + 
                        "INSERT INTO WoocommerceDetails ([WooId],[FirstName],[LastName],[Company],[Email],[Phone],[Address],[City],[State],[Country],[CreatedDate]) VALUES " +
                                  "('" + WooCommerceDetails.id + "','" +
                                    WooCommerceDetails.first_name + "','" +
                                      WooCommerceDetails.last_name + "','" +
                                       WooCommerceDetails.company + "','" +
                                       WooCommerceDetails.email + "','" +
                                       WooCommerceDetails.phone + "','" +
                                       WooCommerceDetails.address_1 + "','" +
                                       WooCommerceDetails.city + "','" +
                                       WooCommerceDetails.state + "','" +
                                       WooCommerceDetails.country + "','" +
                                       WooCommerceDetails.date_created + "')");
                 }
                Records.Reverse();
                if (Records.Count > 0)
                {
                    using (var innerConnection = _connections.NewFor<WoocommerceDetailsRow>())
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

        internal class WooCommerceDetail
        {
            public string id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string role { get; set; }
            public string email { get; set; }
            public string city { get; set; }
            public string postcode { get; set; }
            public string country { get; set; }
            public string state { get; set; }
            public string date_created { get; set; }
            public string phone { get; set; }
            public string address_1 { get; set; }
            public string company { get; set; }
            public string address_2 { get; set; }
           




        }
    }
}
