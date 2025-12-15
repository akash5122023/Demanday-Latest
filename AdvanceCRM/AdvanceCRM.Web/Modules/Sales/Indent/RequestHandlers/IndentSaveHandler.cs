using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Sales.IndentRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Sales.IndentRow;

namespace AdvanceCRM.Sales
{
    public interface IIndentSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class IndentSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IIndentSaveHandler
    {
        public IndentSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}