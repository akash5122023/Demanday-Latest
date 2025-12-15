using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Sales.IndentProductsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Sales.IndentProductsRow;

namespace AdvanceCRM.Sales
{
    public interface IIndentProductsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class IndentProductsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IIndentProductsSaveHandler
    {
        public IndentProductsSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}