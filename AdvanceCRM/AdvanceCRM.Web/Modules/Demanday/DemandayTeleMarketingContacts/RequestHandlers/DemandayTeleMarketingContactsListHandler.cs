using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayTeleMarketingContactsRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingContactsRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingContactsListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingContactsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingContactsListHandler
    {
        public DemandayTeleMarketingContactsListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}