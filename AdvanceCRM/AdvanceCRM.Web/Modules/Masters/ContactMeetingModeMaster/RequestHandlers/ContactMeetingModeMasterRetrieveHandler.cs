using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Masters.ContactMeetingModeMasterRow>;
using MyRow = AdvanceCRM.Masters.ContactMeetingModeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactMeetingModeMasterRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactMeetingModeMasterRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IContactMeetingModeMasterRetrieveHandler
    {
        public ContactMeetingModeMasterRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}