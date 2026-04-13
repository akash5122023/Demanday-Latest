using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Toolkit.ClientSupressionRow>;
using MyRow = AdvanceCRM.Toolkit.ClientSupressionRow;

namespace AdvanceCRM.Toolkit
{
    public interface IClientSupressionListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class ClientSupressionListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IClientSupressionListHandler
    {
        public ClientSupressionListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}