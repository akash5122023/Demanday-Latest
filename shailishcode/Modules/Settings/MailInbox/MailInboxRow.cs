
namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[MailInbox]")]
    [DisplayName("MailInbox"), InstanceName("MailInbox")]
    [ReadPermission("Settings:MailInbox")]
    [ModifyPermission("Settings:MailInbox")]
    public sealed class MailInboxRow : Row<MailInboxRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Host"), Size(200), QuickSearch,NameProperty]
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

        [DisplayName("Auto Email")]
        public Boolean? AutoEmail
        {
            get { return Fields.AutoEmail[this]; }
            set { Fields.AutoEmail[this] = value; }
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

        [DisplayName("Host"), Size(200)]
        public String SHost
        {
            get { return Fields.SHost[this]; }
            set { Fields.SHost[this] = value; }
        }

        [DisplayName("Port")]
        public Int32? SPort
        {
            get { return Fields.SPort[this]; }
            set { Fields.SPort[this] = value; }
        }

        [DisplayName("Sssl"), Column("SSsl")]
        public Boolean? Sssl
        {
            get { return Fields.Sssl[this]; }
            set { Fields.Sssl[this] = value; }
        }

        [DisplayName("Email Id"), Size(200)]
        public String SEmailId
        {
            get { return Fields.SEmailId[this]; }
            set { Fields.SEmailId[this] = value; }
        }

        [DisplayName("Email Password"), Size(200)]
        public String SEmailPassword
        {
            get { return Fields.SEmailPassword[this]; }
            set { Fields.SEmailPassword[this] = value; }
        }

       
        public MailInboxRow()
            : base(Fields)
        {
        }
        
       
        public MailInboxRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Host;
            public Int32Field Port;
            public BooleanField Ssl;
            public StringField EmailId;
            public StringField EmailPassword;
            public BooleanField AutoEmail;
            public StringField Sender;
            public StringField Subject;
            public StringField EmailTemplate;
            public StringField Attachment;
            public StringField SHost;
            public Int32Field SPort;
            public BooleanField Sssl;
            public StringField SEmailId;
            public StringField SEmailPassword;
        }
    }
}
