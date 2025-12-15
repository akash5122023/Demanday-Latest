
namespace AdvanceCRM.Template
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Template"), TableName("[dbo].[CMSTemplate]")]
    [DisplayName("CMS Template"), InstanceName("CMS Template")]
    [ReadPermission("Template:CMS")]
    [ModifyPermission("Template:CMS")]
    [LookupScript("Template.CmsTemplate", Permission = "?")]
    public sealed class CmsTemplateRow : Row<CmsTemplateRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
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

        [DisplayName("Email Template Receipt"), Size(2000), NotNull, TextAreaEditor(Rows = 4)]
        public String EmailTemplateReceipt
        {
            get { return Fields.EmailTemplateReceipt[this]; }
            set { Fields.EmailTemplateReceipt[this] = value; }
        }

        [DisplayName("Closed Email Template"), Size(2000), NotNull, TextAreaEditor(Rows = 4)]
        public String ClosedEmailTemplate
        {
            get { return Fields.ClosedEmailTemplate[this]; }
            set { Fields.ClosedEmailTemplate[this] = value; }
        }

        [DisplayName("Engineer Email Template"), Size(2000), NotNull, TextAreaEditor(Rows = 4)]
        public String EngineerEmailTemplate
        {
            get { return Fields.EngineerEmailTemplate[this]; }
            set { Fields.EngineerEmailTemplate[this] = value; }
        }
        [DisplayName("Sms Template Id"), Column("SMSTemplateID"), Size(20)]
        public String SmsTemplateId
        {
            get { return Fields.SmsTemplateId[this]; }
            set { Fields.SmsTemplateId[this] = value; }
        }

        [DisplayName("Closed Template Id"), Column("ClosedTemplateID"), Size(20)]
        public String ClosedTemplateId
        {
            get { return Fields.ClosedTemplateId[this]; }
            set { Fields.ClosedTemplateId[this] = value; }
        }

        [DisplayName("Emgineer Template Id"), Column("EmgineerTemplateID"), Size(20)]
        public String EmgineerTemplateId
        {
            get { return Fields.EmgineerTemplateId[this]; }
            set { Fields.EmgineerTemplateId[this] = value; }
        }

        [DisplayName("SMS Template"), Column("SMSTemplate"), Size(1000), NotNull, LookupInclude, TextAreaEditor(Rows = 4)]
        public String SMSTemplate
        {
            get { return Fields.SMSTemplate[this]; }
            set { Fields.SMSTemplate[this] = value; }
        }

        [DisplayName("Closed SMS Template"), Column("ClosedSMSTemplate"), Size(1000), NotNull, TextAreaEditor(Rows = 4)]
        public String ClosedSMSTemplate
        {
            get { return Fields.ClosedSMSTemplate[this]; }
            set { Fields.ClosedSMSTemplate[this] = value; }
        }

        [DisplayName("Engineer SMS Template"), Column("EngineerSMSTemplate"), Size(1000), NotNull, TextAreaEditor(Rows = 4)]
        public String EngineerSMSTemplate
        {
            get { return Fields.EngineerSMSTemplate[this]; }
            set { Fields.EngineerSMSTemplate[this] = value; }
        }

        [DisplayName("CC Email Ids"), Column("CCEmails"), Size(1000), TextAreaEditor(Rows = 4), Placeholder("Seperate emails with comma(,)")]
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

        [DisplayName("Email Password"), Size(200), PasswordEditor]
        public String EmailPassword
        {
            get { return Fields.EmailPassword[this]; }
            set { Fields.EmailPassword[this] = value; }
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

        [DisplayName("WhatsApp Closed Template"), Column("WAClosedTemplate"), TextAreaEditor(Rows = 4)]
        public String WaClosedTemplate
        {
            get { return Fields.WaClosedTemplate[this]; }
            set { Fields.WaClosedTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Closed TemplateId"), Column("WAClosedTemplateId"), Size(100)]
        public String WaClosedTemplateId
        {
            get { return Fields.WaClosedTemplateId[this]; }
            set { Fields.WaClosedTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Engineer Template"), Column("WAENGTemplate"), TextAreaEditor(Rows = 4)]
        public String WaengTemplate
        {
            get { return Fields.WaengTemplate[this]; }
            set { Fields.WaengTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Engineer TemplateId"), Column("WAENGTemplateId"), Size(100)]
        public String WaengTemplateId
        {
            get { return Fields.WaengTemplateId[this]; }
            set { Fields.WaengTemplateId[this] = value; }
        }


    
        public CmsTemplateRow()
            : base(Fields)
        {
        }
        public CmsTemplateRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Sender;
            public StringField Subject;
            public StringField EmailTemplate;
            public StringField EmailTemplateReceipt;
            public StringField ClosedEmailTemplate;
            public StringField EngineerEmailTemplate;
            public StringField SMSTemplate;
            public StringField ClosedSMSTemplate;
            public StringField EngineerSMSTemplate;
            public StringField CCEmails;
            public StringField CcsmSs;
            public StringField Host;
            public Int32Field Port;
            public BooleanField SSL;
            public StringField EmailId;
            public StringField EmailPassword;
            public StringField BCCEmails;
            public Int32Field CompanyId;
            public StringField SmsTemplateId;
            public StringField ClosedTemplateId;
            public StringField EmgineerTemplateId;

            public StringField SmsReminder;
            public StringField SmsrTemplateId;
            public StringField WaReminder;
            public StringField WarTemplateId;
            public StringField WaTemplate;
            public StringField WaTemplateId;
            public StringField WaClosedTemplate;
            public StringField WaClosedTemplateId;
            public StringField WaengTemplate;
            public StringField WaengTemplateId;

        }
    }
}
