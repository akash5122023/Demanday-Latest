using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Demanday.DemandayQualityRow>;
using MyRow = AdvanceCRM.Demanday.DemandayQualityRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayQualityRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayQualityRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayQualityRetrieveHandler
    {
        public DemandayQualityRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}