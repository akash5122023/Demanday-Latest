using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Masters.DemandayJobLevelMasterRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Masters.DemandayJobLevelMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayJobLevelMasterSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayJobLevelMasterSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayJobLevelMasterSaveHandler
    {
        public DemandayJobLevelMasterSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}