using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayTeleMarketingMISRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingMISRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingMISListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingMISListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingMISListHandler
    {
        public DemandayTeleMarketingMISListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}