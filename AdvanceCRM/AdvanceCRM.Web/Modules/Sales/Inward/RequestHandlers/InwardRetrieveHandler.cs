using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Sales.InwardRow>;
using MyRow = AdvanceCRM.Sales.InwardRow;

namespace AdvanceCRM.Sales
{
    public interface IInwardRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class InwardRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IInwardRetrieveHandler
    {
        public InwardRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}