using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Products.BomProductsRow>;
using MyRow = AdvanceCRM.Products.BomProductsRow;

namespace AdvanceCRM.Products
{
    public interface IBomProductsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class BomProductsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IBomProductsRetrieveHandler
    {
        public BomProductsRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}