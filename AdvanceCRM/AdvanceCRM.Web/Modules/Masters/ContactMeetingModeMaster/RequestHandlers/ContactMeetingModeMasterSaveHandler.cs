using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Masters.ContactMeetingModeMasterRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Masters.ContactMeetingModeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactMeetingModeMasterSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactMeetingModeMasterSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IContactMeetingModeMasterSaveHandler
    {
        public ContactMeetingModeMasterSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}