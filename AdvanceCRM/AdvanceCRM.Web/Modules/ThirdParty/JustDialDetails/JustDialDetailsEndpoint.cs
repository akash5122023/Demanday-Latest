
namespace AdvanceCRM.ThirdParty.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Enquiry.Endpoints;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Settings;
    using AdvanceCRM.ThirdParty;
    using Newtonsoft.Json.Linq;
    using Serenity;
    using Serenity.Data;
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Data;
    using System.Net.Mail;
    using System.IO;
    using Serenity.Abstractions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Caching.Memory;
    
    using MyRepository = Repositories.JustDialDetailsRepository;
    using MyRow = JustDialDetailsRow;
    using System.Collections.Generic;
    using System.Linq;

    [Route("Services/ThirdParty/JustDialDetails/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class JustDialDetailsController : ServiceEndpoint
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

        public JustDialDetailsController(
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

        [ServiceAuthorize("JustDial:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request);
            DynamicDataReport report = new DynamicDataReport(data.Entities, request.IncludeColumns, typeof(Columns.JustDialDetailsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "JustDial_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        [HttpPost]
        public StandardResponse MoveToEnquiry(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();
            EnquiryRow LastEnquiry;
            var br = UserRow.Fields;
            var UData = new UserRow();

            var data = new JustDialDetailsRow();

            using (var connection = _connections.NewFor<JustDialDetailsRow>())
            {
                var ind = JustDialDetailsRow.Fields;
                data = connection.TryById<JustDialDetailsRow>(request.Id, q => q
                   .SelectTableFields()
                   
                   .Select(ind.Name)
                   .Select(ind.Mobile)
                   .Select(ind.Landline)
                   .Select(ind.Email)
                   .Select(ind.DateTime)
                   .Select(ind.City)
                   .Select(ind.Area)
                   .Select(ind.Category)
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

                    string str = "INSERT INTO Contacts(ContactType,CustomerType,Name,Phone,ResidentialPhone,Email,Address,OwnerId,AssignedId) VALUES('1','1','" + data.Name + "','" + data.Mobile + "','" + data.Landline + "','" + data.Email + "','" + data.Area + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "')";

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
                    var sourceMaster = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where((s.Source == "justdial") || (s.Source == "JustDial") || (s.Source == "Just Dial") || (s.Source == "Just dial") || (s.Source == "justdial"))
                        );

                    if (sourceMaster == null)
                    {
                        response.Status = "Error: JustDial Source not found in Source master\nKindly add in masters and try again";

                        throw new Exception("JustDial Source not found in Source master\nKindly add in masters and try again");
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

                        throw new Exception(" Enquiry Stage not found in Stage master\nKindly add in masters and try again");
                    }

                    GetNextNumberResponse nextNumber = new EnquiryController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(data.DateTime).ToString("yyyy-MM-dd");
                    string feedback = data.Category + "Feedback " + data.Feedback;
                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,AdditionalInfo,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + feedback + "','" + sourceMaster.Id + "','" + stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','" + UData.CompanyId + "')";

                    connection.Execute(str1);

                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(c.Id, desc: true)
                        );

                    connection.Execute("Update JustDialDetails SET IsMoved = 1 WHERE Id = " + request.Id);


                    var JustDialSettings = new JustDialConfigurationRow();

                    var i = JustDialConfigurationRow.Fields;
                    JustDialSettings = connection.First<JustDialConfigurationRow>(l => l
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

                    //if (JustDialSettings.AutoEmail.Value == true && !data.Email.IsNullOrEmpty())
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
                    //        if (JustDialSettings.Host != null)
                    //        {
                    //            MailMessage mm = new MailMessage();
                    //            var addr = new MailAddress(JustDialSettings.EmailId, JustDialSettings.Sender);

                    //            mm.From = addr;
                    //            mm.Sender = addr;
                    //            mm.To.Add(data.Email);
                    //            mm.Subject = JustDialSettings.Subject;
                    //            var msg = JustDialSettings.EmailTemplate;
                    //            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    //            msg = msg.Replace("#customername", data.Name.IsNullOrEmpty() ? "Customer" : data.Name);
                    //            mm.Body = msg;

                    //            if (JustDialSettings.Attachment != null)
                    //            {
                    //                JArray att = JArray.Parse(JustDialSettings.Attachment);
                    //                foreach (var f in att)
                    //                {
                    //                    if (f["Filename"].HasValue())
                    //                    {
                    //                        mm.Attachments.Add(new Attachment(Server.MapPath("~/App_Data/upload/" + f["Filename"].ToString())));
                    //                    }
                    //                }
                    //            }

                    //            mm.IsBodyHtml = true;

                    //            EmailHelper.Send(mm, JustDialSettings.EmailId, JustDialSettings.EmailPassword, (Boolean)JustDialSettings.SSL, JustDialSettings.Host, JustDialSettings.Port.Value);
                    //        }
                    //        else
                    //        {
                    //            MailMessage mm = new MailMessage();
                    //            var addr = new MailAddress(User.EmailId, JustDialSettings.Sender);

                    //            mm.From = addr;
                    //            mm.Sender = addr;
                    //            mm.To.Add(data.Email);
                    //            mm.Subject = JustDialSettings.Subject;
                    //            var msg = JustDialSettings.EmailTemplate;
                    //            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    //            msg = msg.Replace("#customername", data.Name.IsNullOrEmpty() ? "Customer" : data.Name);
                    //            mm.Body = msg;

                    //            if (JustDialSettings.Attachment != null)
                    //            {
                    //                JArray att = JArray.Parse(JustDialSettings.Attachment);
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

                    //if (JustDialSettings.AutoSms.Value == true && !data.Mobile.IsNullOrEmpty())
                    //{
                    //    String msg = JustDialSettings.SMSTemplate;
                    //    String tempId = JustDialSettings.SmsTemplateId;
                    //    msg = msg.Replace("#customername", data.Name.IsNullOrEmpty() ? "Customer" : data.Name);
                    //    data.Mobile = data.Mobile.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                    //    SMSHelper.SendSMS(data.Mobile, msg, tempId);
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
            var br = UserRow.Fields;
            var UData = new UserRow();

            List<JustDialDetailsRow> data1;
            var ind1 = JustDialDetailsRow.Fields;

            using (var connection = _connections.NewFor<JustDialDetailsRow>())
            {

                data1 = connection.List<JustDialDetailsRow>(q => q
                  .SelectTableFields()
                  .Select(ind1.Name)
                  .Select(ind1.Mobile)
                  .Select(ind1.Landline)
                  .Select(ind1.Email)
                  .Select(ind1.DateTime)
                  .Select(ind1.City)
                  .Select(ind1.Area)
                  .Select(ind1.Category)
                  .Select(ind1.Feedback)
                  .Where(ind1.Id.In(request.EnqIds)) // Include only selected rows
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

                    string str = "INSERT INTO Contacts(ContactType,CustomerType,Name,Phone,ResidentialPhone,Email,Address,OwnerId,AssignedId) VALUES('1','1','" + item.Name + "','" + item.Mobile + "','" + item.Landline + "','" + item.Email + "','" + item.Area + "','" + UserId + "','" + UserId + "')";

                    connection.Execute(str);

                    var c = ContactsRow.Fields;
                    var LastContact = connection.First<ContactsRow>(l => l
                        .Select(c.Id)
                        .Select(c.Name)
                        .OrderBy(c.Id, desc: true)
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
                        .Where((s.Source == "justdial") || (s.Source == "JustDial") || (s.Source == "Just Dial") || (s.Source == "Just dial") || (s.Source == "justdial"))
                        );

                    if (sourceMaster == null)
                    {
                        response.Status = "Error: JustDial Source not found in Source master\nKindly add in masters and try again";

                        throw new Exception("JustDial Source not found in Source master\nKindly add in masters and try again");
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

                        throw new Exception(" Enquiry Stage not found in Stage master\nKindly add in masters and try again");
                    }

                    GetNextNumberResponse nextNumber = new EnquiryController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(item.DateTime).ToString("yyyy-MM-dd");
                    string feedback = item.Category + "Feedback " + item.Feedback;
                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,AdditionalInfo,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + feedback + "','" + sourceMaster.Id + "','" + stageMaster.Id + "','" + UserId + "','" + UserId + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','1')";

                    connection.Execute(str1);

                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(c.Id, desc: true)
                        );

                    connection.Execute("Update JustDialDetails SET IsMoved = 1 WHERE Id = " + item.Id);


                    var JustDialSettings = new JustDialConfigurationRow();

                    var i = JustDialConfigurationRow.Fields;
                    JustDialSettings = connection.First<JustDialConfigurationRow>(l => l
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

                    if (JustDialSettings.AutoEmail.Value == true && !item.Email.IsNullOrEmpty())
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
                            if (JustDialSettings.Host != null)
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(JustDialSettings.EmailId, JustDialSettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(item.Email);
                                mm.Subject = JustDialSettings.Subject;
                                var msg = JustDialSettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", item.Name.IsNullOrEmpty() ? "Customer" : item.Name);
                                mm.Body = msg;

                                if (JustDialSettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(JustDialSettings.Attachment);
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

                                EmailHelper.Send(mm, JustDialSettings.EmailId, JustDialSettings.EmailPassword, (Boolean)JustDialSettings.SSL, JustDialSettings.Host, JustDialSettings.Port.Value);
                            }
                            else
                            {
                                MailMessage mm = new MailMessage();
                                var addr = new MailAddress(User.EmailId, JustDialSettings.Sender);

                                mm.From = addr;
                                mm.Sender = addr;
                                mm.To.Add(item.Email);
                                mm.Subject = JustDialSettings.Subject;
                                var msg = JustDialSettings.EmailTemplate;
                                msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                                msg = msg.Replace("#customername", item.Name.IsNullOrEmpty() ? "Customer" : item.Name);
                                mm.Body = msg;

                                if (JustDialSettings.Attachment != null)
                                {
                                    JArray att = JArray.Parse(JustDialSettings.Attachment);
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

                    if (JustDialSettings.AutoSms.Value == true && !item.Mobile.IsNullOrEmpty())
                    {
                        String msg = JustDialSettings.SMSTemplate;
                        String tempId = "XXXXXX";
                        msg = msg.Replace("#customername", item.Name.IsNullOrEmpty() ? "Customer" : item.Name);
                        item.Mobile = item.Mobile.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                        SMSHelper.SendSMS(item.Mobile, msg,tempId);
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
    }
}
