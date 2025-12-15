using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Products.BomProductsRow;

namespace AdvanceCRM.Products
{
    public interface IBomProductsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class BomProductsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IBomProductsDeleteHandler
    {
        public BomProductsDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}