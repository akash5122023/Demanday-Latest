
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
    using MyRow = CMSFollowupsRow;

    public class CMSFollowupsRepository : BaseRepository
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext _context;

        public CMSFollowupsRepository(IRequestContext context, ISqlConnections connections)
            : base(context)
        {
            _context = context;
            _connections = connections;
        }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }
        private List<int> GetNotifyUsers(MyRow entity)
        {
            List<int> userIds;
            CMSRow eq;
            using (var conn = _connections.NewFor<CMSRow>())
            {
                var od = CMSRow.Fields;
                eq = conn.TryById<CMSRow>(entity.CMSId, q => q
                    .SelectTableFields()
                    .Select(od.Id));
            }

            userIds = new CMSRepository(Context, _connections).getNotifyUsers(eq);

            if (!userIds.Contains(entity.RepresentativeId.Value) &&
                Convert.ToInt32(Context.User.GetIdentifier()) != entity.RepresentativeId.Value)
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
            private readonly CMSFollowupsRepository _repository;

            public MySaveHandler(CMSFollowupsRepository repository) : base(repository._context)
            {
                _repository = repository;
            }

            protected override void AfterSave()
            {
                base.AfterSave();

                if (this.IsCreate)
                {
                    UserRow user;
                    using (var connection = _repository._connections.NewFor<UserRow>())
                    {
                        var u = UserRow.Fields;

                        user = connection.TryById<UserRow>(Request.Entity.RepresentativeId, q => q
                        .SelectTableFields()
                        .Select(u.Username));
                    }

                    var Timeline = new TimelineRow();

                    Timeline.EntityType = CMSRow.Fields.TableName;
                    Timeline.EntityId = Request.Entity.CMSId;
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

                notify.Module = Masters.NotificationModules.CMS;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Services/CMS#edit/" + Request.Entity.CMSId;

                notify.Text = Context.User.Identity.Name + " has" + (this.IsUpdate ? " Updated" : " Created") + " a CMS Followup";
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