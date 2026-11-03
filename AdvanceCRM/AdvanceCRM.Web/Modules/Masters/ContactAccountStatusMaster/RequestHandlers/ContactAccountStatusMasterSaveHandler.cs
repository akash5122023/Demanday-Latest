using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Masters.ContactAccountStatusMasterRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Masters.ContactAccountStatusMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactAccountStatusMasterSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactAccountStatusMasterSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IContactAccountStatusMasterSaveHandler
    {
        public ContactAccountStatusMasterSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}