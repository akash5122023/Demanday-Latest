
namespace AdvanceCRM.ThirdParty.Endpoints
{
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System.Data;
    
    using OfficeOpenXml;
    using Serenity.Reporting;
    using Serenity.Web;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using AdvanceCRM.Web.Helpers;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Enquiry.Endpoints;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;    
    using AdvanceCRM.ThirdParty;
    using MyRepository = Repositories.RawTelecallRepository;
    using MyRow = RawTelecallRow;
    using AdvanceCRM.Modules.ThirdParty.IVRDetails;
    using Serenity.Extensions.DependencyInjection;

    [Route("Services/ThirdParty/RawTelecall/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class RawTelecallController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IWebHostEnvironment _env;

        public RawTelecallController(ISqlConnections connections, IWebHostEnvironment env)
        {
            _connections = connections;
            _env = env;
        }

        public RawTelecallController()
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

        [HttpPost, ServiceAuthorize("Telecall:Import")]
        public ActionResult DownloadTemplate(IDbConnection connection, RetrieveRequest request)
        {
            var templateFile = Path.Combine(_env.ContentRootPath, "Templates", "Telecall_Template.xlsx");
            byte[] bytes = System.IO.File.ReadAllBytes(templateFile);

            var output = File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Telecall_Template.xlsx");
            return output;
        }

        [HttpPost]
        public StandardResponse ClickToCall(IUnitOfWork uow, CallRequest request)
        {
            var response = new StandardResponse();

            response.Status = KnowlarityCall.ClickToCall(request.IVRNumber, request.AgentNumber, request.CustomerNumber);
            return response;
        }

        //Excel import
        [HttpPost, ServiceAuthorize("Telecall:Import")]
        public ExcelImportResponse ExcelImport(IUnitOfWork uow, ExcelImportWithUsersRequest request)
        {
            //var Template = new TelecallTemplateRow();

            //using (var connection = _connections.NewFor<ContactsRow>())
            //{
            //    var qt = EnquiryTemplateRow.Fields;
            //    Template = connection.TryFirst<EnquiryTemplateRow>(q => q
            //       .SelectTableFields()
            //       .Select(qt.SMSTemplate)
            //      .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
            //        );
            //}
           

            Check.NotNull(request, "request");
            Check.NotNullOrWhiteSpace(request.FileName, "filename");

            UploadHelper.CheckFileNameSecurity(request.FileName);

            if (!request.FileName.StartsWith("temporary/"))
                throw new ArgumentOutOfRangeException("filename");

            ExcelPackage ep = new ExcelPackage();
            using (var fs = System.IO.File.OpenRead(UploadHelper.DbFilePath(request.FileName)))
                ep.Load(fs);


            var e = MyRow.Fields;

            var response = new ExcelImportResponse();
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
                response.ErrorList.Add("Selected number of users are more than actual number of telecalling in Excel, hence aborting import");

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
                    var CompanyName = Convert.ToString(worksheet.Cells[row, 1].Value ?? "");
                    var Name = Convert.ToString(worksheet.Cells[row, 2].Value ?? "");

                  

                    if (Name.IsNullOrEmpty() == false)
                    {
                        var Phone = Convert.ToString(worksheet.Cells[row, 3].Value ?? "");
                        Phone = Phone.Trim();
                        Phone = Phone.Replace(" ", "");
                        Phone = Phone.Remove(0, Phone.Length - 10);

                        var Email = Convert.ToString(worksheet.Cells[row, 4].Value ?? "");
                        //var Address = Convert.ToString(worksheet.Cells[row, 4].Value ?? "");
                        var AdditionalInfo = Convert.ToString(worksheet.Cells[row, 5].Value ?? "");
                        var UserId = TuserId;

                        //  int comid = ((UserDefinition)Context.User.ToUserDefinition()).CompanyId;
                        var sc = RawTelecallRow.Fields;
                        var contact = uow.Connection.Count<RawTelecallRow>(sc.Phone == Phone);
                        if (contact >= 0)
                        {
                            using (var connection = _connections.NewFor<RawTelecallRow>())
                            {
                                string str = "INSERT INTO RawTelecall(CompanyName,Name,Phone,Email,CreatedBy,AssignedTo) VALUES('" + CompanyName + "','" + Name + "','" + Phone + "','" + Email + "','" + UserId + "','" + UserId + "')";

                                connection.Execute(str);
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
                }
                catch (Exception ex)
                {
                    response.ErrorList.Add("Exception on Row " + row + ": " + ex.Message);
                }
            }

            return response;
        }

        [ServiceAuthorize("Telecall:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.RawTelecallColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "Telecall_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        [HttpPost]
        public StandardResponse MoveToEnquiry(IUnitOfWork uow, StandardRequest request)
        {
            var response = new StandardResponse();
            EnquiryRow LastEnquiry;
            var br = UserRow.Fields;
            var Contacttyp = 2;
            var UData = new UserRow();

            var model = new MyRow();

            using (var connection = _connections.NewFor<MyRow>())
            {
                var ind = MyRow.Fields;
                model = connection.TryById<MyRow>(request.Id, q => q
                   .SelectTableFields()
                   .Select(ind.Name)
                   .Select(ind.Phone)
                   .Select(ind.Email)
                   .Select(ind.Details)
                   .Select(ind.CompanyName)                 
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
                    if (!(string.IsNullOrEmpty)(model.CompanyName))
                    {
                        Contacttyp = 2;
                    }
                    else
                    {
                        Contacttyp = 1;
                    }

                    var c = ContactsRow.Fields;
                    var LastContact = connection.TryFirst<ContactsRow>(l => l
                        .Select(c.Id)
                        .Select(c.Name)
                        .Where(c.Phone == model.Phone)
                        );

                    if (LastContact == null)
                    {
                        string str = "INSERT INTO Contacts(ContactType,CustomerType,Name,Phone,Email,Address,OwnerId,AssignedId) VALUES('"+Contacttyp+"','1','" + model.Name + "','" + model.Phone + "','" + model.Email + "','" + model.Details + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "')";

                        connection.Execute(str);

                        LastContact = connection.First<ContactsRow>(l => l
                            .Select(c.Id)
                            .Select(c.Name)
                            .OrderBy(c.Id, desc: true)
                            );

                        if (model.Name != LastContact.Name)
                        {
                            response.Status = "Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same";

                            throw new Exception("This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");
                        }
                    }

                    var s = SourceRow.Fields;
                    var Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where((s.Source == "TeleCall") || (s.Source == "Tele call") || (s.Source == "Telecall"))
                        );

                    if (Source == null)
                    {
                        string str2 = "INSERT INTO Source(Source) VALUES('TeleCall')";
                        connection.Execute(str2);

                        Source = connection.TryFirst<SourceRow>(l => l
                        .Select(s.Id)
                        .Select(s.Source)
                        .Where(s.Source == "TeleCall")
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
                        .Select(st.Id)
                        .Select(st.Stage)
                        .Where(st.Stage == "TeleCall")
                        );
                    }

                    GetNextNumberResponse nextNumber = new EnquiryController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                    string date = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
                    string feedback = model.Details ;

                    var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,AdditionalInfo,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + feedback + "','" + Source.Id + "','" + stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','" + UData.CompanyId + "')";

                    connection.Execute(str1);

                    var t = EnquiryRow.Fields;
                    var e = EnquiryRow.Fields;
                    LastEnquiry = connection.First<EnquiryRow>(l => l
                        .Select(e.Id)
                        .OrderBy(c.Id, desc: true)
                        );

                    connection.Execute("Update RawTelecall SET IsMoved = 1 WHERE Id = " + request.Id);


                    //var WebsiteEnquirySettings = new WebsiteEnquiryConfigurationRow();

                    //var i = WebsiteEnquiryConfigurationRow.Fields;
                    //WebsiteEnquirySettings = connection.First<WebsiteEnquiryConfigurationRow>(l => l
                    //.SelectTableFields()
                    //    .Select(i.AutoEmail)
                    //    .Select(i.AutoSms)
                    //    .Select(i.Sender)
                    //         .Select(i.Subject)
                    //         .Select(i.SMSTemplate)
                    //         .Select(i.EmailTemplate)
                    //         .Select(i.Host)
                    //         .Select(i.Port)
                    //         .Select(i.SSL)
                    //         .Select(i.EmailId)
                    //         .Select(i.EmailPassword)
                    //);

                    //if (WebsiteEnquirySettings.AutoEmail.Value == true && !model.Email.IsNullOrEmpty())
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
                    //        if (WebsiteEnquirySettings.Host != null)
                    //        {
                    //            MailMessage mm = new MailMessage();
                    //            var addr = new MailAddress(WebsiteEnquirySettings.EmailId, WebsiteEnquirySettings.Sender);

                    //            mm.From = addr;
                    //            mm.Sender = addr;
                    //            mm.To.Add(model.Email);
                    //            mm.Subject = WebsiteEnquirySettings.Subject;
                    //            var msg = WebsiteEnquirySettings.EmailTemplate;
                    //            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    //            msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
                    //            mm.Body = msg;

                    //            if (WebsiteEnquirySettings.Attachment != null)
                    //            {
                    //                JArray att = JArray.Parse(WebsiteEnquirySettings.Attachment);
                    //                foreach (var f in att)
                    //                {
                    //                    if (f["Filename"].HasValue())
                    //                    {
                    //                        mm.Attachments.Add(new Attachment(Server.MapPath("~/App_Data/upload/" + f["Filename"].ToString())));
                    //                    }
                    //                }
                    //            }

                    //            mm.IsBodyHtml = true;

                    //            EmailHelper.Send(mm, WebsiteEnquirySettings.EmailId, WebsiteEnquirySettings.EmailPassword, (Boolean)WebsiteEnquirySettings.SSL, WebsiteEnquirySettings.Host, WebsiteEnquirySettings.Port.Value);
                    //        }
                    //        else
                    //        {
                    //            MailMessage mm = new MailMessage();
                    //            var addr = new MailAddress(User.EmailId, WebsiteEnquirySettings.Sender);

                    //            mm.From = addr;
                    //            mm.Sender = addr;
                    //            mm.To.Add(model.Email);
                    //            mm.Subject = WebsiteEnquirySettings.Subject;
                    //            var msg = WebsiteEnquirySettings.EmailTemplate;
                    //            msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    //            msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
                    //            mm.Body = msg;

                    //            if (WebsiteEnquirySettings.Attachment != null)
                    //            {
                    //                JArray att = JArray.Parse(WebsiteEnquirySettings.Attachment);
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
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            throw new Exception(ex.Message.ToString());
                    //        }
                    //    }

                    //    if (WebsiteEnquirySettings.AutoSms.Value == true && !model.Phone.IsNullOrEmpty())
                    //    {
                    //        String msg = WebsiteEnquirySettings.SMSTemplate;
                    //        msg = msg.Replace("#customername", model.Name.IsNullOrEmpty() ? "Customer" : model.Name);
                    //        model.Phone = model.Phone.Replace("-", "").Replace("+91", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                    //        SMSHelper.SendSMS(model.Phone, msg);
                    //    }
                    //}
                    response.Id = LastEnquiry.Id.Value;
                    response.Status = "Success";
                }
            }
            catch (Exception ex)
            {
                response.Id = -1;
                response.Status = "Error\n" + ex.ToString();
            }

            return response;

        }
    }
}
