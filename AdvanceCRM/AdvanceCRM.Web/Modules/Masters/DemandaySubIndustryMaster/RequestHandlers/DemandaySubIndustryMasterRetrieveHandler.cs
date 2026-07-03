using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Masters.DemandaySubIndustryMasterRow>;
using MyRow = AdvanceCRM.Masters.DemandaySubIndustryMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandaySubIndustryMasterRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandaySubIndustryMasterRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandaySubIndustryMasterRetrieveHandler
    {
        public DemandaySubIndustryMasterRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}