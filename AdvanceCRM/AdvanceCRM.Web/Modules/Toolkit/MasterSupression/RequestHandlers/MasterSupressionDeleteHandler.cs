using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Toolkit.MasterSupressionRow;

namespace AdvanceCRM.Toolkit
{
    public interface IMasterSupressionDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class MasterSupressionDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IMasterSupressionDeleteHandler
    {
        public MasterSupressionDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}