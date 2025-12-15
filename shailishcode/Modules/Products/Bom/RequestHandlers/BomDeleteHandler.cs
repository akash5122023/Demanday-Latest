using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Products.BomRow;

namespace AdvanceCRM.Products
{
    public interface IBomDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class BomDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IBomDeleteHandler
    {
        public BomDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}