using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Toolkit.DemandayCompetitorRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Toolkit.DemandayCompetitorRow;

namespace AdvanceCRM.Toolkit
{
    public interface IDemandayCompetitorSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayCompetitorSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayCompetitorSaveHandler
    {
        public DemandayCompetitorSaveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void SetInternalFields()
        {
            base.SetInternalFields();

            // Dialog adds without a SrNo get the next free serial number.
            if (IsCreate && Row.SrNo == null)
                Row.SrNo = ToolkitSrNoHelper.NextSrNo(Connection, "[dbo].[DemandayCompetitor]");
        }
    }
}