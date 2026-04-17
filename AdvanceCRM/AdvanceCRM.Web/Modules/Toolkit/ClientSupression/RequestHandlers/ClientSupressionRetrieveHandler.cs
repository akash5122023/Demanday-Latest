using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Toolkit.ClientSupressionRow>;
using MyRow = AdvanceCRM.Toolkit.ClientSupressionRow;

namespace AdvanceCRM.Toolkit
{
    public interface IClientSupressionRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class ClientSupressionRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IClientSupressionRetrieveHandler
    {
        public ClientSupressionRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}