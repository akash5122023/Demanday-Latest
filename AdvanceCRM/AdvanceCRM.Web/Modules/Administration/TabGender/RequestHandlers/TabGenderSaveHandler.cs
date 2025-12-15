using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Administration.TabGenderRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Administration.TabGenderRow;

namespace AdvanceCRM.Administration
{
    public interface ITabGenderSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class TabGenderSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, ITabGenderSaveHandler
    {
        public TabGenderSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}