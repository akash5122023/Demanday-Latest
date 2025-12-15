using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Demanday.DemandayEnquiryRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayEnquiryDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayEnquiryDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayEnquiryDeleteHandler
    {
        public DemandayEnquiryDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}