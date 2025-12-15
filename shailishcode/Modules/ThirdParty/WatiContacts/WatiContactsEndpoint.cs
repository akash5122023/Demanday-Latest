
namespace AdvanceCRM.ThirdParty.Endpoints
{
    using AdvanceCRM.Settings;
    using AdvanceCRM.ThirdParty;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Enquiry.Endpoints;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
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
    using Microsoft.Extensions.DependencyInjection;
    using Serenity.Abstractions;
    using System.Data;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Mail;
    
    
    using MyRepository = Repositories.WatiContactsRepository;
    using MyRow = WatiContactsRow;

    [Route("Services/ThirdParty/WatiContacts/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class WatiContactsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public WatiContactsController(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
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

        [ServiceAuthorize("WatiContacts:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            DynamicDataReport report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.WatiContactsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "WatiContacts_" +
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

            var data = new WatiContactsRow();

            using (var connection = _connections.NewFor<WatiContactsRow>())
            {
                var ind = WatiContactsRow.Fields;
                data = connection.TryById<WatiContactsRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.Waid)
                   .Select(ind.FirtName)
                   .Select(ind.FullName)
                   .Select(ind.Phone)
                   .Select(ind.Status)
                   .Select(ind.Source)
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
                    //else
                    //{
                        Contacttyp = 1;
                    //}
                    string date1 = Convert.ToDateTime(data.Created).ToString("yyyy-MM-dd HH:mm:ss");
                    string name = data.FirtName;
                    string additionnal = " State :" + data.Waid;// " State :" + data.State + " Country=" + data.Country;
                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,AdditionalInfo,OwnerId,AssignedId,DateCreated) VALUES('" + Contacttyp + "','81','1','" + name + "','" + data.Phone + "','" + additionnal + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + date1 + "')";

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
                        .Where((s.Source == "Wati") || (s.Source == "WATI") || (s.Source == "WaTi"))
                        );

                    if (Source == null)
                    {
                        string str2 = "INSERT INTO Source(Source) VALUES('WATI')";
                        connection.Execute(str2);

                        Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "WATI")
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
                        string str2 = "INSERT INTO Stage(Stage, Type) VALUES('WATI', 1)";
                        connection.Execute(str2);

                        stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "WATI")
                        );
                    }

                    var enquiryController = HttpContext.RequestServices.GetRequiredService<Enquiry.Endpoints.EnquiryController>();
                    GetNextNumberResponse nextNumber = enquiryController.GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(data.Created).ToString("yyyy-MM-dd HH:mm:ss");

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + Source.Id + "','" + stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','" + UData.CompanyId + "')";

                    connection.Execute(str1);

                    // var t = EnquiryRow.Fields;
                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                        );

                    connection.Execute("Update WatiContacts SET IsMoved = 1 WHERE Id = " + request.Id);


                    var FacebookEnquirySettings = new WatiContactsRow();

                    //var i = WoocommerceRow.Fields;
                    //FacebookEnquirySettings = connection.First<WoocommerceRow>(l => l
                    //.SelectTableFields()
                    //    .Select(i.AutoEmail)
                    //    .Select(i.AutoSms)
                    //    .Select(i.Sender)
                    //         .Select(i.Subject)
                    //         .Select(i.SmsTemplate)
                    //         .Select(i.EmailTemplate)
                    //         .Select(i.Host)
                    //         .Select(i.Port)
                    //         .Select(i.Ssl)
                    //         .Select(i.EmailId)
                    //         .Select(i.EmailPassword)
                    //);

                    //if (FacebookEnquirySettings.AutoEmail.Value == true && !model.Email.IsNullOrEmpty())
                    //{
                    //    var u = UserRow.Fields;
                    //    var User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                    //        .SelectTableFields()
                    //        .Select(u.Host)
                    //        .Select(u.Port)
                    //        .Select(u.SSL)
                    //        .Select(u.EmailId)
                    //        .Select(u.EmailPassword));

                    //    try
                    //    {
                    //        if (FacebookEnquirySettings.Host != null)
                    //        {
                    //            MailMessage mm = new MailMessage();
                    //            var addr = new MailAddress(FacebookEnquirySettings.EmailId, FacebookEnquirySettings.Sender);

                    //            mm.From = addr;
                    //            mm.Sender = addr;
                    //            mm.To.Add(model.Email);
                    //            mm.Subject = FacebookEnquirySettings.Subject;
                    //            var msg = FacebookEnquirySettings.EmailTemplate;
                    //            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    //            msg = msg.Replace("#customername", name.IsNullOrEmpty() ? "Customer" : name);
                    //            mm.Body = msg;

                    //            if (FacebookEnquirySettings.Attachment != null)
                    //            {
                    //                JArray att = JArray.Parse(FacebookEnquirySettings.Attachment);
                    //                foreach (var f in att)
                    //                {
                    //                    if (f["Filename"].HasValue())
                    //                    {
                    //                        mm.Attachments.Add(new Attachment(Server.MapPath("~/App_Data/upload/" + f["Filename"].ToString())));
                    //                    }
                    //                }
                    //            }

                    //            mm.IsBodyHtml = true;

                    //            EmailHelper.Send(mm, FacebookEnquirySettings.EmailId, FacebookEnquirySettings.EmailPassword, (Boolean)FacebookEnquirySettings.Ssl, FacebookEnquirySettings.Host, FacebookEnquirySettings.Port.Value);
                    //        }
                    //        else
                    //        {
                    //            MailMessage mm = new MailMessage();
                    //            var addr = new MailAddress(User.EmailId, FacebookEnquirySettings.Sender);

                    //            mm.From = addr;
                    //            mm.Sender = addr;
                    //            mm.To.Add(model.Email);
                    //            mm.Subject = FacebookEnquirySettings.Subject;
                    //            var msg = FacebookEnquirySettings.EmailTemplate;
                    //            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    //            msg = msg.Replace("#customername", name.IsNullOrEmpty() ? "Customer" : name);
                    //            mm.Body = msg;

                    //            if (FacebookEnquirySettings.Attachment != null)
                    //            {
                    //                JArray att = JArray.Parse(FacebookEnquirySettings.Attachment);
                    //                foreach (var f in att)
                    //                {
                    //                    if (f["Filename"].HasValue())
                    //                    {
                    //                        mm.Attachments.Add(new Attachment(Server.MapPath("~/App_Data/upload/" + f["Filename"].ToString())));
                    //                    }
                    //                }
                    //            }
                    //            mm.IsBodyHtml = true;

                    //            EmailHelper.Send(mm, User.EmailId, User.EmailPassword, (Boolean)User.SSL, User.Host, User.Port.Value);
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        throw new Exception(ex.Message.ToString());
                    //    }
                    //}

                    //if (FacebookEnquirySettings.AutoSms.Value == true && !model.Phone.IsNullOrEmpty())
                    //{
                    //    String msg = FacebookEnquirySettings.SmsTemplate;
                    //    msg = msg.Replace("#customername", name.IsNullOrEmpty() ? "Customer" : name);
                    //    model.Phone = model.Phone.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                    //    SMSHelper.SendSMS(model.Phone, msg);
                    //}
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

            List<WatiContactsRow> data1;
            var ind1 = WatiContactsRow.Fields;


            using (var connection = _connections.NewFor<WatiContactsRow>())
            {
                var ind = WatiContactsRow.Fields;
                data1 = connection.List<WatiContactsRow>( q => q
                   .SelectTableFields()
                   .Select(ind1.Waid)
                   .Select(ind1.FirtName)
                   .Select(ind1.FullName)
                   .Select(ind1.Phone)
                   .Select(ind1.Status)
                   .Select(ind1.Source)
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
                    //if (!(string.IsNullOrEmpty)(data.Company))
                    //{
                    //    Contacttyp = 2;
                    //}
                    //else
                    //{
                    Contacttyp = 1;
                    //}
                    string date1 = Convert.ToDateTime(item.Created).ToString("yyyy-MM-dd HH:mm:ss");
                    string name = item.FirtName;
                    string additionnal = " State :" + item.Waid;// " State :" + data.State + " Country=" + data.Country;
                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,AdditionalInfo,OwnerId,AssignedId,DateCreated) VALUES('" + Contacttyp + "','81','1','" + name + "','" + item.Phone + "','" + additionnal + "','" + UserId + "','" + UserId + "','" + date1 + "')";

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
                        .Where((s.Source == "Wati") || (s.Source == "WATI") || (s.Source == "WaTi"))
                        );

                    if (Source == null)
                    {
                        string str2 = "INSERT INTO Source(Source) VALUES('WATI')";
                        connection.Execute(str2);

                        Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "WATI")
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
                        string str2 = "INSERT INTO Stage(Stage, Type) VALUES('WATI', 1)";
                        connection.Execute(str2);

                        stageMaster = connection.TryFirst<StageRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "WATI")
                        );
                    }

                    var enquiryController = HttpContext.RequestServices.GetRequiredService<Enquiry.Endpoints.EnquiryController>();
                    GetNextNumberResponse nextNumber = enquiryController.GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(item.Created).ToString("yyyy-MM-dd HH:mm:ss");

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + Source.Id + "','" + stageMaster.Id + "','" + UserId + "','" + UserId + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','1')";

                    connection.Execute(str1);

                    // var t = EnquiryRow.Fields;
                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                        );

                     connection.Execute("Update WatiContacts SET IsMoved = 1 WHERE Id = " + item.Id);


                    var FacebookEnquirySettings = new WatiContactsRow();

                    //var i = WoocommerceRow.Fields;
                    //FacebookEnquirySettings = connection.First<WoocommerceRow>(l => l
                    //.SelectTableFields()
                    //    .Select(i.AutoEmail)
                    //    .Select(i.AutoSms)
                    //    .Select(i.Sender)
                    //         .Select(i.Subject)
                    //         .Select(i.SmsTemplate)
                    //         .Select(i.EmailTemplate)
                    //         .Select(i.Host)
                    //         .Select(i.Port)
                    //         .Select(i.Ssl)
                    //         .Select(i.EmailId)
                    //         .Select(i.EmailPassword)
                    //);

                    //if (FacebookEnquirySettings.AutoEmail.Value == true && !model.Email.IsNullOrEmpty())
                    //{
                    //    var u = UserRow.Fields;
                    //    var User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                    //        .SelectTableFields()
                    //        .Select(u.Host)
                    //        .Select(u.Port)
                    //        .Select(u.SSL)
                    //        .Select(u.EmailId)
                    //        .Select(u.EmailPassword));

                    //    try
                    //    {
                    //        if (FacebookEnquirySettings.Host != null)
                    //        {
                    //            MailMessage mm = new MailMessage();
                    //            var addr = new MailAddress(FacebookEnquirySettings.EmailId, FacebookEnquirySettings.Sender);

                    //            mm.From = addr;
                    //            mm.Sender = addr;
                    //            mm.To.Add(model.Email);
                    //            mm.Subject = FacebookEnquirySettings.Subject;
                    //            var msg = FacebookEnquirySettings.EmailTemplate;
                    //            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    //            msg = msg.Replace("#customername", name.IsNullOrEmpty() ? "Customer" : name);
                    //            mm.Body = msg;

                    //            if (FacebookEnquirySettings.Attachment != null)
                    //            {
                    //                JArray att = JArray.Parse(FacebookEnquirySettings.Attachment);
                    //                foreach (var f in att)
                    //                {
                    //                    if (f["Filename"].HasValue())
                    //                    {
                    //                        mm.Attachments.Add(new Attachment(Server.MapPath("~/App_Data/upload/" + f["Filename"].ToString())));
                    //                    }
                    //                }
                    //            }

                    //            mm.IsBodyHtml = true;

                    //            EmailHelper.Send(mm, FacebookEnquirySettings.EmailId, FacebookEnquirySettings.EmailPassword, (Boolean)FacebookEnquirySettings.Ssl, FacebookEnquirySettings.Host, FacebookEnquirySettings.Port.Value);
                    //        }
                    //        else
                    //        {
                    //            MailMessage mm = new MailMessage();
                    //            var addr = new MailAddress(User.EmailId, FacebookEnquirySettings.Sender);

                    //            mm.From = addr;
                    //            mm.Sender = addr;
                    //            mm.To.Add(model.Email);
                    //            mm.Subject = FacebookEnquirySettings.Subject;
                    //            var msg = FacebookEnquirySettings.EmailTemplate;
                    //            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    //            msg = msg.Replace("#customername", name.IsNullOrEmpty() ? "Customer" : name);
                    //            mm.Body = msg;

                    //            if (FacebookEnquirySettings.Attachment != null)
                    //            {
                    //                JArray att = JArray.Parse(FacebookEnquirySettings.Attachment);
                    //                foreach (var f in att)
                    //                {
                    //                    if (f["Filename"].HasValue())
                    //                    {
                    //                        mm.Attachments.Add(new Attachment(Server.MapPath("~/App_Data/upload/" + f["Filename"].ToString())));
                    //                    }
                    //                }
                    //            }
                    //            mm.IsBodyHtml = true;

                    //            EmailHelper.Send(mm, User.EmailId, User.EmailPassword, (Boolean)User.SSL, User.Host, User.Port.Value);
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        throw new Exception(ex.Message.ToString());
                    //    }
                    //}

                    //if (FacebookEnquirySettings.AutoSms.Value == true && !model.Phone.IsNullOrEmpty())
                    //{
                    //    String msg = FacebookEnquirySettings.SmsTemplate;
                    //    msg = msg.Replace("#customername", name.IsNullOrEmpty() ? "Customer" : name);
                    //    model.Phone = model.Phone.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                    //    SMSHelper.SendSMS(model.Phone, msg);
                    //}

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

        [HttpPost]
        public StandardResponse Sync(IUnitOfWork uow)
        {
            var response = new StandardResponse();

            WatiConfigRow Config;

            using (var connection = _connections.NewFor<WatiConfigRow>())
            {
                var s = WatiConfigRow.Fields;
                Config = connection.TryFirst<WatiConfigRow>(q => q
                    .SelectTableFields()
                    .Select(s.Url)
                    .Select(s.Token)                     
                    );
            }

            string uri = Config.Url + "/api/v1/getContacts"; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";
            try
            {
                HttpWebResponse myHttpWebResponse;
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
              //  myHttpWebRequest.Headers.Add("x-api-key: L3WEMukeez1uFPZhiEv6a6vDf2pu9pJPjtziFqN7");
                myHttpWebRequest.Headers.Add("authorization:Bearer " + Config.Token.ToString().Trim());
                myHttpWebRequest.ContentType = "application/json; charset=utf-8";
            
                myHttpWebRequest.Headers.Add("cache-control", "no-cache");
                myHttpWebRequest.Method = "GET";               



                // Set the 'Timeout' property of the HttpWebRequest to 1000 milliseconds.
                myHttpWebRequest.Timeout = 15000;

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                var reader = new StreamReader(myHttpWebResponse.GetResponseStream());

                var watiobjs = JsonConvert.DeserializeObject<dynamic>(reader.ReadToEnd());

                // Dictionary<string, object> result = watiobjs["data"][0];

                List<string> Records = new List<string>();
                foreach (var WatiObject in watiobjs["contact_list"])
                {
                    WatiDetail WatiDetails = new WatiDetail();
                  //  WatiDetails.id = WatiObject.id;//["id"];
                    WatiDetails.FirtName = WatiObject.firstName;//["contact_list"]["firstName"];
                    WatiDetails.FullName = WatiObject.fullName;//["contact_list"]["fullName"];
                    WatiDetails.Waid = WatiObject.id;//["contact_list"]["wAid"];
                    WatiDetails.Phone = WatiObject.phone;//["contact_list"]["phone"];
                    WatiDetails.Source = WatiObject.source;//["contact_list"]["source"];
                    WatiDetails.Status = WatiObject.contactStatus;//["contact_list"]["contactStatus"];
                    WatiDetails.Created = WatiObject.created;//["contact_list"]["created"];

                    //
                    Records.Add("IF NOT EXISTS (SELECT * FROM WatiContacts WHERE WAID ='" + WatiDetails.Waid + "')" +
                        "INSERT INTO WatiContacts ([WAID],[FirtName],[FullName],[Phone],[Source],[Status],[Created]) VALUES " +
                                  "('" + WatiDetails.Waid + "','" +
                                      WatiDetails.FirtName + "','" +
                                      WatiDetails.FullName + "','" +
                                       WatiDetails.Phone + "','" +
                                       WatiDetails.Source + "','" +
                                       WatiDetails.Status + "','" +
                                        Convert.ToDateTime(WatiDetails.Created).ToString("yyyy-MM-dd") + "')");
                }
                Records.Reverse();
                if (Records.Count > 0)
                {
                    using (var innerConnection = _connections.NewFor<WatiContactsRow>())
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

        internal class WatiDetail
        {
            public int id { get; set; }
            public string Waid { get; set; }
            public string FirtName { get; set; }
            public string FullName { get; set; }
            public string Phone { get; set; }
            public string Source { get; set; }
            public string Status { get; set; }
            public string Created { get; set; }
        }

    }
}
