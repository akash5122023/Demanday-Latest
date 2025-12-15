
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
    using Serenity.Extensions.DependencyInjection;
    
    using MyRepository = Repositories.FacebookDetailsRepository;
    using MyRow = FacebookDetailsRow;

[Route("Services/ThirdParty/FacebookDetails/[action]")]
[ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
public class FacebookDetailsController : ServiceEndpoint
{
       private readonly ISqlConnections _connections;
       private readonly IWebHostEnvironment _env;

       // private object rowSelection = new Serenity.GridRowSelectionMixin(this);;

       public FacebookDetailsController(ISqlConnections connections, IWebHostEnvironment env)
       {
           _connections = connections;
           _env = env;
       }

       public FacebookDetailsController()
           : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IWebHostEnvironment>())
       {
       }

        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context, _connections).Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context, _connections).Update(uow, request);
        }

        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
           return new MyRepository(Context, _connections).Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
             return new MyRepository(Context, _connections).Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository(Context, _connections).List(connection, request);
        }
        //
        [ServiceAuthorize("Facebook:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request);
            DynamicDataReport report = new DynamicDataReport(data.Entities, request.IncludeColumns, typeof(Columns.FacebookDetailsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "FacebookEnquiries_" +
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

            var data = new FacebookDetailsRow();

            using (var connection = _connections.NewFor<FacebookDetailsRow>())
            {
                var ind = FacebookDetailsRow.Fields;
                data = connection.TryById<FacebookDetailsRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.Name)
                   .Select(ind.Phone)
                   .Select(ind.Email)
                   .Select(ind.CreatedTime)
                   .Select(ind.CompaignName)
                   .Select(ind.AdSetName)
                   .Select(ind.AdId)
                   .Select(ind.AdSetId)
                   .Select(ind.AdName)
                   .Select(ind.Address)
                   .Select(ind.Company)
                   .Select(ind.Campaignid)
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
                    string date1 = Convert.ToDateTime(data.CreatedTime).ToString("yyyy-MM-dd HH:mm:ss");
                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,Address,AdditionalInfo,OwnerId,AssignedId,DateCreated) VALUES('" + Contacttyp + "','81','1','" + data.Name + "','" + data.Phone + "','" + data.Email + "','" + data.Address + "','" + data.AdditionalDetails + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','"+date1+"')";

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
                        .Where((s.Source == "Facebook") || (s.Source == "FACEBOOK") || (s.Source == "Face book"))
                        );

                    if (Source == null)
                    {
                        string str2 = "INSERT INTO Source(Source) VALUES('Facebook')";
                        connection.Execute(str2);

                        Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "Facebook")
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

                    GetNextNumberResponse nextNumber = new AdvanceCRM.Enquiry.Endpoints.EnquiryController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(data.CreatedTime).ToString("yyyy-MM-dd HH:mm:ss");

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,AdditionalInfo,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','Campaign: " + data.CompaignName + "\nAdd: " + data.AdSetName + "\nFeedback:"+ data.Feedback + "','" + Source.Id + "','" + stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','" + UData.CompanyId + "')";

                    connection.Execute(str1);

                   // var t = EnquiryRow.Fields;
                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                        );

                    connection.Execute("Update FacebookDetails SET IsMoved = 1 WHERE Id = " + request.Id);


                    var FacebookEnquirySettings = new FacebookConfigurationRow();

                    var i = FacebookConfigurationRow.Fields;
                    FacebookEnquirySettings = connection.First<FacebookConfigurationRow>(l => l
                    .SelectTableFields()
                        .Select(i.AutoEmail)
                        .Select(i.AutoSms)
                        .Select(i.Sender)
                             .Select(i.Subject)
                             .Select(i.SMSTemplate)
                             .Select(i.SmsTemplateId)
                             .Select(i.EmailTemplate)
                             .Select(i.Host)
                             .Select(i.Port)
                             .Select(i.SSL)
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
                                msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
                                mm.Body = msg;

                                if (FacebookEnquirySettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(FacebookEnquirySettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            string filePath = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                            mm.Attachments.Add(new Attachment(filePath));
                                        }
                                    }
                                }

                                mm.IsBodyHtml = true;

                                EmailHelper.Send(mm, FacebookEnquirySettings.EmailId, FacebookEnquirySettings.EmailPassword, (Boolean)FacebookEnquirySettings.SSL, FacebookEnquirySettings.Host, FacebookEnquirySettings.Port.Value);
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
                                            string filePath = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
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

                    if (FacebookEnquirySettings.AutoSms.Value == true && !model.Phone.IsNullOrEmpty())
                    {
                        String msg = FacebookEnquirySettings.SMSTemplate;
                        String TempId = FacebookEnquirySettings.SmsTemplateId;
                        msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
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
            List<FacebookDetailsRow> data1;
               var ind1 = FacebookDetailsRow.Fields;

            using (var connection = _connections.NewFor<FacebookDetailsRow>())
            {
              
                data1 = connection.List<FacebookDetailsRow>(q => q
                    .SelectTableFields()
                   .Select(ind1.Name)
                   .Select(ind1.Phone)
                   .Select(ind1.Email)
                   .Select(ind1.CreatedTime)
                   .Select(ind1.CompaignName)
                   .Select(ind1.AdSetName)
                   .Select(ind1.AdId)
                   .Select(ind1.AdSetId)
                   .Select(ind1.AdName)
                   .Select(ind1.Address)
                   .Select(ind1.Company)
                   .Select(ind1.Campaignid)
                   .Select(ind1.Feedback)
                   .Where(ind1.Id.In(request.EnqIds)) // Include only selected rows
                   .Where(ind1.IsMoved=="false")
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
                    var fid = item.Id;
                    var abc = item.Name;
                    string date1 = Convert.ToDateTime(item.CreatedTime).ToString("yyyy-MM-dd HH:mm:ss");
                    string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,Address,AdditionalInfo,OwnerId,AssignedId,DateCreated) VALUES('" + Contacttyp + "','81','1','" + item.Name + "','" + item.Phone + "','" + item.Email + "','" + item.Address + "','" + item.AdditionalDetails + "','" +UserId + "','" + UserId + "','" + date1 + "')";

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
                        .Where((s.Source == "Facebook") || (s.Source == "FACEBOOK") || (s.Source == "Face book"))
                        );

                    if (Source == null)
                    {
                        string str2 = "INSERT INTO Source(Source) VALUES('Facebook')";
                        connection.Execute(str2);

                        Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "Facebook")
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

                    GetNextNumberResponse nextNumber = new AdvanceCRM.Enquiry.Endpoints.EnquiryController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(item.CreatedTime).ToString("yyyy-MM-dd HH:mm:ss");

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,AdditionalInfo,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','Campaign: " + item.CompaignName + "\nAdd: " + item.AdSetName + "\nFeedback:" + item.Feedback + "','" + Source.Id + "','" + stageMaster.Id + "','" + UserId + "','" + UserId + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','1')";

                    connection.Execute(str1);

                    // var t = EnquiryRow.Fields;
                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                        );

                    connection.Execute("Update FacebookDetails SET IsMoved = 1 WHERE Id = " + item.Id);

                    var FacebookEnquirySettings = new FacebookConfigurationRow();

                    var i = FacebookConfigurationRow.Fields;
                    FacebookEnquirySettings = connection.First<FacebookConfigurationRow>(l => l
                    .SelectTableFields()
                        .Select(i.AutoEmail)
                        .Select(i.AutoSms)
                        .Select(i.Sender)
                             .Select(i.Subject)
                             .Select(i.SMSTemplate)
                             .Select(i.EmailTemplate)
                             .Select(i.Host)
                             .Select(i.Port)
                             .Select(i.SSL)
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
                                msg = msg.Replace("#customername", item.Name.IsNullOrEmpty() ? "Customer" : item.Name);
                                mm.Body = msg;

                                if (FacebookEnquirySettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(FacebookEnquirySettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            string filePath = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
                                            mm.Attachments.Add(new Attachment(filePath));
                                        }
                                    }
                                }

                                mm.IsBodyHtml = true;

                                EmailHelper.Send(mm, FacebookEnquirySettings.EmailId, FacebookEnquirySettings.EmailPassword, (Boolean)FacebookEnquirySettings.SSL, FacebookEnquirySettings.Host, FacebookEnquirySettings.Port.Value);
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
                                msg = msg.Replace("#customername", item.Name.IsNullOrEmpty() ? "Customer" : item.Name);
                                mm.Body = msg;

                                if (FacebookEnquirySettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(FacebookEnquirySettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            string filePath = Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString());
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
                    if (FacebookEnquirySettings.AutoSms.Value == true && !item.Phone.IsNullOrEmpty())
                    {
                        String msg = FacebookEnquirySettings.SMSTemplate;
                        msg = msg.Replace("#customername", item.Name.IsNullOrEmpty() ? "Customer" : item.Name);
                        item.Phone = item.Phone.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                        SMSHelper.SendSMS(item.Phone, msg,msg);
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

       

        //[HttpPost]
        //public StandardResponse Sync(IUnitOfWork uow)
        //{
        //    var response = new StandardResponse();
           
        //    FacebookConfigurationRow Config;
        //    var nexturi1 = string.Empty ;

        //    try
        //    {

        //   using (var connection = _connections.NewFor<FacebookConfigurationRow>())
        //    {
        //        var s = FacebookConfigurationRow.Fields;
        //        Config = connection.TryFirst<FacebookConfigurationRow>(q => q
        //            .SelectTableFields()
        //            .Select(s.AppId)
        //            .Select(s.AccessTokenKey)
        //            );
        //    }
        //    //https://graph.facebook.com/v12.0/381661135539885/leadgen_forms?fields=leads%7Bfield_data%2Ccampaign_id%2Ccampaign_name%2Ccreated_time%2Cad_id%2Cadset_id%2Cad_name%2Cadset_name%2Cpartner_name%7D&access_token=EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD
        //    // string uri = "http://facebook.bizpluscrm.com/index.php?action=get-leads";
        //    string uri = "https://graph.facebook.com/v12.0/" + Config.AppId + "/leadgen_forms?limit=10000&fields=leads%7Bfield_data%2Ccampaign_id%2Ccampaign_name%2Ccreated_time%2Cad_id%2Cadset_id%2Cad_name%2Cadset_name%2Cpartner_name%7D&access_token=" + Config.AccessTokenKey; //EAAHCGmL1mYMBAD8liklt4SrZAJXZCVPRDGKZAqXFXEefqdOK2p5cx4j4ZA3b4BW2lQyyfbux9kptl3V5udhzB6EDQEBUl8rCSd8KHcFvItHZA0m1PA8lVEkbRnxVutdJpMxvvo8MgOlxvwByEBi4GYpPgJO4J8S4nDiN4GWQUdX5ubMSztYQh9AbFZCxne8hCb5CbSXnefNAZDZD";
          
        //        HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
        //        myHttpWebRequest.ContentType = "application/json";

        //        myHttpWebRequest.Timeout = 15000;
        //        HttpWebResponse myHttpWebResponse;
        //        myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
        //        StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream());
        //        myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();


        //        var FacebookObjects = JsonConvert.DeserializeObject<dynamic>(reader.ReadToEnd());

        //        // Dictionary<string, object> result = FacebookObjects["data"][0];

        //        List<string> Records = new List<string>();
        //        List<string> Records1 = new List<string>();

        //        // var next = FacebookObjects["data"]["leads"]["Paging"].next;

        //        foreach (var FacebookObject in FacebookObjects["data"])
        //        {
        //            if (FacebookObject["leads"] != null)
        //            {
        //                FacebookDetail FacebookDetails = new FacebookDetail();
        //                foreach (var Fbleads in FacebookObject["leads"])
        //                {
        //                    //if (Fbleads.Next != NULL)
        //                    //{
        //                    var next = Fbleads.Next;
        //                    if (next != null)
        //                    {
        //                        // Fbleads["paging"].next;data[0].leads.paging.next
        //                        var nexturi = next.Last;
        //                        nexturi1 = nexturi.next;
        //                    }
        //                    //}
        //                    foreach (var Fbdata in Fbleads)
        //                    {
        //                        if (Fbdata.Count != null)
        //                        {
        //                            int ii = Fbdata.Count;
        //                            for (int i = 0; i < ii; i++)
        //                            {
        //                                FacebookDetails.campaign_id = Fbdata[i].campaign_id;
        //                                FacebookDetails.campaign_name = Fbdata[i].campaign_name;
        //                                FacebookDetails.created_time = Fbdata[i].created_time;
        //                                FacebookDetails.ad_id = Fbdata[i].ad_id;
        //                                FacebookDetails.ad_name = Fbdata[i].ad_name;
        //                                FacebookDetails.adset_id = Fbdata[i].adset_id;
        //                                FacebookDetails.adset_name = Fbdata[i].adset_name;
        //                                FacebookDetails.id = Fbdata[i].id;
        //                                var addinfo = string.Empty;
        //                                // !Fbdata.ContainsKey("campaign_id") ? "" : Convert.ToString(Fbdata["campaign_id"]).Replace("'", "");
        //                                foreach (var Fbfield in Fbdata[i]["field_data"])
        //                                {
        //                                    if (Fbfield.name == "email" || Fbfield.name == "EMAIL")
        //                                    {
        //                                        FacebookDetails.Email = (string)Fbfield.values[0];
        //                                    }
        //                                    else if (Fbfield.name == "full_name" || Fbfield.name == "FULL_NAME")
        //                                    {
        //                                        FacebookDetails.FullName = (string)Fbfield.values[0];
        //                                    }
        //                                    else if (Fbfield.name == "company_name")
        //                                    {
        //                                        FacebookDetails.Company = (string)Fbfield.values[0];
        //                                    }
        //                                    else if (Fbfield.name == "phone_number" || Fbfield.name == "PHONE")
        //                                    {
        //                                        FacebookDetails.phone_number = (string)Fbfield.values[0];
        //                                    }
        //                                    else if (Fbfield.name == "city" || Fbfield.name == "0")
        //                                    {
        //                                        FacebookDetails.City = (string)Fbfield.values[0];
        //                                    }
        //                                    else if (Fbfield.name != "abc")
        //                                    {
        //                                        addinfo = addinfo + "_" + Fbfield.name + ":" + (string)Fbfield.values[0];
        //                                        FacebookDetails.additional = addinfo;
        //                                    }
        //                                }
        //                                Records.Add("IF NOT EXISTS (SELECT * FROM FacebookDetails WHERE LeadId ='" + FacebookDetails.id + "')" +
        //                                              "INSERT INTO FacebookDetails ([LeadId],[Campaignid],[Address],[AdditionalDetails],[Name],[Company],[Email],[Phone],[CompaignName],[AdId],[AdName],[AdSetId],[AdSetName],[CreatedTime]) VALUES " +
        //                                              "('" + FacebookDetails.id + "','" +
        //                                                   FacebookDetails.campaign_id + "','" +
        //                                                  FacebookDetails.City + "','" +
        //                                                   FacebookDetails.additional + "','" +
        //                                                   FacebookDetails.FullName + "','" +
        //                                                   FacebookDetails.Company + "','" +
        //                                                   FacebookDetails.Email + "','" +
        //                                                   FacebookDetails.phone_number + "','" +
        //                                                   FacebookDetails.campaign_name + "','" +
        //                                                   FacebookDetails.ad_id + "','" +
        //                                                   FacebookDetails.ad_name + "','" +
        //                                                   FacebookDetails.adset_id + "','" +
        //                                                   FacebookDetails.adset_name + "','" +
        //                                                   FacebookDetails.created_time + "')");

        //                                ////Records.Reverse();
        //                                ////Records1.Reverse();
        //                                //if (Records.Count > 0)
        //                                //{
        //                                //    using (var innerConnection = _connections.NewFor<FacebookDetailsRow>())
        //                                //    {
        //                                //        for (int ij = 0; ij < Records.Count; ij++)
        //                                //        {
        //                                //            try
        //                                //            {
        //                                //                innerConnection.Execute(Records[ij]);
        //                                //            }
        //                                //            catch (Exception ex)
        //                                //            {
        //                                //            }

        //                                //        }
        //                                //    }
        //                                //}
        //                            }
        //                        }
        //                        // FacebookDetails.campaign_id = !Fbdata.ContainsKey("campaign_id") ? "" : Convert.ToString(Fbdata["campaign_id"]).Replace("'", "");
        //                    }



        //                    if (nexturi1 != null)
        //                    {
        //                        String apiStr = nexturi1;
        //                        String urinext = apiStr.Trim().Replace("limit=25", "limit=25");


        //                        HttpWebRequest myHttpWebRequest1 = (HttpWebRequest)WebRequest.Create(urinext);
        //                        myHttpWebRequest1.ContentType = "application/json";

        //                        myHttpWebRequest1.Timeout = 15000;
        //                        HttpWebResponse myHttpWebResponse1;
        //                        myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();
        //                        StreamReader reader1 = new StreamReader(myHttpWebResponse1.GetResponseStream());
        //                        myHttpWebResponse1 = (HttpWebResponse)myHttpWebRequest1.GetResponse();


        //                        //var FacebookObjects1 = js.Deserialize<dynamic>(reader1.ReadToEnd());
        //                        var FacebookObjects1 = JsonConvert.DeserializeObject<dynamic>(reader1.ReadToEnd());
        //                        // Dictionary<string, object> result = FacebookObjects["data"][0];JsonConvert.DeserializeObject



        //                        // var next = FacebookObjects["data"]["leads"]["Paging"].next;
        //                        //foreach (Dictionary<string, object> IndiaMartResponsObject in IndiaMartResponsObjects)
        //                        foreach (var Fbdata1 in FacebookObjects1["data"])
        //                        {
        //                            FacebookDetail FacebookDetails1 = new FacebookDetail();
        //                            //if (Fbdata1.Count != null)

        //                            //int ii = Fbdata1.Count;

        //                            FacebookDetails1.campaign_id = Fbdata1.campaign_id;// !Fbdata1.ContainsKey("campaign_id") ? "" : Convert.ToString(Fbdata1["campaign_id"]).Replace("'", "");
        //                            FacebookDetails1.campaign_name = Fbdata1.campaign_name;//!Fbdata1.ContainsKey("campaign_name") ? "" : Convert.ToString(Fbdata1["campaign_name"]).Replace("'", "");// Fbdata1.campaign_name;//
        //                            FacebookDetails1.created_time = Fbdata1.created_time;//!Fbdata1.ContainsKey("campaign_id") ? "" : Convert.ToString(Fbdata1["campaign_id"]).Replace("'", "");// Fbdata1.created_time;
        //                            FacebookDetails1.ad_id = Fbdata1.ad_id;// !Fbdata1.ContainsKey("campaign_id") ? "" : Convert.ToString(Fbdata1["campaign_id"]).Replace("'", "");// Fbdata1.ad_id;
        //                            FacebookDetails1.ad_name = Fbdata1.ad_name;//!Fbdata1.ContainsKey("campaign_id") ? "" : Convert.ToString(Fbdata1["campaign_id"]).Replace("'", "");//Fbdata1.ad_name;
        //                            FacebookDetails1.adset_id = Fbdata1.campaign_name;// !Fbdata1.ContainsKey("campaign_id") ? "" : Convert.ToString(Fbdata1["campaign_id"]).Replace("'", "");// Fbdata1[i].adset_id;
        //                            FacebookDetails1.adset_name = Fbdata1.campaign_name;// !Fbdata1.ContainsKey("campaign_id") ? "" : Convert.ToString(Fbdata1["campaign_id"]).Replace("'", "");// Fbdata1[i].adset_name;
        //                            FacebookDetails1.id = Fbdata1.id;// !Fbdata1.ContainsKey("campaign_id") ? "" : Convert.ToString(Fbdata1["campaign_id"]).Replace("'", "");// Fbdata1[i].id;
        //                            var addinfo = string.Empty;
        //                            //FacebookDetails1.FullName= !Fbdata1.ContainsKey("field_data") ? "" : Convert.ToString(Fbdata1["field_data"]).Replace("'", "");// Fbdata1[i].id;

        //                            foreach (var Fbfield1 in Fbdata1["field_data"])
        //                            {
        //                                if (Fbfield1.name == "email" || Fbfield1.name == "EMAIL")
        //                                {
        //                                    FacebookDetails1.Email = (string)Fbfield1.values[0];
        //                                }
        //                                else if (Fbfield1.name == "full_name" || Fbfield1.name == "FULL_NAME")
        //                                {
        //                                    FacebookDetails1.FullName = (string)Fbfield1.values[0];
        //                                }
        //                                else if (Fbfield1.name == "company_name")
        //                                {
        //                                    FacebookDetails1.Company = (string)Fbfield1.values[0];
        //                                }
        //                                else if (Fbfield1.name == "phone_number" || Fbfield1.name == "PHONE")
        //                                {
        //                                    FacebookDetails1.phone_number = (string)Fbfield1.values[0];
        //                                }
        //                                else if (Fbfield1.name == "city" || Fbfield1.name == "0")
        //                                {
        //                                    FacebookDetails1.City = (string)Fbfield1.values[0];
        //                                }
        //                                else if (Fbfield1.name != "abc")
        //                                {
        //                                    addinfo = addinfo + "_" + Fbfield1.name + ":" + (string)Fbfield1.values[0];
        //                                    FacebookDetails1.additional = addinfo;
        //                                }
        //                            }
        //                            Records.Add("IF NOT EXISTS (SELECT * FROM FacebookDetails WHERE LeadId ='" + FacebookDetails1.id + "')" +
        //                                            "INSERT INTO FacebookDetails ([LeadId],[Campaignid],[Address],[AdditionalDetails],[Name],[Company],[Email],[Phone],[CompaignName],[AdId],[AdName],[AdSetId],[AdSetName],[CreatedTime]) VALUES " +
        //                                            "('" + FacebookDetails1.id + "','" +
        //                                                FacebookDetails1.campaign_id + "','" +
        //                                                FacebookDetails1.City + "','" +
        //                                                FacebookDetails1.additional + "','" +
        //                                                FacebookDetails1.FullName + "','" +
        //                                                FacebookDetails1.Company + "','" +
        //                                                FacebookDetails1.Email + "','" +
        //                                                FacebookDetails1.phone_number + "','" +
        //                                                FacebookDetails1.campaign_name + "','" +
        //                                                FacebookDetails1.ad_id + "','" +
        //                                                FacebookDetails1.ad_name + "','" +
        //                                                FacebookDetails1.adset_id + "','" +
        //                                                FacebookDetails1.adset_name + "','" +
        //                                                FacebookDetails1.created_time + "')");
        //                            //Records1.Reverse();
        //                            ////   Records1.Reverse();
        //                            //if (Records1.Count > 0)
        //                            //{
        //                            //    using (var innerConnection = _connections.NewFor<FacebookDetailsRow>())
        //                            //    {
        //                            //        for (int ij = 0; ij < Records1.Count; ij++)
        //                            //        {
        //                            //            try
        //                            //            {

        //                            //                var str = Records1[ij];

        //                            //                var modified = innerConnection.Execute(Records1[ij]);
        //                            //                var iiid = modified;
        //                            //                innerConnection.Execute(Records1[ij]);

        //                            //            }
        //                            //            catch (Exception ex)
        //                            //            {
        //                            //                response.Status = Records1[ij];
        //                            //            }

        //                            //        }
        //                            //    }
        //                            //}


        //                        }


        //                    }
        //                }
        //            }
        //        }
        //        Records.Reverse();
        //        //   Records1.Reverse();
        //        if (Records.Count > 0)
        //        {
        //            using (var innerConnection = _connections.NewFor<FacebookDetailsRow>())
        //            {
        //                for (int ij = 0; ij < Records.Count; ij++)
        //                {
        //                    try
        //                    {

        //                        var str = Records[ij];

        //                        var modified = innerConnection.Execute(Records[ij]);
        //                        var iiid = modified;
        //                        innerConnection.Execute(Records[ij]);

        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        response.Status = Records[ij];
        //                    }

        //                }
        //            }
        //        }

        //        response.Status = "Sync Done";
        //    }
        //    catch (Exception ex)
        //    {
        //        // throw ex;
        //    }
        //    return response;

        //}

        internal class FacebookDetail
        {
            public string id { get; set; }
            public string lead_id { get; set; }
            public string campaign_id { get; set; }
            public string campaign_name { get; set; }
            public string additional { get; set; }
            public string ad_id { get; set; }
            public string ad_name { get; set; }
            public string adset_id { get; set; }
            public string adset_name { get; set; }
            public string created_time { get; set; }
            public string FullName { get; set; }
            public string City { get; set; }
            public string Company { get; set; }
            public string Email { get; set; }
            public string phone_number { get; set; }
            public string created_at { get; set; }
           



        }


    }
}
