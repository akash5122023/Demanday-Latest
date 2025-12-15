using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Purchase.GrnTwoRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Purchase.GrnTwoRow;

namespace AdvanceCRM.Purchase.GrnTwo
{
    public interface IGrnTwoSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class GrnTwoSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IGrnTwoSaveHandler
    {
        public GrnTwoSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}