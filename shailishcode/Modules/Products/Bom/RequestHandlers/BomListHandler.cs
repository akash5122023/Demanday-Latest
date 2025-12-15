using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Products.BomRow>;
using MyRow = AdvanceCRM.Products.BomRow;

namespace AdvanceCRM.Products
{
    public interface IBomListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class BomListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IBomListHandler
    {
        public BomListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}