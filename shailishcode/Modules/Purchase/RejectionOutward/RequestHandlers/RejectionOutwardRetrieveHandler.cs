using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Purchase.RejectionOutwardRow>;
using MyRow = AdvanceCRM.Purchase.RejectionOutwardRow;

namespace AdvanceCRM.Purchase.RejectionOutward
{
    public interface IRejectionOutwardRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class RejectionOutwardRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IRejectionOutwardRetrieveHandler
    {
        public RejectionOutwardRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}