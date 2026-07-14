using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;

namespace AdvanceCRM.EmailVerification
{
    /// <summary>
    /// Runtime configuration for the Email Verification tool. A single row holds the ZeroBounce
    /// API key and the default per-user quota, editable from the admin "API Setup" form so the
    /// key can be added / changed / removed without redeploying or editing appsettings.json.
    /// </summary>
    [ConnectionKey("Default"), Module("EmailVerification"), TableName("[dbo].[EmailVerificationSettings]")]
    [DisplayName("Email Verification Settings"), InstanceName("Email Verification Settings")]
    public sealed class EmailVerificationSettingsRow : Row<EmailVerificationSettingsRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Api Key"), Size(200)]
        public String ApiKey
        {
            get => fields.ApiKey[this];
            set => fields.ApiKey[this] = value;
        }

        [DisplayName("Default Quota")]
        public Int32? DefaultQuota
        {
            get => fields.DefaultQuota[this];
            set => fields.DefaultQuota[this] = value;
        }

        [DisplayName("Updated By User Id")]
        public Int32? UpdatedByUserId
        {
            get => fields.UpdatedByUserId[this];
            set => fields.UpdatedByUserId[this] = value;
        }

        [DisplayName("Updated Date")]
        public DateTime? UpdatedDate
        {
            get => fields.UpdatedDate[this];
            set => fields.UpdatedDate[this] = value;
        }

        public EmailVerificationSettingsRow() : base() { }
        public EmailVerificationSettingsRow(RowFields fields) : base(fields) { }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField ApiKey;
            public Int32Field DefaultQuota;
            public Int32Field UpdatedByUserId;
            public DateTimeField UpdatedDate;
        }
    }
}
