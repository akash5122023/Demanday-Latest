using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.ListRequest;
using MyResponse = Serenity.Services.ListResponse<AdvanceCRM.DNCContact.DncContactsRow>;
using MyRow = AdvanceCRM.DNCContact.DncContactsRow;

namespace AdvanceCRM.DNCContact
{
    public interface IDncContactsListHandler : IListHandler<MyRow, MyRequest, MyResponse> {}

    public class DncContactsListHandler : ListRequestHandler<MyRow, MyRequest, MyResponse>, IDncContactsListHandler
    {
        public DncContactsListHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}