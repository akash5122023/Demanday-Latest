using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Masters.DemandayCountryMasterRow>;
using MyRow = AdvanceCRM.Masters.DemandayCountryMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayCountryMasterListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayCountryMasterListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayCountryMasterListHandler
    {
        public DemandayCountryMasterListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}