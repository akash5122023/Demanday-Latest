namespace AdvanceCRM.Tasks.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Tasks;
    using AdvanceCRM.Template;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    
    using MyRepository = Repositories.TasksRepository;
    using MyRow = TasksRow;

    [Route("Services/Tasks/Tasks/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class TasksController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public TasksController(ISqlConnections connections)
        {
            _connections = connections;
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


        //Send Mail
        [HttpPost]
        public SendMailResponse SendMail(IUnitOfWork uow, SendMailRequest request)
        {

            var response = new SendMailResponse();

            var data = new TaskData();
            data.emailList = new List<string>();
            using (var connection = _connections.NewFor<TasksRow>())
            {
                var t = TasksRow.Fields;

                data.Task = connection.TryById<TasksRow>(request.Id, q => q
                     .SelectTableFields()
                     .Select(t.Id)
                     .Select(t.Task)
                     .Select(t.Details)
                     .Select(t.AssignedTo)
                     .Select(t.AssignedToEmail)
                     .Select(t.AssignedToPhone)
                     .Select(t.AssignedByDisplayName)
                     .Select(t.ExpectedCompletion)
                     );

                var u = UserRow.Fields;

                data.User = connection.TryById<UserRow>(Context.User.GetIdentifier(), q => q
                    .SelectTableFields()
                    .Select(u.Host)
                    .Select(u.Port)
                    .Select(u.SSL)
                    .Select(u.EmailId)
                    .Select(u.EmailPassword));

                var watcherList = new List<TaskWatcherRow>();
                var w = TaskWatcherRow.Fields;

                watcherList = connection.List<TaskWatcherRow>(q => q
                .SelectTableFields()
                .Select(w.AssignedId)
                .Select(w.AssignedEmail)
                .Where(w.TasksId == data.Task.Id.Value));

                foreach (var user in watcherList)
                {
                    if (!user.AssignedEmail.IsNullOrEmpty())
                        data.emailList.Add(user.AssignedEmail.ToString());
                }
            }

            try
            {
                MailMessage mm = new MailMessage();
                var addr = new MailAddress(data.User.EmailId);

                mm.From = addr;
                mm.Sender = addr;
                mm.To.Add(data.Task.AssignedToEmail);
                mm.Subject = "Task Notification";
                String msg = "You have been assigned with task\nTask: " + data.Task.Task + "\nDetails: " + data.Task.Details + "\nGiven By: " + data.Task.AssignedByDisplayName + "\nClosing Date: " + data.Task.ExpectedCompletion.Value.ToShortDateString();

                mm.Body = msg;

                foreach (var e in data.emailList)
                {
                    mm.CC.Add(new MailAddress(e));
                }

                response.Status = EmailHelper.Send(mm, data.User.EmailId, data.User.EmailPassword, (Boolean)data.User.SSL, data.User.Host, data.User.Port.Value);

            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }


        //Send SMS
        [HttpPost]
        public StandardResponse SendSMS(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new TaskData();
            var qt = OtherTemplatesRow.Fields;
            var Template = new OtherTemplatesRow();
            data.phoneList = new List<string>();

            using (var connection = _connections.NewFor<TasksRow>())
            {
                var t = TasksRow.Fields;

                data.Task = connection.TryById<TasksRow>(request.Id, q => q
                     .SelectTableFields()
                     .Select(t.Id)
                     .Select(t.Task)
                     .Select(t.Details)
                     .Select(t.AssignedTo)
                     .Select(t.AssignedToEmail)
                     .Select(t.AssignedToPhone)
                     .Select(t.AssignedByDisplayName)
                     .Select(t.ExpectedCompletion)
                     );


                Template = connection.TryFirst<OtherTemplatesRow>(q => q
                       .SelectTableFields()
                        .Select(qt.TaskSmsTemplate)
                        .Select(qt.TaskSmsTemplateId)
                    );

                var watcherList = new List<TaskWatcherRow>();
                var w = TaskWatcherRow.Fields;

                watcherList = connection.List<TaskWatcherRow>(q => q
                .SelectTableFields()
                .Select(w.AssignedId)
                .Select(w.AssignedPhone)
                .Where(w.TasksId == data.Task.Id.Value));

                foreach (var user in watcherList)
                {
                    if (!user.AssignedPhone.IsNullOrEmpty())
                        data.phoneList.Add(user.AssignedPhone.ToString());
                }

            }

                // String msg = "You have been assigned with task\nTask: " + data.Task.Task + "\nDetails: " + data.Task.Details + "\nGiven By: " + data.Task.AssignedByDisplayName + "\nClosing Date: " + data.Task.ExpectedCompletion.Value.ToShortDateString();
                String msg = Template.TaskSmsTemplate;
                String TempId = Template.TaskSmsTemplateId;
                msg = msg.Replace("#task", data.Task.Task);
                msg = msg.Replace("#details", data.Task.Details);
                msg = msg.Replace("#givenby", data.Task.AssignedByDisplayName);
                msg = msg.Replace("#closingdate", data.Task.ExpectedCompletion.Value.ToShortDateString());

            
            try
            {
                if (data.phoneList.Count > 0)
                    response.Status = SMSHelper.SendBulkSMS(data.phoneList, msg);
                else
                    response.Status = SMSHelper.SendSMS(data.Task.AssignedToPhone, msg,TempId);
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }

        [HttpPost]
        public StandardResponse SendWati(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new TaskData();

            using (var connection = _connections.NewFor<TasksRow>())
            {
                var t = TasksRow.Fields;

                data.Task = connection.TryById<TasksRow>(request.Id, q => q
                     .SelectTableFields()
                     .Select(t.Id)
                     .Select(t.Task)
                     .Select(t.Details)
                     .Select(t.AssignedTo)
                     .Select(t.AssignedToEmail)
                     .Select(t.AssignedToPhone)
                     .Select(t.AssignedByDisplayName)
                     .Select(t.ExpectedCompletion)
                     );

                var watcherList = new List<TaskWatcherRow>();
                var w = TaskWatcherRow.Fields;

                watcherList = connection.List<TaskWatcherRow>(q => q
                .SelectTableFields()
                .Select(w.AssignedId)
                .Select(w.AssignedPhone)
                .Where(w.TasksId == data.Task.Id.Value));

                foreach (var user in watcherList)
                {
                    if (!user.AssignedPhone.IsNullOrEmpty())
                        data.phoneList.Add(user.AssignedPhone.ToString());
                }


            }
            String msg = "You have been assigned with task\nTask: " + data.Task.Task + "\nDetails: " + data.Task.Details + "\nGiven By: " + data.Task.AssignedByDisplayName + "\nClosing Date: " + data.Task.ExpectedCompletion.Value.ToShortDateString();

            try
            {
                if (data.phoneList.Count > 0)
                {
                    foreach (var phone in data.phoneList)
                    {
                        response.Status = SMSHelper.SendWati(phone, msg);
                    }
                }
                else
                {
                    response.Status = SMSHelper.SendWati(data.Task.AssignedToPhone, msg);
                }
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

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
                    .Select(e.AssignedToDisplayName)
                    .Select(e.AssignedToPhone)
                );
            }

            String msg = request.SMSType;
            String TempId = request.TemplateID;

            if (msg.Trim() == "#customername token can be used in message to auto add customers name")
            {
                throw new Exception("It seems you havent chnaged the default text, hence sending SMS is been cancelled");
            }

            var newmsg = msg.Replace("#customername", contact.AssignedToDisplayName);

            response.Status = SMSHelper.SendSMS(contact.AssignedToPhone, newmsg,TempId);

            if (!response.Status.Contains(("SMS").ToUpper()))
            {
                throw new Exception("SMS Sending Failed!");
            }

            response.Status = "Successfull!!!";

            return response;
        }

        [ServiceAuthorize("Tasks:Export")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.TasksColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "Tasks_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

    }

    public class TaskData
    {
        public TasksRow Task { get; set; }
        public UserRow User { get; set; }
        public List<string> phoneList { get; set; }
        public List<string> emailList { get; set; }
    }
}
