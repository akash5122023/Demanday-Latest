using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.RetrieveRequest;
using MyResponse = Serenity.Services.RetrieveResponse<AdvanceCRM.Masters.ContactAccountTypeMasterRow>;
using MyRow = AdvanceCRM.Masters.ContactAccountTypeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactAccountTypeMasterRetrieveHandler : IRetrieveHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactAccountTypeMasterRetrieveHandler : RetrieveRequestHandler<MyRow, MyRequest, MyResponse>, IContactAccountTypeMasterRetrieveHandler
    {
        public ContactAccountTypeMasterRetrieveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}