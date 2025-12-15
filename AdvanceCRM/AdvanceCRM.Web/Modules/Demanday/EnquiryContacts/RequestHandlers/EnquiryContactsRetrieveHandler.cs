using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Demanday.EnquiryContactsRow>;
using MyRow = AdvanceCRM.Demanday.EnquiryContactsRow;

namespace AdvanceCRM.Demanday
{
    public interface IEnquiryContactsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class EnquiryContactsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IEnquiryContactsRetrieveHandler
    {
        public EnquiryContactsRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}