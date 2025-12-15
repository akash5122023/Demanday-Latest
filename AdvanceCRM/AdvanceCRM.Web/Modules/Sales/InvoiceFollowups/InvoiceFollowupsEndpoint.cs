
namespace AdvanceCRM.Sales.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Common;
    using AdvanceCRM.Sales;
    using AdvanceCRM.Template;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Data;
    
    using static AdvanceCRM.Sales.Endpoints.InvoiceController;
    using MyRepository = Repositories.InvoiceFollowupsRepository;
    using MyRow = InvoiceFollowupsRow;
    using Serenity.Extensions.DependencyInjection;

    [Route("Services/Sales/InvoiceFollowups/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class InvoiceFollowupsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private IRequestContext Context { get; }

        public InvoiceFollowupsController(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context;
        }

        public InvoiceFollowupsController()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
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

        [ServiceAuthorize("Reports:Proforma:Followups")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.InvoiceFollowupsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "ProformaFollowups_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        //Send SMS for Reminder
        [HttpPost]
        public StandardResponse SendSMSReminder(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new InvoiceData();

            using (var connection = _connections.NewFor<InvoiceFollowupsRow>())
            {
                var e = InvoiceFollowupsRow.Fields;
                data.InvoiceFollowups = connection.TryById<InvoiceFollowupsRow>(request.Id, q => q
                    .SelectTableFields()
                    .Select(e.InvoiceContactsId)
                    .Select(e.FollowupDate)
                    .Select(e.FollowupNote)
                    .Select(e.Details)
                    .Select(e.InvoiceAssignedId)
                    );

                var qt = InvoiceTemplateRow.Fields;
                data.Template = connection.TryFirst<InvoiceTemplateRow>(q => q
                   .SelectTableFields()
                   .Select(qt.SmsReminder)
                    .Select(qt.SmsrTemplateId)
                  .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );


                var u = UserRow.Fields;
                data.User = connection.TryById<UserRow>(data.InvoiceFollowups.InvoiceAssignedId, q => q
                    .SelectTableFields()
                    .Select(u.Phone)
                    );


                var c = ContactsRow.Fields;
                data.Contact = connection.TryById<ContactsRow>(data.InvoiceFollowups.InvoiceContactsId, q => q
                    .SelectTableFields()
                    .Select(c.Name)
                    );
            }

            String msg = data.Template.SmsReminder;
            String tempId = data.Template.SmsrTemplateId;
            msg = msg.Replace("#customername", data.Contact.Name);
            msg = msg.Replace("#followupdetails", data.InvoiceFollowups.FollowupNote + " - " + data.InvoiceFollowups.Details);
            msg = msg.Replace("#follwupdatetime", data.InvoiceFollowups.FollowupDate.Value.ToString("yyyy-MM-dd HH:mm"));

            try
            {
                response.Status = "<h6>" + SMSHelper.SendScheduleSMS(data.User.Phone, msg, data.InvoiceFollowups.FollowupDate.Value.AddMinutes(-15),tempId) + "\nReminder scheduled 15 minutes before time</h6>";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }
    }
}
