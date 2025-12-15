using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Sales.OutwardRow>;
using MyRow = AdvanceCRM.Sales.OutwardRow;

namespace AdvanceCRM.Sales
{
    public interface IOutwardListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class OutwardListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IOutwardListHandler
    {
        public OutwardListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}