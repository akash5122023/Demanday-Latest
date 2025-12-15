
namespace AdvanceCRM.Quotation.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Common;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Template;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using Serenity.Extensions.DependencyInjection;
    using System;
    using System.Data;
    
    using MyRepository = Repositories.QuotationFollowupsRepository;
    using MyRow = QuotationFollowupsRow;

    [Route("Services/Quotation/QuotationFollowups/[action]")]
[ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
public class QuotationFollowupsController : ServiceEndpoint
{
    private readonly ISqlConnections _connections;
    private IRequestContext Context { get; }

    public QuotationFollowupsController(ISqlConnections connections, IRequestContext context)
    {
        _connections = connections;
        Context = context;
    }

    public QuotationFollowupsController()
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


        [ServiceAuthorize("Reports:Quotation:Followups")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.QuotationFollowupsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "QuotationFollowups_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        //Send SMS for Reminder
        [HttpPost]
        public StandardResponse SendSMSReminder(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new QuotationData();

            using (var connection = _connections.NewFor<QuotationFollowupsRow>())
            {
                var e = QuotationFollowupsRow.Fields;
                data.QuotationFollowups = connection.TryById<QuotationFollowupsRow>(request.Id, q => q
                    .SelectTableFields()
                    .Select(e.QuotationContactsId)
                    .Select(e.FollowupDate)
                    .Select(e.FollowupNote)
                    .Select(e.Details)
                    .Select(e.QuotationAssignedId)
                    );

                var qt = QuotationTemplateRow.Fields;
                data.Template = connection.TryFirst<QuotationTemplateRow>(q => q
                   .SelectTableFields()
                   .Select(qt.SmsReminder)
                    .Select(qt.SmsrTemplateId)
                  .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );

                var u = UserRow.Fields;
                data.User = connection.TryById<UserRow>(data.QuotationFollowups.QuotationAssignedId, q => q
                    .SelectTableFields()
                    .Select(u.Phone)
                    );


                var c = ContactsRow.Fields;
                data.Contact = connection.TryById<ContactsRow>(data.QuotationFollowups.QuotationContactsId, q => q
                    .SelectTableFields()
                    .Select(c.Name)
                    );
            }

            String msg = data.Template.SmsReminder;
            String tempId = data.Template.SmsrTemplateId;
            msg = msg.Replace("#customername", data.Contact.Name);
            msg = msg.Replace("#followupdetails", data.QuotationFollowups.FollowupNote + " - " + data.QuotationFollowups.Details);
            msg = msg.Replace("#follwupdatetime", data.QuotationFollowups.FollowupDate.Value.ToString("yyyy-MM-dd HH:mm"));

            try
            {
                response.Status = "<h6>" + SMSHelper.SendScheduleSMS(data.User.Phone, msg, data.QuotationFollowups.FollowupDate.Value.AddMinutes(-15),tempId) + "\nReminder scheduled 15 minutes before time</h6>";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }
    }
}
