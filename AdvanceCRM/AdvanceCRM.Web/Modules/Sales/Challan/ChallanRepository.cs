namespace AdvanceCRM.Sales.Repositories
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using AdvanceCRM.Contacts;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MyRow = ChallanRow;

    public class ChallanRepository : BaseRepository
    {
        private readonly ISqlConnections _connections;
        private readonly IRequestContext _context;

        public ChallanRepository(IRequestContext context, ISqlConnections connections)
            : base(context)
        {
            _context = context;
            _connections = connections;
        }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }

        public List<int> GetNotifyUsers(MyRow entity)
        {
            var userIds = new List<int>();

            using (var conn = _connections.NewFor<UserRow>())
            {
                var od = UserRow.Fields;
                var owner = conn.TryById<UserRow>(entity.OwnerId, q => q
                    .SelectTableFields()
                    .Select(od.UserId)
                    .Select(od.UpperLevel)
                    .Select(od.UpperLevel2)
                    .Select(od.UpperLevel3)
                    .Select(od.UpperLevel4)
                    .Select(od.UpperLevel5));

                var assigned = conn.TryById<UserRow>(entity.AssignedId, q => q
                    .SelectTableFields()
                    .Select(od.UserId)
                    .Select(od.UpperLevel)
                    .Select(od.UpperLevel2)
                    .Select(od.UpperLevel3)
                    .Select(od.UpperLevel4)
                    .Select(od.UpperLevel5));

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
            userIds.Remove(Convert.ToInt32(_context.User.GetIdentifier()));

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
            return new MyDeleteHandler(_context).Process(uow, request);
        }

        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
            return new MyRetrieveHandler(_context).Process(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyListHandler(this).Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            private readonly ChallanRepository _repository;

            public MySaveHandler(ChallanRepository repository) : base(repository._context)
            {
                _repository = repository;
            }

            protected override void AfterSave()
            {
                base.AfterSave();

                var userIds = _repository.GetNotifyUsers(Request.Entity);

                var notify = new NotificationsRow
                {
                    Module = Masters.NotificationModules.Challan,
                    InsertDate = DateTime.Now,
                    InsertUserId = Convert.ToInt32(Context.User.GetIdentifier()),
                    Url = "/Challan#edit/" + Request.Entity.Id,
                    Text = _repository._context.User.Identity.Name + " has" + (IsUpdate ? " Updated" : " Created") + " a Challan",
                    UserList = userIds
                };

                new NotificationsRepository(Context).Create(UnitOfWork, new SaveRequest<NotificationsRow>
                {
                    Entity = notify
                });
            }

            protected override void BeforeSave()
            {
                base.BeforeSave();

                if (IsUpdate && Old.Status == Masters.StatusMaster.Closed)
                {
                    if (!_repository._context.Permissions.HasPermission("Challan:Re-open Challan"))
                    {
                        throw new Exception("Authorization failed to change status!");
                    }
                }
            }
        }

        private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
        private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow>
        {
            private readonly ChallanRepository _repository;

            public MyListHandler(ChallanRepository repository) : base(repository._context)
            {
                _repository = repository;
            }

            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                var user = (UserDefinition)_repository._context.User.ToUserDefinition();

                var data = new UsersList();

                using (var connection = _repository._connections.NewFor<UserRow>())
                {
                    var od = UserRow.Fields;
                    data.Users1 = connection.List<UserRow>(q => q
                        .SelectTableFields()
                        .Select(od.UserId)
                        .Where(od.UpperLevel == user.UserId || od.UpperLevel2 == user.UserId || od.UpperLevel3 == user.UserId || od.UpperLevel4 == user.UserId || od.UpperLevel5 == user.UserId));
                }

                var str = fld.OwnerId + " = " + user.UserId + " OR " + fld.AssignedId + " = " + user.UserId;

                var str1 = "";

                foreach (var item in data.Users1)
                {
                    str1 = str1 + " OR " + fld.OwnerId + " = " + item.UserId.Value + " OR " + fld.AssignedId + " = " + item.UserId.Value;
                }

                query.Where(new Criteria("(" + str + str1 + ")"));
            }

            public class UsersList
            {
                public List<UserRow> Users1 { get; set; }
            }
        }
    }
}
