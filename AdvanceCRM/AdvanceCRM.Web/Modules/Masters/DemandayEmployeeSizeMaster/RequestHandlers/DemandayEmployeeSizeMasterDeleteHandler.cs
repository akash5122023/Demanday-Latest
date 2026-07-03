using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Masters.DemandayEmployeeSizeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayEmployeeSizeMasterDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayEmployeeSizeMasterDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayEmployeeSizeMasterDeleteHandler
    {
        public DemandayEmployeeSizeMasterDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}