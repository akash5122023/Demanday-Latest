using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Masters.DemandayEmployeeSizeMasterRow>;
using MyRow = AdvanceCRM.Masters.DemandayEmployeeSizeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayEmployeeSizeMasterRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayEmployeeSizeMasterRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayEmployeeSizeMasterRetrieveHandler
    {
        public DemandayEmployeeSizeMasterRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}