using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryQADetailsRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryQADetailsRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingEnquiryQADetailsListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquiryQADetailsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquiryQADetailsListHandler
    {
        public DemandayTeleMarketingEnquiryQADetailsListHandler(IRequestContext context)
            : base(context)
        {
        }
    }
}
