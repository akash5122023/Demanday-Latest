
namespace AdvanceCRM.Services.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Enquiry.Endpoints;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Modules.ThirdParty.IVRDetails;
    using AdvanceCRM.Services;
    using AdvanceCRM.Template;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Hosting;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    
    using MyRepository = Repositories.TeleCallingRepository;
    using MyRow = TeleCallingRow;
    using Serenity.Extensions.DependencyInjection;
    using System.Collections.Generic;
    using AdvanceCRM.Contacts.Repositories;
    using Serenity;
    using AdvanceCRM.Web.Helpers;
    using OfficeOpenXml;
    using System.Linq;

    [Route("Services/Services/TeleCalling/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class TeleCallingController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IWebHostEnvironment _env;

        public TeleCallingController(ISqlConnections connections, IRequestContext context, IWebHostEnvironment env)
        {
            _connections = connections;
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            _env = env;
        }

        public TeleCallingController()
            : this(Dependency.Resolve<ISqlConnections>(),
                  Dependency.Resolve<IRequestContext>(),
                  Dependency.Resolve<IWebHostEnvironment>())
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

        [HttpPost]
        public ActionResult DownloadTemplate(IDbConnection connection, RetrieveRequest request)
        {
            string templateFile = Path.Combine(_env.ContentRootPath, "Templates", "Telecall_Template.xlsx");
            byte[] bytes = System.IO.File.ReadAllBytes(templateFile);

            var output = File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Telecall_Template.xlsx");
            return output;
        }

        [HttpPost, ServiceAuthorize("TeleCalling:Import")]
        public ExcelImportResponse ExcelImport(IUnitOfWork uow, ExcelImportRequest request)
        {
            Check.NotNull(request, nameof(request));
            Check.NotNullOrWhiteSpace(request.FileName, "filename");

            UploadHelper.CheckFileNameSecurity(request.FileName);

            if (!request.FileName.StartsWith("temporary/"))
                throw new ArgumentOutOfRangeException("filename");

            ExcelPackage ep = new ExcelPackage();
            using (var fs = System.IO.File.OpenRead(UploadHelper.DbFilePath(request.FileName)))
                ep.Load(fs);

            var worksheet = ep.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
                throw new ValidationError("Uploaded excel file does not contain any worksheet");

            var response = new ExcelImportResponse();
            response.ErrorList = new List<string>();

            var contactsRepo = new ContactsRepository(Context, _connections);
            var teleRepo = new MyRepository(Context, _connections);

            using var connection = _connections.NewFor<ContactsRow>();

            var s = SourceRow.Fields;
            var source = connection.TryFirst<SourceRow>(q => q
                .Select(s.Id)
                .Where(s.Source == "TeleCall"));
            if (source == null)
            {
                connection.Execute("INSERT INTO Source(Source) VALUES('TeleCall')");
                source = connection.TryFirst<SourceRow>(q => q.Select(s.Id).Where(s.Source == "TeleCall"));
            }

            var st = StageRow.Fields;
            var stage = connection.TryFirst<StageRow>(q => q
                .Select(st.Id)
                .Where(st.Type == (Int32)StageTypeMaster.Service));
            if (stage == null)
            {
                connection.Execute($"INSERT INTO Stage(Stage, Type) VALUES('Initial', {(int)StageTypeMaster.Service})");
                stage = connection.TryFirst<StageRow>(q => q.Select(st.Id).Where(st.Type == (Int32)StageTypeMaster.Service));
            }

            int userId = Convert.ToInt32(Context.User.GetIdentifier());

            var c = ContactsRow.Fields;
            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                try
                {
                    var name = Convert.ToString(worksheet.Cells[row, 2].Value ?? "");
                    if (string.IsNullOrWhiteSpace(name))
                        continue;

                    var phone = Convert.ToString(worksheet.Cells[row, 3].Value ?? "");
                    phone = phone.Trim().Replace(" ", "");
                    if (phone.Length > 10)
                        phone = phone.Substring(phone.Length - 10);

                    var email = Convert.ToString(worksheet.Cells[row, 4].Value ?? "");
                    var address = Convert.ToString(worksheet.Cells[row, 5].Value ?? "");
                    var details = Convert.ToString(worksheet.Cells[row, 6].Value ?? "");

                    var contact = connection.TryFirst<ContactsRow>(q => q
                        .Select(c.Id)
                        .Where(c.Phone == phone));

                    if (contact == null)
                    {
                        contact = new ContactsRow
                        {
                            ContactType = CTypeMaster.Individual,
                            Name = name,
                            Phone = phone,
                            Email = email,
                            Address = address,
                            AdditionalInfo = details,
                            OwnerId = userId,
                            AssignedId = userId
                        };

                        var saveResponse = contactsRepo.Create(uow, new SaveRequest<ContactsRow>
                        {
                            Entity = contact
                        });
                        contact.Id = (int)saveResponse.EntityId;
                    }

                    var tele = new MyRow
                    {
                        ContactsId = contact.Id,
                        Feedback = details,
                        Status = AppointmentTypeMaster.Open,
                        SourceId = source?.Id,
                        StageId = stage?.Id,
                        Date = DateTime.Now,
                        RepresentativeId = userId,
                        AssignedTo = userId
                    };

                    teleRepo.Create(uow, new SaveRequest<MyRow> { Entity = tele });

                    response.Inserted++;
                }
                catch (Exception ex)
                {
                    response.ErrorList.Add($"Exception on Row {row}: {ex.Message}");
                }
            }

            return response;
        }

        //Send SMS
        [HttpPost]
        public StandardResponse SendSMS(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new TeleCallingData();

            using (var connection = _connections.NewFor<ContactsRow>())
            {

                var t = TeleCallingRow.Fields;
                data.TeleCalling = connection.TryById<TeleCallingRow>(request.Id, q => q
                     .SelectTableFields()
                     .Select(t.Id)
                     .Select(t.ContactsName)
                     .Select(t.ContactsPhone)
                     .Select(t.AppointmentDate)
                     .Select(t.RepresentativeDisplayName)
                     .Select(t.AssignedToDisplayName)
                     .Select(t.ProductsName)
                     .Select(t.AssignedToPhone)
                     );

                var qt = TeleCallingTemplateRow.Fields;
                data.Template = connection.TryFirst<TeleCallingTemplateRow>( q => q
                    .SelectTableFields()
                    .Select(qt.CustomerSms)
                     .Select(qt.CustTemplateId)
                    .Select(qt.CustomerReminderSMS)
                       .Select(qt.CustRTemplateId)
                    .Select(qt.ExecutiveSms)
                    .Select(qt.ExeTemplateId)
                    .Select(qt.ExecutiveReminderSMS)
                    .Select(qt.ExeRTemplateId)
                   .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );
            }
            if (data.TeleCalling == null)
            {
                response.Status = "TeleCalling record not found";
                return response;
            }

            if (data.Template == null)
            {
                response.Status = "SMS template not found";
                return response;
            }
            String msg = "";
            String tempId = "";

            if (request.SMSType == "Customer")
            {
                msg = data.Template.CustomerSms;
                tempId = data.Template.CustTemplateId;
            }
            else if (request.SMSType == "Executive")
            {
                msg = data.Template.ExecutiveSms;
                tempId = data.Template.ExeTemplateId;
            }
            else if (request.SMSType == "Executive Reminder")
            {
                msg = data.Template.ExecutiveReminderSMS;
                tempId = data.Template.ExeRTemplateId;
            }
            else if (request.SMSType == "Customer Reminder")
            {
                msg = data.Template.CustomerReminderSMS;
                tempId = data.Template.CustTemplateId;
            }

            msg = msg ?? string.Empty;
            msg = msg.Replace("#customername", data.TeleCalling.ContactsName);
            msg = msg.Replace("#product", data.TeleCalling.ProductsName);
            msg = msg.Replace("#executive", data.TeleCalling.AssignedToDisplayName);
            if (data.TeleCalling.AppointmentDate.HasValue)
            {
                msg = msg.Replace("#appointmentdate", data.TeleCalling.AppointmentDate.Value.ToShortDateString() + "-" + data.TeleCalling.AppointmentDate.Value.ToShortTimeString());
            }
            msg = msg.Replace("#username", data.TeleCalling.RepresentativeDisplayName);

            String phone = "";

            if (request.SMSType == "Customer" || request.SMSType == "Customer Reminder")
            {
                phone = data.TeleCalling.ContactsPhone;
            }
            else if (request.SMSType == "Executive" || request.SMSType == "Executive Reminder")
            {
                phone = data.TeleCalling.AssignedToPhone;
            }

            try
            {
                response.Status = SMSHelper.SendSMS(phone, msg,tempId);
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }

        //Send Mail
        [HttpPost]
        public SendMailResponse SendMail(IUnitOfWork uow, SendMailRequest request)
        {

            var response = new SendMailResponse();

            var data = new TeleCallingData();

            using (var connection = _connections.NewFor<EnquiryTemplateRow>())
            {
                var u = UserRow.Fields;
                data.User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                    .SelectTableFields()
                    .Select(u.Host)
                    .Select(u.Port)
                    .Select(u.SSL)
                    .Select(u.EmailId)
                    .Select(u.EmailPassword));


                var t = TeleCallingRow.Fields;
                data.TeleCalling = connection.TryById<TeleCallingRow>(request.Id, q => q
                     .SelectTableFields()
                     .Select(t.Id)
                     .Select(t.ContactsName)
                     .Select(t.ContactsPhone)
                     .Select(t.AppointmentDate)
                     .Select(t.RepresentativeDisplayName)
                     .Select(t.AssignedToDisplayName)
                     .Select(t.ProductsName)
                     .Select(t.AssignedToPhone)
                     .Select(t.ContactsEmail)
                     .Select(t.AssignedToEmail)
                     );

                var qt = TeleCallingTemplateRow.Fields;
                data.Template = connection.TryFirst<TeleCallingTemplateRow>( q => q
                    .SelectTableFields()
                    .Select(qt.CustomerEmail)
                    .Select(qt.ExecutiveEmail)
                    .Select(qt.Subject)
                    .Select(qt.From)
                   .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );
            }

            try
            {
                MailMessage mm = new MailMessage();
                var addr = new MailAddress(data.User.EmailId, data.Template.From);

                mm.From = addr;
                mm.Sender = addr;

                if (request.MailType == "Customer")
                {
                    mm.To.Add(data.TeleCalling.ContactsEmail);
                }
                else if (request.MailType == "Executive")
                {
                    mm.To.Add(data.TeleCalling.AssignedToEmail);
                }

                mm.Subject = data.Template.Subject;
                var msg = "";

                if (request.MailType == "Customer")
                {
                    msg = data.Template.CustomerEmail;
                }
                else if (request.MailType == "Executive")
                {
                    msg = data.Template.ExecutiveEmail;
                }

                msg = msg.Replace("#customername", data.TeleCalling.ContactsName);
                msg = msg.Replace("#product", data.TeleCalling.ProductsName);
                msg = msg.Replace("#executive", data.TeleCalling.AssignedToDisplayName);
                msg = msg.Replace("#appointmentdate", data.TeleCalling.AppointmentDate.Value.ToShortDateString() + "-" + data.TeleCalling.AppointmentDate.Value.ToShortTimeString());
                msg = msg.Replace("#username", data.TeleCalling.RepresentativeDisplayName);

                mm.Body = msg;
                mm.IsBodyHtml = true;
                response.Status = EmailHelper.Send(mm, data.User.EmailId, data.User.EmailPassword, (Boolean)data.User.SSL, data.User.Host, data.User.Port.Value);

            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }

        [ServiceAuthorize("TeleCalling:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.TeleCallingColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "TeleCalling_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        [HttpPost]
        public StandardResponse ClickToCall(IUnitOfWork uow, CallRequest request)
        {
            var response = new StandardResponse();

            response.Status = KnowlarityCall.ClickToCall(request.IVRNumber, request.AgentNumber, request.CustomerNumber);
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

        var data = new TeleCallingRow();

        using (var connection = _connections.NewFor<TeleCallingRow>())
        {
            var ind = TeleCallingRow.Fields;
            data = connection.TryById<TeleCallingRow>(request.Id, q => q
               .SelectTableFields()
                .Select(ind.Id)
                 .Select(ind.ContactsName)
                 .Select(ind.ContactsPhone)
                 .Select(ind.AppointmentDate)
                 .Select(ind.RepresentativeDisplayName)
                 .Select(ind.AssignedToDisplayName)
                 .Select(ind.ProductsName)
                 .Select(ind.AssignedToPhone)

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
                // else
                {
                    Contacttyp = 1;
                }
                string date1 = Convert.ToDateTime(data.Date).ToString("yyyy-MM-dd HH:mm:ss");
                string str = "INSERT INTO Contacts(ContactType,Country,CustomerType,Name,Phone,Email,OwnerId,AssignedId,DateCreated) VALUES('" + Contacttyp + "','81','1','" + data.ContactsName + "','" + data.ContactsPhone + "','" + data.ProductsName + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + date1 + "')";

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

                if (data.ContactsName != LastContact.Name)
                {
                    response.Status = "Error: This contact is been added to Contacts master\nBut we were unable to generate enquiry for same";

                    throw new Exception("This contact is been added to Contacts master\nBut we were unable to generate enquiry for same");
                }
                // }

                var s = SourceRow.Fields;
                var Source = connection.TryFirst<SourceRow>(l => l
                    .Select(s.Id)
                    .Select(s.Source)
                    .Where((s.Source == "TeleCalling") || (s.Source == "TELECALLING"))
                    );

                if (Source == null)
                {
                    string str2 = "INSERT INTO Source(Source) VALUES('TeleCalling')";
                    connection.Execute(str2);

                    Source = connection.TryFirst<SourceRow>(l => l
                    .Select(s.Id)
                    .Select(s.Source)
                    .Where(s.Source == "TeleCalling")
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
                    .Where(s.Source == "TeleCalling")
                    );
                }

                GetNextNumberResponse nextNumber = new EnquiryController().GetNextNumber(uow.Connection, new GetNextNumberRequest());
                string date = Convert.ToDateTime(data.Date).ToString("yyyy-MM-dd HH:mm:ss");

                var str1 = "INSERT INTO Enquiry(ContactsId,Date,Status,SourceId,StageId,OwnerId,AssignedId,EnquiryNo,EnquiryN,CompanyId) VALUES('" + LastContact.Id + "','" + date + "','1','" + Source.Id + "','" + stageMaster.Id + "','" + Context.User.GetIdentifier() + "','" + Context.User.GetIdentifier() + "','" + nextNumber.Serial + "','" + nextNumber.SerialN + "','" + UData.CompanyId + "')";

                connection.Execute(str1);

                // var t = EnquiryRow.Fields;
                var e = EnquiryRow.Fields;
                LastEnquiry = connection.First<EnquiryRow>(l => l
                    .Select(e.Id)
                    .OrderBy(e.Id, desc: true)
                    );

                //connection.Execute("Update TeleCalling SET IsMoved = 1 WHERE Id = " + request.Id);
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
        response.Id = LastEnquiry.Id.Value;
        response.Status = "Success";

        return response;

    }




}

    public class TeleCallingData
    {
        public ContactsRow Contact { get; set; }
        public TeleCallingTemplateRow Template { get; set; }
        public UserRow User { get; set; }
        public TeleCallingRow TeleCalling { get; set; }
        public TeleCallingFollowupsRow Followups { get; set; }
    }
}
