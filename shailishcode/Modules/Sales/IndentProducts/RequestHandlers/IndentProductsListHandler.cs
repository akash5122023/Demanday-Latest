using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Sales.IndentProductsRow>;
using MyRow = AdvanceCRM.Sales.IndentProductsRow;

namespace AdvanceCRM.Sales
{
    public interface IIndentProductsListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class IndentProductsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IIndentProductsListHandler
    {
        public IndentProductsListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}