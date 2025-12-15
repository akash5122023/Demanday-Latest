using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Sales.IndentProductsRow>;
using MyRow = AdvanceCRM.Sales.IndentProductsRow;

namespace AdvanceCRM.Sales
{
    public interface IIndentProductsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class IndentProductsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IIndentProductsRetrieveHandler
    {
        public IndentProductsRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}