
namespace AdvanceCRM.Purchase.Repositories
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common.Repositories;
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using Serenity.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MyRow = PurchaseOrderRow;

    public class PurchaseOrderRepository : BaseRepository
    {
        protected ISqlConnections _connections { get; }

        public PurchaseOrderRepository(IRequestContext context, ISqlConnections connections)
            : base(context)
        {
            _connections = connections;
        }

        public PurchaseOrderRepository(IRequestContext context)
            : this(context, Dependency.Resolve<ISqlConnections>())
        {
        }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }
        public List<int> getNotifyUsers(MyRow Entity)
        {
            List<Int32> userIds = new List<Int32>();

            using (var conn = _connections.NewFor<UserRow>())
            {
                var od = UserRow.Fields;
                var owner = new UserRow();
                var assigned = new UserRow();
                owner = conn.TryById<UserRow>(Entity.OwnerId, q => q
                    .SelectTableFields()
                    .Select(od.UserId)
                    .Select(od.UpperLevel)
                    .Select(od.UpperLevel2)
                    .Select(od.UpperLevel3)
                    .Select(od.UpperLevel4)
                    .Select(od.UpperLevel5)
                    );

                assigned = conn.TryById<UserRow>(Entity.AssignedId, q => q
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
            private readonly PurchaseOrderRepository _repository;

            public MySaveHandler(PurchaseOrderRepository repository) : base(repository.Context)
            {
                _repository = repository;
            }

            protected override void AfterSave()
            {
                base.AfterSave();

                List<Int32> userIds = _repository.getNotifyUsers(Request.Entity);

                var notify = new NotificationsRow();

                notify.Module = Masters.NotificationModules.PurchaseOrder;
                notify.InsertDate = System.DateTime.Now;
                notify.InsertUserId = Convert.ToInt32(Context.User.GetIdentifier());
                notify.Url = "/PurchaseOrder#edit/" + Request.Entity.Id;
                notify.Text = Serenity.Authorization.Username + " has" + (this.IsUpdate ? " Updated" : " Created") + " a Purchase Order";
                notify.UserList = userIds;

                new NotificationsRepository(_repository.Context).Create(this.UnitOfWork, new SaveRequest<NotificationsRow>
                {
                    Entity = notify
                });
            }
            protected override void BeforeSave()
            {
                base.BeforeSave();

                if (this.IsUpdate && Old.Status == Masters.StatusMaster.Closed)
                {
                    if (!Serenity.Authorization.HasPermission("PurchaseOrder:Re-open PurchaseOrder"))
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
            private readonly PurchaseOrderRepository _repository;

            public MyListHandler(PurchaseOrderRepository repository) : base(repository.Context)
            {
                _repository = repository;
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

                using (var connection = _repository._connections.NewFor<UserRow>())
                {
                    var od = UserRow.Fields;
                    data.Users1 = connection.List<UserRow>(q => q
                        .SelectTableFields()
                        .Select(od.UserId)
                        .Where(od.UpperLevel == user.UserId || od.UpperLevel2 == user.UserId || od.UpperLevel3 == user.UserId || od.UpperLevel4 == user.UserId || od.UpperLevel5 == user.UserId)
                        );
                }

                var str = fld.OwnerId + " = " + user.UserId + " OR " + fld.AssignedId + " = " + user.UserId;

                var str1 = "";

                foreach (var item in data.Users1)
                {
                    str1 = str1 + " OR " + fld.OwnerId + " = " + item.UserId.Value + " OR " + fld.AssignedId + " = " + item.UserId.Value;
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