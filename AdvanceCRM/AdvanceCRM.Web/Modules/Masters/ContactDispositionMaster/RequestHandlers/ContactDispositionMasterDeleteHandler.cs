using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Masters.ContactDispositionMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactDispositionMasterDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactDispositionMasterDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IContactDispositionMasterDeleteHandler
    {
        public ContactDispositionMasterDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}