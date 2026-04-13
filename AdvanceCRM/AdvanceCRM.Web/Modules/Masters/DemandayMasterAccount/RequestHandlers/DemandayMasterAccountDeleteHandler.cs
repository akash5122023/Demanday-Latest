using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Masters.DemandayMasterAccountRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayMasterAccountDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayMasterAccountDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayMasterAccountDeleteHandler
    {
        public DemandayMasterAccountDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}