using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Masters.ContactAccountTypeMasterRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Masters.ContactAccountTypeMasterRow;

namespace AdvanceCRM.Masters
{
    public interface IContactAccountTypeMasterSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class ContactAccountTypeMasterSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IContactAccountTypeMasterSaveHandler
    {
        public ContactAccountTypeMasterSaveHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}