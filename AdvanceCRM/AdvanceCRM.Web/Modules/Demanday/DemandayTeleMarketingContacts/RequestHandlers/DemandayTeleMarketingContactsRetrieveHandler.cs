using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Demanday.DemandayTeleMarketingContactsRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingContactsRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingContactsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingContactsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingContactsRetrieveHandler
    {
        public DemandayTeleMarketingContactsRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}