using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingEnquiryDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquiryDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquiryDeleteHandler
    {
        public DemandayTeleMarketingEnquiryDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}