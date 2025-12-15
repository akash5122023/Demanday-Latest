
namespace AdvanceCRM.Enquiry.Repositories
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Enquiry;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using Microsoft.AspNetCore.Hosting;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using MyRow = EnquiryAppointmentsRow;

    public class EnquiryAppointmentsRepository : BaseRepository
    {
        private readonly ISqlConnections _connections;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EnquiryAppointmentsRepository(IRequestContext context, ISqlConnections connections, IWebHostEnvironment hostEnvironment)
            : base(context)
        {
            _connections = connections;
            _hostEnvironment = hostEnvironment;
        }

        public EnquiryAppointmentsRepository(IRequestContext context) : base(context) { }

        private MyRow.RowFields fld { get { return MyRow.Fields; } }
        public List<int> GetNotifyUsers(MyRow entity)
        {
            List<int> userIds;
            EnquiryRow eq;
            using (var conn = _connections.NewFor<EnquiryRow>())
            {
                var od = EnquiryRow.Fields;
                eq = conn.TryById<EnquiryRow>(entity.EnquiryId, q => q
                    .SelectTableFields()
                    .Select(od.Id)
                    );
            }
            var repo = new EnquiryRepository(Context, _connections, _hostEnvironment);
            userIds = repo.GetNotifyUsers(eq);

            if (!userIds.Contains(entity.RepresentativeId.Value) && Convert.ToInt32(Context.User.GetIdentifier()) != entity.RepresentativeId.Value)
                userIds.Add(entity.RepresentativeId.Value);

            return userIds;
        }

        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(this).Process(uow, request, SaveRequestType.Create);
        }

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(this).Process(uow, request, SaveRequestType.Update);
        }

        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
            return new MyDeleteHandler(Context).Process(uow, request);
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRetrieveHandler(Context).Process(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyListHandler(Context).Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            private readonly EnquiryAppointmentsRepository _repository;
            private readonly ISqlConnections _connections;
            private readonly IWebHostEnvironment _hostEnvironment;

            public MySaveHandler(EnquiryAppointmentsRepository repository) : base(repository.Context)
            {
                _repository = repository;
                _connections = repository._connections;
                _hostEnvironment = repository._hostEnvironment;
            }

            protected override void AfterSave()
            {
                base.AfterSave();

                if (this.IsCreate)
                {
                    UserRow user;
                    using (var connection = _connections.NewFor<UserRow>())
                    {
                        var u = UserRow.Fields;

                        user = connection.TryById<UserRow>(Request.Entity.RepresentativeId, q => q
                        .SelectTableFields()
                        .Select(u.Username));
                    }

                    var Timeline = new TimelineRow();

                    Timeline.EntityType = EnquiryRow.Fields.TableName;
                    Timeline.EntityId = Request.Entity.EnquiryId;
                    Timeline.InsertDate = null;
                    Timeline.Type = 5;
                    Timeline.Text = "Appointment Scheduled Added and Assigned To: <b>" + user.Username + "</b><br/> Agenda: " + Request.Entity.Details;
                    Timeline.ClearAssignment(TimelineRow.Fields.InsertDate);

                    new TimelineRepository(Context).Create(this.UnitOfWork, new SaveRequest<TimelineRow>
                    {
                        Entity = Timeline
                    });
                }

                List<Int32> userIds = _repository.GetNotifyUsers(Request.Entity);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.Enquiry;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Enquiry#edit/" + Request.Entity.EnquiryId;

                notify.Text = Context.User.Identity.Name + " has" + (this.IsUpdate ? " Updated" : " Created") + " an Enquiry Appointment";
                notify.UserList = userIds;

                new NotificationsRepository(Context).Create(this.UnitOfWork, new SaveRequest<NotificationsRow>
                {
                    Entity = notify
                });
            }
        }
         private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow> { public MyListHandler(IRequestContext context) : base(context) { } }
    }
}