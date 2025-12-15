using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Purchase.RejectionOutwardRow>;
using MyRow = AdvanceCRM.Purchase.RejectionOutwardRow;

namespace AdvanceCRM.Purchase.RejectionOutward
{
    public interface IRejectionOutwardListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class RejectionOutwardListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IRejectionOutwardListHandler
    {
        public RejectionOutwardListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}