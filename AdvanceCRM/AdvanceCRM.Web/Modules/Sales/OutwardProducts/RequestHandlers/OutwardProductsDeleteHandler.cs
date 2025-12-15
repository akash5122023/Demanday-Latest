using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Sales.OutwardProductsRow;

namespace AdvanceCRM.Sales
{
    public interface IOutwardProductsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class OutwardProductsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IOutwardProductsDeleteHandler
    {
        public OutwardProductsDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}