using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Sales.IndentRow>;
using MyRow = AdvanceCRM.Sales.IndentRow;

namespace AdvanceCRM.Sales
{
    public interface IIndentRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class IndentRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IIndentRetrieveHandler
    {
        public IndentRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}