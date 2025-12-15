
namespace AdvanceCRM.Services.Repositories
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Services;
    using Microsoft.AspNetCore.Mvc;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MyRow = AdvanceCRM.Services.TeleCallingRow;

    public class TeleCallingRepository : BaseRepository
    {
        protected ISqlConnections _connections { get; }

        public TeleCallingRepository(IRequestContext context, ISqlConnections connections)
            : base(context)
        {
            _connections = connections;
        }

        public TeleCallingRepository(IRequestContext context)
            : base(context)
        {
        }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }

        public List<int> GetNotifyUsers(MyRow entity)
        {
            var userIds = new List<int>();

            using (var conn = _connections.NewFor<UserRow>())
            {
                var od = UserRow.Fields;
                var owner = new UserRow();
                var assigned = new UserRow();

                owner = conn.TryById<UserRow>(entity.RepresentativeId, q => q
                    .SelectTableFields()
                    .Select(od.UserId)
                    .Select(od.UpperLevel)
                    .Select(od.UpperLevel2)
                    .Select(od.UpperLevel3)
                    .Select(od.UpperLevel4)
                    .Select(od.UpperLevel5)
                    );

                assigned = conn.TryById<UserRow>(entity.AssignedTo, q => q
                    .SelectTableFields()
                    .Select(od.UserId)
                    .Select(od.UpperLevel)
                    .Select(od.UpperLevel2)
                    .Select(od.UpperLevel3)
                    .Select(od.UpperLevel4)
                    .Select(od.UpperLevel5)
                    );

                if (owner.HasValue())
                {
                    userIds.Add(owner.UserId.Value);
                    userIds.Add(owner.UpperLevel.Value);
                    userIds.Add(owner.UpperLevel2.Value);
                    userIds.Add(owner.UpperLevel3.Value);
                    userIds.Add(owner.UpperLevel4.Value);
                    userIds.Add(owner.UpperLevel5.Value);
                }

                if (assigned.HasValue())
                {
                    userIds.Add(assigned.UserId.Value);
                    userIds.Add(assigned.UpperLevel.Value);
                    userIds.Add(assigned.UpperLevel2.Value);
                    userIds.Add(assigned.UpperLevel3.Value);
                    userIds.Add(assigned.UpperLevel4.Value);
                    userIds.Add(assigned.UpperLevel5.Value);
                }
            }

            userIds = userIds.Distinct().ToList();
            userIds.Remove(Convert.ToInt32(Context.User.GetIdentifier()));

            return userIds;
        }

        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
             return new MySaveHandler(Context, _connections).Process(uow, request, SaveRequestType.Create);
        }

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(Context, _connections).Process(uow, request, SaveRequestType.Update);
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
             return new MyListHandler(Context, _connections).Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            private readonly ISqlConnections _connections;

            public MySaveHandler(IRequestContext context, ISqlConnections connections) : base(context)
            {
                _connections = connections;
            }
            protected override void AfterSave()
            {
                base.AfterSave();

                int type = 0;
                string str = "";
                if (this.IsCreate)
                {
                    UserRow user;
                    using (var conn = _connections.NewFor<UserRow>())
                    {
                        var u = UserRow.Fields;

                        user = conn.TryById<UserRow>(Request.Entity.RepresentativeId, q => q
                        .SelectTableFields()
                        .Select(u.Username));
                    }
                    type = 1;

                    str = "TeleCalling Created and Represented by " + user.Username + "</b>"; ;
                }
                else if (this.IsUpdate)
                {
                    if (Old.Status != Request.Entity.Status)
                    {
                        str = "Status Changed to <b>" + Request.Entity.Status.GetEnumDescription(Localizer) + "</b>.<br/>";
                        type = 2;
                    }

                    if (Request.Entity.AssignedTo.HasValue() && Old.AssignedTo != Request.Entity.AssignedTo)
                    {
                        UserRow user;
                        using (var conn = _connections.NewFor<UserRow>())
                        {
                            var u = UserRow.Fields;

                            user = conn.TryById<UserRow>(Request.Entity.AssignedTo, q => q
                            .SelectTableFields()
                            .Select(u.Username));
                        }

                        str = str + "TeleCalling Assigned to <b>" + user.Username + "</b>.<br/>";
                        type = 3;
                    }
                    if ( Old.StageId != Request.Entity.StageId)
                    {
                        StageRow stage;
                        using (var conn = _connections.NewFor<StageRow>())
                        {
                            var u = StageRow.Fields;

                            stage = conn.TryById<StageRow>(Request.Entity.StageId, q => q
                            .SelectTableFields()
                            .Select(u.Stage));
                        }
                        str = "Stage Changed to <b>" + stage.Stage + "</b>.<br/>";
                        type = 4;
                    }
                }

                if (type != 0 && str.Length > 0)
                {
                    var Timeline = new TimelineRow();

                    Timeline.EntityType = Request.Entity.Table;
                    Timeline.EntityId = Row.Id;
                    Timeline.InsertDate = null;
                    Timeline.Type = type;
                    Timeline.Text = str;
                    Timeline.ClearAssignment(TimelineRow.Fields.InsertDate);

                    new TimelineRepository(Context).Create(this.UnitOfWork, new SaveRequest<TimelineRow>
                    {
                        Entity = Timeline
                    });
                }

                var repo = new TeleCallingRepository(Context, _connections);
                List<Int32> userIds = repo.GetNotifyUsers(Request.Entity);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.TeleCalling;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Services/TeleCalling#edit/" + Request.Entity.Id;

                notify.Text = Context.User.Identity.Name + " has" + (this.IsUpdate ? " Updated" : " Created") + " a TeleCalling";
                notify.UserList = userIds;

                new NotificationsRepository(Context).Create(this.UnitOfWork, new SaveRequest<NotificationsRow>
                {
                    Entity = notify
                });
            }
        }
         private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow>
        {
            private readonly ISqlConnections _connections;

            public MyListHandler(IRequestContext context, ISqlConnections connections) : base(context)
            {
                _connections = connections;
            }

            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                var user = (UserDefinition)Context.User.ToUserDefinition();

                if (user.UserId == 1)
                {
                    return;
                }

                var data = new UsersList();

                using (var connection = _connections.NewFor<UserRow>())
                {
                    var od = UserRow.Fields;
                    data.Users1 = connection.List<UserRow>(q => q
                        .SelectTableFields()
                        .Select(od.UserId)
                        .Where(od.UpperLevel == user.UserId || od.UpperLevel2 == user.UserId || od.UpperLevel3 == user.UserId || od.UpperLevel4 == user.UserId || od.UpperLevel5 == user.UserId)
                        );
                }

                var str = fld.RepresentativeId + " = " + user.UserId + " OR " + fld.AssignedTo + " = " + user.UserId;

                var str1 = "";

                foreach (var item in data.Users1)
                {
                    str1 = str1 + " OR " + fld.RepresentativeId + " = " + item.UserId.Value + " OR " + fld.AssignedTo + " = " + item.UserId.Value;
                }

                query.Where(new Criteria("(" + str + str1 + ")"));

                //query.Where((fld.AssignedBy == user.UserId) | (fld.AssignedTo == user.UserId) | (fld.AssignedBy == item.UserId.Value) |(fld.AssignedTo == item.UserId.Value));
            }

            public class UsersList
            {
                public List<UserRow> Users1 { get; set; }
            }
        }
    }
}