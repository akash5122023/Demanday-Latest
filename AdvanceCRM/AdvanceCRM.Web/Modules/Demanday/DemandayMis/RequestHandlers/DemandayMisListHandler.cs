using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayMisRow>;
using MyRow = AdvanceCRM.Demanday.DemandayMisRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayMisListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayMisListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayMisListHandler
    {
        public DemandayMisListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}