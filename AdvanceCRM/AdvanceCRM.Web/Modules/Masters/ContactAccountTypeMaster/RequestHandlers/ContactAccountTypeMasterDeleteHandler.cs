using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Masters.ContactAccountTypeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactAccountTypeMasterDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactAccountTypeMasterDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IContactAccountTypeMasterDeleteHandler
    {
        public ContactAccountTypeMasterDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}