
namespace AdvanceCRM.Sales.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Template;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using Serenity.Extensions.DependencyInjection;
    using System;
    using System.Data;
    
    using static AdvanceCRM.Sales.Endpoints.SalesController;
    using MyRepository = Repositories.SalesFollowupsRepository;
    using MyRow = SalesFollowupsRow;

    [Route("Services/Sales/SalesFollowups/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class SalesFollowupsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private IRequestContext Context { get; }

        public SalesFollowupsController(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context;
        }

        public SalesFollowupsController()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
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

        [ServiceAuthorize("Reports:Sales:Followups")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.SalesFollowupsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "SalesFollowups_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        //Send SMS for Reminder
        [HttpPost]
        public StandardResponse SendSMSReminder(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new SalesData();

            using (var connection = _connections.NewFor<MyRow>())
            {
                var e = MyRow.Fields;
                data.SalesFollowups = connection.TryById<MyRow>(request.Id, q => q
                    .SelectTableFields()
                    .Select(e.SalesContactsId)
                    .Select(e.FollowupDate)
                    .Select(e.FollowupNote)
                    .Select(e.Details)
                    .Select(e.SalesAssignedId)
                    );

                var qt = InvoiceTemplateRow.Fields;
                data.Template = connection.TryFirst<InvoiceTemplateRow>(q => q
                   .SelectTableFields()
                   .Select(qt.SmsReminder)
                    .Select(qt.SmsrTemplateId)
                  .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );

                var u = UserRow.Fields;
                data.User = connection.TryById<UserRow>(data.SalesFollowups.SalesAssignedId, q => q
                    .SelectTableFields()
                    .Select(u.Phone)
                    );


                var c = ContactsRow.Fields;
                data.Contact = connection.TryById<ContactsRow>(data.SalesFollowups.SalesContactsId, q => q
                    .SelectTableFields()
                    .Select(c.Name)
                    );
            }
            String msg = data.Template.SmsReminder;
            String tempId = data.Template.SmsrTemplateId;
            msg = msg.Replace("#customername", data.Contact.Name);
            msg = msg.Replace("#followupdetails", data.SalesFollowups.FollowupNote + " - " + data.SalesFollowups.Details);
            msg = msg.Replace("#follwupdatetime", data.SalesFollowups.FollowupDate.Value.ToString("yyyy-MM-dd HH:mm"));
            try
            {
                response.Status = "<h6>" + SMSHelper.SendScheduleSMS(data.User.Phone, msg, data.SalesFollowups.FollowupDate.Value.AddMinutes(-15),tempId) + "\nReminder scheduled 15 minutes before time</h6>";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }
    }
}
