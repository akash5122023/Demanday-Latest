using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Demanday.DemandayMisRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayMisDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayMisDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayMisDeleteHandler
    {
        public DemandayMisDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}