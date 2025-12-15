
namespace AdvanceCRM.Enquiry.Endpoints
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Common;
    using AdvanceCRM.Template;
    using AdvanceCRM.Settings;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Reporting;
    using Serenity.Services;
    using Serenity.Web;
    using System;
    using System.Data;
    using System.IO;
    using System.Net;
    
    using MyRepository = Repositories.EnquiryFollowupsRepository;
    using MyRow = EnquiryFollowupsRow;

    [Route("Services/Enquiry/EnquiryFollowups/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class EnquiryFollowupsController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public EnquiryFollowupsController(ISqlConnections connections)
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

        [ServiceAuthorize("Reports:Enquiry:Followups")]
        public FileContentResult ListExcel(IDbConnection connection, ListRequest request)
        {
            var data = List(connection, request).Entities;
            var report = new DynamicDataReport(data, request.IncludeColumns, typeof(Columns.EnquiryFollowupsColumns));
            var bytes = new ReportRepository().Render(report);
            return ExcelContentResult.Create(bytes, "EnquiryFollowups_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
        }

        //Send SMS for Reminder
        [HttpPost]
        public StandardResponse SendSMSReminder(IUnitOfWork uow, SendSMSRequest request)
        {
            var response = new StandardResponse();

            var data = new EnquiryData();

            using (var connection = _connections.NewFor<EnquiryFollowupsRow>())
            {
                var e = EnquiryFollowupsRow.Fields;
                data.EnquiryFollowups = connection.TryById<EnquiryFollowupsRow>(request.Id, q => q
                    .SelectTableFields()
                    .Select(e.EnquiryContactsId)
                    .Select(e.FollowupDate)
                    .Select(e.FollowupNote)
                    .Select(e.Details)
                    .Select(e.EnquiryAssignedId)
                    );

                var qt = EnquiryTemplateRow.Fields;
                data.Template = connection.TryFirst<EnquiryTemplateRow>(q => q
                   .SelectTableFields()
                   .Select(qt.SmsReminder)
                    .Select(qt.SmsrTemplateId)
                  .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
                    );


                var u = UserRow.Fields;
                data.User = connection.TryById<UserRow>(data.EnquiryFollowups.EnquiryAssignedId, q => q
                    .SelectTableFields()
                    .Select(u.Phone)
                    );


                var c = ContactsRow.Fields;
                data.Contact = connection.TryById<ContactsRow>(data.EnquiryFollowups.EnquiryContactsId, q => q
                    .SelectTableFields()
                    .Select(c.Name)
                    );
            }
            String msg = data.Template.SmsReminder;
            String tempId = data.Template.SmsrTemplateId;
            msg = msg.Replace("#customername", data.Contact.Name);
            msg = msg.Replace("#followupdetails", data.EnquiryFollowups.FollowupNote + " - " + data.EnquiryFollowups.Details);
            msg = msg.Replace("#follwupdatetime", data.EnquiryFollowups.FollowupDate.Value.ToString("yyyy-MM-dd HH:mm"));
            try
            {
                response.Status = "<h6>" + SMSHelper.SendScheduleSMS(data.User.Phone, msg, data.EnquiryFollowups.FollowupDate.Value.AddMinutes(-15), tempId) + "\nReminder scheduled 15 minutes before time</h6>";
            }
            catch (Exception ex)
            {
                response.Status = ex.Message.ToString();
            }

            return response;
        }

        //[HttpPost]
        //public StandardResponse SendWAReminder(IUnitOfWork uow, SendSMSRequest request)
        //{
        //    var response = new StandardResponse();

        //    var data = new EnquiryData();
        //    var c = ContactsRow.Fields;

        //    using (var connection = _connections.NewFor<ContactsRow>())
        //    {
        //        data.Contact = connection.TryById<ContactsRow>(request.Id, q => q
        //             .SelectTableFields()
        //             .Select(c.Id)
        //             .Select(c.Name)
        //             .Select(c.Whatsapp)
        //             .Select(c.Phone)
        //             );

        //        var qt = EnquiryTemplateRow.Fields;
        //        data.Template = connection.TryFirst<EnquiryTemplateRow>(q => q
        //           .SelectTableFields()
        //           .Select(qt.WaTemplate)
        //           .Select(qt.WaTemplateId)
        //          .Where(qt.CompanyId == ((UserDefinition)Context.User.ToUserDefinition()).CompanyId)
        //            );

        //    }
        //    string con = data.Contact.Whatsapp;
        //    if (data.Contact.Whatsapp == null)
        //    {
        //        con = data.Contact.Phone;
        //    }

        //    String temId = data.Template.WaTemplateId;

        //    response.Status = WAHelper.SendBizWA(con, temId);
        //    return response;
        //}
    }
}