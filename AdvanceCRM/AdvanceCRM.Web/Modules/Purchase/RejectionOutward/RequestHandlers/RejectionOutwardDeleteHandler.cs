using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Purchase.RejectionOutwardRow;

namespace AdvanceCRM.Purchase.RejectionOutward
{
    public interface IRejectionOutwardDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class RejectionOutwardDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IRejectionOutwardDeleteHandler
    {
        public RejectionOutwardDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}