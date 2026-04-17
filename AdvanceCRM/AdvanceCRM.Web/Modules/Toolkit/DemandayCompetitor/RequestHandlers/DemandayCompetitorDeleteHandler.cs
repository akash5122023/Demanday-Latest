using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.DeleteRequest;
using MyResponse = Serenity.Services.DeleteResponse;
using MyRow = AdvanceCRM.Toolkit.DemandayCompetitorRow;

namespace AdvanceCRM.Toolkit
{
    public interface IDemandayCompetitorDeleteHandler : IDeleteHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayCompetitorDeleteHandler : DeleteRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayCompetitorDeleteHandler
    {
        public DemandayCompetitorDeleteHandler(IRequestContext context)
             : base(context)
        {
        }
    }
}