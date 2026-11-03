using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Masters.ContactOutreachModeMasterRow>;
using MyRow = AdvanceCRM.Masters.ContactOutreachModeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactOutreachModeMasterRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactOutreachModeMasterRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IContactOutreachModeMasterRetrieveHandler
    {
        public ContactOutreachModeMasterRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}