using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Sales.IndentRow>;
using MyRow = AdvanceCRM.Sales.IndentRow;

namespace AdvanceCRM.Sales
{
    public interface IIndentListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class IndentListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IIndentListHandler
    {
        public IndentListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}