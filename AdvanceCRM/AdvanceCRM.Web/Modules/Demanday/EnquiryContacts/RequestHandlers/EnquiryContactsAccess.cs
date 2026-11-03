using Serenity;
using Serenity.Services;
using System;

namespace AdvanceCRM.Demanday
{
    /// <summary>
    /// Who sees which Enquiry Contacts. A regular employee works only on the records they created;
    /// a team leader (EnquiryContacts:TeamLeader) and an administrator see every employee's records.
    /// </summary>
    public static class EnquiryContactsAccess
    {
        public static bool SeesAll(IRequestContext context)
        {
            return context.Permissions.HasPermission(AdvanceCRM.Administration.PermissionKeys.Security) ||
                   context.Permissions.HasPermission("EnquiryContacts:TeamLeader");
        }

        public static int? CurrentUserId(IRequestContext context)
        {
            var id = context.User?.GetIdentifier();
            return int.TryParse(id, out var userId) ? userId : (int?)null;
        }

        /// <summary>Throws unless the user may work on a record created by <paramref name="ownerId"/>.</summary>
        public static void EnsureCanAccess(IRequestContext context, int? ownerId)
        {
            if (SeesAll(context))
                return;
            if (ownerId == null || ownerId != CurrentUserId(context))
                throw new ValidationError("AccessDenied", "You can only work on your own records.");
        }
    }
}
