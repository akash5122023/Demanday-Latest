using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Masters.DemandaySubIndustryMasterRow>;
using MyRow = AdvanceCRM.Masters.DemandaySubIndustryMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandaySubIndustryMasterListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandaySubIndustryMasterListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandaySubIndustryMasterListHandler
    {
        public DemandaySubIndustryMasterListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}