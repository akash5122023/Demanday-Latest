using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Toolkit.MasterSupressionRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Toolkit.MasterSupressionRow;

namespace AdvanceCRM.Toolkit
{
    public interface IMasterSupressionSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class MasterSupressionSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IMasterSupressionSaveHandler
    {
        public MasterSupressionSaveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void SetInternalFields()
        {
            base.SetInternalFields();

            // Dialog adds without a SrNo get the next free serial number.
            if (IsCreate && Row.SrNo == null)
                Row.SrNo = ToolkitSrNoHelper.NextSrNo(Connection, "[dbo].[MasterSupression]");
        }
    }
}