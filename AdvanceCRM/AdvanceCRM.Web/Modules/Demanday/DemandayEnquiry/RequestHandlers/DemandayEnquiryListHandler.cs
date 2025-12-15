using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayEnquiryRow>;
using MyRow = AdvanceCRM.Demanday.DemandayEnquiryRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayEnquiryListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayEnquiryListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayEnquiryListHandler
    {
        public DemandayEnquiryListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}