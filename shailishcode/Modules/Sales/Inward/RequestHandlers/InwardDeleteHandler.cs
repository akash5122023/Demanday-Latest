using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Sales.InwardRow;

namespace AdvanceCRM.Sales
{
    public interface IInwardDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class InwardDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IInwardDeleteHandler
    {
        public InwardDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}