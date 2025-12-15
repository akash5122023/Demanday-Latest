using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayQualityRow>;
using MyRow = AdvanceCRM.Demanday.DemandayQualityRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayQualityListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayQualityListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayQualityListHandler
    {
        public DemandayQualityListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}