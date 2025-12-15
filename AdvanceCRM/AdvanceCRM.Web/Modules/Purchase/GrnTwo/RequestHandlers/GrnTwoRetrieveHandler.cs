using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Purchase.GrnTwoRow>;
using MyRow = AdvanceCRM.Purchase.GrnTwoRow;

namespace AdvanceCRM.Purchase.GrnTwo
{
    public interface IGrnTwoRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class GrnTwoRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IGrnTwoRetrieveHandler
    {
        public GrnTwoRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}