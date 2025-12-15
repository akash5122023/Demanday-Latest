namespace AdvanceCRM.Accounting.Repositories
{
    using AdvanceCRM.Administration;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using MyRow = ExpenseManagementRow;

    public class ExpenseManagementRepository : BaseRepository
    {
        public ExpenseManagementRepository(IRequestContext context) : base(context) { }

        private static MyRow.RowFields fld => MyRow.Fields;

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
            return new MyListHandler(Context).Process(connection, request);
        }

        private class MySaveHandler : SaveRequestHandler<MyRow>
        {
            public MySaveHandler(IRequestContext context) : base(context) { }
        }

        private class MyDeleteHandler : DeleteRequestHandler<MyRow>
        {
            public MyDeleteHandler(IRequestContext context) : base(context) { }
        }

        private class MyRetrieveHandler : RetrieveRequestHandler<MyRow>
        {
            public MyRetrieveHandler(IRequestContext context) : base(context) { }
        }

        private class MyListHandler : ListRequestHandler<MyRow>
        {
            public MyListHandler(IRequestContext context) : base(context) { }

            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);

                // Get current user ID from request context
                int userId = Convert.ToInt32(Context.User.GetIdentifier());
                var userIds = new List<int> { userId };

                var connection = this.Connection;
                var od = UserRow.Fields;

                void AddUsersToList(IEnumerable<UserRow> users)
                {
                    foreach (var item in users)
                    {
                        if (item.UserId != null && !userIds.Contains(item.UserId.Value))
                            userIds.Add(item.UserId.Value);
                    }
                }

                AddUsersToList(connection.List<UserRow>(q => q.Select(od.UserId).Where(od.UpperLevel == userId)));
                AddUsersToList(connection.List<UserRow>(q => q.Select(od.UserId).Where(od.UpperLevel2 == userId)));
                AddUsersToList(connection.List<UserRow>(q => q.Select(od.UserId).Where(od.UpperLevel3 == userId)));
                AddUsersToList(connection.List<UserRow>(q => q.Select(od.UserId).Where(od.UpperLevel4 == userId)));
                AddUsersToList(connection.List<UserRow>(q => q.Select(od.UserId).Where(od.UpperLevel5 == userId)));

                // Build Criteria using Serenity expression builder
                var criteria = Criteria.Empty;
                foreach (var id in userIds)
                {
                    criteria |= (fld.RepresentativeId == id) | (fld.ApprovedBy == id);
                }

                query.Where(criteria);
            }
        }
    }
}
