using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Masters.DemandayJobLevelMasterRow>;
using MyRow = AdvanceCRM.Masters.DemandayJobLevelMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayJobLevelMasterRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayJobLevelMasterRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayJobLevelMasterRetrieveHandler
    {
        public DemandayJobLevelMasterRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}