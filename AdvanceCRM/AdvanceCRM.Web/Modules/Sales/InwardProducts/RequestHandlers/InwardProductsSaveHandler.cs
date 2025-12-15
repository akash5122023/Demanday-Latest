using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Sales.InwardProductsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Sales.InwardProductsRow;

namespace AdvanceCRM.Sales
{
    public interface IInwardProductsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class InwardProductsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IInwardProductsSaveHandler
    {
        public InwardProductsSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}