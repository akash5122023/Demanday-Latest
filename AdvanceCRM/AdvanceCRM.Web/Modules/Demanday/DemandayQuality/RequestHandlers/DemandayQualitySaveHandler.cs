using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayQualityRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayQualityRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayQualitySaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayQualitySaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayQualitySaveHandler
    {
        public DemandayQualitySaveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void BeforeSave()
        {
            base.BeforeSave();

            // Stamp the creating user so OwnerUsername (jOwner.[Username]) resolves
            // and shows up when exporting Quality data. Only set when not already
            // provided (e.g. carried over by Move-to-Quality or supplied in import).
            if (IsCreate && Row.OwnerId == null)
            {
                var identifier = User?.GetIdentifier();
                if (!string.IsNullOrEmpty(identifier) && int.TryParse(identifier, out var userId))
                    Row.OwnerId = userId;
            }
        }
    }
}