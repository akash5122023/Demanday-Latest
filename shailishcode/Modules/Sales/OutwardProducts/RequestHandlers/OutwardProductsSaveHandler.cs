using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Sales.OutwardProductsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Sales.OutwardProductsRow;

namespace AdvanceCRM.Sales
{
    public interface IOutwardProductsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class OutwardProductsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IOutwardProductsSaveHandler
    {
        public OutwardProductsSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}