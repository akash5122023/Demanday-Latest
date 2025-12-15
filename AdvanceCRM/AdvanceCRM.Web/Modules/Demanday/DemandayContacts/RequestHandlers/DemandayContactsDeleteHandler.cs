using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Demanday.DemandayContactsRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayContactsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayContactsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayContactsDeleteHandler
    {
        public DemandayContactsDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}