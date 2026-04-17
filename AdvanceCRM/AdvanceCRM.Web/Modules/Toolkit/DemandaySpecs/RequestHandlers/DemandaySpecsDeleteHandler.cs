using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Toolkit.DemandaySpecsRow;

namespace AdvanceCRM.Toolkit
{
    public interface IDemandaySpecsDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandaySpecsDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandaySpecsDeleteHandler
    {
        public DemandaySpecsDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}