using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Masters.DemandayMasterAccountRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Masters.DemandayMasterAccountRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayMasterAccountSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayMasterAccountSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayMasterAccountSaveHandler
    {
        public DemandayMasterAccountSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}