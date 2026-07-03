using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.DNCContact.DncContactsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.DNCContact.DncContactsRow;

namespace AdvanceCRM.DNCContact
{
    public interface IDncContactsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DncContactsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDncContactsSaveHandler
    {
        public DncContactsSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}