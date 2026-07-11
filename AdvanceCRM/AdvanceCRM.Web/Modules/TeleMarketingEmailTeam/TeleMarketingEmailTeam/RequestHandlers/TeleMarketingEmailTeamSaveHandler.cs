using Serenity;
using Serenity.Data;
using Serenity.Services;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.TeleMarketingEmailTeam.TeleMarketingEmailTeamRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.TeleMarketingEmailTeam.TeleMarketingEmailTeamRow;

namespace AdvanceCRM.TeleMarketingEmailTeam
{
    public interface ITeleMarketingEmailTeamSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class TeleMarketingEmailTeamSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, ITeleMarketingEmailTeamSaveHandler
    {
        public TeleMarketingEmailTeamSaveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void BeforeSave()
        {
            base.BeforeSave();

            if (IsCreate && Row.Status == null)
                Row.Status = TeleMarketingEmailTeamStatus.Pending;

            // Stamp "Created By" only when not already supplied (Move-to-Quality carries
            // the TM Team Leader's owner across).
            if (IsCreate && Row.OwnerId == null)
            {
                var identifier = User?.GetIdentifier();
                if (!string.IsNullOrEmpty(identifier) && int.TryParse(identifier, out var userId))
                    Row.OwnerId = userId;
            }
        }

        protected override void AfterSave()
        {
            base.AfterSave();

            // DemandayTeleMarketingQualiltyId is [ReadOnly] so an update request never carries it –
            // fall back to the stored row for updates.
            var qualityId = Row.DemandayTeleMarketingQualiltyId ?? Old?.DemandayTeleMarketingQualiltyId;
            if (qualityId == null)
                return;

            // Only touch TM Quality when the client actually submitted a Status.
            if (!Row.IsAssigned(MyRow.Fields.Status))
                return;

            // Nothing to mirror if the value did not move.
            if (!IsCreate && Old != null && Old.Status == Row.Status)
                return;

            var fld = Demanday.DemandayTeleMarketingQualiltyRow.Fields;
            new SqlUpdate(fld.TableName)
                .Set(fld.EmailTeamStatus, (int?)Row.Status)
                .WhereEqual(fld.Id, qualityId.Value)
                .Execute(Connection);
        }
    }
}
