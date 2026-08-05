using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingEnquirySaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquirySaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquirySaveHandler
    {
        private readonly ITMEnquirySyncHandler tmEnquirySyncHandler;

        public DemandayTeleMarketingEnquirySaveHandler(IRequestContext context, ITMEnquirySyncHandler tmEnquirySyncHandler)
             : base(context)
        {
            this.tmEnquirySyncHandler = tmEnquirySyncHandler;
        }

        protected override void BeforeSave()
        {
            base.BeforeSave();

            // For Create only
            if (IsCreate)
                Row.OwnerId = int.Parse(User.GetIdentifier());
        }

        protected override void AfterSave()
        {
            base.AfterSave();

            // Auto-sync to TMEnquiry table
            try
            {
                tmEnquirySyncHandler.SyncToTMEnquiry(UnitOfWork, Row);
            }
            catch (Exception ex)
            {
                // Log but don't throw - sync failure shouldn't fail the main operation
                System.Diagnostics.Debug.WriteLine($"TMEnquiry sync error: {ex.Message}");
            }
        }
    }
}
