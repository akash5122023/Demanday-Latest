using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Masters.ContactDispositionMasterRow>;
using MyRow = AdvanceCRM.Masters.ContactDispositionMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactDispositionMasterRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactDispositionMasterRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IContactDispositionMasterRetrieveHandler
    {
        public ContactDispositionMasterRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}