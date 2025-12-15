using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayMisRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayMisRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayMisSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayMisSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayMisSaveHandler
    {
        public DemandayMisSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}