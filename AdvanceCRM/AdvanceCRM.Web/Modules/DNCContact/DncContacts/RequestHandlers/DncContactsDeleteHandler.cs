using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.DNCContact.DncContactsRow;

namespace AdvanceCRM.DNCContact
{
    public interface IDncContactsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DncContactsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDncContactsDeleteHandler
    {
        public DncContactsDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}