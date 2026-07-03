using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Masters.DemandayCountryMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandayCountryMasterDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayCountryMasterDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayCountryMasterDeleteHandler
    {
        public DemandayCountryMasterDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}