using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Settings.AiConfigurationRow;

namespace AdvanceCRM.Settings
{
    public interface IAiConfigurationDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class AiConfigurationDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IAiConfigurationDeleteHandler
    {
        public AiConfigurationDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}