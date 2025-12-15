using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Demanday.DemandayVerificationRow>;
using MyRow = AdvanceCRM.Demanday.DemandayVerificationRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayVerificationRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayVerificationRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayVerificationRetrieveHandler
    {
        public DemandayVerificationRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}