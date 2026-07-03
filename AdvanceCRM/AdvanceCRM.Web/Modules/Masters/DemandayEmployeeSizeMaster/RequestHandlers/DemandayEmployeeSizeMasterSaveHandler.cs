using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Masters.DemandayEmployeeSizeMasterRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Masters.DemandayEmployeeSizeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayEmployeeSizeMasterSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayEmployeeSizeMasterSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayEmployeeSizeMasterSaveHandler
    {
        public DemandayEmployeeSizeMasterSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}