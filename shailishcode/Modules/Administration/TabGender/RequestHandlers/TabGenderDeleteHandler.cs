using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Administration.TabGenderRow;

namespace AdvanceCRM.Administration
{
    public interface ITabGenderDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class TabGenderDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, ITabGenderDeleteHandler
    {
        public TabGenderDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}