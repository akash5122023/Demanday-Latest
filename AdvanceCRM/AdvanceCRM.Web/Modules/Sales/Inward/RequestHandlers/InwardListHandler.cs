using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Sales.InwardRow>;
using MyRow = AdvanceCRM.Sales.InwardRow;

namespace AdvanceCRM.Sales
{
    public interface IInwardListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class InwardListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IInwardListHandler
    {
        public InwardListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}