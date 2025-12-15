using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Purchase.GrnProductsTwoRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Purchase.GrnProductsTwoRow;

namespace AdvanceCRM.Purchase.GrnProductsTwo
{
    public interface IGrnProductsTwoSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class GrnProductsTwoSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IGrnProductsTwoSaveHandler
    {
        public GrnProductsTwoSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}