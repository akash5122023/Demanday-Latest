using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayTeleMarketingQualiltyRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingQualiltyRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingQualiltySaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingQualiltySaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingQualiltySaveHandler
    {
        public DemandayTeleMarketingQualiltySaveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void BeforeSave()
        {
            base.BeforeSave();

            // Stamp the creating user so OwnerUsername (jOwner.[Username]) resolves
            // and shows up when exporting. Only set when not already provided
            // (e.g. carried over by a Move action or resolved from import).
            if (IsCreate && Row.OwnerId == null)
            {
                var identifier = User?.GetIdentifier();
                if (!string.IsNullOrEmpty(identifier) && int.TryParse(identifier, out var userId))
                    Row.OwnerId = userId;
            }
        }
    }
}