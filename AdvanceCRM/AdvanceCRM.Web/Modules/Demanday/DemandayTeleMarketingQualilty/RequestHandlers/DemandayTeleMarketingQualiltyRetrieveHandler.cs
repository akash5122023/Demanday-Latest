using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Demanday.DemandayTeleMarketingQualiltyRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingQualiltyRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingQualiltyRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingQualiltyRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingQualiltyRetrieveHandler
    {
        public DemandayTeleMarketingQualiltyRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}