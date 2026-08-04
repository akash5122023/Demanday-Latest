using Serenity;
using Serenity.Services;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.VerifySheet.TMEnquiryRow>;
using MyRow = AdvanceCRM.VerifySheet.TMEnquiryRow;

namespace AdvanceCRM.VerifySheet
{
    public interface ITMEnquiryRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> { }

    public class TMEnquiryRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, ITMEnquiryRetrieveHandler
    {
        public TMEnquiryRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}
