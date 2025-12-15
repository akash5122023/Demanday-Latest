using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Purchase.GrnTwoRow;

namespace AdvanceCRM.Purchase.GrnTwo
{
    public interface IGrnTwoDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class GrnTwoDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IGrnTwoDeleteHandler
    {
        public GrnTwoDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}