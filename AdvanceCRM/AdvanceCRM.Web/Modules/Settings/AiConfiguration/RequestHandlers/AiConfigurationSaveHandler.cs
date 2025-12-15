using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Settings.AiConfigurationRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Settings.AiConfigurationRow;

namespace AdvanceCRM.Settings
{
    public interface IAiConfigurationSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class AiConfigurationSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IAiConfigurationSaveHandler
    {
        public AiConfigurationSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}