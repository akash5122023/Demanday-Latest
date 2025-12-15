using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Purchase.GrnProductsTwoRow;

namespace AdvanceCRM.Purchase.GrnProductsTwo
{
    public interface IGrnProductsTwoDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class GrnProductsTwoDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IGrnProductsTwoDeleteHandler
    {
        public GrnProductsTwoDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}