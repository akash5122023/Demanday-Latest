using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Masters.ContactOutreachModeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactOutreachModeMasterDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactOutreachModeMasterDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IContactOutreachModeMasterDeleteHandler
    {
        public ContactOutreachModeMasterDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}