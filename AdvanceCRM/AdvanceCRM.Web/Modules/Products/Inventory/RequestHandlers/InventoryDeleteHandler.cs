using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Products.InventoryRow;

namespace AdvanceCRM.Products
{
    public interface IInventoryDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class InventoryDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IInventoryDeleteHandler
    {
        public InventoryDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}