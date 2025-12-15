
namespace AdvanceCRM.Template
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Template"), TableName("[dbo].[AMCTemplate]")]
    [DisplayName("AMC Template"), InstanceName("AMC Template")]
    [ReadPermission("Template:AMC")]
    [ModifyPermission("Template:AMC")]
    [LookupScript("Template.AMCTemplate", Permission = "?")]
    public sealed class AMCTemplateRow : Row<AMCTemplateRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
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

        [DisplayName("Email Template Receipt"), Size(2000), NotNull, TextAreaEditor(Rows = 4)]
        public String EmailTemplateReceipt
        {
            get { return Fields.EmailTemplateReceipt[this]; }
            set { Fields.EmailTemplateReceipt[this] = value; }
        }

        [DisplayName("SMS Template"), Column("SMSTemplate"), Size(1000), NotNull, LookupInclude, TextAreaEditor(Rows = 4)]
        public String SMSTemplate
        {
            get { return Fields.SMSTemplate[this]; }
            set { Fields.SMSTemplate[this] = value; }
        }

        [DisplayName("CC Email Ids"), Column("CCEmails"), Size(1000), TextAreaEditor(Rows = 4), Placeholder("Seperate emails with comma(,)")]
        public String CCEmails
        {
            get { return Fields.CCEmails[this]; }
            set { Fields.CCEmails[this] = value; }
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
        [DisplayName("Sms Temp Id"), Column("SMSTempID"), Size(20)]
        public String SmsTempId
        {
            get { return Fields.SmsTempId[this]; }
            set { Fields.SmsTempId[this] = value; }
        }

        [DisplayName("Visit Temp Id"), Column("VisitTempID"), Size(20)]
        public String VisitTempId
        {
            get { return Fields.VisitTempId[this]; }
            set { Fields.VisitTempId[this] = value; }
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

        [DisplayName("Visit SMS Template"), Column("VisitSMSTemplate"), Size(1000), TextAreaEditor(Rows = 4)]
        public String VisitSMSTemplate
        {
            get { return Fields.VisitSMSTemplate[this]; }
            set { Fields.VisitSMSTemplate[this] = value; }
        }

        [DisplayName("Company"), ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName"), NotNull, LookupInclude]
        public Int32? CompanyId
        {
            get { return Fields.CompanyId[this]; }
            set { Fields.CompanyId[this] = value; }
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

        [DisplayName("WhatsApp Visit Template"), Column("WAVisitTemplate"),TextAreaEditor(Rows = 4)]
        public String WaVisitTemplate
        {
            get { return Fields.WaVisitTemplate[this]; }
            set { Fields.WaVisitTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Visit TemplateId"), Column("WAVisitTemplateId"), Size(100)]
        public String WaVisitTemplateId
        {
            get { return Fields.WaVisitTemplateId[this]; }
            set { Fields.WaVisitTemplateId[this] = value; }
        }

      

        public AMCTemplateRow()
            : base(Fields)
        {
        }
          public AMCTemplateRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Sender;
            public StringField Subject;
            public StringField EmailTemplateReceipt;
            public StringField SMSTemplate;
            public StringField CCEmails;
            public StringField TermsConditions;
            public StringField Host;
            public Int32Field Port;
            public BooleanField SSL;
            public StringField EmailId;
            public StringField EmailPassword;
            public StringField BCCEmails;
            public StringField VisitSMSTemplate;
            public Int32Field CompanyId;
            public StringField SmsTempId;
            public StringField VisitTempId;
            public StringField WaTemplate;
            public StringField WaTemplateId;
            public StringField WaVisitTemplate;
            public StringField WaVisitTemplateId;

        }
    }
}
