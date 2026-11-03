using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Masters.ContactAccountStatusMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactAccountStatusMasterDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactAccountStatusMasterDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IContactAccountStatusMasterDeleteHandler
    {
        public ContactAccountStatusMasterDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}