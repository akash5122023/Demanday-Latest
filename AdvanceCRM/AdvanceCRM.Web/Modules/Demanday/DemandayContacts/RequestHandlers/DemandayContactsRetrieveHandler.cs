using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Demanday.DemandayContactsRow>;
using MyRow = AdvanceCRM.Demanday.DemandayContactsRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayContactsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayContactsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayContactsRetrieveHandler
    {
        public DemandayContactsRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}