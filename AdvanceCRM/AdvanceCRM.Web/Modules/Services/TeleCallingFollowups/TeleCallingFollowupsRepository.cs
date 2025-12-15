
namespace AdvanceCRM.Services.Repositories
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Services;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using MyRow = TeleCallingFollowupsRow;

    public class TeleCallingFollowupsRepository : BaseRepository
    {
        private readonly ISqlConnections _connections;

        public TeleCallingFollowupsRepository(IRequestContext context, ISqlConnections connections)
            : base(context)
        {
            _connections = connections;
        }

        public TeleCallingFollowupsRepository(IRequestContext context) : base(context)
        {
        }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }

        private List<int> GetNotifyUsers(MyRow entity)
        {
            TeleCallingRow header;
            using (var conn = _connections.NewFor<TeleCallingRow>())
            {
                var od = TeleCallingRow.Fields;
                header = conn.TryById<TeleCallingRow>(entity.TeleCallingId, q => q
                    .SelectTableFields()
                    .Select(od.RepresentativeId));
            }

            var userIds = new TeleCallingRepository(Context, _connections)
                            .GetNotifyUsers(header);

            if (!userIds.Contains(entity.RepresentativeId.Value) &&
                int.Parse(Context.User.GetIdentifier()) != entity.RepresentativeId.Value)
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
            private readonly TeleCallingFollowupsRepository _repository;
            private readonly ISqlConnections _connections;

            public MySaveHandler(TeleCallingFollowupsRepository repository)
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

                    Timeline.EntityType = TeleCallingRow.Fields.TableName;
                    Timeline.EntityId = Request.Entity.TeleCallingId;
                    Timeline.InsertDate = null;
                    Timeline.Type = 4;
                    Timeline.Text = "Followup Added and Assigned To: <b>" + user.Username + "</b><br/> Followup Note: " + Request.Entity.FollowupNote;
                    Timeline.ClearAssignment(TimelineRow.Fields.InsertDate);

                    new TimelineRepository(Context).Create(this.UnitOfWork, new SaveRequest<TimelineRow>
                    {
                        Entity = Timeline
                    });
                }

                List<int> userIds = _repository.GetNotifyUsers(Request.Entity);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.TeleCalling;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Services/TeleCalling#edit/" + Request.Entity.TeleCallingId;

                notify.Text = Serenity.Authorization.Username + " has" + (this.IsUpdate ? " Updated" : " Created") + " a TeleCalling Appointment";
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