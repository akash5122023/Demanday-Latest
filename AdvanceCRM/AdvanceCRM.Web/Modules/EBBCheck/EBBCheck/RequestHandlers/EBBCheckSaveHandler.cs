using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.EBBCheck.EBBCheckRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.EBBCheck.EBBCheckRow;

namespace AdvanceCRM.EBBCheck
{
    public interface IEBBCheckSaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class EBBCheckSaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IEBBCheckSaveHandler
    {
        // Only this permission (given to the Quality team) may set/alter the Status field.
        public const string ChangeStatusPermission = "EBBCheck:ChangeStatus";

        public EBBCheckSaveHandler(IRequestContext context)
             : base(context)
        {
        }

        protected override void BeforeSave()
        {
            base.BeforeSave();

            var canChangeStatus = Context.Permissions.HasPermission(ChangeStatusPermission);

            if (IsCreate)
            {
                // Stamp the creating (Enquiry) user + time automatically.
                var identifier = User?.GetIdentifier();
                if (!string.IsNullOrEmpty(identifier) && int.TryParse(identifier, out var userId))
                {
                    Row.UserId = userId;
                    Row.OwnerId = userId;   // "Created By"
                }

                Row.TimeStamp = DateTime.Now;
                if (Row.Date == null)
                    Row.Date = DateTime.Now;

                // New rows always start as "Waiting"; only Quality controls status afterwards.
                Row.Status = EbbStatus.Waiting;
            }
            else
            {
                if (Row.IsAssigned(EBBCheckRow.Fields.Status) && !canChangeStatus)
                {
                    // A non-Quality user tried to change the Status – block it.
                    if (Old != null && Row.Status != Old.Status)
                        throw new ValidationError("PermissionDenied", null,
                            "Only the Quality team is allowed to change the Status.");
                }

                // First Name / Email / Date are insert-only – keep whatever was saved
                // when the record was created, no matter what the request contains.
                if (Old != null)
                {
                    Row.FirstName = Old.FirstName;
                    Row.Email = Old.Email;
                    Row.Date = Old.Date;
                }
            }
        }
    }
}
