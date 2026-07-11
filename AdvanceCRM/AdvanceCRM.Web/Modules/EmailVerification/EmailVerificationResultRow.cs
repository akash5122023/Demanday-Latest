using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace AdvanceCRM.EmailVerification
{
    /// <summary>
    /// Shared cache of verified emails. Every successful verification is stored here so that any
    /// user can see a previously-known Valid / Invalid result for an email without spending a
    /// fresh check. Keyed (uniquely) by <see cref="Email"/>.
    /// </summary>
    [ConnectionKey("Default"), Module("EmailVerification"), TableName("[dbo].[EmailVerificationResult]")]
    [DisplayName("Email Verification Result"), InstanceName("Email Verification Result")]
    public sealed class EmailVerificationResultRow : Row<EmailVerificationResultRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Email"), Size(320), NotNull]
        public String Email
        {
            get => fields.Email[this];
            set => fields.Email[this] = value;
        }

        [DisplayName("Status"), Size(50)]
        public String Status
        {
            get => fields.Status[this];
            set => fields.Status[this] = value;
        }

        [DisplayName("Sub Status"), Size(100)]
        public String SubStatus
        {
            get => fields.SubStatus[this];
            set => fields.SubStatus[this] = value;
        }

        [DisplayName("Message"), Size(500)]
        public String Message
        {
            get => fields.Message[this];
            set => fields.Message[this] = value;
        }

        [DisplayName("Verified By User Id")]
        public Int32? VerifiedByUserId
        {
            get => fields.VerifiedByUserId[this];
            set => fields.VerifiedByUserId[this] = value;
        }

        [DisplayName("Verified Date")]
        public DateTime? VerifiedDate
        {
            get => fields.VerifiedDate[this];
            set => fields.VerifiedDate[this] = value;
        }

        public EmailVerificationResultRow() : base() { }
        public EmailVerificationResultRow(RowFields fields) : base(fields) { }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Email;
            public StringField Status;
            public StringField SubStatus;
            public StringField Message;
            public Int32Field VerifiedByUserId;
            public DateTimeField VerifiedDate;
        }
    }
}
