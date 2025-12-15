using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Sales.OutwardProductsRow>;
using MyRow = AdvanceCRM.Sales.OutwardProductsRow;

namespace AdvanceCRM.Sales
{
    public interface IOutwardProductsListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class OutwardProductsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IOutwardProductsListHandler
    {
        public OutwardProductsListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}