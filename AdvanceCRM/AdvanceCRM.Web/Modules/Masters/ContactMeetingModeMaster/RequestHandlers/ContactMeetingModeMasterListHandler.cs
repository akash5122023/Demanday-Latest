using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Masters.ContactMeetingModeMasterRow>;
using MyRow = AdvanceCRM.Masters.ContactMeetingModeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactMeetingModeMasterListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactMeetingModeMasterListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IContactMeetingModeMasterListHandler
    {
        public ContactMeetingModeMasterListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}