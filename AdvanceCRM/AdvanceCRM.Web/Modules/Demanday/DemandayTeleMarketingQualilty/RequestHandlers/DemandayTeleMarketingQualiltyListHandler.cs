using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayTeleMarketingQualiltyRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingQualiltyRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingQualiltyListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingQualiltyListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingQualiltyListHandler
    {
        public DemandayTeleMarketingQualiltyListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}