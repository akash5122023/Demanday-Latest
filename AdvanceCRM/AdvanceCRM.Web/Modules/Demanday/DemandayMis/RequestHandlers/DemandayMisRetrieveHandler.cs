using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Demanday.DemandayMisRow>;
using MyRow = AdvanceCRM.Demanday.DemandayMisRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayMisRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayMisRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayMisRetrieveHandler
    {
        public DemandayMisRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}