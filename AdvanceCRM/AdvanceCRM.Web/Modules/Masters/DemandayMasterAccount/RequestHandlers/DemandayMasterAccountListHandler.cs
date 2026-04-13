using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Masters.DemandayMasterAccountRow>;
using MyRow = AdvanceCRM.Masters.DemandayMasterAccountRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayMasterAccountListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayMasterAccountListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayMasterAccountListHandler
    {
        public DemandayMasterAccountListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}