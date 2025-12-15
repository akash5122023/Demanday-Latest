
namespace AdvanceCRM.Template
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Template"), TableName("[dbo].[AppointmentTemplate]")]
    [DisplayName("Appointment Template"), InstanceName("Appointment Template")]
    [ReadPermission("Template:Appointment")]
    [ModifyPermission("Template:Appointment")]
    [LookupScript("Template.AppointmentTemplate", Permission = "?")]
    public sealed class AppointmentTemplateRow : Row<AppointmentTemplateRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity, IdProperty]
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

        [DisplayName("Email Template"), Size(2000), NotNull, TextAreaEditor(Rows = 4)]
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

        [DisplayName("SMS Template"), Column("SMSTemplate"), Size(1000), NotNull, TextAreaEditor(Rows = 4)]
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

        [DisplayName("Monday SMS"), Column("MondaySMS"), Size(500), TextAreaEditor(Rows = 4)]
        public String MondaySMS
        {
            get { return Fields.MondaySMS[this]; }
            set { Fields.MondaySMS[this] = value; }
        }

        [DisplayName("Tuesday SMS"), Column("TuesdaySMS"), Size(500), TextAreaEditor(Rows = 4)]
        public String TuesdaySMS
        {
            get { return Fields.TuesdaySMS[this]; }
            set { Fields.TuesdaySMS[this] = value; }
        }

        [DisplayName("Wednesday SMS"), Column("WednesdaySMS"), Size(500), TextAreaEditor(Rows = 4)]
        public String WednesdaySMS
        {
            get { return Fields.WednesdaySMS[this]; }
            set { Fields.WednesdaySMS[this] = value; }
        }

        [DisplayName("Thursday SMS"), Column("ThursdaySMS"), Size(500), TextAreaEditor(Rows = 4)]
        public String ThursdaySMS
        {
            get { return Fields.ThursdaySMS[this]; }
            set { Fields.ThursdaySMS[this] = value; }
        }
        [DisplayName("Sms Temp Id"), Column("SMSTempID"), Size(20)]
        public String SmsTempId
        {
            get { return Fields.SmsTempId[this]; }
            set { Fields.SmsTempId[this] = value; }
        }

        [DisplayName("Mon Temp Id"), Column("MonTempID"), Size(20)]
        public String MonTempId
        {
            get { return Fields.MonTempId[this]; }
            set { Fields.MonTempId[this] = value; }
        }

        [DisplayName("Tue Temp Id"), Column("TueTempID"), Size(20)]
        public String TueTempId
        {
            get { return Fields.TueTempId[this]; }
            set { Fields.TueTempId[this] = value; }
        }

        [DisplayName("Wed Temp Id"), Column("WedTempID"), Size(20)]
        public String WedTempId
        {
            get { return Fields.WedTempId[this]; }
            set { Fields.WedTempId[this] = value; }
        }

        [DisplayName("Thur Temp Id"), Column("ThurTempID"), Size(20)]
        public String ThurTempId
        {
            get { return Fields.ThurTempId[this]; }
            set { Fields.ThurTempId[this] = value; }
        }

        [DisplayName("Fri Temp Id"), Column("FriTempID"), Size(20)]
        public String FriTempId
        {
            get { return Fields.FriTempId[this]; }
            set { Fields.FriTempId[this] = value; }
        }

        [DisplayName("Sat Temp Id"), Column("SatTempID"), Size(20)]
        public String SatTempId
        {
            get { return Fields.SatTempId[this]; }
            set { Fields.SatTempId[this] = value; }
        }

        [DisplayName("Sun Temp Id"), Column("SunTempID"), Size(20)]
        public String SunTempId
        {
            get { return Fields.SunTempId[this]; }
            set { Fields.SunTempId[this] = value; }
        }


        [DisplayName("Friday SMS"), Column("FridaySMS"), Size(500), TextAreaEditor(Rows = 4)]
        public String FridaySMS
        {
            get { return Fields.FridaySMS[this]; }
            set { Fields.FridaySMS[this] = value; }
        }

        [DisplayName("Saturday SMS"), Column("SaturdaySMS"), Size(500), TextAreaEditor(Rows = 4)]
        public String SaturdaySMS
        {
            get { return Fields.SaturdaySMS[this]; }
            set { Fields.SaturdaySMS[this] = value; }
        }

        [DisplayName("Sunday SMS"), Column("SundaySMS"), Size(500), TextAreaEditor(Rows = 4)]
        public String SundaySMS
        {
            get { return Fields.SundaySMS[this]; }
            set { Fields.SundaySMS[this] = value; }
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

        [DisplayName("WhatsApp Monday Template"), Column("WAMonTemplate"), TextAreaEditor(Rows = 4)]
        public String WaMonTemplate
        {
            get { return Fields.WaMonTemplate[this]; }
            set { Fields.WaMonTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Monday TemplateId"), Column("WAMonTemplateId"), Size(100)]
        public String WaMonTemplateId
        {
            get { return Fields.WaMonTemplateId[this]; }
            set { Fields.WaMonTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Tuesday Template"), Column("WATueTemplate"), TextAreaEditor(Rows = 4)]
        public String WaTueTemplate
        {
            get { return Fields.WaTueTemplate[this]; }
            set { Fields.WaTueTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Tuesday TemplateId"), Column("WATueTemplateId"), Size(100)]
        public String WaTueTemplateId
        {
            get { return Fields.WaTueTemplateId[this]; }
            set { Fields.WaTueTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Wednesday Template"), Column("WAWedTemplate"), TextAreaEditor(Rows = 4)]
        public String WaWedTemplate
        {
            get { return Fields.WaWedTemplate[this]; }
            set { Fields.WaWedTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Wednesday TemplateId"), Column("WAWebTemplateId"), Size(100)]
        public String WaWebTemplateId
        {
            get { return Fields.WaWebTemplateId[this]; }
            set { Fields.WaWebTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Thursday Template"), Column("WAThurTemplate"), TextAreaEditor(Rows = 4)]
        public String WaThurTemplate
        {
            get { return Fields.WaThurTemplate[this]; }
            set { Fields.WaThurTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Thursday TemplateId"), Column("WAThurTemplateId"), Size(100)]
        public String WaThurTemplateId
        {
            get { return Fields.WaThurTemplateId[this]; }
            set { Fields.WaThurTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Friday Template"), Column("WAFriTemplate"), TextAreaEditor(Rows = 4)]
        public String WaFriTemplate
        {
            get { return Fields.WaFriTemplate[this]; }
            set { Fields.WaFriTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Friday Template Id"), Column("WAFriTemplateId"), Size(100)]
        public String WaFriTemplateId
        {
            get { return Fields.WaFriTemplateId[this]; }
            set { Fields.WaFriTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Saturday Template"), Column("WASatTemplate"), TextAreaEditor(Rows = 4)]
        public String WaSatTemplate
        {
            get { return Fields.WaSatTemplate[this]; }
            set { Fields.WaSatTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Saturday TemplateId"), Column("WASatTemplateId"), Size(100)]
        public String WaSatTemplateId
        {
            get { return Fields.WaSatTemplateId[this]; }
            set { Fields.WaSatTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Sunday Template"), Column("WASunTemplate"), TextAreaEditor(Rows = 4)]
        public String WaSunTemplate
        {
            get { return Fields.WaSunTemplate[this]; }
            set { Fields.WaSunTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Sunday TemplateId"), Column("WASunTemplateId"), Size(100)]
        public String WaSunTemplateId
        {
            get { return Fields.WaSunTemplateId[this]; }
            set { Fields.WaSunTemplateId[this] = value; }
        }
      

        public AppointmentTemplateRow()
            : base(Fields)
        {
        }
        
        public AppointmentTemplateRow(RowFields fields)
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
            public StringField MondaySMS;
            public StringField TuesdaySMS;
            public StringField WednesdaySMS;
            public StringField ThursdaySMS;
            public StringField FridaySMS;
            public StringField SaturdaySMS;
            public StringField SundaySMS;
            public Int32Field CompanyId;
            public StringField SmsTempId;
            public StringField MonTempId;
            public StringField TueTempId;
            public StringField WedTempId;
            public StringField ThurTempId;
            public StringField FriTempId;
            public StringField SatTempId;
            public StringField SunTempId;

            public StringField WaTemplate;
            public StringField WaTemplateId;
            public StringField WaMonTemplate;
            public StringField WaMonTemplateId;
            public StringField WaTueTemplate;
            public StringField WaTueTemplateId;
            public StringField WaWedTemplate;
            public StringField WaWebTemplateId;
            public StringField WaThurTemplate;
            public StringField WaThurTemplateId;
            public StringField WaFriTemplate;
            public StringField WaFriTemplateId;
            public StringField WaSatTemplate;
            public StringField WaSatTemplateId;
            public StringField WaSunTemplate;
            public StringField WaSunTemplateId;


        }
    }
}
