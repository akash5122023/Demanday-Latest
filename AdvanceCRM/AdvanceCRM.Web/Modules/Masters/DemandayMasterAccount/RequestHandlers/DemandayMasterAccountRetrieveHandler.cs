using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Masters.DemandayMasterAccountRow>;
using MyRow = AdvanceCRM.Masters.DemandayMasterAccountRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayMasterAccountRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayMasterAccountRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayMasterAccountRetrieveHandler
    {
        public DemandayMasterAccountRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}