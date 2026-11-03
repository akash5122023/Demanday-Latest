using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Masters.ContactAccountStatusMasterRow>;
using MyRow = AdvanceCRM.Masters.ContactAccountStatusMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactAccountStatusMasterListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactAccountStatusMasterListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IContactAccountStatusMasterListHandler
    {
        public ContactAccountStatusMasterListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}