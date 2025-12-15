using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryRow>;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingEnquiryListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquiryListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquiryListHandler
    {
        public DemandayTeleMarketingEnquiryListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}