using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Products.BomProductsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Products.BomProductsRow;

namespace AdvanceCRM.Products
{
    public interface IBomProductsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class BomProductsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IBomProductsSaveHandler
    {
        public BomProductsSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}