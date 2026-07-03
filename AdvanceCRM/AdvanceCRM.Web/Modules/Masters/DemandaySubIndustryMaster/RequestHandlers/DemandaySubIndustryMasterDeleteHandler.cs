using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Masters.DemandaySubIndustryMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IDemandaySubIndustryMasterDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandaySubIndustryMasterDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandaySubIndustryMasterDeleteHandler
    {
        public DemandaySubIndustryMasterDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}