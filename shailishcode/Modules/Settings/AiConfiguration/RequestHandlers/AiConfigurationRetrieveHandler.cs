using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Settings.AiConfigurationRow>;
using MyRow = AdvanceCRM.Settings.AiConfigurationRow;

namespace AdvanceCRM.Settings
{
    public interface IAiConfigurationRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class AiConfigurationRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IAiConfigurationRetrieveHandler
    {
        public AiConfigurationRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}