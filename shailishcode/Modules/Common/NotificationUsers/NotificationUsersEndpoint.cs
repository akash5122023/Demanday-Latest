
namespace AdvanceCRM.Common.Endpoints
{
    using Serenity;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Services;
    using System.Data;
    
    using MyRepository = Repositories.NotificationUsersRepository;
    using MyRow =NotificationUsersRow;

    [Route("Services/Common/NotificationUsers/[action]")]
    [ConnectionKey(typeof(MyRow)), ServiceAuthorize(typeof(MyRow))]
    public class NotificationUsersController : ServiceEndpoint
    {
        private readonly ISqlConnections _connections;

        public NotificationUsersController(ISqlConnections connections)
        {
            _connections = connections;
        }
        [HttpPost, AuthorizeCreate(typeof(MyRow))]
        public SaveResponse Create(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context).Create(uow, request);
        }

        [HttpPost, AuthorizeUpdate(typeof(MyRow))]
        public SaveResponse Update(IUnitOfWork uow, SaveRequest<MyRow> request)
        {
           return new MyRepository(Context).Update(uow, request);
        }
 
        [HttpPost, AuthorizeDelete(typeof(MyRow))]
        public DeleteResponse Delete(IUnitOfWork uow, DeleteRequest request)
        {
           return new MyRepository(Context).Delete(uow, request);
        }

        [HttpPost]
        public RetrieveResponse<MyRow> Retrieve(IDbConnection connection, RetrieveRequest request)
        {
             return new MyRepository(Context).Retrieve(connection, request);
        }

        [HttpPost]
        public ListResponse<MyRow> List(IDbConnection connection, ListRequest request)
        {
            var sqlStr = "UPDATE NotificationUsers SET IsChecked = 1 WHERE UserId = " + Context.User.GetIdentifier();

            //Saving to log
            connection.Execute(sqlStr);
            return new MyRepository(Context).List(connection, request);
        }

        [HttpPost]
        public StandardResponse ClearNotification(IUnitOfWork uow, StandardRequest request)
        {
            var sqlStr = "UPDATE NotificationUsers SET IsChecked = 1 WHERE Id = " + request.Id;

            using (var connection = _connections.NewFor<MyRow>())
            {
                //Saving to log
                connection.Execute(sqlStr);
            }

            return new StandardResponse();
        }
    }
}
