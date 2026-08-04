using Serenity;
using Serenity.Services;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.VerifySheet.TMEnquiryRow;

namespace AdvanceCRM.VerifySheet
{
    public interface ITMEnquiryDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> { }

    public class TMEnquiryDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, ITMEnquiryDeleteHandler
    {
        public TMEnquiryDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}
