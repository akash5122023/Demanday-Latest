using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Sales.InwardProductsRow;

namespace AdvanceCRM.Sales
{
    public interface IInwardProductsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class InwardProductsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IInwardProductsDeleteHandler
    {
        public InwardProductsDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}