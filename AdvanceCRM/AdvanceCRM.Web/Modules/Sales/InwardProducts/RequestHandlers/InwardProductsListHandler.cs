using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Sales.InwardProductsRow>;
using MyRow = AdvanceCRM.Sales.InwardProductsRow;

namespace AdvanceCRM.Sales
{
    public interface IInwardProductsListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class InwardProductsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IInwardProductsListHandler
    {
        public InwardProductsListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}