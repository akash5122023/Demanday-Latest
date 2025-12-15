using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayVerificationRow>;
using MyRow = AdvanceCRM.Demanday.DemandayVerificationRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayVerificationListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayVerificationListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayVerificationListHandler
    {
        public DemandayVerificationListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}