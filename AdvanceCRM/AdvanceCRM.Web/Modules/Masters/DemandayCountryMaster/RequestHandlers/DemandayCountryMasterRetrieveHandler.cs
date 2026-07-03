using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Masters.DemandayCountryMasterRow>;
using MyRow = AdvanceCRM.Masters.DemandayCountryMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayCountryMasterRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayCountryMasterRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayCountryMasterRetrieveHandler
    {
        public DemandayCountryMasterRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}