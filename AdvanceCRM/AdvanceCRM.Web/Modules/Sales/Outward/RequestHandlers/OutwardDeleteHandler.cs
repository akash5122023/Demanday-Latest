using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Sales.OutwardRow;

namespace AdvanceCRM.Sales
{
    public interface IOutwardDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class OutwardDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IOutwardDeleteHandler
    {
        public OutwardDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}