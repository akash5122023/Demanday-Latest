using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Purchase.GrnProductsTwoRow>;
using MyRow = AdvanceCRM.Purchase.GrnProductsTwoRow;

namespace AdvanceCRM.Purchase.GrnProductsTwo
{
    public interface IGrnProductsTwoRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class GrnProductsTwoRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IGrnProductsTwoRetrieveHandler
    {
        public GrnProductsTwoRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}