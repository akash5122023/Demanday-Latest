using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Masters.DemandayJobFunctionMasterRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Masters.DemandayJobFunctionMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayJobFunctionMasterSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayJobFunctionMasterSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayJobFunctionMasterSaveHandler
    {
        public DemandayJobFunctionMasterSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}