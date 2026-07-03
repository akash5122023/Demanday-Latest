using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Masters.DemandaySubIndustryMasterRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Masters.DemandaySubIndustryMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandaySubIndustryMasterSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandaySubIndustryMasterSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandaySubIndustryMasterSaveHandler
    {
        public DemandaySubIndustryMasterSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}