using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Administration.TabGenderRow>;
using MyRow = AdvanceCRM.Administration.TabGenderRow;

namespace AdvanceCRM.Administration
{
    public interface ITabGenderListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class TabGenderListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, ITabGenderListHandler
    {
        public TabGenderListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}