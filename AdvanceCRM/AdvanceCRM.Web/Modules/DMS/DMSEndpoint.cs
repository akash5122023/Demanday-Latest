
namespace AdvanceCRM.DMS.Endpoints
{
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System.Data;
    
    using MyRepository = Repositories.DMSRepository;
    using MyRow =DMSRow;
    using Serenity.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;

    [Route("Services/DMS/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class DMSController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;
        private IRequestContext Context { get; }

        [ActivatorUtilitiesConstructor]
        public DMSController(ISqlConnections connections, IRequestContext context)
        {
            _connections = connections;
            Context = context;
        }

        public DMSController()
            : this(Dependency.Resolve<ISqlConnections>(), Dependency.Resolve<IRequestContext>())
        {
        }

        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context, _connections).Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context, _connections).Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
           return new MyRepository(Context, _connections).Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
             return new MyRepository(Context, _connections).Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            return new MyRepository(Context, _connections).List(connection, request);
        }
    }
}
