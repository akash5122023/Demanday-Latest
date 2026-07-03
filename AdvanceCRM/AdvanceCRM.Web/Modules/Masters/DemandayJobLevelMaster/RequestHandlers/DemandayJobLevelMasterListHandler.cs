using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Masters.DemandayJobLevelMasterRow>;
using MyRow = AdvanceCRM.Masters.DemandayJobLevelMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayJobLevelMasterListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayJobLevelMasterListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayJobLevelMasterListHandler
    {
        public DemandayJobLevelMasterListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}