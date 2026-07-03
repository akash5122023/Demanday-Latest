using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Masters.DemandayJobFunctionMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayJobFunctionMasterDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayJobFunctionMasterDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayJobFunctionMasterDeleteHandler
    {
        public DemandayJobFunctionMasterDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}