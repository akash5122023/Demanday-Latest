
namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[InteraktConfig]")]
    [DisplayName("Interakt Config"), InstanceName("Interakt Config")]
    [ReadPermission("Settings:Interakt")]
    [ModifyPermission("Settings:Interakt")]
    public sealed class InteraktConfigRow : Row<InteraktConfigRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Secret Key"), Size(1000), NotNull, QuickSearch,NameProperty]
        public String SecretKey
        {
            get { return Fields.SecretKey[this]; }
            set { Fields.SecretKey[this] = value; }
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

        [DisplayName("Sender"), Size(250)]
        public String Sender
        {
            get { return Fields.Sender[this]; }
            set { Fields.Sender[this] = value; }
        }

        [DisplayName("Subject"), Size(250)]
        public String Subject
        {
            get { return Fields.Subject[this]; }
            set { Fields.Subject[this] = value; }
        }

        [DisplayName("Email Template"), Size(2000)]
        public String EmailTemplate
        {
            get { return Fields.EmailTemplate[this]; }
            set { Fields.EmailTemplate[this] = value; }
        }

        [DisplayName("Attachment"), Size(1000)]
        public String Attachment
        {
            get { return Fields.Attachment[this]; }
            set { Fields.Attachment[this] = value; }
        }

        [DisplayName("Sms Template"), Column("SMSTemplate"), Size(1000)]
        public String SmsTemplate
        {
            get { return Fields.SmsTemplate[this]; }
            set { Fields.SmsTemplate[this] = value; }
        }

        [DisplayName("Template Id"), Column("TemplateID"), Size(1000)]
        public String TemplateId
        {
            get { return Fields.TemplateId[this]; }
            set { Fields.TemplateId[this] = value; }
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

        [DisplayName("Ssl"), Column("SSL")]
        public Boolean? Ssl
        {
            get { return Fields.Ssl[this]; }
            set { Fields.Ssl[this] = value; }
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

        [DisplayName("WhatsApp Template"), Column("WATemplate"), TextAreaEditor(Rows = 4)]
        public String WaTemplate
        {
            get { return Fields.WaTemplate[this]; }
            set { Fields.WaTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Template Id"), Column("WATemplateId"), Size(100)]
        public String WaTemplateId
        {
            get { return Fields.WaTemplateId[this]; }
            set { Fields.WaTemplateId[this] = value; }
        }

      
        public InteraktConfigRow()
            : base(Fields)
        {
        }
        
        public InteraktConfigRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField SecretKey;
            public BooleanField AutoEmail;
            public BooleanField AutoSms;
            public StringField Sender;
            public StringField Subject;
            public StringField EmailTemplate;
            public StringField Attachment;
            public StringField SmsTemplate;
            public StringField TemplateId;
            public StringField Host;
            public Int32Field Port;
            public BooleanField Ssl;
            public StringField EmailId;
            public StringField EmailPassword;
            public StringField WaTemplate;
            public StringField WaTemplateId;
        }
    }
}
