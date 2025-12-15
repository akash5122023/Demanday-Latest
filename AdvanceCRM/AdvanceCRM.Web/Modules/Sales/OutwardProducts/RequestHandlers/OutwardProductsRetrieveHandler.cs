using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Sales.OutwardProductsRow>;
using MyRow = AdvanceCRM.Sales.OutwardProductsRow;

namespace AdvanceCRM.Sales
{
    public interface IOutwardProductsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class OutwardProductsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IOutwardProductsRetrieveHandler
    {
        public OutwardProductsRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}