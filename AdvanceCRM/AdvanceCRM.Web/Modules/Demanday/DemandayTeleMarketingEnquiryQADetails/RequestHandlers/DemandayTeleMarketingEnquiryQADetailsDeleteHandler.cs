using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryQADetailsRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingEnquiryQADetailsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquiryQADetailsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquiryQADetailsDeleteHandler
    {
        public DemandayTeleMarketingEnquiryQADetailsDeleteHandler(IRequestContext context)
            : base(context)
        {
        }
    }
}
