using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Masters.ContactAccountStatusMasterRow>;
using MyRow = AdvanceCRM.Masters.ContactAccountStatusMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactAccountStatusMasterRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactAccountStatusMasterRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IContactAccountStatusMasterRetrieveHandler
    {
        public ContactAccountStatusMasterRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}