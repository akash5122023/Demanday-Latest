using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Masters.ContactDispositionMasterRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Masters.ContactDispositionMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactDispositionMasterSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactDispositionMasterSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IContactDispositionMasterSaveHandler
    {
        public ContactDispositionMasterSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}