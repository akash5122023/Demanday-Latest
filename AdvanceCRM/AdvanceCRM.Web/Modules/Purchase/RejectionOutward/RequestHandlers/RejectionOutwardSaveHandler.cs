using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Purchase.RejectionOutwardRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Purchase.RejectionOutwardRow;

namespace AdvanceCRM.Purchase.RejectionOutward
{
    public interface IRejectionOutwardSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class RejectionOutwardSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IRejectionOutwardSaveHandler
    {
        public RejectionOutwardSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}