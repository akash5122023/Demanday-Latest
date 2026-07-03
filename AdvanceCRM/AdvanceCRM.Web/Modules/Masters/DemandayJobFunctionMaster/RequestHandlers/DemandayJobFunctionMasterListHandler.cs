using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Masters.DemandayJobFunctionMasterRow>;
using MyRow = AdvanceCRM.Masters.DemandayJobFunctionMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayJobFunctionMasterListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayJobFunctionMasterListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayJobFunctionMasterListHandler
    {
        public DemandayJobFunctionMasterListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}