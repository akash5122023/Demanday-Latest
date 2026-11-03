using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Masters.ContactOutreachModeMasterRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Masters.ContactOutreachModeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactOutreachModeMasterSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactOutreachModeMasterSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IContactOutreachModeMasterSaveHandler
    {
        public ContactOutreachModeMasterSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}