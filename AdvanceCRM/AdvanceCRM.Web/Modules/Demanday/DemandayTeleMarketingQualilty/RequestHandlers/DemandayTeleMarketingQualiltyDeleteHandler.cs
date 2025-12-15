using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingQualiltyRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingQualiltyDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingQualiltyDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingQualiltyDeleteHandler
    {
        public DemandayTeleMarketingQualiltyDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}