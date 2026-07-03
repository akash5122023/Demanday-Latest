using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Masters.DemandayEmployeeSizeMasterRow>;
using MyRow = AdvanceCRM.Masters.DemandayEmployeeSizeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayEmployeeSizeMasterListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayEmployeeSizeMasterListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayEmployeeSizeMasterListHandler
    {
        public DemandayEmployeeSizeMasterListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}