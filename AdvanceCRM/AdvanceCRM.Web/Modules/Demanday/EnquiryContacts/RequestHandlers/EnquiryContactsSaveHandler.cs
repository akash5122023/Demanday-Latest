using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.EnquiryContactsRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.EnquiryContactsRow;

namespace AdvanceCRM.Demanday
{
    public interface IEnquiryContactsSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class EnquiryContactsSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IEnquiryContactsSaveHandler
    {
        public EnquiryContactsSaveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void ValidateRequest()
        {
            base.ValidateRequest();

            // An employee can only change their own records (Old is the stored row on update).
            if (IsUpdate)
                EnquiryContactsAccess.EnsureCanAccess(Context, Old?.OwnerId);
        }

        protected override void BeforeSave()
        {
            base.BeforeSave();

            // Stamp the creator so the record belongs to them. Imports may carry a Created By already;
            // only an employee is kept from creating records on someone else's behalf.
            if (IsCreate && (Row.OwnerId == null || !EnquiryContactsAccess.SeesAll(Context)))
                Row.OwnerId = EnquiryContactsAccess.CurrentUserId(Context) ?? Row.OwnerId;

            // Ownership never moves on an edit made by an employee.
            if (IsUpdate && !EnquiryContactsAccess.SeesAll(Context))
                Row.OwnerId = Old.OwnerId;
        }
    }
}
