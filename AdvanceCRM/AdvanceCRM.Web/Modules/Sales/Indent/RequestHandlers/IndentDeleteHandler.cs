using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Sales.IndentRow;

namespace AdvanceCRM.Sales
{
    public interface IIndentDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class IndentDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IIndentDeleteHandler
    {
        public IndentDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}