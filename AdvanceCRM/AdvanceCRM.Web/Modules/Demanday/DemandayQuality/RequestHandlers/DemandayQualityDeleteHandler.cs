using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Demanday.DemandayQualityRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayQualityDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayQualityDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayQualityDeleteHandler
    {
        public DemandayQualityDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}