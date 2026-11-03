using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Masters.ContactOutreachModeMasterRow>;
using MyRow = AdvanceCRM.Masters.ContactOutreachModeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactOutreachModeMasterListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactOutreachModeMasterListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IContactOutreachModeMasterListHandler
    {
        public ContactOutreachModeMasterListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}