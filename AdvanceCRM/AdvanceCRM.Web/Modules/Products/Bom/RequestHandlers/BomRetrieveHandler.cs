using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Products.BomRow>;
using MyRow = AdvanceCRM.Products.BomRow;

namespace AdvanceCRM.Products
{
    public interface IBomRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class BomRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IBomRetrieveHandler
    {
        public BomRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}