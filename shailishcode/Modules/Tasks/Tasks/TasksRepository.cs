
namespace AdvanceCRM.Tasks.Repositories
{
    using Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Tasks;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using Serenity.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MyRow = TasksRow;

    public class TasksRepository : BaseRepository
    {
        protected ISqlConnections _connections { get; }

        public TasksRepository(IRequestContext context, ISqlConnections connections)
            : base(context)
        {
            _connections = connections;
        }

        public TasksRepository(IRequestContext context)
            : this(context, Dependency.Resolve<ISqlConnections>())
        {
        }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }

        public List<Int32> getNotifyUsers(MyRow Entity)
        {
            List<Int32> userIds = new List<Int32>();

            using (var conn = _connections.NewFor<UserRow>())
            {
                var od = UserRow.Fields;
                var owner = new UserRow();
                var assigned = new UserRow();
                owner = conn.TryById<UserRow>(Entity.AssignedBy, q => q
                    .SelectTableFields()
                    .Select(od.UserId)
                    .Select(od.UpperLevel)
                    .Select(od.UpperLevel2)
                    .Select(od.UpperLevel3)
                    .Select(od.UpperLevel4)
                    .Select(od.UpperLevel5)
                    );

                assigned = conn.TryById<UserRow>(Entity.AssignedTo, q => q
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

                if (Entity.WatcherList.HasValue() && Entity.WatcherList.Count > 0)
                    userIds.AddRange(Entity.WatcherList);
            }

            userIds = userIds.Distinct().ToList();
            userIds.Remove(Convert.ToInt32(Context.User.GetIdentifier()));

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
            return new MyListHandler(this).Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            private readonly TasksRepository _repository;

            public MySaveHandler(TasksRepository repository) : base(repository.Context)
            {
                _repository = repository;
            }

            protected override void AfterSave()
            {
                base.AfterSave();

                int type = 0;
                string str = "";
                if (this.IsCreate)
                {
                    UserRow user;
                    using (var connection = _repository._connections.NewFor<UserRow>())
                    {
                        var u = UserRow.Fields;

                        user = connection.TryById<UserRow>(Request.Entity.AssignedTo, q => q
                        .SelectTableFields()
                        .Select(u.Username));
                    }
                    type = 1;

                    str = "Task Created by " + Authorization.Username + ".<br/>Task Assigned to " + user.Username;
                }
                else if (this.IsUpdate)
                {
                    if (Old.StatusId != Request.Entity.StatusId)
                    {
                        TaskStatusRow status;
                        using (var connection = _repository._connections.NewFor<TaskStatusRow>())
                        {
                            var s = TaskStatusRow.Fields;

                            status = connection.TryById<TaskStatusRow>(Request.Entity.StatusId, q => q
                            .SelectTableFields()
                            .Select(s.Status));
                        }

                        str = "Status Changed to <b>" + status.Status + "</b>.<br/>";
                        type = 2;
                    }

                    if (Old.AssignedTo != Request.Entity.AssignedTo)
                    {
                        UserRow user;
                        using (var connection = _repository._connections.NewFor<UserRow>())
                        {
                            var u = UserRow.Fields;

                            user = connection.TryById<UserRow>(Request.Entity.AssignedTo, q => q
                            .SelectTableFields()
                            .Select(u.Username));
                        }

                        str = str + "Task Assigned to <b>" + user.Username + "</b>.<br/>";
                        type = 3;
                    }
                    if (Old.ExpectedCompletion != Request.Entity.ExpectedCompletion)
                    {
                        //StageRow stage;
                        //using (var conn = _connections.NewFor<StageRow>())
                        //{
                        //    var u = StageRow.Fields;

                        //    stage = conn.TryById<StageRow>(Request.Entity.StageId, q => q
                        //    .SelectTableFields()
                        //    .Select(u.Stage));
                        //}
                        str = "Expected Completion Date is Changed to <b>" + Request.Entity.ExpectedCompletion + "</b>.<br/>";
                        type = 5;
                    }
                }

                if (type != 0 && str.Length > 0)
                {
                    var Timeline = new TimelineRow();

                    Timeline.EntityType = Row.Table;
                    Timeline.EntityId = Row.Id.Value;
                    Timeline.InsertDate = null;
                    Timeline.Type = type;
                    Timeline.Text = str;
                    Timeline.ClearAssignment(TimelineRow.Fields.InsertDate);

                    new TimelineRepository(_repository.Context).Create(this.UnitOfWork, new SaveRequest<TimelineRow>
                    {
                        Entity = Timeline
                    });
                }

                List<Int32> userIds = _repository.getNotifyUsers(Request.Entity);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.Tasks;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/Tasks#edit/" + Row.Id.Value;

                notify.Text = Serenity.Authorization.Username + " has" + (this.IsUpdate ? " Updated" : " Created") + " a Task";
                notify.UserList = userIds;

                new NotificationsRepository(_repository.Context).Create(this.UnitOfWork, new SaveRequest<NotificationsRow>
                {
                    Entity = notify
                });
            }
        }
         private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow>
        {
            private readonly TasksRepository _repository;

            public MyListHandler(TasksRepository repository) : base(repository.Context)
            {
                _repository = repository;
            }

            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                int uid = 0;
                var User = new UserRow();
                var user = (UserDefinition)Context.User.ToUserDefinition();
                if (user.UserId != 1)
                {

                    using (var connection = _repository._connections.NewFor<UserRow>())
                    {
                        var qt = UserRow.Fields;
                        User = connection.TryFirst<UserRow>(q => q
                           .SelectTableFields()
                           .Select(qt.Enquiry)
                          .Where(qt.UserId == Convert.ToInt32(user.UserId))
                         );
                    }

                    if (User != null && User.Tasks == true)
                    {
                        uid = Convert.ToInt32(User.UserId);
                    }

                }
                //For products filter
                if (user.UserId == 1 || user.UserId == uid)
                {
                    return;
                }

                var data = new UsersList();

                using (var connection = _repository._connections.NewFor<UserRow>())
                {
                    var od = UserRow.Fields;
                    data.Users1 = connection.List<UserRow>(q => q
                        .SelectTableFields()
                        .Select(od.UserId)
                        .Where(od.UpperLevel == user.UserId || od.UpperLevel2 == user.UserId || od.UpperLevel3 == user.UserId || od.UpperLevel4 == user.UserId || od.UpperLevel5 == user.UserId)
                        );

                    var mr = TaskWatcherRow.Fields;
                    data.Users2 = connection.List<TaskWatcherRow>(q => q
                        .SelectTableFields()
                        .Select(mr.TasksId)
                        .Where(mr.AssignedId == user.UserId)
                        );
                }

                var str = fld.AssignedBy + " = " + user.UserId + " OR " + fld.AssignedTo + " = " + user.UserId;

                var str1 = "";
                var str2 = "";

                foreach (var item in data.Users1)
                {
                    str1 = str1 + " OR " + fld.AssignedBy + " = " + item.UserId.Value + " OR " + fld.AssignedTo + " = " + item.UserId.Value;
                }

                foreach (var item in data.Users2)
                {
                    str2 = str2 + " OR " + fld.Id + " = " + item.TasksId.Value;
                }
                query.Where(new Criteria("(" + str + str1 + str2 + ")"));

                //query.Where((fld.AssignedBy == user.UserId) | (fld.AssignedTo == user.UserId) | (fld.AssignedBy == item.UserId.Value) |(fld.AssignedTo == item.UserId.Value));
            }

            public class UsersList
            {
                public List<UserRow> Users1 { get; set; }
                public List<TaskWatcherRow> Users2 { get; set; }
            }
        }
    }
}