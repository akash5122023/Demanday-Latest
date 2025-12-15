using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Demanday.DemandayVerificationRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayVerificationDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayVerificationDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayVerificationDeleteHandler
    {
        public DemandayVerificationDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}