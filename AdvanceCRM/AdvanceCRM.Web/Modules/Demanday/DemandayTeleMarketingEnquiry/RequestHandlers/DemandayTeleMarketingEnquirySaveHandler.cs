using Serenity;
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
        public DemandayTeleMarketingEnquirySaveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void BeforeSave()
        {
            base.BeforeSave();

            // For Create only
            if (IsCreate)
                Row.OwnerId = int.Parse(User.GetIdentifier());
        }
    }
}
