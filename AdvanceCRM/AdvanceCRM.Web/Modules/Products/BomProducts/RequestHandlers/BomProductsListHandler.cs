using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Products.BomProductsRow>;
using MyRow = AdvanceCRM.Products.BomProductsRow;

namespace AdvanceCRM.Products
{
    public interface IBomProductsListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class BomProductsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IBomProductsListHandler
    {
        public BomProductsListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}