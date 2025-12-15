using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Products.BomRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Products.BomRow;

namespace AdvanceCRM.Products
{
    public interface IBomSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class BomSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IBomSaveHandler
    {
        public BomSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}