using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Sales.OutwardRow>;
using MyRow = AdvanceCRM.Sales.OutwardRow;

namespace AdvanceCRM.Sales
{
    public interface IOutwardRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class OutwardRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IOutwardRetrieveHandler
    {
        public OutwardRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}