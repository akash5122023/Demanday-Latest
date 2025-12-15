using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayQualityRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayQualityRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayQualitySaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayQualitySaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayQualitySaveHandler
    {
        public DemandayQualitySaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}