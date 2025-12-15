using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Administration.TabGenderRow>;
using MyRow = AdvanceCRM.Administration.TabGenderRow;

namespace AdvanceCRM.Administration
{
    public interface ITabGenderRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class TabGenderRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, ITabGenderRetrieveHandler
    {
        public TabGenderRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}