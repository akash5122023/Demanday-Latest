using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingContactsRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingContactsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingContactsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingContactsDeleteHandler
    {
        public DemandayTeleMarketingContactsDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}