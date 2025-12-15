
namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[Facebook]")]
    [DisplayName("Facebook Configuration"), InstanceName("Facebook Configuration")]
    [ReadPermission("Settings:Facebook")]
    [ModifyPermission("Settings:Facebook")]
    public sealed class FacebookConfigurationRow : Row<FacebookConfigurationRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }
        [DisplayName("App Id"), Column("AppID"), Size(100)]
        public String AppId
        {
            get { return Fields.AppId[this]; }
            set { Fields.AppId[this] = value; }
        }

        [DisplayName("Access Token Key"), Column("AccessToken Key"), Size(1000)]
        public String AccessTokenKey
        {
            get { return Fields.AccessTokenKey[this]; }
            set { Fields.AccessTokenKey[this] = value; }
        }
        [DisplayName("Token Expiry Date")]
        public DateTime? TokenExpiryDate
        {
            get { return Fields.TokenExpiryDate[this]; }
            set { Fields.TokenExpiryDate[this] = value; }
        }


        [DisplayName("Sender"), Size(250)]
        public String Sender
        {
            get { return Fields.Sender[this]; }
            set { Fields.Sender[this] = value; }
        }

        [DisplayName("Subject"), Size(250), NameProperty]
        public String Subject
        {
            get { return Fields.Subject[this]; }
            set { Fields.Subject[this] = value; }
        }

        [DisplayName("Email Template"), Size(2000), TextAreaEditor(Rows = 8)]
        public String EmailTemplate
        {
            get { return Fields.EmailTemplate[this]; }
            set { Fields.EmailTemplate[this] = value; }
        }

        [DisplayName("Attachments"), Size(1000), LargeFileUploadEditor(CopyToHistory = true, FilenameFormat = "Attachments/~")]
        public String Attachment
        {
            get { return Fields.Attachment[this]; }
            set { Fields.Attachment[this] = value; }
        }

        [DisplayName("SMS Template"), Column("SMSTemplate"), Size(1000), TextAreaEditor(Rows = 4)]
        public String SMSTemplate
        {
            get { return Fields.SMSTemplate[this]; }
            set { Fields.SMSTemplate[this] = value; }
        }

        [DisplayName("Host"), Size(200)]
        public String Host
        {
            get { return Fields.Host[this]; }
            set { Fields.Host[this] = value; }
        }

        [DisplayName("Port")]
        public Int32? Port
        {
            get { return Fields.Port[this]; }
            set { Fields.Port[this] = value; }
        }

        [DisplayName("SSL"), Column("SSL")]
        public Boolean? SSL
        {
            get { return Fields.SSL[this]; }
            set { Fields.SSL[this] = value; }
        }

        [DisplayName("Email Id"), Size(200)]
        public String EmailId
        {
            get { return Fields.EmailId[this]; }
            set { Fields.EmailId[this] = value; }
        }

        [DisplayName("Email Password"), Size(200)]
        public String EmailPassword
        {
            get { return Fields.EmailPassword[this]; }
            set { Fields.EmailPassword[this] = value; }
        }

        [DisplayName("Auto Email")]
        public Boolean? AutoEmail
        {
            get { return Fields.AutoEmail[this]; }
            set { Fields.AutoEmail[this] = value; }
        }

        [DisplayName("Auto Sms"), Column("AutoSMS")]
        public Boolean? AutoSms
        {
            get { return Fields.AutoSms[this]; }
            set { Fields.AutoSms[this] = value; }
        }
        
        [DisplayName("API Url"), Size(1000), NotMapped]
        public String API
        {
            get { return Fields.API[this]; }
            set { Fields.API[this] = value; }
        }

        [DisplayName("SMS TemplateId"), Column("SMSTemplateId"), Size(100)]
        public String SmsTemplateId
        {
            get { return Fields.SmsTemplateId[this]; }
            set { Fields.SmsTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Template"), Column("WATemplate"), TextAreaEditor(Rows = 4)]
        public String WaTemplate
        {
            get { return Fields.WaTemplate[this]; }
            set { Fields.WaTemplate[this] = value; }
        }

        [DisplayName("WhatsApp TemplateId"), Column("WATemplateId"), Size(100)]
        public String WaTemplateId
        {
            get { return Fields.WaTemplateId[this]; }
            set { Fields.WaTemplateId[this] = value; }
        }
       

        public FacebookConfigurationRow()
            : base(Fields)
        {
        }
        
        public FacebookConfigurationRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Sender;
            public StringField Subject;
            public StringField EmailTemplate;
            public StringField Attachment;
            public StringField SMSTemplate;
            public StringField Host;
            public Int32Field Port;
            public BooleanField SSL;
            public StringField EmailId;
            public StringField EmailPassword;
            public BooleanField AutoEmail;
            public BooleanField AutoSms;
            public StringField AppId;
            public StringField AccessTokenKey;
            public DateTimeField TokenExpiryDate;
            public StringField API;

            public StringField SmsTemplateId;
            public StringField WaTemplate;
            public StringField WaTemplateId;
        }
    }
}
