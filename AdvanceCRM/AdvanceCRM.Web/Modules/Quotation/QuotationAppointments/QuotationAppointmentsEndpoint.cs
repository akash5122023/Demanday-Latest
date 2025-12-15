
namespace AdvanceCRM.Quotation.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Template;
    using Newtonsoft.Json.Linq;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Hosting;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Data;
    using System.IO;
    using System.Net.Mail;
    
    using MyRepository = Repositories.QuotationAppointmentsRepository;
    using MyRow = QuotationAppointmentsRow;

    [Route("Services/Quotation/QuotationAppointments/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class QuotationAppointmentsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private readonly IWebHostEnvironment _env;

        public QuotationAppointmentsController(ISqlConnections connections, IWebHostEnvironment env)
        {
            _connections = connections;
            _env = env;
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

        [ServiceAuthorize("Reports:Quotation:Appointments")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.QuotationAppointmentsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "QuotationAppointments_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        //Send SMS
        [HttpPost]
        public StandardResponse SendSMS(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var appointment = new MyRow();
            var template = new AppointmentTemplateRow();
            using (var connection = _connections.NewFor<MyRow>())
            {
                var e = MyRow.Fields;
                appointment = connection.TryById<MyRow>(request.Id, q => q
                    .SelectTableFields()
                    .Select(e.QuotationId)
                    .Select(e.QuotationContactsId)
                    .Select(e.AppointmentDate)
                    .Select(e.ContactName)
                    .Select(e.ContactPhone)
                    );


                var qt = AppointmentTemplateRow.Fields;
                template = connection.TryFirst<AppointmentTemplateRow>( q => q
                    .SelectTableFields()
                    .Select(qt.SMSTemplate)
                    .Select(qt.SmsTempId)
                   .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );
            }

            String msg = template.SMSTemplate;
            String TempID = template.SmsTempId;

            msg = msg.Replace("#customername", appointment.ContactName);
            msg = msg.Replace("#appointmentdate", appointment.AppointmentDate.ToDateTimeFormat());
            response.Status = SMSHelper.SendSMS(appointment.ContactPhone, msg,TempID);
            return response;
        }

        //Send Mail
        [HttpPost]
        public SendMailResponse SendMail(IUnitOfWork uow, SendMailRequest request)
        {

            var response = new SendMailResponse();
            var template = new AppointmentTemplateRow();
            var user = new UserRow();
            var appointment = new MyRow();

            using (var connection = _connections.NewFor<AppointmentTemplateRow>())
            {
                var c = AppointmentTemplateRow.Fields;
                template = connection.TryFirst<AppointmentTemplateRow>( q => q
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
                user = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                    .SelectTableFields()
                    .Select(u.Host)
                    .Select(u.Port)
                    .Select(u.SSL)
                    .Select(u.EmailId)
                    .Select(u.EmailPassword));


                var e = MyRow.Fields;

                appointment = connection.TryById<MyRow>(request.Id, q => q
                   .SelectTableFields()
                    .Select(e.QuotationId)
                   .Select(e.QuotationContactsId)
                    .Select(e.AppointmentDate)
                   .Select(e.ContactName)
                   .Select(e.ContactEmail));
            }

            try
            {
                if (template.Host != null)
                {
                    MailMessage mm = new MailMessage();
                    var addr = new MailAddress(template.EmailId, template.Sender);

                    mm.From = addr;
                    mm.Sender = addr;
                    mm.To.Add(appointment.ContactEmail);
                    mm.Subject = template.Subject;
                    var msg = template.EmailTemplate;
                    msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    msg = msg.Replace("#customername", appointment.ContactName);
                    msg = msg.Replace("#appointmentdate", appointment.AppointmentDate.ToDateTimeFormat());
                    mm.Body = msg;

                    if (template.Attachment != null)
                    {
                        JArray att = JArray.Parse(template.Attachment);
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

                    response.Status = EmailHelper.Send(mm, template.EmailId, template.EmailPassword, (Boolean)template.SSL, template.Host, template.Port.Value);
                }
                else
                {
                    MailMessage mm = new MailMessage();
                    var addr = new MailAddress(user.EmailId, template.Sender);

                    mm.From = addr;
                    mm.Sender = addr;
                    mm.To.Add(appointment.ContactEmail);
                    mm.Subject = template.Subject;
                    var msg = template.EmailTemplate;
                    msg = msg.Replace("#username", Context.User.ToUserDefinition().DisplayName);
                    msg = msg.Replace("#customername", appointment.ContactName);

                    msg = msg.Replace("#appointmentdate", appointment.AppointmentDate.ToDateTimeFormat());
                    mm.Body = msg;

                    if (template.Attachment != null)
                    {
                        JArray att = JArray.Parse(template.Attachment);
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

                    response.Status = EmailHelper.Send(mm, user.EmailId, user.EmailPassword, (Boolean)user.SSL, user.Host, user.Port.Value);
                }
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }
    }
}
