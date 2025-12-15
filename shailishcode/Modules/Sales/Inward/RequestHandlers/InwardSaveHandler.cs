using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Sales.InwardRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Sales.InwardRow;

namespace AdvanceCRM.Sales
{
    public interface IInwardSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class InwardSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IInwardSaveHandler
    {
        public InwardSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}