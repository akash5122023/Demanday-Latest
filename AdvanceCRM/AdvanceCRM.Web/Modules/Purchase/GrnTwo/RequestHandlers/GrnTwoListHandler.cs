using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Purchase.GrnTwoRow>;
using MyRow = AdvanceCRM.Purchase.GrnTwoRow;

namespace AdvanceCRM.Purchase.GrnTwo
{
    public interface IGrnTwoListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class GrnTwoListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IGrnTwoListHandler
    {
        public GrnTwoListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}