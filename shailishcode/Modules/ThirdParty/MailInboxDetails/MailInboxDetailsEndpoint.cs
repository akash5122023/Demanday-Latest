
namespace AdvanceCRM.ThirdParty.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Services.Endpoints;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Settings;
    using AdvanceCRM.Settings.Endpoints;
    using AdvanceCRM.ThirdParty;
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    //using System.Web.Security;
    //using System.Web.UI;
    //using System.Web.UI.WebControls;
    //using System.Web.UI.WebControls.WebParts;
    //using System.Web.UI.HtmlControls;
    using System.IO;
    using System.Net.NetworkInformation;
    using System.Net.Security;
    using System.Net.Sockets;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System.Data;   
    using System.Net.Mail;
   
     
   
    using MyRepository = Repositories.MailInboxDetailsRepository;
    using MyRow = MailInboxDetailsRow;
    using MailKit.Net.Imap;
    using MailKit;

    using AdvanceCRM.Services;
    using Newtonsoft.Json.Linq;
    using Serenity;
    using MimeKit;
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Serenity.Abstractions;

    [Route("Services/ThirdParty/MailInboxDetails/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class MailInboxDetailsController : ServiceEndpoint
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

        public MailInboxDetailsController(
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
            MyRow LastEnquiry;
            MailInboxRow Config;
            DateTime StartDate;
            using (var connection = _connections.NewFor<MailInboxRow>())
            {
                var s = MailInboxRow.Fields;
                Config = connection.TryFirst<MailInboxRow>(q => q
                    .SelectTableFields()
                    .Select(s.EmailId)
                    .Select(s.EmailPassword)
                    .Select(s.Ssl)
                    .Select(s.Port)
                    .Select(s.Host)
                    ) ;

                var i = MyRow.Fields;
                LastEnquiry = connection.TryFirst<MyRow>(q => q
                .SelectTableFields()
                .Select(i.CreatedDate)
                .OrderBy(i.CreatedDate, true)
                );

            }
            try
            {
                //var m = new List<dynamic>();

                bool ssl = Convert.ToBoolean(Config.Ssl);
                int port = Convert.ToInt32(Config.Port);
                var host = Convert.ToString(Config.Host);

            //if (LastEnquiry == null)
            //{
                StartDate = DateTime.Now.AddDays(0);
            //}
            //else
            //{

            //    StartDate = Convert.ToDateTime(LastEnquiry.CreatedDate);
            //}

                  ImapClient client = new ImapClient();
                //ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                client.Connect(host, port, true);
                // If you want to disable an authentication mechanism,
                // you can do so by removing the mechanism like this:
                client.AuthenticationMechanisms.Remove("XOAUTH");

                
                //client.AuthenticationMechanisms.Remove("XOAUTH");
                client.Authenticate(Config.EmailId, Config.EmailPassword);
              
                MailDetail MailDetails = new MailDetail();
                MimeMessage messagess = new MimeMessage();
                FolderAccess inboxAccess = client.Inbox.Open(FolderAccess.ReadOnly);
                IMailFolder destination = client.GetFolder("Inbox");
                List<string> Records = new List<string>();
                int no = client.Inbox.Search(MailKit.Search.SearchQuery.SentSince(StartDate)).Count();
                IList <UniqueId> uids=client.Inbox.Search(MailKit.Search.SearchQuery.SentSince(StartDate));
            
                foreach (UniqueId msgId in uids)
                {
                    messagess = destination.GetMessage(msgId);
                   
                    MailDetails.From =Convert.ToString(messagess.From);
                    MailDetails.To = Convert.ToString(messagess.To);
                    MailDetails.Subject = messagess.Subject;
                    MailDetails.CreatedDate = Convert.ToString(messagess.Date);
                    MailDetails.ToName = messagess.MessageId;
                    //MailDetails.Content = messagess.TextBody;

                    var mail1 = string.Empty; var mail = string.Empty;
                    var reg = new Regex("\".*?\"");
                    var reg1 = new Regex("<(.*?)>");
                    var matches1 = reg1.Matches(MailDetails.From);
                    var matches = reg.Matches(MailDetails.From);

                    if (matches.Count != 0)
                    {
                        mail = matches[0].Groups[0].Value;
                       mail = mail.Replace("\"", " ");

                    }
                    if (matches1.Count != 0)
                    {
                        mail1 = matches1[0].Groups[0].Value;
                        mail1 = mail1.Replace("\"", " ");

                    }

                    MailDetails.To = MailDetails.To.Replace("'", " ");
                    MailDetails.From = MailDetails.From.Replace("'", " ");
                    MailDetails.Subject = MailDetails.Subject.Replace("'", " ");
                  //  var content = "Content";
                    //int lenght = MailDetails.Content.Length;
                    //if (lenght > 500)
                    //{
                    //    MailDetails.Content = MailDetails.Content.Substring(0, 500);
                    //}
                    //else
                    //{
                    //    MailDetails.Content = MailDetails.Content;
                    //}
                  
                    Records.Add("IF NOT EXISTS(SELECT * FROM MailInboxDetails WHERE ToName = '" + MailDetails.ToName + "')" +
                            "INSERT INTO MailInboxDetails ([Subject],[To],[ToName],[From],[FromName],[FromAddress],[CreatedDate]) VALUES " +
                                        "('" + MailDetails.Subject + "','" +
                                             //  MailDetails.messsageId + "','" +
                                           //  MailDetails.Content + "','" +
                                             MailDetails.To + "','" +
                                             MailDetails.ToName + "','" +
                                             //MailDetails.ToAddress + "','" +
                                             MailDetails.From + "','" +
                                            mail+ "','" +
                                            mail1 + "','" +
                                             Convert.ToDateTime(MailDetails.CreatedDate).ToString("yyyy-MM-dd hh:mm:ss") + "')");//[Content],

                        // }
                    //}
                    //catch(Exception ex)
                    //{
                    //    response.Status = ex.Message.ToString();
                    //}
                }


                
                Records.Reverse();
                if (Records.Count > 0)
                {
                    using (var innerConnection = _connections.NewFor<MailInboxDetailsRow>())
                    {
                        for (int ij = 0; ij < Records.Count; ij++)
                        {
                            try
                            {
                               var modified = innerConnection.Execute(Records[ij]);
                                if (modified > 0)
                                {

                                    var tiks = MailInboxDetailsRow.Fields;
                                    var tik = innerConnection.TryFirst<MailInboxDetailsRow>(q => q
                                      .SelectTableFields()
                                      .Select(tiks.Id)
                                      .Select(tiks.Subject)
                                      .OrderBy(tiks.Id, desc: true)
                                      //.Where(tiks.ToName== MailDetails.ToName)
                                      );
                                    int id = Convert.ToInt32(tik.Id);
                                    
                                        var Contacttyp = 2;
                                        TicketRow LastEnquiry1;
                                        var br = UserRow.Fields;
                                        var UData = new UserRow();
                                        var model = new MyRow();

                                        var data = new MailInboxDetailsRow();

                                        using (var connection = _connections.NewFor<MailInboxDetailsRow>())
                                        {
                                            var ind = MailInboxDetailsRow.Fields;
                                            data = connection.TryById<MailInboxDetailsRow>(id, q => q
                                               .SelectTableFields()
                                               .Select(ind.FromAddress)
                                               .Select(ind.Subject)
                                               .Select(ind.ToName)
                                               .Select(ind.Phone)
                                               .Select(ind.From)
                                               .Select(ind.ToAddress)
                                               .Select(ind.To)
                                               .Select(ind.CreatedDate)
                                               .Select(ind.Content)
                                               .Select(ind.FromName)
                                               );

                                            UData = connection.First<UserRow>(q => q
                                            .SelectTableFields()
                                            .Select(br.CompanyId)
                                            .Where(br.UserId == Context.User.GetIdentifier())
                                           );
                                        }
                                    if (data.Subject.StartsWith("RE:") == false || tik.Subject.StartsWith("Re:") == false)
                                    {
                                        var mail1 = string.Empty; var mail = string.Empty;
                                        var reg = new Regex("\".*?\"");
                                        var reg1 = new Regex("<(.*?)>");
                                        var matches1 = reg1.Matches(data.From);
                                        var matches = reg.Matches(data.From);

                                        if (matches != null)
                                        {
                                            mail = matches[0].Groups[0].Value;
                                            mail = mail.Replace("\"", " ");

                                        }
                                        if (matches1 != null)
                                        {
                                            mail1 = matches1[0].Groups[0].Value;
                                             mail1 = mail1.Replace("\"", " ");

                                        }



                                        //var result = from Match match in Regex.Match(data.From, "")
                                        //             select match.ToString();
                                        //response.Status= result;

                                        if (data.Content != null)
                                        { data.Content = data.Content.Replace(",", " "); }

                                        if (data.Subject != null)
                                        {
                                            data.Subject = data.Subject.Replace(",", " ");          //  data.GlUserCompanyName = data.GlUserCompanyName.Replace(",", " ");
                                        }
                                        if (data.Content != null)
                                        {
                                            data.Content = data.Content.Replace("\'", "");
                                        }
                                        if (data.Content != null)
                                        {
                                            data.Content = data.Content.Replace("\'", "");
                                        }
                                        if (data.Content != null)
                                        {

                                            data.Content = data.Content.Replace("\"", "");
                                        }



                                        try
                                        {
                                            using (var connection = _connections.NewFor<TicketRow>())
                                            {
                                                //  string Additional = data.Priority + ", Message:" + data.EnqMessage;
                                                var str1 = "INSERT INTO Ticket(ProductsId,Name,Phone,Priority,ComplaintDetails,AssignedId) VALUES('1','" + data.FromName + "','" + data.Phone + "','1','" + data.Subject + "','" + Context.User.GetIdentifier() + "')";

                                                connection.Execute(str1);

                                                var e = TicketRow.Fields;
                                                LastEnquiry1 = connection.First<TicketRow>(l => l
                                                    .Select(e.Id)
                                                    .OrderBy(e.Id, desc: true)
                                                    );

                                                connection.Execute("Update MailInboxDetails SET IsMoved = 1 WHERE Id = " + id);

                                                //   connection.Execute("Update Contacts SET Name='" + data.SenderName + "' WHERE Id=" + LastContact.Id);

                                                //string stru = "Update Contacts Set Name='" + data.SenderName + "' where Id='" + LastContact.Id + "'";
                                                //connection.Execute(stru);

                                                var IndiaMartSettings = new MailInboxRow();

                                                var i = MailInboxRow.Fields;
                                                IndiaMartSettings = connection.First<MailInboxRow>(l => l
                                                .SelectTableFields()
                                                    .Select(i.AutoEmail)
                                                    .Select(i.Sender)
                                                    .Select(i.Subject)
                                                    .Select(i.EmailTemplate)
                                                         .Select(i.Host)
                                                         .Select(i.Port)
                                                         .Select(i.EmailId)
                                                         .Select(i.EmailPassword)
                                                );

                                                if (IndiaMartSettings.AutoEmail.Value == true && !data.FromAddress.IsEmptyOrNull())
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
                                                        if (IndiaMartSettings.SHost != null)
                                                        {
                                                            MailMessage mm = new MailMessage();
                                                            var addr = new MailAddress(IndiaMartSettings.SEmailId, IndiaMartSettings.Sender);

                                                            mm.From = addr;
                                                            mm.Sender = addr;
                                                            mm.To.Add(data.FromAddress);
                                                            mm.Subject = data.Subject.ToString() + "(Ticket No :" + LastEnquiry1.Id + ")";
                                                            var msg = IndiaMartSettings.EmailTemplate;
                                                            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                                            msg = msg.Replace("#customername", data.FromAddress.IsNullOrEmpty() ? "Customer" : data.FromAddress);
                                                            msg = msg.Replace("#ticket",Convert.ToString(LastEnquiry1.Id));
                                                            mm.Body = msg;

                                                            if (IndiaMartSettings.Attachment != null)
                                                            {
                                                                JArray att = JArray.Parse(IndiaMartSettings.Attachment);
                                                                foreach (var f in att)
                                                                {
                                                                    if (f["Filename"].HasValue())
                                                                    {
                                                                        mm.Attachments.Add(new Attachment(Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString())));
                                                                    }
                                                                }
                                                            }

                                                            mm.IsBodyHtml = true;

                                                             EmailHelper.Send(mm, IndiaMartSettings.SEmailId, IndiaMartSettings.SEmailPassword, (Boolean)IndiaMartSettings.Sssl, IndiaMartSettings.SHost, IndiaMartSettings.SPort.Value);
                                                        }
                                                        else
                                                        {
                                                            MailMessage mm = new MailMessage();
                                                            var addr = new MailAddress(User.EmailId, IndiaMartSettings.Sender);

                                                            mm.From = addr;
                                                            mm.Sender = addr;
                                                            mm.To.Add(data.FromAddress);
                                                            mm.Subject = data.Subject.ToString() + "(Ticket No :" + LastEnquiry1.Id + ")";
                                                            var msg = IndiaMartSettings.EmailTemplate;
                                                            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                                            msg = msg.Replace("#customername", data.FromName.IsNullOrEmpty() ? "Customer" : data.FromName);
                                                            msg = msg.Replace("#ticket", Convert.ToString(LastEnquiry1.Id));
                                                            mm.Body = msg;

                                                            if (IndiaMartSettings.Attachment != null)
                                                            {
                                                                JArray att = JArray.Parse(IndiaMartSettings.Attachment);
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


                                            }
                                            response.Id = LastEnquiry.Id.Value;
                                            response.Status = "Success";
                                        }

                                        catch (Exception ex)
                                        {
                                            response.Id = -1;
                                            response.Status = "Error\n" + ex.ToString();
                                        }

                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                response.Status = ex.Message.ToString();
                            }

                        }
                    }

                }
                response.Status = "Sync completed";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
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


        [HttpPost]
        public StandardResponse MoveToTicket(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();
            var Contacttyp = 2;
            TicketRow LastEnquiry;
            var br = UserRow.Fields;
            var UData = new UserRow();
            var model = new MyRow();

            var data = new MailInboxDetailsRow();

            using (var connection = _connections.NewFor<MailInboxDetailsRow>())
            {
                var ind = MailInboxDetailsRow.Fields;
                data = connection.TryById<MailInboxDetailsRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.FromAddress)
                   .Select(ind.Subject)
                   .Select(ind.ToName)
                   .Select(ind.Phone)
                   .Select(ind.From)
                   .Select(ind.ToAddress)
                   .Select(ind.To)
                   .Select(ind.CreatedDate)
                   .Select(ind.Content)
                   .Select(ind.FromName)
                   );

                UData = connection.First<UserRow>(q => q
                .SelectTableFields()
                .Select(br.CompanyId)
                .Where(br.UserId == Context.User.GetIdentifier())
               );
            }

            var mail1 = string.Empty;var mail=string.Empty;
            var reg = new Regex("\".*?\"");
            var reg1 = new Regex("<(.*?)>");
            var matches1 = reg1.Matches(data.From);
            var matches = reg.Matches(data.From);

            if (matches != null)
            {  mail = matches[0].Groups[0].Value;
                mail = mail.Replace("\"", " ");
                
            }
            if (matches1 != null)
            {
                mail1 = matches1[0].Groups[0].Value;
                mail1 = mail1.Replace("\"", " ");

            }



            //var result = from Match match in Regex.Match(data.From, "")
            //             select match.ToString();
            //response.Status= result;

            if (data.Content != null)
            { data.Content = data.Content.Replace(",", " "); }
           
            if (data.Subject != null)
            {
                data.Subject = data.Subject.Replace(",", " ");          //  data.GlUserCompanyName = data.GlUserCompanyName.Replace(",", " ");
            }
            if (data.Content != null)
            {
                data.Content = data.Content.Replace("\'", "");
            }
            if (data.Content != null)
            {
                data.Content = data.Content.Replace("\'", "");
            }
               if (data.Content != null)
            {

                data.Content = data.Content.Replace("\"", "");
            }
                 


            try
            {
                using (var connection = _connections.NewFor<TicketRow>())
                {    
                    //  string Additional = data.Priority + ", Message:" + data.EnqMessage;
                    var str1 = "INSERT INTO Ticket(ProductsId,Name,Phone,Priority,ComplaintDetails,AssignedId) VALUES('1','" + data.FromName + "','" + data.Phone + "','1','" + data.Content + "','" + Context.User.GetIdentifier() + "')";

                    connection.Execute(str1);

                    var e = TicketRow.Fields;
                    LastEnquiry = connection.First<TicketRow>(l => l
                        .Select(e.Id)
                        .OrderBy(e.Id, desc: true)
                        );

                    connection.Execute("Update MailInboxDetails SET IsMoved = 1 WHERE Id = " + request.Id);

                    //   connection.Execute("Update Contacts SET Name='" + data.SenderName + "' WHERE Id=" + LastContact.Id);

                    //string stru = "Update Contacts Set Name='" + data.SenderName + "' where Id='" + LastContact.Id + "'";
                    //connection.Execute(stru);

                    var IndiaMartSettings = new MailInboxRow();

                    var i = MailInboxRow.Fields;
                    IndiaMartSettings = connection.First<MailInboxRow>(l => l
                    .SelectTableFields()
                        .Select(i.AutoEmail)                       
                        .Select(i.Sender)
                        .Select(i.Subject)                             
                        .Select(i.EmailTemplate)
                             .Select(i.Host)
                             .Select(i.Port)                             
                             .Select(i.EmailId)
                             .Select(i.EmailPassword)
                    );

                    if (IndiaMartSettings.AutoEmail.Value == true && !data.FromAddress.IsEmptyOrNull())
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
                            if (IndiaMartSettings.SHost != null)
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(IndiaMartSettings.SEmailId, IndiaMartSettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(data.FromAddress);
                                mm.Subject = IndiaMartSettings.Subject;
                                var msg = IndiaMartSettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", data.FromAddress.IsNullOrEmpty() ? "Customer" : data.FromAddress);
                                mm.Body = msg;

                                if (IndiaMartSettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(IndiaMartSettings.Attachment);
                                    foreach (var f in att)
                                    {
                                        if (f["Filename"].HasValue())
                                        {
                                            mm.Attachments.Add(new Attachment(Path.Combine(_env.ContentRootPath, "App_Data", "upload", f["Filename"].ToString())));
                                        }
                                    }
                                }

                                mm.IsBodyHtml = true;

                                EmailHelper.Send(mm, IndiaMartSettings.SEmailId, IndiaMartSettings.SEmailPassword, (Boolean)IndiaMartSettings.Sssl, IndiaMartSettings.SHost, IndiaMartSettings.SPort.Value);
                            }
                            else
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(User.EmailId, IndiaMartSettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(data.FromAddress);
                                mm.Subject = IndiaMartSettings.Subject;
                                var msg = IndiaMartSettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", data.FromName.IsNullOrEmpty() ? "Customer" : data.FromName);
                                mm.Body = msg;

                                if (IndiaMartSettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(IndiaMartSettings.Attachment);
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



        internal class MailDetail
        {
           
            public string Subject { get; set; }          
            public string ToName { get; set; }   
            public string To { get; set; }
            public string From { get; set; }
            public string CreatedDate { get; set; }

           public string Content { get; set; }

        }
        }
}
