
namespace AdvanceCRM.Services.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    
    using AdvanceCRM.Common;
    using AdvanceCRM.Services;
    using AdvanceCRM.Template;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Data;
    using System.IO;
    using System.Net;
    
    using MyRepository = Repositories.AMCVisitPlannerRepository;
    using MyRow = AMCVisitPlannerRow;

    [Route("Services/Services/AMCVisitPlanner/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class AMCVisitPlannerController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public AMCVisitPlannerController(ISqlConnections connections)
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

        [ServiceAuthorize("Reports:AMC:Visits")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.AMCVisitPlannerColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "AMCVisitFollowups_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        //Send SMS for Reminder
        [HttpPost]
        public StandardResponse SendVisitSMS(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new AMCData();

            using (var connection = _connections.NewFor<AMCVisitPlannerRow>())
            {
                var a = AMCTemplateRow.Fields;
                data.AMCTemplate = connection.TryFirst<AMCTemplateRow>( q => q
                    .SelectTableFields()
                    .Select(a.VisitSMSTemplate)
                    .Select(a.SmsTempId)
                   .Where(a.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );

                var e = AMCVisitPlannerRow.Fields;
                data.AMCVisits = connection.TryById<AMCVisitPlannerRow>(request.Id, q => q
                    .SelectTableFields()
                    .Select(e.AMCContactsId)
                    .Select(e.VisitDate)
                    .Select(e.VisitDetails)
                    
                    .Select(e.AMCAssignedId)
                    );


                var u = UserRow.Fields;
                data.User = connection.TryById<UserRow>(data.AMCVisits.AMCAssignedId, q => q
                    .SelectTableFields()
                    .Select(u.DisplayName)
                    .Select(u.Phone)
                    );


                var c = ContactsRow.Fields;
                data.Contact = connection.TryById<ContactsRow>(data.AMCVisits.AMCContactsId, q => q
                    .SelectTableFields()
                    .Select(c.Name)
                    .Select(c.Phone)
                    );
            }

            String msg = data.AMCTemplate.VisitSMSTemplate;
            String tempId = data.AMCTemplate.SmsTempId;



            msg = msg.Replace("#customername", data.Contact.Name);
            msg = msg.Replace("#engineername", data.User.DisplayName);
            msg = msg.Replace("#completiondate", data.AMCVisits.CompletionDate.Value.ToShortDateString());

            try
            {
                response.Status = SMSHelper.SendSMS(data.Contact.Phone, msg,tempId);
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }

    }
    public class AMCData
    {
        public ContactsRow Contact { get; set; }
        public AMCTemplateRow AMCTemplate { get; set; }
        public UserRow User { get; set; }
        public AMCVisitPlannerRow AMCVisits { get; set; }
        public CompanyDetailsRow Company { get; set; }
    }
}
