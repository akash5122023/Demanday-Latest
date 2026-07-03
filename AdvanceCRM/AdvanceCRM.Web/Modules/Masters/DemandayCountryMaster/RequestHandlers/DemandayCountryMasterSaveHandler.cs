using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Masters.DemandayCountryMasterRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Masters.DemandayCountryMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayCountryMasterSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayCountryMasterSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayCountryMasterSaveHandler
    {
        public DemandayCountryMasterSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}