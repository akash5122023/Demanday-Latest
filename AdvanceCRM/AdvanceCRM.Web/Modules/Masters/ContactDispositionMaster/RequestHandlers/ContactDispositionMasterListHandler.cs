using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.Masters.ContactDispositionMasterRow>;
using MyRow = AdvanceCRM.Masters.ContactDispositionMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactDispositionMasterListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactDispositionMasterListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IContactDispositionMasterListHandler
    {
        public ContactDispositionMasterListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}