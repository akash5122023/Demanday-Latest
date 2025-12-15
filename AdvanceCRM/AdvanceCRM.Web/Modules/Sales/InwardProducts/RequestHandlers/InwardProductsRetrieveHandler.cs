using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Sales.InwardProductsRow>;
using MyRow = AdvanceCRM.Sales.InwardProductsRow;

namespace AdvanceCRM.Sales
{
    public interface IInwardProductsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class InwardProductsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IInwardProductsRetrieveHandler
    {
        public InwardProductsRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}