using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Toolkit.ClientSupressionRow;

namespace AdvanceCRM.Toolkit
{
    public interface IClientSupressionDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class ClientSupressionDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IClientSupressionDeleteHandler
    {
        public ClientSupressionDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}