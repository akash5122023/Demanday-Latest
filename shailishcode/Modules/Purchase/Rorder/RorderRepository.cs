namespace AdvanceCRM.Purchase.Repositories
{
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Data;
    using System.Linq;
    using MyRow = RorderRow;

    public class RorderRepository : BaseRepository
    {
        protected ISqlConnections _connections { get; }

        public RorderRepository(IRequestContext context, ISqlConnections connections)
            : base(context)
        {
            _connections = connections;
        }

        private static MyRow.RowFields fld { get { return MyRow.Fields; } }

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

        // If you need the custom list with Quantity < MinimumStock filter:
        //public ListResponse<MyRow> ListReorderItems(IDbConnection connection, ListRequest request)
        //{
        //    var listHandler = new MyListHandler(Context);

        //    // Apply custom filter for reorder items
        //    listHandler.CustomFilter = query =>
        //    {
        //        query.Where(new Criteria(fld.Quantity) < new Criteria(fld.MinimumStock));
        //    };

        //    return listHandler.Process(connection, request);
        //}

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
            public Action<SqlQuery> CustomFilter { get; set; }

            public MyListHandler(IRequestContext context) : base(context) { }

            protected override void ApplyFilters(SqlQuery query)
            {
                base.ApplyFilters(query);
                CustomFilter?.Invoke(query);
            }
        }
    }
}