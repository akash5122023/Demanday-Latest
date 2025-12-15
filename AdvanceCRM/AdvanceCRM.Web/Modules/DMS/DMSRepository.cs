
namespace AdvanceCRM.DMS.Repositories
{
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using Administration;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using MyRow =DMSRow;

    public class DMSRepository : BaseRepository
    {
        private readonly ISqlConnections _connections;
        public DMSRepository(IRequestContext context ,ISqlConnections connections) : base(context) {
            _connections = connections;
        }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }

        //public static List<Int32> getNotifyUsers(MyRow Entity)
        //{
        //    List<Int32> userIds = new List<Int32>();

        //    using (var conn = _connections.NewFor<UserRow>())
        //    {
        //        var od = UserRow.Fields;
        //        var owner = new UserRow();
        //        var assigned = new UserRow();

        //        if (owner.HasValue())
        //        {
        //            owner = conn.TryById<UserRow>(Entity.OwnerId, q => q
        //            .SelectTableFields()
        //            .Select(od.UserId)
        //            .Select(od.UpperLevel)
        //            .Select(od.UpperLevel2)
        //            .Select(od.UpperLevel3)
        //            .Select(od.UpperLevel4)
        //            .Select(od.UpperLevel5)
        //            );
        //        }
        //        if (assigned.HasValue())
        //        {
        //            assigned = conn.TryById<UserRow>(Entity.AssignedId, q => q
        //            .SelectTableFields()
        //            .Select(od.UserId)
        //            .Select(od.UpperLevel)
        //            .Select(od.UpperLevel2)
        //            .Select(od.UpperLevel3)
        //            .Select(od.UpperLevel4)
        //            .Select(od.UpperLevel5)
        //            );
        //        }

        //        if (owner.HasValue())
        //        {
        //            userIds.Add(owner.UserId.Value);
        //            userIds.Add(owner.UpperLevel.Value);
        //            userIds.Add(owner.UpperLevel2.Value);
        //            userIds.Add(owner.UpperLevel3.Value);
        //            userIds.Add(owner.UpperLevel4.Value);
        //            userIds.Add(owner.UpperLevel5.Value);
        //        }
        //        if (assigned.HasValue())
        //        {
        //            userIds.Add(assigned.UserId.Value);
        //            userIds.Add(assigned.UpperLevel.Value);
        //            userIds.Add(assigned.UpperLevel2.Value);
        //            userIds.Add(assigned.UpperLevel3.Value);
        //            userIds.Add(assigned.UpperLevel4.Value);
        //            userIds.Add(assigned.UpperLevel5.Value);
        //        }
        //        //if (Entity.MultiAssignList.HasValue() && Entity.MultiAssignList.Count > 0)
        //        //    userIds.AddRange(Entity.MultiAssignList);
        //    }

        //    userIds = userIds.Distinct().ToList();
        //    userIds.Remove(Convert.ToInt32(Context.User.GetIdentifier()));

        //    return userIds;
        //}

        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
             return new MySaveHandler(Context).Process(uow, request, SaveRequestType.Create);
        }

        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
            return new MySaveHandler(Context).Process(uow, request, SaveRequestType.Update);
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
            public MySaveHandler(IRequestContext context) : base(context) { }

            protected override void BeforeSave()
            {
                base.BeforeSave();
                var user = (UserDefinition)Context.User.ToUserDefinition();

                if (this.IsCreate)
                {
                    Request.Entity.OwnerId = user.UserId;
                    Request.Entity.CreateDate = DateTime.Now;
                }
                Request.Entity.LastUpdatedId = user.UserId;
                Request.Entity.UpdateDate = DateTime.Now;
            }
        }
         private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow> {
            private readonly DMSRepository _repository;

            public MyListHandler(DMSRepository repository) : base(repository.Context) {
                _repository = repository;
            }

            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                var user = (UserDefinition)Context.User.ToUserDefinition();

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

                query.Where(new Criteria("(" + str + ")"));


                //query.Where((fld.OwnerId == user.UserId) | (fld.AssignedId == user.UserId) | (fld.OwnerId == item.UserId.Value) |(fld.AssignedId == item.UserId.Value));
            }

            public class UsersList
            {
                public List<UserRow> Users1 { get; set; }

            }
        }
    }
}