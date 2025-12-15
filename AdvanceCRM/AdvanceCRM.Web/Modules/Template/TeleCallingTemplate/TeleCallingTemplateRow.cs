
namespace AdvanceCRM.Template
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Template"), TableName("[dbo].[TeleCallingTemplate]")]
    [DisplayName("Tele Calling Template"), InstanceName("Tele Calling Template")]
    [ReadPermission("Template:TeleCalling")]
    [ModifyPermission("Template:TeleCalling")]
    [LookupScript("Template.TeleCallingTemplate", Permission = "?")]
    public sealed class TeleCallingTemplateRow : Row<TeleCallingTemplateRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("From"), Size(500), NotNull, QuickSearch,NameProperty]
        public String From
        {
            get { return Fields.From[this]; }
            set { Fields.From[this] = value; }
        }

        [DisplayName("Subject"), Size(500), NotNull]
        public String Subject
        {
            get { return Fields.Subject[this]; }
            set { Fields.Subject[this] = value; }
        }

        [DisplayName("Customer SMS"), Column("CustomerSMS"), Size(500), NotNull, TextAreaEditor(Rows = 4)]
        public String CustomerSms
        {
            get { return Fields.CustomerSms[this]; }
            set { Fields.CustomerSms[this] = value; }
        }
        [DisplayName("Cust Template Id"), Column("CustTemplateID"), Size(20)]
        public String CustTemplateId
        {
            get { return Fields.CustTemplateId[this]; }
            set { Fields.CustTemplateId[this] = value; }
        }

        [DisplayName("Exe Template Id"), Column("ExeTemplateID"), Size(20)]
        public String ExeTemplateId
        {
            get { return Fields.ExeTemplateId[this]; }
            set { Fields.ExeTemplateId[this] = value; }
        }

        [DisplayName("Cust R Template Id"), Column("CustRTemplateID"), Size(20)]
        public String CustRTemplateId
        {
            get { return Fields.CustRTemplateId[this]; }
            set { Fields.CustRTemplateId[this] = value; }
        }

        [DisplayName("Exe R Template Id"), Column("ExeRTemplateID"), Size(20)]
        public String ExeRTemplateId
        {
            get { return Fields.ExeRTemplateId[this]; }
            set { Fields.ExeRTemplateId[this] = value; }
        }

        [DisplayName("Executive SMS"), Column("ExecutiveSMS"), Size(500), NotNull, TextAreaEditor(Rows = 4)]
        public String ExecutiveSms
        {
            get { return Fields.ExecutiveSms[this]; }
            set { Fields.ExecutiveSms[this] = value; }
        }

        [DisplayName("Customer Email"), Size(500), NotNull, TextAreaEditor(Rows = 4)]
        public String CustomerEmail
        {
            get { return Fields.CustomerEmail[this]; }
            set { Fields.CustomerEmail[this] = value; }
        }

        [DisplayName("Executive Email"), Size(500), NotNull, TextAreaEditor(Rows = 4)]
        public String ExecutiveEmail
        {
            get { return Fields.ExecutiveEmail[this]; }
            set { Fields.ExecutiveEmail[this] = value; }
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


        [DisplayName("Customer Reminder SMS"), Column("CustomerReminderSMS"), Size(500), TextAreaEditor(Rows = 4)]
        public String CustomerReminderSMS
        {
            get { return Fields.CustomerReminderSMS[this]; }
            set { Fields.CustomerReminderSMS[this] = value; }
        }

        [DisplayName("Executive Reminder SMS"), Column("ExecutiveReminderSMS"), Size(500), TextAreaEditor(Rows = 4)]
        public String ExecutiveReminderSMS
        {
            get { return Fields.ExecutiveReminderSMS[this]; }
            set { Fields.ExecutiveReminderSMS[this] = value; }
        }

        [DisplayName("Company"), ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName"), NotNull, LookupInclude]
        public Int32? CompanyId
        {
            get { return Fields.CompanyId[this]; }
            set { Fields.CompanyId[this] = value; }
        }

        [DisplayName("WhatsApp Customer Template"), Column("WACustomTemplate"), TextAreaEditor(Rows = 4)]
        public String WaCustomTemplate
        {
            get { return Fields.WaCustomTemplate[this]; }
            set { Fields.WaCustomTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Customer TemplateId"), Column("WACustomTemplateId"), Size(100)]
        public String WaCustomTemplateId
        {
            get { return Fields.WaCustomTemplateId[this]; }
            set { Fields.WaCustomTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Executive Template"), Column("WAExeTemplate"), TextAreaEditor(Rows = 4)]
        public String WaExeTemplate
        {
            get { return Fields.WaExeTemplate[this]; }
            set { Fields.WaExeTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Executive TemplateId"), Column("WAExeTemplateId"), Size(100)]
        public String WaExeTemplateId
        {
            get { return Fields.WaExeTemplateId[this]; }
            set { Fields.WaExeTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Reminder Customer Template"), Column("RWACustomTemplate"), TextAreaEditor(Rows = 4)]
        public String RwaCustomTemplate
        {
            get { return Fields.RwaCustomTemplate[this]; }
            set { Fields.RwaCustomTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Reminder Customer TemplateId"), Column("RWACustomTemplateId"), Size(100)]
        public String RwaCustomTemplateId
        {
            get { return Fields.RwaCustomTemplateId[this]; }
            set { Fields.RwaCustomTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Reminder Executive Template"), Column("RWAExeTemplate"), TextAreaEditor(Rows = 4)]
        public String RwaExeTemplate
        {
            get { return Fields.RwaExeTemplate[this]; }
            set { Fields.RwaExeTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Reminder Executive TemplateId"), Column("RWAExeTemplateId"), Size(100)]
        public String RwaExeTemplateId
        {
            get { return Fields.RwaExeTemplateId[this]; }
            set { Fields.RwaExeTemplateId[this] = value; }
        }


     

        public TeleCallingTemplateRow()
            : base(Fields)
        {
        }
        

        public TeleCallingTemplateRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField From;
            public StringField Subject;
            public StringField CustomerSms;
            public StringField ExecutiveSms;
            public StringField CustomerEmail;
            public StringField ExecutiveEmail;
            public StringField CustomerReminderSMS;
            public StringField ExecutiveReminderSMS;
            public Int32Field CompanyId;
            public StringField CustTemplateId;
            public StringField ExeTemplateId;
            public StringField CustRTemplateId;
            public StringField ExeRTemplateId;
            public StringField WaCustomTemplate;
            public StringField WaCustomTemplateId;
            public StringField WaExeTemplate;
            public StringField WaExeTemplateId;
            public StringField RwaCustomTemplate;
            public StringField RwaCustomTemplateId;
            public StringField RwaExeTemplate;
            public StringField RwaExeTemplateId;

            public StringField SmsReminder;
            public StringField SmsrTemplateId;
            public StringField WaReminder;
            public StringField WarTemplateId;
        }
    }
}
