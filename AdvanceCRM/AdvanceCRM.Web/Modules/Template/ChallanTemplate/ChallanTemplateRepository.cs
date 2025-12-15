
namespace AdvanceCRM.Template.Repositories
{
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System;
    using System.Data;
    using Serenity.Extensions.DependencyInjection;
    using MyRow = ChallanTemplateRow;

    public class ChallanTemplateRepository : BaseRepository
    {
        private readonly ISqlConnections _connections;

        public ChallanTemplateRepository(IRequestContext context, ISqlConnections connections) : base(context)
        {
            _connections = connections;
        }

        public ChallanTemplateRepository(IRequestContext context)
            : this(context, Dependency.Resolve<ISqlConnections>())
        {
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
            if ((long)request.EntityId == -1)
            {
                var user = (UserDefinition)Context.User.ToUserDefinition();
                using (var conn = _connections.NewFor<MyRow>())
                {
                    var f = MyRow.Fields;
                    var row = conn.TryFirst<MyRow>(q => q
                    .SelectTableFields()
                    .Select(f.Id)
                    .Where(f.CompanyId == user.CompanyId));
                    request.EntityId = row.Id;
                }
            }
            return new MyRetrieveHandler(Context).Process(connection, request);
        }

        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
             return new MyListHandler(Context).Process(connection, request);
        }

         private class MySaveHandler : SaveRequestHandler<MyRow> { public MySaveHandler(IRequestContext context) : base(context) { } }
         private class MyDeleteHandler : DeleteRequestHandler<MyRow> { public MyDeleteHandler(IRequestContext context) : base(context) { } }
       private class MyRetrieveHandler : RetrieveRequestHandler<MyRow> { public MyRetrieveHandler(IRequestContext context) : base(context) { } }
        private class MyListHandler : ListRequestHandler<MyRow> { public MyListHandler(IRequestContext context) : base(context) { } }
    }
}