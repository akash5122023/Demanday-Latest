using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Toolkit.ClientSupressionRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Toolkit.ClientSupressionRow;

namespace AdvanceCRM.Toolkit
{
    public interface IClientSupressionSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class ClientSupressionSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IClientSupressionSaveHandler
    {
        public ClientSupressionSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}