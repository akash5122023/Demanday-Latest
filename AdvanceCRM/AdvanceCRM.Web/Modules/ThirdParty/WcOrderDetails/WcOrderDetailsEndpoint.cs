
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
    using Microsoft.AspNetCore.Hosting;
    using Serenity.Extensions.DependencyInjection;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System.Data;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Mail;
    
    using MyRepository = Repositories.WcOrderDetailsRepository;
    using MyRow = WcOrderDetailsRow;

    [Route("Services/ThirdParty/WcOrderDetails/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class WcOrderDetailsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IWebHostEnvironment _env;

        public WcOrderDetailsController(ISqlConnections connections, IWebHostEnvironment env)
        {
            _connections = connections;
            _env = env;
        }

        public WcOrderDetailsController()
            : this(Dependency.Resolve<ISqlConnections>(),
                   Dependency.Resolve<IWebHostEnvironment>())
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
            return new MyRepository(Context).List(connection, request);        }

       
        [ServiceAuthorize("Woocommerce:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            DynamicDataReport report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.WcOrderDetailsColumns));
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

            var data = new WcOrderDetailsRow();

            using (var connection = _connections.NewFor<WcOrderDetailsRow>())
            {
                var ind = WcOrderDetailsRow.Fields;
                data = connection.TryById<WcOrderDetailsRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.BFirstName)
                   .Select(ind.BLastName)
                   .Select(ind.BPhone)
                   .Select(ind.BEmail)
                   .Select(ind.DateCreated)
                   .Select(ind.BAddress1)
                   .Select(ind.BState)
                   .Select(ind.BCity)
                   .Select(ind.BAddress2)
                   .Select(ind.BCompany)
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
                    if (!(string.IsNullOrEmpty)(data.BCompany))
                    {
                        Contacttyp = 2;
                    }
                    else
                    {
                        Contacttyp = 1;
                    }
                    string date1 = Convert.ToDateTime(data.DateCreated).ToString("yyyy-MM-dd HH:mm:ss");
                    string name = data.BFirstName + " " + data.BLastName;
                    string additionnal = data.BCity + " State :" + data.BState + " Country=" + data.BCountry;
                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,Address,AdditionalInfo,OwnerId,AssignedId,DateCreated) VALUES('" + Contacttyp + "','81','1','" + name + "','" + data.BPhone + "','" + data.BEmail + "','" + data.BAddress1 + "','" + additionnal + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + date1 + "')";

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
                    string date = Convert.ToDateTime(data.DateCreated).ToString("yyyy-MM-dd HH:mm:ss");

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + Source.Id + "','" + stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','" + UData.CompanyId + "')";

                    connection.Execute(str1);

                    // var t = EnquiryRow.Fields;
                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                        );

                    connection.Execute("Update WcOrderDetails SET IsMoved = 1 WHERE Id = " + request.Id);


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

                    if (FacebookEnquirySettings.AutoEmail.Value == true && !model.BEmail.IsNullOrEmpty())
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
                                mm.To.Add(model.BEmail);
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
                                            var filePath = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                            mm.Attachments.Add(new Attachment(filePath));
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
                                mm.To.Add(model.BEmail);
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

                    if (FacebookEnquirySettings.AutoSms.Value == true && !model.BPhone.IsNullOrEmpty())
                    {
                        String msg = FacebookEnquirySettings.SmsTemplate;
                        String TempId = FacebookEnquirySettings.TemplateId;
                        msg = msg.Replace("#customername", name.IsNullOrEmpty() ? "Customer" : name);
                        model.BPhone = model.BPhone.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                        SMSHelper.SendSMS(model.BPhone, msg, TempId);
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
            // var model = new MyRow();

            List<WcOrderDetailsRow> data1;
            var ind1 = WcOrderDetailsRow.Fields;

            using (var connection = _connections.NewFor<WcOrderDetailsRow>())
            {
               
                data1 = connection.List<WcOrderDetailsRow>( q => q
                   .SelectTableFields()
                   .Select(ind1.BFirstName)
                   .Select(ind1.BLastName)
                   .Select(ind1.BPhone)
                   .Select(ind1.BEmail)
                   .Select(ind1.DateCreated)
                   .Select(ind1.BAddress1)
                   .Select(ind1.BState)
                   .Select(ind1.BCity)
                   .Select(ind1.BAddress2)
                   .Select(ind1.BCompany)
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
                    if (!(string.IsNullOrEmpty)(item.BCompany))
                    {
                        Contacttyp = 2;
                    }
                    else
                    {
                        Contacttyp = 1;
                    }
                    string date1 = Convert.ToDateTime(item.DateCreated).ToString("yyyy-MM-dd HH:mm:ss");
                    string name = item.BFirstName + " " + item.BLastName;
                    string additionnal = item.BCity + " State :" + item.BState + " Country=" + item.BCountry;
                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,Address,AdditionalInfo,OwnerId,AssignedId,DateCreated) VALUES('" + Contacttyp + "','81','1','" + name + "','" + item.BPhone + "','" + item.BEmail + "','" + item.BAddress1 + "','" + additionnal + "','" + UserId + "','" +UserId + "','" + date1 + "')";

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
                    string date = Convert.ToDateTime(item.DateCreated).ToString("yyyy-MM-dd HH:mm:ss");

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + Source.Id + "','" + stageMaster.Id + "','" + UserId + "','" + UserId + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','1')";

                    connection.Execute(str1);

                    // var t = EnquiryRow.Fields;
                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                        );

                  //  connection.Execute("Update WcOrderDetails SET IsMoved = 1 WHERE Id = " + request.Id);


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

                    if (FacebookEnquirySettings.AutoEmail.Value == true && !item.BEmail.IsNullOrEmpty())
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
                                mm.To.Add(item.BEmail);
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
                                            var filePath = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                            mm.Attachments.Add(new Attachment(filePath));
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
                                mm.To.Add(item.BEmail);
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

                    if (FacebookEnquirySettings.AutoSms.Value == true && !item.BPhone.IsNullOrEmpty())
                    {
                        String msg = FacebookEnquirySettings.SmsTemplate;
                        msg = msg.Replace("#customername", name.IsNullOrEmpty() ? "Customer" : name);
                        item.BPhone = item.BPhone.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                        SMSHelper.SendSMS(item.BPhone, msg,msg);
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

            string uri = Config.SiteUrl + "/wp-json/wc/v3/orders?consumer_key=" + Config.ConsumerKey + "&consumer_secret=" + Config.ConsumerSecret; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";
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

                List<string> Records = new List<string>();
                foreach (var WooCommerceObject in WooCommerceObjects)
                {
                    WooCommerceDetail WooCommerceDetails = new WooCommerceDetail();
                    WooCommerceDetails.Wcoid = WooCommerceObject.id;
                    WooCommerceDetails.ParentId = WooCommerceObject.parent_id;
                    WooCommerceDetails.Status = WooCommerceObject.status;
                    WooCommerceDetails.Currency = WooCommerceObject.currency;
                    WooCommerceDetails.IncludedTax = WooCommerceObject.prices_include_tax;
                    WooCommerceDetails.DateCreated = WooCommerceObject.date_created;
                    WooCommerceDetails.DateModified = WooCommerceObject.date_modified;
                    WooCommerceDetails.DiscountTotal = WooCommerceObject.discount_total;
                    WooCommerceDetails.DiscountTax = WooCommerceObject.discount_tax;
                    WooCommerceDetails.ShippingTotal = WooCommerceObject.shipping_total;
                    WooCommerceDetails.ShipppingTax = WooCommerceObject.shipping_tax;
                    WooCommerceDetails.CartTax = WooCommerceObject.cart_tax;
                    WooCommerceDetails.Total = WooCommerceObject.total;
                    WooCommerceDetails.TotalTax = WooCommerceObject.total_tax;
                    WooCommerceDetails.CustomerId = WooCommerceObject.customer_id;
                    WooCommerceDetails.OrderKey = WooCommerceObject.order_key;
                    WooCommerceDetails.BFirstName = WooCommerceObject["billing"]["first_name"];
                    WooCommerceDetails.BLastName = WooCommerceObject["billing"]["last_name"] ;
                    WooCommerceDetails.BCompany = WooCommerceObject["billing"]["company"] ;
                    WooCommerceDetails.BAddress1 = WooCommerceObject["billing"]["address_1"] ;
                    WooCommerceDetails.BAddress2 = WooCommerceObject["billing"]["address_2"] ;
                    WooCommerceDetails.BCity = WooCommerceObject["billing"]["city"]; 
                    WooCommerceDetails.BState = WooCommerceObject["billing"]["state"]; 
                    WooCommerceDetails.BPostCode = WooCommerceObject["billing"]["postcode"]; 
                    WooCommerceDetails.BCountry = WooCommerceObject["billing"]["country"]; 
                    WooCommerceDetails.BEmail = WooCommerceObject["billing"]["email"]; 
                    WooCommerceDetails.BPhone = WooCommerceObject["billing"]["phone"]; 
                    WooCommerceDetails.SFirstName = WooCommerceObject["shipping"]["first_name"];
                    WooCommerceDetails.SLastName = WooCommerceObject["shipping"]["last_name"];
                    WooCommerceDetails.SCompany = WooCommerceObject["shipping"]["company"];
                    WooCommerceDetails.SAddress1 = WooCommerceObject["shipping"]["address_1"];
                    WooCommerceDetails.SAddress2 = WooCommerceObject["shipping"]["address_2"];
                    WooCommerceDetails.SCity = WooCommerceObject["shipping"]["city"];
                    WooCommerceDetails.SPostCode = WooCommerceObject["shipping"]["postcode"];
                    WooCommerceDetails.SCountry = WooCommerceObject["shipping"]["country"];
                    WooCommerceDetails.SState = WooCommerceObject["shipping"]["state"];
                    WooCommerceDetails.SPhone = WooCommerceObject["shipping"]["phone"];
                    WooCommerceDetails.PaymentMethod = WooCommerceObject.payment_method_title;
                    WooCommerceDetails.TransactionId = WooCommerceObject.transaction_id;
                    WooCommerceDetails.CustomerIp = WooCommerceObject.customer_ip_address;
                    WooCommerceDetails.ItemId = WooCommerceObject["line_items"][0].id;
                    WooCommerceDetails.ItemName = WooCommerceObject["line_items"][0].name;
                    WooCommerceDetails.ProductId = WooCommerceObject["line_items"][0].product_id;
                    WooCommerceDetails.VariationId = WooCommerceObject["line_items"][0].variation_id;
                    WooCommerceDetails.Quantity = WooCommerceObject["line_items"][0].quantity;
                    WooCommerceDetails.TaxClass = WooCommerceObject["line_items"][0].tax_class;
                    WooCommerceDetails.SubTotal = WooCommerceObject["line_items"][0].subtotal;
                    WooCommerceDetails.SubTotaltax = WooCommerceObject["line_items"][0].subtotal_tax;
                    WooCommerceDetails.ItemTotal = WooCommerceObject["line_items"][0].total;
                    WooCommerceDetails.ItemTotaltax = WooCommerceObject["line_items"][0].total_tax;
                    if (WooCommerceObject["tax_lines"] == null)
                    {
                        WooCommerceDetails.TaxRateCode = WooCommerceObject["tax_lines"][0].rate_code;
                        WooCommerceDetails.TaxRateId = WooCommerceObject["tax_lines"][0].rate_id;
                        WooCommerceDetails.TaxLabel = WooCommerceObject["tax_lines"][0].label;
                        WooCommerceDetails.TaxCompound = WooCommerceObject["tax_lines"][0].compound;
                        WooCommerceDetails.TaxTotal = WooCommerceObject["tax_lines"][0].tax_total;
                        WooCommerceDetails.TaxShippingTotal = WooCommerceObject["tax_lines"][0].shipping_tax_total;
                        WooCommerceDetails.TaxRatePercent = WooCommerceObject["tax_lines"][0].rate_percent;
                    }
                   //56 Records

                    Records.Add("IF NOT EXISTS (SELECT * FROM [WCOrderDetails] WHERE WCOID ='" + WooCommerceDetails.Wcoid + "')" +
                        "INSERT INTO [WCOrderDetails] ([WCOID],[ParentID],[Status],[Currency],[IncludedTax],[DateCreated],[DateModified],[DiscountTotal],[DiscountTax]" + 
                        ",[ShippingTotal],[ShipppingTax],[CartTax],[Total],[TotalTax],[CustomerID],[OrderKey],[BFirstName],[BLastName],[BCompany],[BEmail],[BPhone]" +
                        ",[BPostCode],[BAddress1],[BAddress2],[BCity],[BState],[BCountry],[SFirstName],[SLastName],[SCompany],[SPhone],[SPostCode],[SAddress1],[SAddress2]" +
                        ",[SCity],[SState],[SCountry],[PaymentMethod],[TransactionID],[CustomerIP],[ItemId],[ItemName],[ProductId],[VariationId],[Quantity],[TaxClass]" +
                        ",[SubTotal],[SubTotaltax],[ItemTotal],[ItemTotaltax],[TaxRateCode],[TaxRateId],[TaxLabel],[TaxCompound],[TaxTotal],[TaxShippingTotal],[TaxRatePercent]) VALUES " +
                       "('" + WooCommerceDetails.Wcoid + "','" + WooCommerceDetails.ParentId + "','" + WooCommerceDetails.Status + "','" + WooCommerceDetails.Currency + "','" +
                        WooCommerceDetails.IncludedTax + "','" + WooCommerceDetails.DateCreated + "','" + WooCommerceDetails.DateModified + "','" + WooCommerceDetails.DiscountTotal + "','" +
                        WooCommerceDetails.DiscountTax + "','" + WooCommerceDetails.ShippingTotal + "','" + WooCommerceDetails.ShipppingTax + "','" + WooCommerceDetails.CartTax + "','" +
                        WooCommerceDetails.Total + "','" + WooCommerceDetails.TotalTax + "','" + WooCommerceDetails.CustomerId + "','" + WooCommerceDetails.OrderKey + "','" +
                        WooCommerceDetails.BFirstName + "','" + WooCommerceDetails.BLastName + "','" + WooCommerceDetails.BCompany + "','" + WooCommerceDetails.BEmail + "','" +
                        WooCommerceDetails.BPhone + "','" + WooCommerceDetails.BPostCode + "','" + WooCommerceDetails.BAddress1 + "','" + WooCommerceDetails.BAddress2 + "','" +
                        WooCommerceDetails.BCity + "','" + WooCommerceDetails.BState + "','" + WooCommerceDetails.BCountry + "','" + WooCommerceDetails.SFirstName + "','" +
                        WooCommerceDetails.SLastName + "','" + WooCommerceDetails.SCompany + "','" + WooCommerceDetails.SPhone + "','" + WooCommerceDetails.SPostCode + "','" +
                        WooCommerceDetails.SAddress1 + "','" + WooCommerceDetails.BAddress2 + "','" + WooCommerceDetails.SCity + "','" + WooCommerceDetails.SState + "','" +
                        WooCommerceDetails.SCountry + "','" + WooCommerceDetails.PaymentMethod + "','" + WooCommerceDetails.TransactionId + "','" + WooCommerceDetails.CustomerIp + "','" +
                        WooCommerceDetails.ItemId + "','" + WooCommerceDetails.ItemName + "','" + WooCommerceDetails.ProductId + "','" + WooCommerceDetails.VariationId + "','" +
                        WooCommerceDetails.Quantity + "','" + WooCommerceDetails.TaxClass + "','" + WooCommerceDetails.SubTotal + "','" + WooCommerceDetails.SubTotaltax + "','" +
                        WooCommerceDetails.ItemTotal + "','" + WooCommerceDetails.ItemTotaltax + "','" + WooCommerceDetails.TaxRateCode + "','" + WooCommerceDetails.TaxRateId + "','" +
                        WooCommerceDetails.TaxLabel + "','" + WooCommerceDetails.TaxCompound + "','" + WooCommerceDetails.TaxTotal + "','" + WooCommerceDetails.TaxShippingTotal + "','" +
                        WooCommerceDetails.TaxRatePercent + "')");
                    using (var innerConnection = _connections.NewFor<WcOrderDetailsRow>())
                    {
                        for (int ij = 0; ij < Records.Count; ij++)
                        {
                            try
                            {
                                innerConnection.Execute(Records[ij]);
                                Records.Clear();
                            }
                            catch (Exception ex)
                            {
                            }

                        }
                    }
                }
                //Records.Reverse();
                //if (Records.Count > 0)
                //{
                //    using (var innerConnection = _connections.NewFor<WcOrderDetailsRow>())
                //    {
                //        for (int ij = 0; ij < Records.Count; ij++)
                //        {
                //            try
                //            {
                //                innerConnection.Execute(Records[ij]);
                //            }
                //            catch (Exception ex)
                //            {
                //            }

                //        }
                //    }
                //}
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
            public string Wcoid { get; set; }
            public string ParentId { get; set; }
            public string Status { get; set; }
            public string Currency { get; set; }
            public string IncludedTax { get; set; }
            public string DateCreated { get; set; }
            public string DateModified { get; set; }
            public string DiscountTotal { get; set; }
            public string DiscountTax { get; set; }
            public string ShippingTotal { get; set; }
            public string ShipppingTax { get; set; }
            public string CartTax { get; set; }
            public string Total { get; set; }
            public string TotalTax { get; set; }
            public string CustomerId { get; set; }
            public string OrderKey { get; set; }
            public string BFirstName { get; set; }
            public string BLastName { get; set; }
            public string BCompany { get; set; }
            public string BEmail { get; set; }
            public string BPhone { get; set; }
            public string BPostCode { get; set; }
            public string BAddress1 { get; set; }
            public string BAddress2 { get; set; }
            public string BCity { get; set; }
            public string BState { get; set; }
            public string BCountry { get; set; }
            public string SFirstName { get; set; }
            public string SLastName { get; set; }
            public string SCompany { get; set; }
            public string SPhone { get; set; }
            public string SPostCode { get; set; }
            public string SAddress1 { get; set; }
            public string SAddress2 { get; set; }
            public string SCity { get; set; }
            public string SState { get; set; }
            public string SCountry { get; set; }
            public string PaymentMethod { get; set; }
            public string TransactionId { get; set; }
            public string CustomerIp { get; set; }
            public string ItemId { get; set; }
            public string ItemName { get; set; }
            public string ProductId { get; set; }
            public string VariationId { get; set; }
            public string Quantity { get; set; }
            public string TaxClass { get; set; }
            public string SubTotal { get; set; }
            public string SubTotaltax { get; set; }
            public string ItemTotal { get; set; }
            public string ItemTotaltax { get; set; }
            //
            public string TaxRateCode { get; set; }
            public string TaxRateId { get; set; }
            public string TaxLabel { get; set; }
            public string TaxCompound { get; set; }
            public string TaxTotal { get; set; }
            public string TaxShippingTotal { get; set; }
            public string TaxRatePercent { get; set; }

        }
    }
}
