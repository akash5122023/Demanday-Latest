using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Products.InventoryRow>;
using MyRow = AdvanceCRM.Products.InventoryRow;

namespace AdvanceCRM.Products
{
    public interface IInventoryListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class InventoryListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IInventoryListHandler
    {
        public InventoryListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}