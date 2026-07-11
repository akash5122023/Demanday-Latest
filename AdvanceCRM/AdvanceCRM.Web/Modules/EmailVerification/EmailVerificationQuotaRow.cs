using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace AdvanceCRM.EmailVerification
{
    /// <summary>
    /// Per-user search limit for the Email Verification tool. <see cref="AllowedCount"/> is the
    /// quota an admin grants; <see cref="UsedCount"/> is how many verifications the user has spent.
    /// Cached (already-known) emails do not consume the quota. One row per user.
    /// </summary>
    [ConnectionKey("Default"), Module("EmailVerification"), TableName("[dbo].[EmailVerificationQuota]")]
    [DisplayName("Email Verification Quota"), InstanceName("Email Verification Quota")]
    public sealed class EmailVerificationQuotaRow : Row<EmailVerificationQuotaRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("User Id"), NotNull]
        public Int32? UserId
        {
            get => fields.UserId[this];
            set => fields.UserId[this] = value;
        }

        [DisplayName("Allowed Count"), NotNull]
        public Int32? AllowedCount
        {
            get => fields.AllowedCount[this];
            set => fields.AllowedCount[this] = value;
        }

        [DisplayName("Used Count"), NotNull]
        public Int32? UsedCount
        {
            get => fields.UsedCount[this];
            set => fields.UsedCount[this] = value;
        }

        public EmailVerificationQuotaRow() : base() { }
        public EmailVerificationQuotaRow(RowFields fields) : base(fields) { }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field UserId;
            public Int32Field AllowedCount;
            public Int32Field UsedCount;
        }
    }
}
