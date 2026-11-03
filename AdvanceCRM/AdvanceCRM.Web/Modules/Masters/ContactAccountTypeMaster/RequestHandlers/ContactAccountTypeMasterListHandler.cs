using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Masters.ContactAccountTypeMasterRow>;
using MyRow = AdvanceCRM.Masters.ContactAccountTypeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactAccountTypeMasterListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactAccountTypeMasterListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IContactAccountTypeMasterListHandler
    {
        public ContactAccountTypeMasterListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}