
namespace AdvanceCRM.Template
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Template"), TableName("[dbo].[InvoiceTemplate]")]
    [DisplayName("Invoice Template"), InstanceName("Invoice Template")]
    [ReadPermission("Template:Invoice")]
    [ModifyPermission("Template:Invoice")]
    [LookupScript("Template.InvoiceTemplate", Permission = "?")]
    public sealed class InvoiceTemplateRow : Row<InvoiceTemplateRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }
        [DisplayName("WATemplate"), Column("WATemplate"), Size(1000), TextAreaEditor(Rows = 4),NameProperty]
        public String WaTemplate
        {
            get { return Fields.WaTemplate[this]; }
            set { Fields.WaTemplate[this] = value; }
        }

        [DisplayName("Sender"), Size(250), NotNull, QuickSearch]
        public String Sender
        {
            get { return Fields.Sender[this]; }
            set { Fields.Sender[this] = value; }
        }

        [DisplayName("Subject"), Size(250), NotNull, QuickSearch]
        public String Subject
        {
            get { return Fields.Subject[this]; }
            set { Fields.Subject[this] = value; }
        }

        [DisplayName("Email Template"), Size(2000), NotNull, TextAreaEditor(Rows = 4)]
        public String EmailTemplate
        {
            get { return Fields.EmailTemplate[this]; }
            set { Fields.EmailTemplate[this] = value; }
        }

        [DisplayName("SMS Template"), Column("SMSTemplate"), Size(1000), NotNull, LookupInclude, TextAreaEditor(Rows = 4)]
        public String SMSTemplate
        {
            get { return Fields.SMSTemplate[this]; }
            set { Fields.SMSTemplate[this] = value; }
        }

        [DisplayName("Terms Conditions"), Size(2000), TextAreaEditor(Rows = 4)]
        public String TermsConditions
        {
            get { return Fields.TermsConditions[this]; }
            set { Fields.TermsConditions[this] = value; }
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

        [DisplayName("Email Id"), Size(200), EmailEditor]
        public String EmailId
        {
            get { return Fields.EmailId[this]; }
            set { Fields.EmailId[this] = value; }
        }
        [DisplayName("Template Id"), Column("TemplateID"), Size(20)]
        public String TemplateId
        {
            get { return Fields.TemplateId[this]; }
            set { Fields.TemplateId[this] = value; }
        }

        [DisplayName("Email Password"), Size(200), PasswordEditor]
        public String EmailPassword
        {
            get { return Fields.EmailPassword[this]; }
            set { Fields.EmailPassword[this] = value; }
        }

        [DisplayName("CC Email IDs"), Column("CCEmails"), Size(1000), TextAreaEditor(Rows = 4), Placeholder("Seperate emails with comma(,)")]
        public String CCEmails
        {
            get { return Fields.CCEmails[this]; }
            set { Fields.CCEmails[this] = value; }
        }

        [DisplayName("CC SMS"), Column("CCSMSs"), Size(1000), TextAreaEditor(Rows = 4), Placeholder("Seperate phone numbers with comma(,)")]
        public String CcsmSs
        {
            get { return Fields.CcsmSs[this]; }
            set { Fields.CcsmSs[this] = value; }
        }

        [DisplayName("Attachment"), Size(1000), LargeFileUploadEditor(CopyToHistory = true, FilenameFormat = "Attachments/~")]
        public String Attachment
        {
            get { return Fields.Attachment[this]; }
            set { Fields.Attachment[this] = value; }
        }

        [DisplayName("BCC Email Ids"), Column("BCCEmails"), Size(500), TextAreaEditor(Rows = 4), Placeholder("Seperate emails with comma(,)")]
        public String BCCEmails
        {
            get { return Fields.BCCEmails[this]; }
            set { Fields.BCCEmails[this] = value; }
        }

        [DisplayName("Company"), ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName"), NotNull, LookupInclude]
        public Int32? CompanyId
        {
            get { return Fields.CompanyId[this]; }
            set { Fields.CompanyId[this] = value; }
        }

        [DisplayName("SMS Reminder Template"), Column("SMSReminder"), TextAreaEditor(Rows = 4)]
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

        [DisplayName("WhatsApp Template Id"), Column("WATemplateId"), Size(100)]
        public String WaTemplateId
        {
            get { return Fields.WaTemplateId[this]; }
            set { Fields.WaTemplateId[this] = value; }
        }

      
        public InvoiceTemplateRow()
            : base(Fields)
        {
        }
      
        public InvoiceTemplateRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Sender;
            public StringField Subject;
            public StringField EmailTemplate;
            public StringField SMSTemplate;
            public StringField TermsConditions;
            public StringField Host;
            public Int32Field Port;
            public BooleanField SSL;
            public StringField TemplateId;
            public StringField EmailId;
            public StringField EmailPassword;
            public StringField CCEmails;
            public StringField CcsmSs;
            public StringField Attachment;
            public StringField BCCEmails;
            public Int32Field CompanyId;
            public StringField WaTemplate;
            public StringField SmsReminder;
            public StringField SmsrTemplateId;
            public StringField WaReminder;
            public StringField WarTemplateId;
            public StringField WaTemplateId;
        }
    }
}
