
namespace AdvanceCRM.Sales.Repositories
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Sales;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using MyRow = InvoiceAppointmentsRow;
    using Serenity.Extensions.DependencyInjection;

    public class InvoiceAppointmentsRepository : BaseRepository
    {
        private readonly ISqlConnections _connections;

        public InvoiceAppointmentsRepository(IRequestContext context, ISqlConnections connections)
            : base(context)
        {
            _connections = connections;
        }

        public InvoiceAppointmentsRepository(IRequestContext context) : this(context, Dependency.Resolve<ISqlConnections>())
        {
        }

        private static MyRow.RowFields fld => MyRow.Fields;

        public List<int> GetNotifyUsers(MyRow entity)
        {
            List<int> userIds;
            InvoiceRow header;
            using (var conn = _connections.NewFor<InvoiceRow>())
            {
                var od = InvoiceRow.Fields;
                header = conn.TryById<InvoiceRow>(entity.InvoiceId, q => q
                    .SelectTableFields()
                    .Select(od.Id));
            }

            userIds = InvoiceRepository.getNotifyUsers(header, Context, _connections);

            if (!userIds.Contains(entity.RepresentativeId.Value) &&
                Convert.ToInt32(Context.User.GetIdentifier()) != entity.RepresentativeId.Value)
            {
                userIds.Add(entity.RepresentativeId.Value);
            }

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
            private readonly InvoiceAppointmentsRepository _repository;
            private readonly ISqlConnections _connections;

            public MySaveHandler(InvoiceAppointmentsRepository repository)
                : base(repository.Context)
            {
                _repository = repository;
                _connections = repository._connections;
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

                    Timeline.EntityType = InvoiceRow.Fields.TableName;
                    Timeline.EntityId = Request.Entity.InvoiceId;
                    Timeline.InsertDate = null;
                    Timeline.Type = 5;
                    Timeline.Text = "Appointment Scheduled Added and Assigned To: <b>" + user.Username + "</b><br/> Agenda: " + Request.Entity.Details;
                    Timeline.ClearAssignment(TimelineRow.Fields.InsertDate);

                    new TimelineRepository(Context).Create(this.UnitOfWork, new SaveRequest<TimelineRow>
                    {
                        Entity = Timeline
                    });
                }

                List<int> userIds = _repository.GetNotifyUsers(Request.Entity);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.Proforma;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Proforma#edit/" + Request.Entity.InvoiceId;

                notify.Text = Serenity.Authorization.Username + " has" + (this.IsUpdate ? " Updated" : " Created") + " a Proforma Appointment";
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