
namespace AdvanceCRM.Template
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Template"), TableName("[dbo].[EnquiryTemplate]")]
    [DisplayName("Enquiry Template"), InstanceName("Enquiry Template")]
    [ReadPermission("Template:Enquiry")]
    [ModifyPermission("Template:Enquiry")]
    [LookupScript("Template.EnquiryTemplate", Permission = "?")]
    public sealed class EnquiryTemplateRow : Row<EnquiryTemplateRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Sender"), Size(250), NotNull, QuickSearch,NameProperty]
        public String Sender
        {
            get { return Fields.Sender[this]; }
            set { Fields.Sender[this] = value; }
        }

        [DisplayName("Subject"), Size(250), NotNull]
        public String Subject
        {
            get { return Fields.Subject[this]; }
            set { Fields.Subject[this] = value; }
        }
        [DisplayName("WATemplate"), Column("WATemplate"), Size(1000), TextAreaEditor(Rows = 4)]
        public String WaTemplate
        {
            get { return Fields.WaTemplate[this]; }
            set { Fields.WaTemplate[this] = value; }
        }

        [DisplayName("Email Template"), Size(2000), NotNull, TextAreaEditor(Rows = 10), Placeholder("You can use token #username to auto include Username as signature name")]
        public String EmailTemplate
        {
            get { return Fields.EmailTemplate[this]; }
            set { Fields.EmailTemplate[this] = value; }
        }

        [DisplayName("Attachment"), Size(1000), LargeFileUploadEditor(CopyToHistory = true, FilenameFormat = "Attachments/~")]
        public String Attachment
        {
            get { return Fields.Attachment[this]; }
            set { Fields.Attachment[this] = value; }
        }

        [DisplayName("SMS Template"), Column("SMSTemplate"), Size(1000), NotNull, LookupInclude, TextAreaEditor(Rows = 4), Placeholder("You can use token #customername to auto include Contacts name in SMS")]
        public String SMSTemplate
        {
            get { return Fields.SMSTemplate[this]; }
            set { Fields.SMSTemplate[this] = value; }
        }

        [DisplayName("Host"), Size(200), Placeholder("example: smtp.gmail.com")]
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

        [DisplayName("Email Id"), Size(200), Placeholder("example: mail@yourdomin.com"), EmailEditor]
        public String EmailId
        {
            get { return Fields.EmailId[this]; }
            set { Fields.EmailId[this] = value; }
        }

        [DisplayName("Email Password"), Size(200), PasswordEditor]
        public String EmailPassword
        {
            get { return Fields.EmailPassword[this]; }
            set { Fields.EmailPassword[this] = value; }
        }

        [DisplayName("Company"), ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName"), NotNull, LookupInclude]
        public Int32? CompanyId
        {
            get { return Fields.CompanyId[this]; }
            set { Fields.CompanyId[this] = value; }
        }
        [DisplayName("Template Id"), Column("TemplateID"), Size(20)]
        public String TemplateId
        {
            get { return Fields.TemplateId[this]; }
            set { Fields.TemplateId[this] = value; }
        }

        [DisplayName("SMS Reminder"), Column("SMSReminder"), TextAreaEditor(Rows = 4)]
        public String SmsReminder
        {
            get { return Fields.SmsReminder[this]; }
            set { Fields.SmsReminder[this] = value; }
        }

        [DisplayName("SMS Reminder TemplateId"), Column("SMSRTemplateId"), Size(100)]
        public String SmsrTemplateId
        {
            get { return Fields.SmsrTemplateId[this]; }
            set { Fields.SmsrTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Reminder Template"), Column("WAReminder"), TextAreaEditor(Rows = 4)]
        public String WaReminder
        {
            get { return Fields.WaReminder[this]; }
            set { Fields.WaReminder[this] = value; }
        }

        [DisplayName("WhatsApp Reminder TemplateId"), Column("WARTemplateId"), Size(100)]
        public String WarTemplateId
        {
            get { return Fields.WarTemplateId[this]; }
            set { Fields.WarTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp TemplateId"), Column("WATemplateId"), Size(100)]
        public String WaTemplateId
        {
            get { return Fields.WaTemplateId[this]; }
            set { Fields.WaTemplateId[this] = value; }
        }

       

        public EnquiryTemplateRow()
            : base(Fields)
        {
        }
        
        public EnquiryTemplateRow(RowFields fields)
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
            public Int32Field CompanyId;
            public StringField TemplateId;
            public StringField WaTemplate;
            public StringField SmsReminder;
            public StringField SmsrTemplateId;
            public StringField WaReminder;
            public StringField WarTemplateId;
            public StringField WaTemplateId;

        }
    }
}
