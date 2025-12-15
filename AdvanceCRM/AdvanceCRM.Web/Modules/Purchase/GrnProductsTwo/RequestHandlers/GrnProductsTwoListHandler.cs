using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Purchase.GrnProductsTwoRow>;
using MyRow = AdvanceCRM.Purchase.GrnProductsTwoRow;

namespace AdvanceCRM.Purchase.GrnProductsTwo
{
    public interface IGrnProductsTwoListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class GrnProductsTwoListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IGrnProductsTwoListHandler
    {
        public GrnProductsTwoListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}