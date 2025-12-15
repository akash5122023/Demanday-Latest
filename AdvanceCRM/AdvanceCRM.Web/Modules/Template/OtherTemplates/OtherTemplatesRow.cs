
namespace AdvanceCRM.Template
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Template"), TableName("[dbo].[OtherTemplates]")]
    [DisplayName("Other Templates"), InstanceName("Other Templates")]
    [ReadPermission("Administration:General")]
    [ModifyPermission("Administration:General")]
    public sealed class OtherTemplatesRow : Row<OtherTemplatesRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("SMS Template"), Column("TicketSMSTemplate"), QuickSearch, TextAreaEditor(Rows = 4),NameProperty]
        public String TicketSmsTemplate
        {
            get { return Fields.TicketSmsTemplate[this]; }
            set { Fields.TicketSmsTemplate[this] = value; }
        }

        [DisplayName("SMS TemplateId"), Column("TicketSMSTemplateID"), Size(100)]
        public String TicketSmsTemplateId
        {
            get { return Fields.TicketSmsTemplateId[this]; }
            set { Fields.TicketSmsTemplateId[this] = value; }
        }

        [DisplayName("SMS Template"), Column("TaskSMSTemplate"), TextAreaEditor(Rows = 4)]
        public String TaskSmsTemplate
        {
            get { return Fields.TaskSmsTemplate[this]; }
            set { Fields.TaskSmsTemplate[this] = value; }
        }

        [DisplayName("SMS TemplateId"), Column("TaskSMSTemplateID"), Size(100)]
        public String TaskSmsTemplateId
        {
            get { return Fields.TaskSmsTemplateId[this]; }
            set { Fields.TaskSmsTemplateId[this] = value; }
        }

        [DisplayName("SMS Template"), Column("FeedbackSMSTemplate"), TextAreaEditor(Rows = 4)]
        public String FeedbackSmsTemplate
        {
            get { return Fields.FeedbackSmsTemplate[this]; }
            set { Fields.FeedbackSmsTemplate[this] = value; }
        }

        [DisplayName("SMS TemplateId"), Column("FeedbackSMSTemplateID"), Size(100)]
        public String FeedbackSmsTemplateId
        {
            get { return Fields.FeedbackSmsTemplateId[this]; }
            set { Fields.FeedbackSmsTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Template"), Column("FeedbackWATemplate"), TextAreaEditor(Rows = 4)]
        public String FeedbackWaTemplate
        {
            get { return Fields.FeedbackWaTemplate[this]; }
            set { Fields.FeedbackWaTemplate[this] = value; }
        }

        [DisplayName("WhatsApp TemplateId"), Column("FeedbackSWATemplateID"), Size(100)]
        public String FeedbackSwaTemplateId
        {
            get { return Fields.FeedbackSwaTemplateId[this]; }
            set { Fields.FeedbackSwaTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Template"), Column("TicketWATemplate"), TextAreaEditor(Rows = 4)]
        public String TicketWaTemplate
        {
            get { return Fields.TicketWaTemplate[this]; }
            set { Fields.TicketWaTemplate[this] = value; }
        }

        [DisplayName("WhatsApp TemplateId"), Column("TicketWATemplateID"), Size(100)]
        public String TicketWaTemplateId
        {
            get { return Fields.TicketWaTemplateId[this]; }
            set { Fields.TicketWaTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Template"), Column("TaskWATemplate"), TextAreaEditor(Rows = 4)]
        public String TaskWaTemplate
        {
            get { return Fields.TaskWaTemplate[this]; }
            set { Fields.TaskWaTemplate[this] = value; }
        }

        [DisplayName("WhatsApp TemplateId"), Column("TaskWATemplateID"), Size(100)]
        public String TaskWaTemplateId
        {
            get { return Fields.TaskWaTemplateId[this]; }
            set { Fields.TaskWaTemplateId[this] = value; }
        }

        [DisplayName("SMS Template"), Column("OTPSMSTemplate"), TextAreaEditor(Rows = 4)]
        public String OtpsmsTemplate
        {
            get { return Fields.OtpsmsTemplate[this]; }
            set { Fields.OtpsmsTemplate[this] = value; }
        }

        [DisplayName("SMS TemplateId"), Column("OTPSMSTemplateID"), Size(100)]
        public String OtpsmsTemplateId
        {
            get { return Fields.OtpsmsTemplateId[this]; }
            set { Fields.OtpsmsTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Template"), Column("OTPWATemplate"), TextAreaEditor(Rows = 4)]
        public String OtpwaTemplate
        {
            get { return Fields.OtpwaTemplate[this]; }
            set { Fields.OtpwaTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Template Id"), Column("OTPWATemplateID"), Size(100)]
        public String OtpwaTemplateId
        {
            get { return Fields.OtpwaTemplateId[this]; }
            set { Fields.OtpwaTemplateId[this] = value; }
        }

      
        public OtherTemplatesRow()
            : base(Fields)
        {
        }
        
        public OtherTemplatesRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField TicketSmsTemplate;
            public StringField TicketSmsTemplateId;
            public StringField TaskSmsTemplate;
            public StringField TaskSmsTemplateId;
            public StringField FeedbackSmsTemplate;
            public StringField FeedbackSmsTemplateId;
            public StringField FeedbackWaTemplate;
            public StringField FeedbackSwaTemplateId;
            public StringField TicketWaTemplate;
            public StringField TicketWaTemplateId;
            public StringField TaskWaTemplate;
            public StringField TaskWaTemplateId;
            public StringField OtpsmsTemplate;
            public StringField OtpsmsTemplateId;
            public StringField OtpwaTemplate;
            public StringField OtpwaTemplateId;
        }
    }
}
