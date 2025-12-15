using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Demanday.DemandayContactsRow>;
using MyRow = AdvanceCRM.Demanday.DemandayContactsRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayContactsListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayContactsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayContactsListHandler
    {
        public DemandayContactsListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}