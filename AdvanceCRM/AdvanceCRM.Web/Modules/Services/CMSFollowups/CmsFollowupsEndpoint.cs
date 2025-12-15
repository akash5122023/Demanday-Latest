
namespace AdvanceCRM.Services.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Template;
    using AdvanceCRM.Common;
    using AdvanceCRM.Services;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Data;
    
    using MyRepository = Repositories.CMSFollowupsRepository;
    using MyRow = CMSFollowupsRow;

    [Route("Services/Services/CMSFollowups/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class CMSFollowupsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public CMSFollowupsController(ISqlConnections connections)
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

        [ServiceAuthorize("Reports:CMS:Followups")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.CMSFollowupsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "QuotationFollowups_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        [HttpPost]
        public StandardResponse SendSMSReminder(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new CMSData();

            using (var connection = _connections.NewFor<CMSFollowupsRow>())
            {
                var e = CMSFollowupsRow.Fields;
                data.CMSFollowups = connection.TryById<CMSFollowupsRow>(request.Id, q => q
                    .SelectTableFields()
                    .Select(e.CMSContactsId)
                    .Select(e.FollowupDate)
                    .Select(e.FollowupNote)
                    .Select(e.Details)
                    .Select(e.CMSAssignedTo)
                    );

                var qt = CmsTemplateRow.Fields;
                data.Template = connection.TryFirst<CmsTemplateRow>(q => q
                   .SelectTableFields()
                   .Select(qt.SmsReminder)
                   .Select(qt.SmsrTemplateId)
                  .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );


                var u = UserRow.Fields;
                data.User = connection.TryById<UserRow>(data.CMSFollowups.CMSAssignedTo, q => q
                    .SelectTableFields()
                    .Select(u.Phone)
                    );


                var c = ContactsRow.Fields;
                data.Contact = connection.TryById<ContactsRow>(data.CMSFollowups.CMSContactsId, q => q
                    .SelectTableFields()
                    .Select(c.Name)
                    );
            }

            String msg = data.Template.SmsReminder;
            String tempId = data.Template.SmsrTemplateId;
            msg = msg.Replace("#customername", data.Contact.Name);
            msg = msg.Replace("#followupdetails", data.CMSFollowups.FollowupNote + " - " + data.CMSFollowups.Details);
            msg = msg.Replace("#follwupdatetime", data.CMSFollowups.FollowupDate.Value.ToString("yyyy-MM-dd HH:mm"));

            try
            {
                response.Status = "<h6>" + SMSHelper.SendScheduleSMS(data.User.Phone, msg, data.CMSFollowups.FollowupDate.Value.AddMinutes(-15),tempId) + "\nReminder scheduled 15 minutes before time</h6>";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }
    }
}
