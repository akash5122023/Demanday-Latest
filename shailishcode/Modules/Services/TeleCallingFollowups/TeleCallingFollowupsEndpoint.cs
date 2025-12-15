
namespace AdvanceCRM.Services.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Common;
    using AdvanceCRM.Services;
    using Serenity;
    using AdvanceCRM.Template;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Data;
    
    using MyRepository = Repositories.TeleCallingFollowupsRepository;
    using MyRow = TeleCallingFollowupsRow;

    [Route("Services/Services/TeleCallingFollowups/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class TeleCallingFollowupsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public TeleCallingFollowupsController(ISqlConnections connections)
        {
            _connections = connections;
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


        [ServiceAuthorize("Reports:TeleCalling:Followups")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.TeleCallingFollowupsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "TeleCallingFollowups_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        //Send SMS for Reminder
        [HttpPost]
        public StandardResponse SendSMSReminder(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new TeleCallingData();

            using (var connection = _connections.NewFor<TeleCallingFollowupsRow>())
            {
                var e = TeleCallingFollowupsRow.Fields;
                data.Followups = connection.TryById<TeleCallingFollowupsRow>(request.Id, q => q
                    .SelectTableFields()
                    .Select(e.TeleCallingId)
                    .Select(e.TeleCallingContactsId)
                    .Select(e.FollowupDate)
                    .Select(e.FollowupNote)
                    .Select(e.Details)
                    .Select(e.TeleCallingAssignedTo)
                    );

                var qt = TeleCallingTemplateRow.Fields;
                data.Template = connection.TryFirst<TeleCallingTemplateRow>(q => q
                   .SelectTableFields()
                   .Select(qt.SmsReminder)
                   .Select(qt.SmsrTemplateId)
                  .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );

                var u = UserRow.Fields;
                data.User = connection.TryById<UserRow>(data.Followups.TeleCallingAssignedTo, q => q
                    .SelectTableFields()
                    .Select(u.Phone)
                    );


                var c = ContactsRow.Fields;
                data.Contact = connection.TryById<ContactsRow>(data.Followups.TeleCallingContactsId, q => q
                    .SelectTableFields()
                    .Select(c.Name)
                    );
            }

            String msg = data.Template.SmsReminder;
            String tempId = data.Template.SmsrTemplateId;
            msg = msg.Replace("#customername", data.Contact.Name);
            msg = msg.Replace("#followupdetails", data.Followups.FollowupNote + " - " + data.Followups.Details);
            msg = msg.Replace("#follwupdatetime", data.Followups.FollowupDate.Value.ToString("yyyy-MM-dd HH:mm"));


            //String msg = "Reminder for Enquiry followup of client " + data.Contact.Name + "\nDetails:\n" + data.Followups.FollowupNote + " - " + data.Followups.Details + "\nat 02:14 PM\nFollwup Time:" + data.Followups.FollowupDate.Value.ToString("yyyy-MM-dd HH:mm");
            //String tempId = "XXXXXXXX";
            try
            {
                response.Status = "<h6>" + SMSHelper.SendScheduleSMS(data.User.Phone, msg, data.Followups.FollowupDate.Value.AddMinutes(-15),tempId) + "\nReminder scheduled 15 minutes before time</h6>";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }
    }
}
