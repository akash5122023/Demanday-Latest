using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayVerificationRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayVerificationRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayVerificationSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayVerificationSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayVerificationSaveHandler
    {
        public DemandayVerificationSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}