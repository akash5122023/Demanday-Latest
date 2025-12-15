using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Demanday.DemandayEnquiryRow>;
using MyRow = AdvanceCRM.Demanday.DemandayEnquiryRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayEnquiryRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayEnquiryRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayEnquiryRetrieveHandler
    {
        public DemandayEnquiryRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}