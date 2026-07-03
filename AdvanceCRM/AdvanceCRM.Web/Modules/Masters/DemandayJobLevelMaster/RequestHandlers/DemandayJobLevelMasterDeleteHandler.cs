using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Masters.DemandayJobLevelMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayJobLevelMasterDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayJobLevelMasterDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayJobLevelMasterDeleteHandler
    {
        public DemandayJobLevelMasterDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}