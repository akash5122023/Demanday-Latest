using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Sales.OutwardRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Sales.OutwardRow;

namespace AdvanceCRM.Sales
{
    public interface IOutwardSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class OutwardSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IOutwardSaveHandler
    {
        public OutwardSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}