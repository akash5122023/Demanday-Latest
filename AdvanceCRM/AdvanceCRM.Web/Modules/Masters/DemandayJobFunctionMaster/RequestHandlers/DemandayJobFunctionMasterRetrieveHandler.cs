using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Masters.DemandayJobFunctionMasterRow>;
using MyRow = AdvanceCRM.Masters.DemandayJobFunctionMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayJobFunctionMasterRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayJobFunctionMasterRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayJobFunctionMasterRetrieveHandler
    {
        public DemandayJobFunctionMasterRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}