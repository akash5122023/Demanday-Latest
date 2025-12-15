using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Sales.IndentProductsRow;

namespace AdvanceCRM.Sales
{
    public interface IIndentProductsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class IndentProductsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IIndentProductsDeleteHandler
    {
        public IndentProductsDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}