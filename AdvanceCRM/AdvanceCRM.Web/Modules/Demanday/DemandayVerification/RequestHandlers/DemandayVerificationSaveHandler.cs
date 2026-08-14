using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayVerificationRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayVerificationRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayVerificationSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayVerificationSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayVerificationSaveHandler
    {
        public DemandayVerificationSaveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void BeforeSave()
        {
            base.BeforeSave();

            // "Created By" is what keeps a record visible to the person who put it there:
            // DemandayVerificationListHandler shows a user the rows assigned to them by Agent
            // Name plus the rows they own. A sheet carrying no owner column left this null, so
            // the Team Leader who imported it lost sight of every row they had just brought in -
            // each one was visible only to the agent named on it. Stamped only when the request
            // does not already carry an owner, so an import that does name one still wins.
            if (IsCreate && Row.OwnerId == null)
            {
                var identifier = User?.GetIdentifier();
                if (!string.IsNullOrEmpty(identifier) && int.TryParse(identifier, out var userId))
                    Row.OwnerId = userId;
            }
        }
    }
}