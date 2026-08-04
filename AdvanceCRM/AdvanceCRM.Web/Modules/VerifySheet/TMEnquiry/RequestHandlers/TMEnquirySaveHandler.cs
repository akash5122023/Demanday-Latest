using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.VerifySheet.TMEnquiryRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.VerifySheet.TMEnquiryRow;

namespace AdvanceCRM.VerifySheet
{
    public interface ITMEnquirySaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> { }

    public class TMEnquirySaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, ITMEnquirySaveHandler
    {
        public TMEnquirySaveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void BeforeSave()
        {
            base.BeforeSave();

            if (IsCreate)
            {
                Row.CreatedOn = DateTime.Now;
                Row.CreatedBy = User?.GetIdentifier();
            }
            else
            {
                Row.UpdatedOn = DateTime.Now;
                Row.UpdatedBy = User?.GetIdentifier();
            }
        }
    }
}
