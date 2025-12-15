using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Products.InventoryRow>;
using MyRow = AdvanceCRM.Products.InventoryRow;

namespace AdvanceCRM.Products
{
    public interface IInventoryRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class InventoryRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IInventoryRetrieveHandler
    {
        public InventoryRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}