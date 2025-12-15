using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Settings.AiConfigurationRow>;
using MyRow = AdvanceCRM.Settings.AiConfigurationRow;

namespace AdvanceCRM.Settings
{
    public interface IAiConfigurationListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class AiConfigurationListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IAiConfigurationListHandler
    {
        public AiConfigurationListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}