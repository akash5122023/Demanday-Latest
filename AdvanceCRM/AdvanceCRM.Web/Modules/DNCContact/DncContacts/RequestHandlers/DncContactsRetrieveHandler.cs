using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.DNCContact.DncContactsRow>;
using MyRow = AdvanceCRM.DNCContact.DncContactsRow;

namespace AdvanceCRM.DNCContact
{
    public interface IDncContactsRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class DncContactsRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IDncContactsRetrieveHandler
    {
        public DncContactsRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}