using Serenity;
using Serenity.Services;
using System;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayEnquiryRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayEnquiryRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayEnquirySaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayEnquirySaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayEnquirySaveHandler
    {
        public DemandayEnquirySaveHandler(IRequestContext context)
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
