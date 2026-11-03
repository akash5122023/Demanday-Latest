using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Masters.ContactMeetingModeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactMeetingModeMasterDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactMeetingModeMasterDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IContactMeetingModeMasterDeleteHandler
    {
        public ContactMeetingModeMasterDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}