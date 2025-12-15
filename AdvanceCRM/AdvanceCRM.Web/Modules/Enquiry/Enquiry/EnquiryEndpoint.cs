
namespace AdvanceCRM.Enquiry.Endpoints
{
    using Administration;

    using MailChimp.Net;
    using Serenity;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Settings;
    using RestSharp;
    using System.Text;
    using System.Net.Http;
    using Serenity.Data;
    using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using Serenity.Extensions.DependencyInjection;
    using AdvanceCRM.Web.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Net.Mail;

    using Template;
    using MyRepository = Repositories.EnquiryRepository;
    using MyRow = EnquiryRow;
    using System.Threading.Tasks;
    using System.Linq;
    using Nito.AsyncEx;
    using System.Collections;
    using MailChimp.Net.Models;
    using OfficeOpenXml;
    using AdvanceCRM.Masters;
    using System.Configuration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Common;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Settings;
    using Newtonsoft.Json.Linq;
    using AdvanceCRM.Products;
    using AdvanceCRM.Modules.ThirdParty.IVRDetails;
    using AdvanceCRM.Quotation.Endpoints;
    using Microsoft.Extensions.Caching.Memory;
    using Serenity.Abstractions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    [Route("Services/Enquiry/Enquiry/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class EnquiryController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        private readonly IUserAccessor userAccessor;
        private readonly IPermissionService permissionService;
        private readonly IRequestContext requestContext;
        private readonly IMemoryCache memoryCache;
        private readonly ITypeSource typeSource;
        private readonly IUserRetrieveService userRetriever;
        private readonly MailChimpManager Manager;

        [ActivatorUtilitiesConstructor]
        public EnquiryController(
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
            this.userAccessor = userAccessor;
            this.permissionService = permissionService;
            this.requestContext = requestContext;
            this.memoryCache = memoryCache;
            this.typeSource = typeSource;
            this.userRetriever = userRetriever;
            _connections = connections;
            _configuration = configuration;
            _env = env;
            Manager = new MailChimpManager(assignKey());
            UploadHelper.Configure(configuration, env);
        }

        public EnquiryController()
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

        public ListResponse<MyRow> List(IDbConnection connection, EnquiryListRequest request)
        {
            return new MyRepository(Context).List(connection, request);
        }

        //Get 
        [HttpPost]
        public StandardResponse FBEnquiry(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var Modules = _configuration["AppSettings:Modules"];

            List<string> ver;
            ver = null;

            if (Modules != null)
            {
                ver = Modules.Split(',').ToList<string>();
            }

            if (ver.Contains("FBEnquiry"))
                response.Status = "Remove";
            else
                response.Status = "No";

            return response;
        }

        //Send Mail
        [HttpPost]
        public SendMailResponse SendMail(IUnitOfWork uow, SendMailRequest request)
        {
            var response = new SendMailResponse();

            var data = new EnquiryData();

            using (var connection = _connections.NewFor<EnquiryTemplateRow>())
            {
                var com = CompanyDetailsRow.Fields;
                data.Company = connection.TryFirst<CompanyDetailsRow>(q => q
                    .SelectTableFields()
                    .Select(com.MailToSubContacts)
                    .Select(com.MailToOrganisation)
                    .Where(com.Id == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId));


                var c = EnquiryTemplateRow.Fields;
                data.Template = connection.TryFirst<EnquiryTemplateRow>(q => q
                     .SelectTableFields()
                     .Select(c.Sender)
                     .Select(c.Subject)
                     .Select(c.EmailTemplate)
                     .Select(c.Host)
                     .Select(c.Port)
                     .Select(c.SSL)
                     .Select(c.EmailId)
                     .Select(c.EmailPassword)
                   .Where(c.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId));

                var u = UserRow.Fields;
                data.User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                    .SelectTableFields()
                    .Select(u.Phone)
                    .Select(u.Host)
                    .Select(u.Port)
                    .Select(u.SSL)
                    .Select(u.EmailId)
                    .Select(u.EmailPassword));


                var quo = EnquiryRow.Fields;

                data.Enquiry = connection.TryById<EnquiryRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(quo.ContactsName)
                   .Select(quo.ContactsPhone)
                   .Select(quo.ContactsEmail)
                   .Select(quo.ContactsContactType)
                   .Select(quo.ContactPersonEmail)
                   .Select(quo.ContactPersonName));

                var us = UserRow.Fields;
                data.User = connection.TryFirst<UserRow>(q => q
                      .SelectTableFields()
                      .Select(us.Phone)
                      .Where(us.DisplayName == Context.User.ToUserDefinition().DisplayName));
            }

            try
            {
                if (data.Template.Host != null)
                {
                    MailMessage mm = new MailMessage();
                    var addr = new MailAddress(data.Template.EmailId, data.Template.Sender);

                    mm.From = addr;
                    mm.Sender = addr;
                    bool Organisation = Convert.ToBoolean(data.Company.MailToOrganisation);
                    int contacttype = Convert.ToInt32(data.Enquiry.ContactsContactType);
                    if (contacttype == 2 && Organisation == true)
                    {
                        mm.To.Add(data.Enquiry.ContactsEmail);
                    }
                    else if (contacttype == 1)
                    {
                        mm.To.Add(data.Enquiry.ContactsEmail);
                    }
                    if (data.Company.MailToSubContacts == true)
                    {
                        if (data.Enquiry.ContactPersonEmail != null)
                        {
                            mm.To.Add(data.Enquiry.ContactPersonEmail);
                        }
                    }
                    mm.Subject = data.Template.Subject;
                    var msg = data.Template.EmailTemplate;
                    msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    // msg = msg.Replace("#usermobile", Context.User.ToUserDefinition().Phone);
                    msg = msg.Replace("#customername", data.Enquiry.ContactsName);
                    msg = msg.Replace("#Phone", data.User.Phone);
                    //msg = msg.Replace("#Phone", Context.User.ToUserDefinition().Phone);
                    mm.Body = msg;

                    if (data.Template.Attachment != null)
                    {
                        JArray att = JArray.Parse(data.Template.Attachment);
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

                    response.Status = EmailHelper.Send(mm, data.Template.EmailId, data.Template.EmailPassword, (Boolean)data.Template.SSL, data.Template.Host, data.Template.Port.Value);
                }
                else
                {
                    MailMessage mm = new MailMessage();
                    var addr = new MailAddress(data.User.EmailId, data.Template.Sender);

                    mm.From = addr;
                    mm.Sender = addr;
                    bool Organisation = Convert.ToBoolean(data.Company.MailToOrganisation);
                    int contacttype = Convert.ToInt32(data.Enquiry.ContactsContactType);
                    if (contacttype == 2 && Organisation == true)
                    {
                        mm.To.Add(data.Enquiry.ContactsEmail);
                    }
                    else if (contacttype == 1)
                    {
                        mm.To.Add(data.Enquiry.ContactsEmail);
                    }
                    if (data.Company.MailToSubContacts == true)
                    {
                        if (data.Enquiry.ContactPersonEmail != null)
                        {
                            mm.CC.Add(data.Enquiry.ContactPersonEmail);
                        }
                    }

                    mm.Subject = data.Template.Subject;
                    var msg = data.Template.EmailTemplate;
                    msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    msg = msg.Replace("#customername", data.Enquiry.ContactsName);
                    //msg = msg.Replace("#Phone", data.Enquiry.ContactsPhone);
                    msg = msg.Replace("#Phone", data.User.Phone);
                    mm.Body = msg;

                    if (data.Template.Attachment != null)
                    {
                        JArray att = JArray.Parse(data.Template.Attachment);
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

                    response.Status = EmailHelper.Send(mm, data.User.EmailId, data.User.EmailPassword, (Boolean)data.User.SSL, data.User.Host, data.User.Port.Value);
                }
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }

        //Send Whatsapp for SMS Sender
        [HttpPost]
        public StandardResponse SendWA(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new EnquiryData();

            using (var connection = _connections.NewFor<ContactsRow>())
            {
                var c = ContactsRow.Fields;

                data.Contact = connection.TryById<ContactsRow>(request.Id, q => q
                     .SelectTableFields()
                     .Select(c.Id)
                     .Select(c.Name)
                     .Select(c.Phone)
                     );

                var qt = EnquiryTemplateRow.Fields;
                data.Template = connection.TryFirst<EnquiryTemplateRow>(q => q
                   .SelectTableFields()
                   .Select(qt.SMSTemplate)
                  .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );

            }
            String msg = data.Template.SMSTemplate;

            msg = msg.Replace("#customername", data.Contact.Name);
            response.Status = WAHelper.SendWA(data.Contact.Phone, msg);
            return response;
        }
        //SendBulk Mail
        //[HttpPost, ServiceAuthorize("SMS:BulkMail")]
        //public BulkMailResponse SendBulkMail(BulkRequest request1)
        //{
        //    //this.rowSelection.getSelectedKeys()

        //    var response = new BulkMailResponse();
        //    MyRow contact;
        //    var br = UserRow.Fields;
        //    var UData = new UserRow();

        //    BizMailConfigRow Config;

        //    using (var connection = _connections.NewFor<BizMailConfigRow>())
        //    {

        //        UData = connection.First<UserRow>(q => q
        //     .SelectTableFields()
        //     .Select(br.CompanyId)
        //     .Where(br.UserId == Context.User.GetIdentifier())
        //    );

        //        var s = BizMailConfigRow.Fields;
        //        Config = connection.TryFirst<BizMailConfigRow>(q => q
        //            .SelectTableFields()
        //            .Select(s.Apiurl)
        //            .Select(s.Apikey)
        //            // .Where(s.CompanyId == Convert.ToInt32(UData.CompanyId))
        //            );
        //    }


        //    var client = new RestClient(Config.Apiurl + "/campaigns");
        //    var request = new RestRequest(RestSharp.Method.POST);            
        //    request.AddHeader("cache-control", "no-cache");
        //    request.AddHeader("x-mw-public-key", Config.Apikey);
        //    request.AddHeader("content-type", "application/x-www-form-urlencoded");
        //   // string encodedStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(Request.Entity.Name));
        //   // request.AddParameter("application/x-www-form-urlencoded", "campaign%5Bname%5D=" + Request.Entity.Name + "&campaign%5Btype%5D=autoresponder&campaign%5Bfrom_name%5D=" + Request.Entity.FromName + "&campaign%5Bfrom_email%5D=" + Request.Entity.FromEmail + "&campaign%5Bsubject%5D=" + Request.Entity.Subject + "&campaign%5Breply_to%5D=" + Request.Entity.ReplyTo + "&campaign%5Blist_uid%5D=" + Request.Entity.ListId + "&campaign%5BSend_at%5D=2022-07-18 10-12-22&campaign%5Btemplate%5D%5Bcontent%5D=" + encodedStr, ParameterType.RequestBody);

        //    // request.AddParameter("application/x-www-form-urlencoded", "campaign%5Bname%5D%3AAmit%0Acampaign%5Btype%5D%3Aautoresponder%0Acampaign%5Bfrom_name%5D%3Afname%0Acampaign%5Bfrom_email%5D%3Aamitk1116%40gmail.com%0Acampaign%5Bsubject%5D%3ASubject%0Acampaign%5Breply_to%5D%3Aamitk1116%40gmail.com%0Acampaign%5Blist_uid%5D%3Aot8811zhweca3%0Acampaign%5BSend_at%5D%3A2022-07-18%2010-12-22%0Acampaign%5Btemplate%5D%5Bcontent%5D%3AGood%20Morning", ParameterType.RequestBody);
        //    IRestResponse response1 = client.Execute(request);

        //    return response;
        //}

        //Send SMS for SMS Sender
        [HttpPost]
        public StandardResponse SendSMS(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new EnquiryData();

            using (var connection = _connections.NewFor<ContactsRow>())
            {
                var c = ContactsRow.Fields;

                data.Contact = connection.TryById<ContactsRow>(request.Id, q => q
                     .SelectTableFields()
                     .Select(c.Id)
                     .Select(c.Name)
                     .Select(c.Phone)
                     );

                var qt = EnquiryTemplateRow.Fields;
                data.Template = connection.TryFirst<EnquiryTemplateRow>(q => q
                    .SelectTableFields()
                    .Select(qt.SMSTemplate)
                    .Select(qt.TemplateId)
                   .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );

            }
            String msg = data.Template.SMSTemplate;
            String temId = data.Template.TemplateId;

            msg = msg.Replace("#customername", data.Contact.Name);
            response.Status = SMSHelper.SendSMS(data.Contact.Phone, msg, temId);
            return response;
        }

        [HttpPost]
        public StandardResponse SendWati(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new EnquiryData();
            var c = ContactsRow.Fields;

            using (var connection = _connections.NewFor<ContactsRow>())
            {
                data.Contact = connection.TryById<ContactsRow>(request.Id, q => q
                     .SelectTableFields()
                     .Select(c.Id)
                     .Select(c.Name)
                     .Select(c.Whatsapp)
                     .Select(c.Phone)
                     );

                var qt = EnquiryTemplateRow.Fields;
                data.Template = connection.TryFirst<EnquiryTemplateRow>(q => q
                   .SelectTableFields()
                   .Select(qt.WaTemplate)
                   .Select(qt.WaTemplateId)
                  .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );

            }
            string con = data.Contact.Whatsapp;
            if (data.Contact.Whatsapp == null)
            {
                con = data.Contact.Phone;
            }

            String temId = data.Template.WaTemplateId;

            response.Status = WAHelper.SendBizWA(con, temId);
            return response;
        }

        [HttpPost, ServiceAuthorize("SMS:BulkSMS")]
        public StandardResponse SendBulkSMS(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            MyRow contact;

            var e = MyRow.Fields;

            using (var connection = _connections.NewFor<MyRow>())
            {
                contact = connection.TryById<MyRow>(request.Id, q => q
                    .Select(e.Id)
                    .Select(e.ContactsName)
                    .Select(e.ContactsPhone)
                );
            }

            String msg = request.SMSType;
            String TempId = request.TemplateID;

            if (msg.Trim() == "#customername token can be used in message to auto add customers name")
            {
                throw new Exception("It seems you havent chnaged the default text, hence sending SMS is been cancelled");
            }

            var newmsg = msg.Replace("#customername", contact.ContactsName);

            response.Status = SMSHelper.SendSMS(contact.ContactsPhone, newmsg, TempId);

            if (!response.Status.Contains(("SMS").ToUpper()))
            {
                throw new Exception("SMS Sending Failed!");
            }

            response.Status = "Successfull!!!";

            return response;
        }

        [HttpPost, ServiceAuthorize("SMS:BulkMail")]
        public StandardResponse SendBulkMail(IUnitOfWork uow, SendEmailRequest request)
        {
            var response = new StandardResponse();

            MyRow contact;

            var e = MyRow.Fields;

            using (var connection = _connections.NewFor<MyRow>())
            {
                contact = connection.TryById<MyRow>(request.Id, q => q
                    .Select(e.Id)
                    .Select(e.ContactsName)
                    .Select(e.ContactsPhone)
                    .Select(e.ContactsEmail)
                );
            }
            var to = contact.ContactsEmail;
            var msg = request.EmailType;
            var sub = request.Subject;
            DateTime dt = request.Senddate;

            List<string> Records = new List<string>();

            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);

            var newmsg = msg.Replace("#customername", contact.ContactsName);

            response.Status = MailHelper.SendBulkMail(contact.ContactsName, to, sub, newmsg, dt);


            //  response.Status = "Successfull!!!";

            return response;
        }

        [HttpPost, ServiceAuthorize("Enquiry:Import")]
        public ActionResult DownloadTemplate(IDbConnection connection, RetrieveRequest request)
        {
            string templateFile = Path.Combine(_env.ContentRootPath, "Templates", "Enquiries_Template.xlsx");
            byte[] bytes = System.IO.File.ReadAllBytes(templateFile);

            var Output = File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Enquiries_Template.xlsx");
            return Output;
        }

        //Excel import
        [HttpPost, ServiceAuthorize("Enquiry:Import")]
        public ExcelImportResponse ExcelImport(IUnitOfWork uow, ExcelImportWithUsersRequest request)
        {
            var Template = new EnquiryTemplateRow();

            using (var connection = _connections.NewFor<ContactsRow>())
            {
                var qt = EnquiryTemplateRow.Fields;
                Template = connection.TryFirst<EnquiryTemplateRow>(q => q
                    .SelectTableFields()
                    .Select(qt.SMSTemplate)
                   .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );
            }

            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.FileName))
                throw new ArgumentNullException(nameof(request.FileName));

            // validate file name
            UploadHelper.CheckFileNameSecurity(request.FileName);

            if (!request.FileName.StartsWith("temporary/", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentOutOfRangeException("filename");

            string physicalPath = UploadHelper.DbFilePath(request.FileName);

            ExcelPackage ep = new ExcelPackage();
            using (var fs = new FileStream(physicalPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ep.Load(fs);
            }


            var e = MyRow.Fields;
            var response = new ExcelImportResponse();
            response.Inserted = 0;
            response.Updated = 0;
            response.ErrorList = new List<string>();

            var worksheet = ep.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new ValidationError("Uploaded excel file does not contain any worksheet");


            int NoOfUsrs = request.UIds.Count();

            if (NoOfUsrs < 1)
            {
                response.ErrorList.Add("No users selected");

                return response;
            }

            int NoOfEnquiries = worksheet.Dimension.Rows;

            if (NoOfUsrs > NoOfEnquiries)
            {
                response.ErrorList.Add("Selected number of users are more than actual number of enquiries in Excel, hence aborting import");

                return response;
            }

            int recordPerUser = NoOfEnquiries / NoOfUsrs;

            int counter = 0;
            int currRecCount = 1;
            string TuserId = request.UIds.ElementAt(counter);

            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                try
                {
                    var Name = ((worksheet.Cells[row, 1].Value ?? "")).ToString();
                    if (Name.IsNullOrEmpty() == false)
                    {
                        var Phone = Convert.ToString(worksheet.Cells[row, 2].Value ?? "");
                        Phone = Phone.Trim();
                        Phone = Phone.Replace(" ", "");
                        Phone = Phone.Remove(0, Phone.Length - 10);

                        var Email = Convert.ToString(worksheet.Cells[row, 3].Value ?? "");
                        var Address = Convert.ToString(worksheet.Cells[row, 4].Value ?? "");
                        var AdditionalInfo = Convert.ToString(worksheet.Cells[row, 5].Value ?? "");
                        var UserId = TuserId;

                        int comid = ((UserDefinition)Context.User.ToUserDefinition()).CompanyId;


                        using (var connection = _connections.NewFor<ContactsRow>())
                        {
                            string str = "INSERT INTO Contacts(ContactType,Name,Phone,Email,Address,OwnerId,AssignedId) VALUES('1'," + "N'" + Name + "','" + Phone + "','" + Email + "','" + Address + "','" + UserId + "','" + UserId + "')";

                            connection.Execute(str);

                            var c = ContactsRow.Fields;
                            var LastContact = connection.TryFirst<ContactsRow>(l => l
                                .Select(c.Id)
                                .Select(c.Name)
                                .OrderBy(c.Id, desc: true)
                                );

                            if (Name != LastContact.Name)
                            {
                                response.ErrorList.Add("Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");

                                return response;
                            }

                            var s = SourceRow.Fields;
                            var Source = connection.TryFirst<SourceRow>(l => l
                                .Select(s.Id)
                                .Select(s.Source)
                                .Where((s.Source == "Excel") || (s.Source == "excel"))
                                );

                            if (Source == null)
                            {
                                response.ErrorList.Add("Error: Excel Source not found in Source master\nKindly add in masters and try again");

                                return response;
                            }

                            GetNextNumberResponse nextNumber = GetNextNumber(uow.Connection, new GetNextNumberRequest());
                            var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,AdditionalInfo,SourceId,StageId,OwnerId,AssignedId,CompanyId,EnquiryNo,EnquiryN) VALUES('" + LastContact.Id + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1','" + AdditionalInfo + "','" + Source.Id + "','1','" + UserId + "','" + UserId + "'," + comid + "," + nextNumber.Serial + ",'" + nextNumber.SerialN + "')";

                            connection.Execute(str1);

                            if (currRecCount == recordPerUser)
                            {
                                if ((counter < NoOfUsrs) && (counter != (NoOfUsrs - 1)))
                                {
                                    TuserId = request.UIds.ElementAt(counter + 1);
                                    currRecCount = 0;
                                }
                                counter++;
                            }
                        }
                        currRecCount++;

                        response.Inserted = response.Inserted + 1;
                    }
                }
                catch (Exception ex)
                {
                    response.ErrorList.Add("Exception on Row " + row + ": " + ex.Message);
                }
            }

            return response;
        }

        [ServiceAuthorize("Enquiry:Export")]
        public FileContentResult ListExcel(IDbConnection connection, EnquiryListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.EnquiryColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "Enquiry_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        //MoveToQuotation
        [HttpPost]
        [ServiceAuthorize("Enquiry:Move to Quotation")]
        public StandardResponse MoveToQuotation(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();

            var exist = new QuotationRow();
            var i = QuotationRow.Fields;
            exist = uow.Connection.TryFirst<QuotationRow>(q => q
            .SelectTableFields()
            .Select(i.Id)
            .Where(i.EnquiryNo == request.Id));

            if (exist != null)
            {
                response.Id = exist.Id.Value;
                response.Status = "Already Moved!";
                return response;
            }

            var data = new EnquiryData();

            var enq = EnquiryRow.Fields;
            data.Enquiry = uow.Connection.TryById<EnquiryRow>(request.Id, q => q
               .SelectTableFields()
               .Select(enq.ContactsId)
               .Select(enq.ContactPersonId)
               .Select(enq.Type)
               .Select(enq.AdditionalInfo)
               .Select(enq.SourceId)
               .Select(enq.StageId)
               .Select(enq.BranchId)
               .Select(enq.CompanyId)
               .Select(enq.OwnerId)
               .Select(enq.AssignedId)
               .Select(enq.ReferenceName)
               .Select(enq.ReferencePhone)
               .Select(enq.LostReason)
               .Select(enq.EnquiryN)
               .Select(enq.Status)
               .Select(enq.Date)
               );

            var enqp = EnquiryProductsRow.Fields;
            data.EnquiryProducts = uow.Connection.List<EnquiryProductsRow>(q => q
                .SelectTableFields()
                .Select(enqp.ProductsId)
                .Select(enqp.Quantity)
                .Select(enqp.Mrp)
                .Select(enqp.Capacity)
                .Select(enqp.Unit)
                .Select(enqp.SellingPrice)
                .Select(enqp.Price)
                .Select(enqp.Discount)
                .Select(enqp.Description)
                .Where(enqp.EnquiryId == request.Id)
                );

            var enqma = MultiRepEnquiryRow.Fields;
            data.MultiAssign = uow.Connection.List<MultiRepEnquiryRow>(q => q
                .SelectTableFields()
                .Select(enqma.Id)
                .Select(enqma.AssignedId)
                .Where(enqp.EnquiryId == request.Id)
                );

            var cmp = CompanyDetailsRow.Fields;
            data.Company = uow.Connection.TryById<CompanyDetailsRow>(1, q => q
                .SelectTableFields()
                .Select(cmp.AllowMovingNonClosedRecords)
                );

            if (data.Company.AllowMovingNonClosedRecords != true)
            {
                if (data.Enquiry.Status == (Masters.StatusMaster)1)
                {
                    throw new Exception("Please set the status of this Enquiry as closed, Pending before moving to Quotation");
                }
            }

            // try
            {
                using (var connection = _connections.NewFor<QuotationRow>())
                {
                    dynamic typ, brnh, subcontact;

                    if (data.Enquiry.Type != null)
                        typ = (int)data.Enquiry.Type.Value;
                    else
                        typ = "null";

                    if (data.Enquiry.BranchId != null)
                        brnh = Convert.ToString(data.Enquiry.BranchId.Value);
                    else
                        brnh = "null";

                    if (data.Enquiry.ContactPersonId != null)
                        subcontact = (int)data.Enquiry.ContactPersonId.Value;
                    else
                        subcontact = "null";

                    GetNextNumberResponse nextNumber = new QuotationController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    String str = "INSERT INTO Quotation(QuotationNo,CompanyId,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,BranchId,OwnerId,AssignedId,ReferenceName,ReferencePhone, EnquiryNo, EnquiryDate,QuotationN,EnquiryN,ContactPersonId) VALUES(" + nextNumber.Serial + ",'" + Convert.ToString(data.Enquiry.CompanyId.Value) + "','" + Convert.ToString(data.Enquiry.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",N'" + data.Enquiry.AdditionalInfo + "','" + Convert.ToString(data.Enquiry.SourceId) + "','" + Convert.ToString(data.Enquiry.StageId.Value) + "'," + brnh + ",'" + Convert.ToString(data.Enquiry.OwnerId.Value) + "','" + Convert.ToString(data.Enquiry.AssignedId.Value) + "','" + data.Enquiry.ReferenceName + "','" + data.Enquiry.ReferencePhone + "'," + request.Id + ",'" + data.Enquiry.Date.Value.ToString("yyyy-MM-dd") + "','" + nextNumber.SerialN + "','" + data.Enquiry.EnquiryN + "'," + subcontact + ")";
                    //str = str.Replace("\n", " char(13) ");
                    connection.Execute(str);

                    var quo = QuotationRow.Fields;
                    data.LastQuot = connection.TryFirst<QuotationRow>(l => l
                    .Select(quo.Id)
                    .Select(quo.ContactsId)
                    .OrderBy(quo.Id, desc: true)
                    );
                }

                if (data.Enquiry.ContactsId == data.LastQuot.ContactsId)
                {
                    //throw new Exception("Something went wrong while generating Quotation from Enquiry\nOnly Quotation got generated, products are not copied");
                    using (var connection = _connections.NewFor<MultiRepQuotationRow>())
                    {
                        foreach (var item1 in data.MultiAssign)
                        {
                            String str1 = "INSERT INTO MultiRepQuotation(AssignedId,QuotationId) VALUES('" + Convert.ToString(item1.AssignedId.Value) + "','" + Convert.ToString(data.LastQuot.Id.Value) + "')";

                            connection.Execute(str1);
                        }
                    }
                }
                if (data.Enquiry.ContactsId == data.LastQuot.ContactsId)
                {
                    //throw new Exception("Something went wrong while generating Quotation from Enquiry\nOnly Quotation got generated, products are not copied");
                    using (var connection = _connections.NewFor<QuotationProductsRow>())
                    {
                        foreach (var item in data.EnquiryProducts)
                        {
                            var pro = ProductsRow.Fields;
                            var enqproduct = connection.TryById<ProductsRow>(item.ProductsId.Value, l => l
                            .Select(pro.TaxId1)
                            .Select(pro.TaxId1Type)
                            .Select(pro.TaxId1Percentage)
                            .Select(pro.TaxId2)
                            .Select(pro.TaxId2Type)
                            .Select(pro.TaxId2Percentage));

                            dynamic capacity;
                            if (item.Capacity != null)
                                capacity = Convert.ToString(item.Capacity);
                            else
                                capacity = "null";



                            String str = "INSERT INTO QuotationProducts(ProductsId,Capacity,Quantity,MRP,SellingPrice,Price,Unit,Discount,TaxType1,Percentage1,TaxType2,Percentage2,QuotationId,DiscountAmount,Description) VALUES('" + Convert.ToString(item.ProductsId.Value) + "','" + capacity + "','" + Convert.ToString(item.Quantity.Value) + "','" + Convert.ToString(item.Mrp.Value) + "','" + Convert.ToString(item.SellingPrice.Value) + "','" + Convert.ToString(item.Price.Value) + "','" + item.Unit + "','" + Convert.ToString(item.Discount.Value) + "','" + Convert.ToString(enqproduct.TaxId1Type) + "','" + Convert.ToString(enqproduct.TaxId1Percentage) + "','" + Convert.ToString(enqproduct.TaxId2Type) + "','" + Convert.ToString(enqproduct.TaxId2Percentage) + "','" + Convert.ToString(data.LastQuot.Id.Value) + "','0','" + item.Description + "')";

                            connection.Execute(str);
                        }
                    }
                }

                response.Id = data.LastQuot.Id.Value;
                response.Status = "Enquiry moved to Quotation successfully";
            }
            //catch (Exception ex)
            //{
            //    response.Status = ex.Message.ToString();
            //}

            return response;
        }


        [HttpPost]
        [ServiceAuthorize("Enquiry:Move To Enquiry")]
        public StandardResponse MoveToEnquiry(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();

            var data = new EnquiryData();

            var enq = EnquiryRow.Fields;
            data.Enquiry = uow.Connection.TryById<EnquiryRow>(request.Id, q => q
               .SelectTableFields()
               .Select(enq.ContactsId)
               .Select(enq.ContactPersonId)
               .Select(enq.Type)
               .Select(enq.AdditionalInfo)
               .Select(enq.SourceId)
               .Select(enq.StageId)
               .Select(enq.BranchId)
               .Select(enq.OwnerId)
             .Select(enq.CompanyId)
               .Select(enq.AssignedId)
               .Select(enq.ReferenceName)
               .Select(enq.ReferencePhone)
               .Select(enq.LostReason)
               .Select(enq.EnquiryN)
               .Select(enq.Status)
               .Select(enq.Date)
               );

            var enqp = EnquiryProductsRow.Fields;
            data.EnquiryProducts = uow.Connection.List<EnquiryProductsRow>(q => q
                .SelectTableFields()
                .Select(enqp.ProductsId)
                .Select(enqp.Quantity)
                .Select(enqp.Mrp)
                .Select(enqp.Capacity)
                .Select(enqp.Unit)
                .Select(enqp.SellingPrice)
                .Select(enqp.Price)
                .Select(enqp.Discount)
                .Select(enqp.Description)
                .Where(enqp.EnquiryId == request.Id)
                );

            var enqma = MultiRepEnquiryRow.Fields;
            data.MultiAssign = uow.Connection.List<MultiRepEnquiryRow>(q => q
                  .SelectTableFields()
                  .Select(enqma.Id)
                  .Select(enqma.AssignedId)
                  .Where(enqp.EnquiryId == request.Id)
                );

            var cmp = CompanyDetailsRow.Fields;
            data.Company = uow.Connection.TryById<CompanyDetailsRow>(1, q => q
                .SelectTableFields()
                .Select(cmp.AllowMovingNonClosedRecords)
                );


            {
                using (var connection = _connections.NewFor<QuotationRow>())
                {
                    dynamic typ, brnh, subcontact;

                    if (data.Enquiry.Type != null)
                        typ = (int)data.Enquiry.Type.Value;
                    else
                        typ = "null";

                    if (data.Enquiry.BranchId != null)
                        brnh = Convert.ToString(data.Enquiry.BranchId.Value);
                    else
                        brnh = "null";

                    if (data.Enquiry.ContactPersonId != null)
                        subcontact = (int)data.Enquiry.ContactPersonId.Value;
                    else
                        subcontact = "null";

                    GetNextNumberResponse nextNumber = GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    String str = "INSERT INTO Enquiry(EnquiryNo,ContactsId,Date,Status,Type,AdditionalInfo,SourceId,StageId,BranchId,OwnerId,AssignedId,ReferenceName,ReferencePhone,EnquiryN,ContactPersonId,CompanyId) VALUES(" + nextNumber.Serial + ",'" + Convert.ToString(data.Enquiry.ContactsId.Value) + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','1'," + typ + ",N'" + data.Enquiry.AdditionalInfo + "','" + Convert.ToString(data.Enquiry.SourceId) + "','" + Convert.ToString(data.Enquiry.StageId.Value) + "'," + brnh + ",'" + Convert.ToString(data.Enquiry.OwnerId.Value) + "','" + Convert.ToString(data.Enquiry.AssignedId.Value) + "','" + data.Enquiry.ReferenceName + "','" + data.Enquiry.ReferencePhone + "','" + nextNumber.SerialN + "','" + Convert.ToString(data.Enquiry.ContactPersonId.Value) + "','" + data.Enquiry.CompanyId + "')";
                    //str = str.Replace("\n", " char(13) ");
                    connection.Execute(str);

                    var quo = EnquiryRow.Fields;
                    data.Enquiry = connection.TryFirst<EnquiryRow>(l => l
                    .Select(quo.Id)
                    .Select(quo.ContactsId)
                    .OrderBy(quo.Id, desc: true)
                    );
                }


                if (data.Enquiry.ContactsId == data.Enquiry.ContactsId)
                {
                    //throw new Exception("Something went wrong while generating Quotation from Enquiry\nOnly Quotation got generated, products are not copied");
                    using (var connection = _connections.NewFor<EnquiryProductsRow>())
                    {
                        foreach (var item in data.EnquiryProducts)
                        {
                            var pro = ProductsRow.Fields;
                            var enqproduct = connection.TryById<ProductsRow>(item.ProductsId.Value, l => l
                            .Select(pro.TaxId1)
                            .Select(pro.TaxId1Type)
                            .Select(pro.TaxId1Percentage)
                            .Select(pro.TaxId2)
                            .Select(pro.TaxId2Type)
                            .Select(pro.TaxId2Percentage));

                            dynamic capacity;
                            if (item.Capacity != null)
                                capacity = Convert.ToString(item.Capacity);
                            else
                                capacity = "null";

                            String str = "INSERT INTO EnquiryProducts(ProductsId,Capacity,Quantity,MRP,SellingPrice,Price,EnquiryId,Description) VALUES('" + Convert.ToString(item.ProductsId.Value) + "','" + capacity + "','" + Convert.ToString(item.Quantity.Value) + "','" + Convert.ToString(item.Mrp.Value) + "','" + Convert.ToString(item.SellingPrice.Value) + "','" + Convert.ToString(item.Price.Value) + "','" + Convert.ToString(data.Enquiry.Id.Value) + "','" + item.Description + "')";

                            connection.Execute(str);
                        }
                    }
                }

                response.Id = data.Enquiry.Id.Value;
                response.Status = "Enquiry Cloned successfully";
            }


            return response;
        }

        //Add to MailChimp

        private string assignKey()
        {
            MailChimpConfigurationRow MCConfig;

            using (var connection = _connections.NewFor<MailChimpConfigurationRow>())
            {
                var s = MailChimpConfigurationRow.Fields;
                MCConfig = connection.TryById<MailChimpConfigurationRow>(1, q => q
                .SelectTableFields()
                .Select(s.ApiKey)
                );
            }

            return MCConfig.ApiKey;
        }

        private async Task<IEnumerable> ListDetails()
        {
            var listoptions = new MailChimp.Net.Core.ListRequest
            {
                Limit = 1000
            };
            var ModelList = await Manager.Lists.GetAllAsync(listoptions);
            var NewList = ModelList.OrderBy(x => x.Stats.MemberCount).ToList();
            return ViewBag.ListData = NewList;
        }


        [HttpPost]
        public MailChimpResponse AddToMailChimp(MailChimpRequest request)
        {
            var response = new MailChimpResponse();

            List<EnquiryRow> contacts;
            List<EnquiryRow> contacts1 = new List<MyRow>();


            var e = EnquiryRow.Fields;

            using (var connection = _connections.NewFor<EnquiryRow>())
            {
                contacts = connection.List<EnquiryRow>(q => q
                    .Select(e.Id)
                    .Select(e.ContactsName)
                    .Select(e.ContactsEmail)
                    .Where(e.ContactsEmail.IsNotNull())
                );
            }

            foreach (var item in request.MailChimpIds)
            {
                contacts1.Add(contacts.Find(d => d.Id.ToString() == item));
            }

            contacts1.RemoveAll(x => x == null);


            var result1 = AsyncContext.Run(() => ListDetails());

            var EnquiryListId = "";

            foreach (var item in ViewBag.ListData)
            {
                if (item.Name == request.ListName.Trim())
                {
                    EnquiryListId = item.Id;
                    break;
                }
            }

            if (EnquiryListId.IsNullOrEmpty())
            {
                response.MailChimpReturnResponse = "List '" + request.ListName + "' not found, please create this list in MailChimp section";
                return response;
            }

            foreach (var item in contacts1)
            {
                //Add mailchimp sync code here for this list to add it to MailChimp Contact List

                //Id of contacts list

                try
                {
                    var member = new Member
                    {
                        EmailAddress = item.ContactsEmail,
                        Status = Status.Subscribed,
                        StatusIfNew = Status.Subscribed,
                        ListId = EnquiryListId,
                        EmailType = "html",
                        IpSignup = HttpContext.Connection.RemoteIpAddress.ToString(),
                        TimestampSignup = DateTime.UtcNow.ToString("s"),
                        MergeFields = new Dictionary<string, object>
                {
                    {"FNAME", item.ContactsName },
                    {"LNAME","" }
                }
                    };
                    var result = AsyncContext.Run(() => Manager.Members.AddOrUpdateAsync(EnquiryListId, member));
                }
                catch (Exception ex)
                {

                    response.MailChimpReturnResponse = "Error\n" + ex.Message.ToString();
                }
            }

            if (contacts1.Count() > 0)
            {
                response.MailChimpReturnResponse = "Success";
            }
            else
            {
                response.MailChimpReturnResponse = "In selected list no valid email Id where found";
            }

            return response;
        }

        [HttpPost]
        public StandardResponse ClickToCall(IUnitOfWork uow, CallRequest request)
        {
            var response = new StandardResponse();

            response.Status = KnowlarityCall.ClickToCall(request.IVRNumber, request.AgentNumber, request.CustomerNumber);
            return response;
        }

        public GetNextNumberResponse GetNextNumber(IDbConnection connection, GetNextNumberRequest request)
        {
            //if(up)
            var response = new GetNextNumberResponse();
            response.Serial = "1";
            try
            {
                var sl = MyRow.Fields;
                var data = new MyRow();
                var user = Serenity.Authorization.UserDefinition as UserDefinition;
                if (user == null)
                    return null;

                var companyId = user.CompanyId;

                var br1 = CompanyDetailsRow.Fields;
                var Bdata = new CompanyDetailsRow();
                Bdata = connection.First<CompanyDetailsRow>(q => q
                    .SelectTableFields()
                    .Select(br1.EnquiryPrefix)
                    .Select(br1.EnquirySuffix)
                    .Select(br1.YearInPrefix)
                    .Select(br1.EnqStartNo)
                    .Where(br1.Id == companyId)
                );
                // if (request.BranchId.Trim() != "")
                {
                    data = connection.TryFirst<MyRow>(q => q
                        .SelectTableFields()
                        .Select(sl.Id)
                        .Select(sl.EnquiryNo)
                        .Where(sl.CompanyId == companyId)
                        .OrderBy(sl.Id, desc: true)
                    );
                }


                //var year = Convert.ToString(DateTime.Now.Year);
                //// var year = yearF.Substring(yearF.Length );
                ////   var year1 = Convert.ToString(Convert.ToInt32(year) + 1);

                //var Ypre = year;//+ "" + year1;

                //if (data != null)
                //{
                //    response.SerialN = Bdata.EnquiryPrefix + "-" + Ypre + "-" + (data.EnquiryNo + 1).ToString();
                //    response.Serial = (data.EnquiryNo + 1).ToString();
                //}


                var Ypre = string.Empty;
                // int month = 2;
                int month = Convert.ToInt32(DateTime.Now.Month);
                var year = Convert.ToString(DateTime.Now.Year);

                if (Bdata.YearInPrefix == AdvanceCRM.Masters.YearInPrefix.Year)
                {
                    Ypre = year;
                }
                else if (Bdata.YearInPrefix == AdvanceCRM.Masters.YearInPrefix.FinacialYear)
                {
                    var yearF = year.Substring(year.Length - 2);
                    if (month >= 4)
                    {
                        var year1 = Convert.ToString(Convert.ToInt32(yearF) + 1);
                        Ypre = yearF + "" + year1;
                    }
                    else
                    {
                        var year1 = Convert.ToString(Convert.ToInt32(yearF) - 1);
                        Ypre = year1 + "" + yearF;
                    }
                }


                string stPre = string.Empty;
                string stsuf = string.Empty;
                if (Bdata.EnquiryPrefix != null)
                {
                    stPre = Bdata.EnquiryPrefix;
                }
                if (Bdata.EnquirySuffix != null)
                {
                    stsuf = Bdata.EnquirySuffix;
                }

                if (Bdata.EnqStartNo == null)
                {
                    if (data != null)
                    {

                        response.SerialN = stPre + "" + Ypre + "" + (data.EnquiryNo + 1).ToString() + "" + stsuf;

                        response.Serial = (data.EnquiryNo + 1).ToString();
                    }
                    else
                    {
                        response.SerialN = stPre + "" + Ypre + "" + (1).ToString() + "" + stsuf;

                        response.Serial = (1).ToString();
                    }
                }
                else
                {
                    if (data != null)
                    {

                        response.SerialN = stPre + "" + Ypre + "" + (Bdata.EnqStartNo).ToString() + "" + stsuf;

                        response.Serial = (Bdata.EnqStartNo).ToString();
                    }
                    else
                    {
                        response.SerialN = stPre + "" + Ypre + "" + (Bdata.EnqStartNo).ToString() + "" + stsuf;

                        response.Serial = (Bdata.EnqStartNo).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return response;
        }

    }

    public class EnquiryData
    {
        public ContactsRow Contact { get; set; }
        public EnquiryRow Enquiry { get; set; }

        public BizWaConfigRow Config { get; set; }
        //public EnquiryRow LastEnq { get; set; }
        public List<EnquiryProductsRow> EnquiryProducts { get; set; }

        public List<MultiRepEnquiryRow> MultiAssign { get; set; }
        public EnquiryTemplateRow Template { get; set; }
        public BulkMailConfigRow Template1 { get; set; }
        public UserRow User { get; set; }
        public QuotationRow LastQuot { get; set; }
        public EnquiryFollowupsRow EnquiryFollowups { get; set; }
        public CompanyDetailsRow Company { get; set; }
    }


}
